using System.Net;

namespace NadinSoft.Application.Common
{
    public class Result<T>
    {
        public Result()
        {
            
        }
        public Result(T? value, bool isSuccessful = true, List<Error>? errors = null)
        {
            Value = value;
            IsSuccessful = isSuccessful;
            Errors = errors;
        }
        public T? Value { get; set; }
        public bool IsSuccessful { get; set; }
        public List<Error> Errors { get; set; }
    }
}
