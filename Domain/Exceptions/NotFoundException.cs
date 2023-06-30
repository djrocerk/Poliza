namespace Domain.Exceptions
{
    public class NotFoundException : BusinessException
    {
        public NotFoundException(string message, IEnumerable<string>? errors = null)
            : base(404, message, errors)
        {
        }
    }
}
