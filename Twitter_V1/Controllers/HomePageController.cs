using Microsoft.AspNetCore.Mvc;
using Twitter_V1.Models;

namespace Twitter_V1.Controllers
{
    public class HomePageController : Controller
    {

        // Repository class where all tweet related operations are performed
        TweetRepository TRep = new TweetRepository();



        public ActionResult Feed() {
            if (HttpContext.Request.Cookies.ContainsKey("UserEmail"))
            {
                return View("Feed");
            } 
            else
            {
                // Storing error message in the viewbag        
                ViewBag.Message = "Login or Signup to Access the Page";
                return RedirectToAction("index", "LoginSignup");
            }
            
        }


        // This function is called when user tweets something
        [HttpPost]
        public async Task<ViewResult> TweetForm(string Text)
        {
            Tweet t = new Tweet
            {
                Name = Request.Cookies["UserName"],
                Email = Request.Cookies["UserEmail"],
                Text = Text
            };

            ViewBag.Message = await TRep.AddTweetAsync(t);

            return View("Feed");
        }
    }
}
