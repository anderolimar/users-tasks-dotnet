using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersTasks.Interfaces;
using UsersTasks.Models;
using UsersTasks.Models.Dto;
using UsersTasks.Models.Responses;
using UsersTasks.Models.Validations;

namespace UsersTasks.Controllers
{
    [Route("api/usuarios/{userid}/tarefas")]
    [ApiController]
    [Authorize]
    public class TasksController : ApiController
    {
        IUserTasksService _service;

        public TasksController(IUserTasksService service) { _service = service; } 

        [HttpGet]
        public async Task<ActionResult<UserTaskResponse>> GetUserTasks(
            [Required]int userid, [FromQuery, GreaterThanZero] int page = 1, [FromQuery, GreaterThanZero] int pagesize = 10) { 
            var userTask = await _service.GetUserTasks(userid, page, pagesize);
            return SendApiResponse(userTask);
        }

        [HttpPost]
        public async Task<ActionResult<NewUserTaskResponse>> AddUserTasks([Required] int userid, [FromBody] NewUserTask newUserTask)
        {
            var createdUserTask = await _service.AddUserTask(userid, newUserTask);
            return SendApiResponse(createdUserTask);
        }
    }
}
