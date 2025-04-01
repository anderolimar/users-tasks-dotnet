using System.Net;
using UsersTasks.Models.Business;

namespace UsersTasks.Models.Responses
{
    public class UserUpdatedResponse : ApiResponse<bool>
    {
        public UserUpdatedResponse(bool updated = true) : base() { Result = updated; }
    }

    public class UserResponse: ApiResponse<User>
    {
        public UserResponse(): base() { }

        public UserResponse(User? user) : base() { Result = user!; }

        public UserResponse(ApiResponse apiResponse) : base(apiResponse) { }
    }

    public class NewUserResponse : ApiResponse<User>
    {
        public NewUserResponse(User? user) : base() { Result = user!; StatusCode = (int)HttpStatusCode.Created; }

        public NewUserResponse(ApiResponse apiResponse) : base(apiResponse) { }

        public NewUserResponse() : base() { }
    }

    public class UsersResponse : ApiPagedListResponse<User>
    {
        public UsersResponse(List<User> users, int page, int pageSize, int total) : base()
        { 
            Result = new PagedList<User>(users, page, pageSize, total); 
        }

        public UsersResponse(ApiResponse apiResponse) : base(apiResponse) { }
    }

    public class UserNotFoundReponse : UserResponse
    {
        public UserNotFoundReponse(): base() { 
            StatusCode = (int)HttpStatusCode.NotFound;  
            ErrorCode = "USER_NOT_FOUND"; 
            Message = "User Not Found"; 
        }
    }

    public class UserAlreadyExistsReponse : NewUserResponse
    {
        public UserAlreadyExistsReponse() : base()
        {
            StatusCode = (int)HttpStatusCode.BadRequest;
            ErrorCode = "USER_ALREADY_EXISTS";
            Message = "User already exists";
        }
    }

    public class NewUserUnknownErrorReponse : NewUserResponse
    {
        public NewUserUnknownErrorReponse() : base()
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
            ErrorCode = "UNKNOWN_ERROR";
            Message = "Unknown error";
        }
    }
}
