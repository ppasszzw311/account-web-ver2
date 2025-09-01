using Account_Web.Models;
using Account_Web.Models.Dtos;
using Account_Web.Services;
using Microsoft.AspNetCore.Mvc;

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

    // GET: /Factory/GetAll
    [HttpGet]
    public async  Task<ActionResult<IEnumerable<Factory>>> GetAll()
    {
        var result = await _factoryService.GetAllFactories();
        return Ok(result);
    }

    // GET: /Factory/GetById/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Factory>> GetById(int id)
    {
        var result = await _factoryService.GetFactoryById(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    // POST: /Factory/Create
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] FactoryRequestDto factory)
    {
        var createdFactory = new Factory
        {
            FactoryId = factory.FactoryId,
            FactoryName = factory.FactoryName
        };
        await _factoryService.CreateFactory(createdFactory);
        return CreatedAtAction(nameof(GetById), new { id = createdFactory.Id }, createdFactory);
    }

    // PUT: /Factory/Update/1
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] EditFactoryDto factory)
    {
        var existingFactory = await _factoryService.GetFactoryById(id);
        if (existingFactory == null)
        {
            return NotFound();
        }
        existingFactory.FactoryId = factory.FactoryId;
        existingFactory.FactoryName = factory.FactoryName;
        await _factoryService.UpdateFactory(existingFactory);
        return NoContent();
    }

    // DELETE: /Factory/Delete/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var existingFactory = await _factoryService.GetFactoryById(id);
        if (existingFactory == null)
        {
            return NotFound();
        }
        // Assuming there's a Delete method in the service
        await _factoryService.DeleteFactory(id);
        return NoContent();
    }

    // 建立假的資料
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