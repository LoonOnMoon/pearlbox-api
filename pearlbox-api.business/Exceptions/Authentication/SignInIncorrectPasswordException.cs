namespace pearlbox_api.business.Exceptions.Authentication
{
    public class SignInIncorrectPasswordException : Exception
    {
        public SignInIncorrectPasswordException(string credentials)
            : base(String.Format("Failed to authenticate user with credentials \"{0}\". Incorrect Password.", credentials))
        {}
    }
}