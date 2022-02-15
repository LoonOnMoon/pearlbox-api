namespace pearlbox_api.business.Exceptions.Authentication
{
    public class SignInUserDoesNotExistException : Exception
    {
        public SignInUserDoesNotExistException(string credentials)
            : base(String.Format("User with credentials \"{0}\" does not exist.", credentials))
        {}
    }
}