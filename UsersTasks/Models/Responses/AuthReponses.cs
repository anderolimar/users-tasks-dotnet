using System.Net;
using UsersTasks.Models.Auth;
using UsersTasks.Models.Dto;

namespace UsersTasks.Models.Responses
{

    public class AuthResponse: ApiResponse<Token>
    {
        public AuthResponse(): base() { }

        public AuthResponse(Token? token) : base() { Result = token!; }

        public AuthResponse(ApiResponse apiResponse) : base(apiResponse) { }
    }

    

    public class AuthInvalidCredentialReponse : AuthResponse
    {
        public AuthInvalidCredentialReponse(): base() { 
            StatusCode = (int)HttpStatusCode.Unauthorized;  
            ErrorCode = "INVALID_CREDENTIALS"; 
            Message = "Invalid Credentiasl"; 
        }
    }
}
