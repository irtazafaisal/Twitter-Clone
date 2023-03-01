using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;
using Twitter_V1.Models;
using System.Linq;
namespace Twitter_V1.Controllers
{
    public class LoginSignupController : Controller
    {
        // Repository class where all user related operations are performed
        UserRepository URep = new UserRepository();


        // This is the function that is called on startup
        public ViewResult Index() {
            // The cookies must not exist on the Index page
            HttpContext.Response.Cookies.Delete("UserId");
            HttpContext.Response.Cookies.Delete("UserName");
            HttpContext.Response.Cookies.Delete("UserEmail");

            return View("Index"); 
        }

        private readonly IWebHostEnvironment Environment;

        public LoginSignupController(IWebHostEnvironment
            environment)
        {

            Environment = environment;
        }


        // This function is called when user clicks login
        [HttpPost]
        public ActionResult LoginForm(string Email, string Password)
        {
            // If the user with this email and password exists we send them to the HomePage, else keep them at the current Index page
            if (URep.UserEmailPasswordCorrect(Email, Password))
            {
                User u = URep.GetUserInfo(Email, Password);

                // Adding user info in cookies
                HttpContext.Response.Cookies.Append("UserId", u.Id.ToString());
                HttpContext.Response.Cookies.Append("UserName", u.Name);
                HttpContext.Response.Cookies.Append("UserEmail", Email);

                return RedirectToAction("Feed", "HomePage");
            }
            else
            {
                // Storing error message in the viewbag        
                ViewBag.Message = "Incorrect Email or Password";
                return View("Index");
            }
        }

        public ActionResult Logout()
        {
            return RedirectToAction("index", "LoginSignup");
        }


        // This function is called when user clicks signup
        [HttpPost]
        public async Task<ViewResult> SignupFormAsync(string Name, string Email, string Password, List<IFormFile> postedFiles)
        {
            string wwwPath = this.Environment.WebRootPath;
            string path = Path.Combine(wwwPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            User u = new User { 
                Name= Name,
                Email=Email,
                Password=Password,
                IsAdmin=0,
                Pic = "default.jpg"
            };

            // The message from AddUserAsync is stored in viewbag and sent to homepage
            ViewBag.Message = await URep.AddUserAsync(u);

            User u_entered = URep.GetUserInfo(Email, Password);

            foreach (var file in postedFiles)
            {
                var fileName = Path.GetFileName(file.FileName);
                string id = u_entered.Id.ToString();
                fileName = id + ".jpg";
                var pathWithFileName = Path.Combine(path, fileName);
                using (FileStream stream = new
                    FileStream(pathWithFileName,
                    FileMode.Create))
                {
                    file.CopyTo(stream);
                    ViewBag.Message = "file uploaded successfully";
                }
            }



            return View("Index");
        }
    }
}
