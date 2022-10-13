namespace WebAPI.Wrappers
{

    // basic wrapper
    public class Response<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public Response()
        {

        }

        public Response(T data)
        {
            Data = data;
            Succeeded = true;
        }
    }
}
