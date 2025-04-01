using UsersTasks.Interfaces;
using NSubstitute;
using UsersTasks.Services;
using UsersTasks.Models.Dto;
using NSubstitute.ExceptionExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UsersTasks.Models.Responses;
using Microsoft.Extensions.Logging;
using NSubstitute.ReturnsExtensions;
using UsersTasks.Models.Business;


namespace UsersTasksAPITests.Services
{
    public class UsersServiceTest
    {
        [Fact]
        public async Task TestAddingNewUser()
        {
            var expectedUser = new User() { Name = "Teste0", Email = "test@test.com" };
            var userRepo = Substitute.For<IUsersRepository>();
            userRepo.InsertAsync(Arg.Is<User>(u =>  u.Name == expectedUser.Name && u.Email == expectedUser.Email ))
                .Returns(Task.FromResult(expectedUser));

            var cache = Substitute.For<ICacheService>();

            var logger = Substitute.For<ILogger<UsersService>>();

            var service = new UsersService(userRepo, cache, logger);
            var result = await service.AddUser(new NewUser() { Name = expectedUser.Name, Email = expectedUser.Email });

            Assert.Equal(201, result.StatusCode);
            Assert.Equal(expectedUser, result.Result);
        }

        [Fact]
        public async Task TestAddingExistingUser()
        {
            var expectedUser = new User() { Name = "Teste0", Email = "test@test.com" };
            var userRepo = Substitute.For<IUsersRepository>();
            userRepo.FirstOrDefault(Arg.Any<Expression<Func<User, bool>>>())
                .Returns(expectedUser);
            userRepo.InsertAsync(Arg.Is<User>(u => u.Name == expectedUser.Name && u.Email == expectedUser.Email))
                .Throws(new DbUpdateException());

            var cache = Substitute.For<ICacheService>();

            var logger = Substitute.For<ILogger<UsersService>>();

            var service = new UsersService(userRepo, cache, logger);
            var result = await service.AddUser(new NewUser() { Name = expectedUser.Name, Email = expectedUser.Email });

            Assert.Equal(400, result.StatusCode);
            Assert.Equal("USER_ALREADY_EXISTS", result.ErrorCode);
            Assert.Equal("User already exists", result.Message);
            Assert.Equal(null, result.Result);
        }

        [Fact]
        public async Task TestAddingUserError()
        {
            var expectedUser = new User() { Name = "Teste0", Email = "test@test.com" };
            var userRepo = Substitute.For<IUsersRepository>();
            userRepo.FirstOrDefault(Arg.Any<Expression<Func<User, bool>>>())
                .ReturnsNull();
            userRepo.InsertAsync(Arg.Is<User>(u => u.Name == expectedUser.Name && u.Email == expectedUser.Email))
               .Throws(new Exception());

            var cache = Substitute.For<ICacheService>();

            var logger = Substitute.For<ILogger<UsersService>>();

            var service = new UsersService(userRepo, cache, logger);
            var result = await service.AddUser(new NewUser() { Name = expectedUser.Name, Email = expectedUser.Email });

            Assert.Equal(500, result.StatusCode);
            Assert.Equal("INTERNAL_SERVER_ERROR", result.ErrorCode);
            Assert.Equal("Internal Server Error", result.Message);
            Assert.Equal(null, result.Result);
        }


        [Fact]
        public async Task TestGetUser()
        {
            var expectedUser = new User() { Id =1,  Name = "Teste0", Email = "test@test.com" };
            var userRepo = Substitute.For<IUsersRepository>();
            userRepo.FindByIdAsync(Arg.Is<int>(u => u == expectedUser.Id))
                .Returns(Task.FromResult(expectedUser));

            var cache = Substitute.For<ICacheService>();
            cache.GetOrAddCacheDataAsync(Arg.Any<string>(), Arg.Any<Func<CancellationToken, Task<User>>>())
                .Returns(Task.FromResult(expectedUser));

            var logger = Substitute.For<ILogger<UsersService>>();

            var service = new UsersService(userRepo, cache, logger);
            var result = await service.GetUser(expectedUser.Id);

            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expectedUser, result.Result);
        }

