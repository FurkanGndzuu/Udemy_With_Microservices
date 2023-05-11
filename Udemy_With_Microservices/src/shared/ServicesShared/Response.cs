using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ServicesShared
{
    public class Response<T>
    {
        public T Data { get; private set; }
        [JsonIgnore]
        public int StatusCode{ get; set; }
        [JsonIgnore]
        public bool isSuccess { get; set; }
        public List<string> Errors { get; set; }

        public static Response<T> Success(T data, int StatusCode) => new Response<T>()
        {
            Data = data,
            StatusCode = StatusCode
        };
        public static Response<T> Success(int StatusCode) => new Response<T>()
        {
             StatusCode = StatusCode
        };

        public static Response<T> Fail(string Error, int StatusCode) => new Response<T>()
        {
            StatusCode = StatusCode,
            Errors = new List<string>() { Error }
        };

        public static Response<T> Fail(List<string> Errors, int StatusCode) => new Response<T>()
        {
            Errors = Errors,
            StatusCode = StatusCode
        };
    }
}
