using Common.Exceptions;
using Data.Contracts;
using Entittes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProjectApi.Models;
using Services.Services;
using WebFramework.Api;

namespace MyProjectApi.Controllers;

public class UserController : BaseController
{
    private readonly IUserRepository userRepository;

    private readonly IJwtService jwtService;

    public UserController(IUserRepository userRepository, IJwtService jwtService)
    {
        this.userRepository = userRepository;
        this.jwtService = jwtService;
    }

    [HttpGet]
    public async Task<ApiResult<IEnumerable<User>>> Get(CancellationToken cancellationToken)
    {
       var users = await userRepository.GetAllAsync(cancellationToken);
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ApiResult<User>> Get(int id, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(cancellationToken, id);

        if(user == null)
        {
            return NotFound();
        }

       await userRepository.UpdateSecurityStampAsync(user, cancellationToken);

        return user;
    }

    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<string> Token(string userName, string password, CancellationToken cancellationToken)
    {
        var user =await userRepository.GetByUserAndPass(userName, password, cancellationToken);
        if (user == null)
            throw new BadRequestExceptions("نام کاربری یا رمز عبور اشتباه می باشد");

        var jwt =await jwtService.GenerateAsync(user);
        return jwt;
    }

    [HttpPost]
    public async Task Create(UserDto userDto, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Age = userDto.Age,
            UserName = userDto.UserName,
            Gender = userDto.Gender,
            Email = userDto.Email,
            Password =userDto.Password
        };

        await userRepository.AddAsync(user, userDto.Password, cancellationToken);
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
