using Common.Exceptions;
using Data.Contracts;
using Entittes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyProjectApi.Models;
using Services.Services;
using WebFramework.Api;

namespace MyProjectApi.Controllers;

public class UserController : BaseController
{
    private readonly IUserRepository userRepository;

    private readonly IJwtService jwtService;

    private readonly UserManager<User> userManager;

    private readonly RoleManager<Role> roleManager;

    private readonly SignInManager<User> signInManager;

    public UserController(IUserRepository userRepository, IJwtService jwtService, UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
    {
        this.userRepository = userRepository;
        this.jwtService = jwtService;
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.signInManager = signInManager;
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
        var user = await userManager.FindByIdAsync(id.ToString());

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
        // var user =await userRepository.GetByUserAndPass(userName, password, cancellationToken);

        var user =await userManager.FindByNameAsync(userName);
    
        if (user == null)
            throw new BadRequestExceptions("نام کاربری یا رمز عبور اشتباه می باشد"); 

        var isPasswordValid = await userManager.CheckPasswordAsync(user, password);

        if(!isPasswordValid)
        {
            throw new BadRequestExceptions("نام کاربری یا رمز عبور اشتباه می باشد");
        }

        await userManager.UpdateSecurityStampAsync(user);

        var jwt =await jwtService.GenerateAsync(user);
        return jwt;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task Create(UserDto userDto, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Age = userDto.Age,
            FullName = userDto.UserName,
            Gender = userDto.Gender,
            UserName = userDto.UserName,
            Email = userDto.Email
        };

        var result =await userManager.CreateAsync(user,userDto.Password);

        var role = new Role()
        {
            Name ="Admin",
            Description ="admin role"
        };

        await roleManager.CreateAsync(role);

        await userManager.AddToRoleAsync(user,role.Name);

        //await userRepository.AddAsync(user, userDto.Password, cancellationToken);
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
