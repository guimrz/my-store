namespace MyStore.Core.Mvc.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, IEnumerable<string>> Errors { get; }

        public ValidationException(IDictionary<string, IEnumerable<string>> errors)
        {
            Errors = errors;
        }
    }
}
