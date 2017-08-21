using InstaSharper.Classes;

namespace App
{
    public class SessionData : UserSessionData
    {
        public SessionData(string userName, string userPassword)
        {
            UserName = userName;
            Password = userPassword;
        }
    }
}
