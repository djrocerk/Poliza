namespace Domain.Exceptions
{
    public class BadRequestException : BusinessException
    {
        public BadRequestException(string message, IEnumerable<string>? errors = null)
            : base(400, message, errors)
        {
        }
    }
}
