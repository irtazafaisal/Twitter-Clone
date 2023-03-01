namespace Twitter_V1.Models
{
    public class UserRepository
    {
        public bool EmailExists(string Email)
        {
            var db = new UserContext();

            // Stores 1 if user exists because there will only be at most 1 user with an email, else stores 0
            var exists = db.Users.Where(p => p.Email == Email).ToList().Count; 
            
            return exists > 0;
        }

        public bool UserEmailPasswordCorrect(string Email, string Password)
        {
            var db = new UserContext();

            // Stores 1 if user with given email and password exists, else stores 0
            var correct = db.Users.Where(p => p.Email == Email && p.Password == Password).ToList().Count;

            return correct > 0;
        }

        public User GetUserInfo(string Email, string Password)
        {
            var db = new UserContext();

            // Stores 1 if user with given email and password exists, else stores 0
            var user =  (from u in db.Users
                        where u.Email == Email && u.Password == Password
                        select u ).First();
            return user;
        }

        public static string GetProfilePic(string Email)
        {
            var db = new UserContext();

            // return the name of user profle pic which is stored in the local data folder
            var pic = (from u in db.Users
                        where u.Email == Email
                        select u.Pic).First();
            return pic.ToString();
        }

        public async Task<string> AddUserAsync(User u)
        {
            if (EmailExists(u.Email))
                return "This Email already exists";
            var db = new UserContext();

            db.Users.Add(u);

            await db.SaveChangesAsync();

			return "Signup Successful";
		}
    }
}
