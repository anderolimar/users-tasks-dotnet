using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UsersTasks.Models.Responses;

namespace UsersTasks.Controllers
{
    public class ApiController : ControllerBase
    {
        public JsonResult SendApiResponse<T>(ApiResponse<T> apiResponse) {
            this.HttpContext.Response.StatusCode = apiResponse.StatusCode;
            var result = new JsonResult(apiResponse);
            return result;
        }
    }
}
