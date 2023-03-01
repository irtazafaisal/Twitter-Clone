namespace Twitter_V1.Models
{
    public class TweetRepository
    {
        public static List<Tweet> GetAllTweets()
        {
            var db = new TweetContext();

            // Returning all tweets as a list
            return (from t in db.Tweets
                    select t).ToList();
        }

        public static List<Tweet> GetAllTweetsBy(string email)
        {
            var db = new TweetContext();

            // Returning all tweets by a specific user as a list
            return (from t in db.Tweets
                    where t.Email == email
                    select t).ToList();
        }

        public async Task<string> AddTweetAsync(Tweet t)
        {
            var db = new TweetContext();

            db.Tweets.Add(t);

            await db.SaveChangesAsync();

            return "Tweet Added Successfully";
        }

        public async Task<string> DeleteTweetAsync(int TweetId)
        {
            var db = new TweetContext();
            
            var t = (from twt in db.Tweets
                    where twt.Id == Convert.ToInt32(TweetId)
                    select twt ).FirstOrDefault();
            db.Remove(t);

            await db.SaveChangesAsync();
            

            return "Tweet Deleted Successfully";
        }
    }
}
