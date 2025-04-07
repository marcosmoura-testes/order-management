namespace Application.CustomException
{
    /// <summary>
    /// Exception thrown when validation fails.
    /// </summary>
    public class ValidationCustomException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationCustomException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ValidationCustomException(string message) : base(message) { }
    }

}

