using Account_Web.Repositorys;
using Microsoft.AspNetCore.Mvc;
using Account_Web.Models;
using Account_Web.Repositorys;
using Account_Web.Services;

namespace Account_Web.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("/getall")]
    public ActionResult<IEnumerable<User>> GetAllUser()
    {
        var users = _userService.GetAllUsers();
        return Ok(users);
    }
    [HttpGet("/getbyid/{id}")]

    public ActionResult<User> GetUserById(int id)
    {
        var user = _userService.GetUserById(id);
        return Ok(user);
    }
        

    [HttpGet("createTwo")]
    public ActionResult CreateUser()
    {
        var user1 = new User
                { UserId = "U001", UserName = "minMing", Password = "121dd", Email = "abcd@mail.com", FactoryId = 1 };
        var user2 = new User
                { UserId = "U002", UserName = "BigMing", Password = "grji", Email = "fgss@mail.com", FactoryId = 2 };
        _userService.CreateTwoUsers(user1, user2);
        return Ok("新增成功");
    }
}