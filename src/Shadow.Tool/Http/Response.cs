using System;

namespace Shadow.Tool.Http
{
    public abstract class Response
    {
        protected Response()
        {
            
        }

        protected Response(Exception error)
        {
            Error = error;
        }

        public Exception Error { get; }

        public bool IsError
        {
            get { return Error != null; }
        }
    }

    public class Response<T> : Response
    {
        public Response(Exception ex) : base(ex)
        {

        }

        public Response(T data)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
