namespace WebAPI.Exceptions
{
    public abstract class FilterException : System.Exception
    {
        public FilterException(string message) : base(message) { }
    }

    public class InvalidFilterRangesException : FilterException
    {
        public InvalidFilterRangesException(string message) : base(message) { }
    }
}
