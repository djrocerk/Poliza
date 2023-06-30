namespace Domain.Exceptions
{
    public class InternalException : BusinessException
    {
        public InternalException(string message, IEnumerable<string>? errors = null)
            : base(500, message, errors)
        {
        }
    }
}
