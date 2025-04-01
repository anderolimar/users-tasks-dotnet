using System.Net;
using UsersTasks.Models.Business;

namespace UsersTasks.Models.Responses
{
    public class UserTaskUpdatedResponse : ApiResponse<bool>
    {
        public UserTaskUpdatedResponse(bool updated = true) { Result = updated; }
    }

    public class UserTaskResponse: ApiResponse<UserTask>
    {
        public UserTaskResponse(UserTask? userTask) { Result = userTask!; }

        public UserTaskResponse() : base() {}
    }

    public class NewUserTaskResponse : ApiResponse<UserTask>
    {
        public NewUserTaskResponse(UserTask? userTask) { 
            Result = userTask!; StatusCode = (int)HttpStatusCode.Created; }

        public NewUserTaskResponse(ApiResponse apiResponse) : base(apiResponse) { }
    }

    public class UserTasksResponse : ApiPagedListResponse<UserTask>
    {
        public UserTasksResponse(List<UserTask> users, int page, int pageSize, int total) { 
            Result = new PagedList<UserTask>(users, page, pageSize, total); 
        }

        public UserTasksResponse(ApiResponse apiResponse) : base(apiResponse) { }
    }
}
