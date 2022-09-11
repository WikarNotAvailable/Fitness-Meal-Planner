namespace WebAPI.Wrappers
{

    // basic wrapper
    public class Response<T>
    {
        public T data { get; set; }
        public bool succeeded { get; set; }
        public Response()
        {

        }

        public Response(T _data)
        {
            data = _data;
            succeeded = true;
        }
    }
}
