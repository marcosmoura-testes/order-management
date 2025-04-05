namespace Application.CustomException
{
    public class ValidationCustomException : Exception
    {
        public ValidationCustomException(string message) : base(message) { }
    }

}

