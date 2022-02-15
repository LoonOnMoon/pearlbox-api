namespace pearlbox_api.business.Exceptions.Authentication
{
    public class SignUpUserExistsException : Exception
    {
        public SignUpUserExistsException(string email, string userName)
            : base(String.Format("A user with credentials email: {0} and userName: {1} already exists.", email, userName))
        {}
    }
}