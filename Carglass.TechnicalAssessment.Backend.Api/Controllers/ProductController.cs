using Carglass.TechnicalAssessment.Backend.BL;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Carglass.TechnicalAssessment.Backend.Api.Controllers;

[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{
    private readonly IProductAppService _appService;
    public ProductsController(IProductAppService appService)
    {
        _appService = appService;
    }

    [HttpGet]
    public IEnumerable<ProductDto> GetAll()
    {
        return _appService.GetAll();
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok(_appService.GetById(id));
    }

    [HttpPost]
    public IActionResult Create([FromBody] ProductDto dto)
    {
        _appService.Create(dto);
        return Ok();
    }

    [HttpPut]
    public IActionResult Update([FromBody] ProductDto dto)
    {
        _appService.Update(dto);
        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] ProductDto dto)
    {
        _appService.Delete(dto);
        return Ok();
    }
}