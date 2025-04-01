using System.ComponentModel;
using System.Net;
using System.Text.Json.Serialization;

namespace UsersTasks.Models.Responses
{
    public class ApiResponse<T>
    {
        public ApiResponse() { StatusCode = 200; }

        public ApiResponse(ApiResponse apiResponse) { SetApiReponse(apiResponse); }

        [JsonIgnore]
        public int StatusCode { get; set; }
        [JsonPropertyName("result"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public T Result { get; set; }
        [JsonPropertyName("message"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Message { get; set; }
        [JsonPropertyName("errorCode"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string ErrorCode { get; set; }

        public void SetApiReponse(ApiResponse apiResponse) {
            StatusCode = apiResponse.StatusCode;
            ErrorCode = apiResponse.ErrorCode;
            Message = apiResponse.Message;
        }
    }

    public class ApiResponse : ApiResponse<int>
    { 
    }

    public class ApiList<T>
    {
        [JsonPropertyName("items"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        List<T> Items;
        [JsonPropertyName("total"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        int Total;
        [JsonPropertyName("page"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        int Page;
        [JsonPropertyName("limit"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        int Limit;

        public ApiList(List<T> items) : base() { Items = items; }
    }

    public class ApiPagedListResponse<T> : ApiResponse<PagedList<T>>
    {
        public ApiPagedListResponse() { }
        public ApiPagedListResponse(ApiResponse apiResponse) : base(apiResponse) { }
    }

    public class InternalServerErrorResponse : ApiResponse
    {
        public InternalServerErrorResponse() { 
            StatusCode = (int)HttpStatusCode.InternalServerError;
            Message = "Internal Server Error";
            ErrorCode = "INTERNAL_SERVER_ERROR"; 
        }

    }
}
