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

    public class EntityValidatonException : EntityException
    {
        public EntityValidatonException(string message) : base(message) { }
    }

    public class IncorrectCredentialsException : EntityException
    {
        public IncorrectCredentialsException(string message) : base(message) { }
    }
}
