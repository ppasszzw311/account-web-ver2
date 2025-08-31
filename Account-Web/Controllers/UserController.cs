using Account_Web.Repositorys;
using Microsoft.AspNetCore.Mvc;
using Account_Web.Models;
using Account_Web.Repositorys;
using Account_Web.Services;

namespace Account_Web.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("/getall")]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUser()
    {
        var users = await _userService.GetAllUsers();
        return Ok(users);
    }
    [HttpGet("/getbyid/{id}")]

    public async Task<ActionResult<User>> GetUserById(int id)
    {
        var user = await _userService.GetUserById(id);
        return Ok(user);
    }
        

    [HttpGet("createTwo")]
    public async Task<ActionResult> CreateUser()
    {
        var user1 = new User
                { UserId = "U001", UserName = "minMing", Password = "121dd", Email = "abcd@mail.com", FactoryId = 3 };
        var user2 = new User
                { UserId = "U002", UserName = "BigMing", Password = "grji", Email = "fgss@mail.com", FactoryId = 4};
        await _userService.CreateTwoUsers(user1, user2);
        return Ok("新增成功");
    }
}