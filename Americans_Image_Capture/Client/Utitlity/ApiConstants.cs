namespace Americans_Image_Capture.Client.Utitlity
{
    public class ApiConstants
    {
        private const string BaseUrl = "https://localhost:7146/api";
        //private const string BaseUrl = "http://mag-egr-mts.dover-global.net/api";

        //American Image

        public const string getimageDetails = $"{BaseUrl}/MaagAmericansImage/getall";
        public const string AddimageDetails = $"{BaseUrl}/MaagAmericansImage/maagdata";

      //  public const string AppLogin = $"{BaseUrl}/LoginDetails/login";
        public const string Adduser = $"{BaseUrl}/LoginDetails/add";
        public const string GetUser = $"{BaseUrl}/Users/getuser";

        public const string AppLogin = $"{BaseUrl}/Account/login";
        public const string LDAPLogin = $"{BaseUrl}/Account/login-ldap";
        public const string LoginUserDetails = $"{BaseUrl}/users/adduser";
        public const string LoginUserDelete = $"{BaseUrl}/users";

    }
}
