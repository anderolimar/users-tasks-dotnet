using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using UsersTasks.Interfaces;
using UsersTasks.Models.Dto;
using UsersTasks.Services;
using NSubstitute.ExceptionExtensions;
using Microsoft.Extensions.Logging;
using UsersTasks.Models.Business;

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

            var cache = Substitute.For<ICacheService>();
            cache.GetOrAddCacheDataAsync(Arg.Any<string>(), Arg.Any<Func<CancellationToken, Task<UserTask>>>())
                .Returns(Task.FromResult(expectedUserTask));

            var logger = Substitute.For<ILogger<UsersService>>();

            var service = new UserTasksService(userTaskRepo, cache, logger);
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

            var cache = Substitute.For<ICacheService>();
            cache.GetOrAddCacheDataAsync(Arg.Any<string>(), Arg.Any<Func<CancellationToken, Task<UserTask>>>())
                .Throws(new Exception());

            var logger = Substitute.For<ILogger<UsersService>>();

            var service = new UserTasksService(userTaskRepo, cache, logger);
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
