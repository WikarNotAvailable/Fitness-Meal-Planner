namespace WebAPI.Exceptions
{
    public abstract class EntityException : System.Exception
    {
        public EntityException(string message) : base(message) { }
    }
    public class EntityNotFoundException : EntityException
    {
        public EntityNotFoundException(string message) : base(message) { }
    }
}
