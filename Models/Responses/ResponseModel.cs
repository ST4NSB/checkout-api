using System.Net;

namespace Models.Responses
{
    public class ResponseModel<TResult>
    {
        public HttpStatusCode Status { get; set; }
        public TResult Result { get; set; }
        public string ErrorMessage { get; set; }
    }
}
