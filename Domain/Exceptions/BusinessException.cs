namespace Domain.Exceptions
{
    public abstract class BusinessException : Exception
    {
        public int StatusCode { get; set; }
        public IEnumerable<string>? Errors { get; set; }

        public BusinessException(int statusCode, string message,
            IEnumerable<string>? errors)
            : base(message)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
    }
}
