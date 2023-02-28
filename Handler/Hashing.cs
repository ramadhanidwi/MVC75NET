namespace MVC75NET.Handler
{
    public class Hashing
    {
        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public static string HashPassword(string password)      //Untuk menghashing password dari bentuk plain text 
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }

        public static bool ValidatePassword(string password, string correctHash)    //mencocokan antara password yang asli dengan password hasil hashing 
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash); 
        }
    }
}
