using Account_Web.Services;
using Microsoft.AspNetCore.Mvc;
using Account_Web.Models;

namespace Account_Web.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class FactoryController: ControllerBase
{
    private readonly IFactoryService _factoryService;

    public FactoryController(IFactoryService factoryService)
    {
        _factoryService = factoryService;
    }

    [HttpGet]
    public async  Task<ActionResult<IEnumerable<Factory>>> GetAllFactories()
    {
        var result = await _factoryService.GetAllFactories();
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult> CreateMock()
    {
        var factories = new List<Factory>
        {
            new Factory { FactoryId = "FAC01", FactoryName = "台中廠" },
            new Factory { FactoryId = "FAC02", FactoryName = "台南廠" }
        };
        await _factoryService.CreateMockFactory(factories);
        return Ok();
    }
    
}