        [Fact]
        public async Task TestGetUserError()
        {
            var expectedUser = new User() { Id = 1, Name = "Teste0", Email = "test@test.com" };
            var userRepo = Substitute.For<IUsersRepository>();
            userRepo.FindByIdAsync(Arg.Is<int>(u => u == expectedUser.Id))
                .Throws(new Exception());

            var cache = Substitute.For<ICacheService>();
            cache.GetOrAddCacheDataAsync(Arg.Any<string>(), Arg.Any<Func<CancellationToken, Task<User>>>())
                .Throws(new Exception());

            var log = Substitute.For<ILogger<UsersService>>();

            var service = new UsersService(userRepo, cache, log);
            var result = await service.GetUser(expectedUser.Id);


            Assert.Equal(500, result.StatusCode);
            Assert.Equal("INTERNAL_SERVER_ERROR", result.ErrorCode);
            Assert.Equal("Internal Server Error", result.Message);
            Assert.Equal(null, result.Result);
        }


        [Fact]
        public async Task TestGetUsers()
        {
            var expectedUser1 = new User() { Id = 1, Name = "Teste1", Email = "test1@test.com" };
            var expectedUser2 = new User() { Id = 2, Name = "Teste2", Email = "test2@test.com" };
            var expectedUsersList = new List<User>() { expectedUser1, expectedUser2 };
            var expextedResult = new PagedList<User>(expectedUsersList, 1, 5, 2);


            var userRepo = Substitute.For<IUsersRepository>();
            userRepo.CountAsync()
                .Returns(Task.FromResult(expectedUsersList.Count));
            userRepo.GetAllAsync(0, 5)
                .Returns(Task.FromResult(expectedUsersList));

            var cache = Substitute.For<ICacheService>();
            cache.GetOrAddCacheDataAsync(Arg.Any<string>(), Arg.Any<Func<CancellationToken, Task<int>>>())
                .Returns(Task.FromResult(2));

            cache.GetOrAddCacheDataAsync(Arg.Any<string>(), Arg.Any<Func<CancellationToken, Task<List<User>>>>())
                .Returns(Task.FromResult(expectedUsersList));

            var logger = Substitute.For<ILogger<UsersService>>();

            var service = new UsersService(userRepo, cache, logger);
            var result = await service.GetUsers(expextedResult.PageNumber, expextedResult.PageSize);

            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expextedResult, result.Result);
        }

        [Fact]
        public async Task TestGetUsersError()
        {
            var expectedUser1 = new User() { Id = 1, Name = "Teste1", Email = "test1@test.com" };
            var expectedUser2 = new User() { Id = 2, Name = "Teste2", Email = "test2@test.com" };
            var expectedUsersList = new List<User>() { expectedUser1, expectedUser2 };
            var expextedResult = new PagedList<User>(expectedUsersList, 1, 5, 2);


            var userRepo = Substitute.For<IUsersRepository>();
            userRepo.CountAsync()
                .Returns(Task.FromResult(expectedUsersList.Count));
            userRepo.GetAllAsync(0, 5)
                .Throws(new Exception());

            var cache = Substitute.For<ICacheService>();
            cache.GetOrAddCacheDataAsync(Arg.Any<string>(), Arg.Any<Func<CancellationToken, Task<int>>>())
                .Returns(Task.FromResult(2));

            cache.GetOrAddCacheDataAsync(Arg.Any<string>(), Arg.Any<Func<CancellationToken, Task<List<User>>>>())
                .Throws(new Exception());

            var logger = Substitute.For<ILogger<UsersService>>();

            var service = new UsersService(userRepo, cache, logger);
            var result = await service.GetUsers(expextedResult.PageNumber, expextedResult.PageSize);

            Assert.Equal(500, result.StatusCode);
            Assert.Equal("INTERNAL_SERVER_ERROR", result.ErrorCode);
            Assert.Equal("Internal Server Error", result.Message);
            Assert.Equal(null, result.Result);
        }
    }
}
