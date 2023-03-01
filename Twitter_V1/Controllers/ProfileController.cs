using Microsoft.AspNetCore.Mvc;
using Twitter_V1.Models;
using System.IO;
using Microsoft.AspNetCore.Http;


namespace Twitter_V1.Controllers
{
    public class ProfileController : Controller
    {
        TweetRepository TRep = new TweetRepository();
        UserRepository URep = new UserRepository();


        public ActionResult UserProfile() {
            if (HttpContext.Request.Cookies.ContainsKey("UserEmail"))
            {
                return View("UserProfile");
            } else
            {
                // Storing error message in the viewbag        
                ViewBag.Message = "Login or Signup to Access the Page";
                return RedirectToAction("index", "LoginSignup");
            }
        }

        // This function is called when user deletes tweets 
        [HttpPost]
        public async Task<ViewResult> DeleteTweet(int TweetId)
        {
            await TRep.DeleteTweetAsync(TweetId);

            return View("UserProfile");
        }





    }
}
