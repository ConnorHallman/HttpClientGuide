using System.Text.Json.Serialization;

namespace HttpClientGuide.Shared.Dto
{
    public class ApiResponse
    {
        public bool Succes { get; set; }
        public string ResponseMessage { get; set; }
        [JsonConstructor]
        public ApiResponse() { }
        public ApiResponse(bool success, string responseMessage)
        {
            Succes = success;
            ResponseMessage = responseMessage;
        }
    }
    public class ApiResponse<T> : ApiResponse where T : class
    {
        public T Data { get; set; }
        [JsonConstructor]
        public ApiResponse() { }
        public ApiResponse(bool success, string responseMessage, T data) : base(success, responseMessage)
        {
            Data = data;
        }
    }
}
