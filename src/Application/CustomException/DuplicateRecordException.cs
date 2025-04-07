namespace Application.CustomException
{
    /// <summary>
    /// Exception thrown when a duplicate record is encountered.
    /// </summary>
    public class DuplicateRecordException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateRecordException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DuplicateRecordException(string message) : base(message) { }
    }

}
