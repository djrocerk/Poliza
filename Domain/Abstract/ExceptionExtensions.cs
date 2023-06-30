namespace Domain.Abstract
{
    public static class ExceptionExtensions
    {
        public static string ToDetailedString(this Exception exception)
        {
            if (exception == null) return null;

            var properties = exception.GetType()
                   .GetProperties();
            var fields = properties.Select(property => new
            {
                property.Name,
                Value = property.GetValue(exception, null)
            })
                .Select(x => string.Format(
                    "{0} : {1}", x.Name, x.Value != null ? x.Value.ToString() : string.Empty
                 ));

            return string.Join("\n", fields);
        }
    }
}
