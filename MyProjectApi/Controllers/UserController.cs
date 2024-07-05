using Data.Contracts;
using Entittes;
using Microsoft.AspNetCore.Mvc;
using MyProjectApi.Models;
using WebFramework.Api;

namespace MyProjectApi.Controllers;

public class UserController : BaseController
{
    private readonly IUserRepository userRepository;
    public UserController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ApiResult<IEnumerable<User>>> Get(CancellationToken cancellationToken)
    {
       var users = await userRepository.GetAllAsync(cancellationToken);
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResult<User>> Get(int id, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(cancellationToken, id);
        return user;
    }

    [HttpPost]
    public async Task Create(UserDto userDto, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Age = userDto.Age,
            FullName = userDto.FullName,
            Gender = userDto.Gender,
            UserName = userDto.UserName,
            Email = userDto.Email
        };

        await userRepository.AddAsync(user, cancellationToken);
    }

    [HttpPut]
    public async Task<ApiResult> Update(User user, CancellationToken cancellationToken)
    {
        userRepository.Attach(user);
        await userRepository.UpdateAsync(user, cancellationToken);
        return Ok();
    }

    [HttpDelete]
    public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(cancellationToken, id);
        await userRepository.DeleteAsync(user, cancellationToken);
        return Ok();
    }
}
