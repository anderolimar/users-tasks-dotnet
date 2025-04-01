using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using UsersTasks.Interfaces;
using UsersTasks.Models.Dto;
using UsersTasks.Models;
using UsersTasks.Services;
using NSubstitute.ExceptionExtensions;

namespace UsersTasksAPITests.Services
{
    public class UserTasksServiceTest
    {
        [Fact]
        public async Task TestGetUserTasks() {

            var expectedUserTask = new UserTask() { 
                UserId = 1, Title = "Task test", Description = "Task Description", Status = UserTaskStatus.PENDING };
            var userTaskRepo = Substitute.For<IUserTasksRepository>();
            userTaskRepo.InsertAsync(Arg.Is<UserTask>(
                u => u.UserId == expectedUserTask.UserId && u.Title == expectedUserTask.Title
                && u.Description == expectedUserTask.Description && u.Status == expectedUserTask.Status))
                .Returns(Task.FromResult(expectedUserTask));

            var service = new UserTasksService(userTaskRepo);
            var result = await service.AddUserTask(expectedUserTask.UserId, new NewUserTask() { 
                Title = expectedUserTask.Title, Description = expectedUserTask.Description, 
                Status = expectedUserTask.Status});

            Assert.Equal(201, result.StatusCode);
            Assert.Equal(result.Result, expectedUserTask);

        }

        [Fact]
        public async Task TestGetUserTasksError()
        {
            var expectedUserTask = new UserTask()
            {
                UserId = 1,
                Title = "Task test",
                Description = "Task Description",
                Status = UserTaskStatus.PENDING
            };
            var userTaskRepo = Substitute.For<IUserTasksRepository>();
            userTaskRepo.InsertAsync(Arg.Is<UserTask>(
                u => u.UserId == expectedUserTask.UserId && u.Title == expectedUserTask.Title
                && u.Description == expectedUserTask.Description && u.Status == expectedUserTask.Status))
                .Throws(new Exception());

            var service = new UserTasksService(userTaskRepo);
            var result = await service.AddUserTask(expectedUserTask.UserId, new NewUserTask()
            {
                Title = expectedUserTask.Title,
                Description = expectedUserTask.Description,
                Status = expectedUserTask.Status
            });

            Assert.Equal(500, result.StatusCode);
            Assert.Equal("INTERNAL_SERVER_ERROR", result.ErrorCode);
            Assert.Equal("Internal Server Error", result.Message);

            Assert.Equal(null, result.Result);

        }
    }
}
