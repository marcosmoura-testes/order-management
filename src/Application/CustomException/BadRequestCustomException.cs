namespace Application.CustomException
{
    public class BadRequestCustomException : Exception
    {
        public BadRequestCustomException(string message) : base(message) { }
    }

}
