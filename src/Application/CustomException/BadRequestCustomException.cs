namespace Application.CustomException
{
    /// <summary>
    /// Exception thrown for bad requests.
    /// </summary>
    public class BadRequestCustomException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestCustomException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BadRequestCustomException(string message) : base(message) { }
    }

}
