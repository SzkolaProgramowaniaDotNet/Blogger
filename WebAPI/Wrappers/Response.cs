using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Wrappers
{
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
