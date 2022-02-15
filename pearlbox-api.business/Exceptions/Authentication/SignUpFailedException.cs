namespace pearlbox_api.business.Exceptions.Authentication
{
    public class SignUpFailedException : Exception
    {
        public SignUpFailedException()
            : base("Something went wrong when creating a user account.")
        {}
    }
}