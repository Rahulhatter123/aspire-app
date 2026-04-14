using Clean_Architecture_Sample.Application.DTOs;
using Clean_Architecture_Sample.Application.Features.Product.Commands;
using Clean_Architecture_Sample.Application.Features.Product.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clean_Architecture_Sample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //Tests
    [HttpGet("products")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllProductsQuery());
        return Ok(result);
    }

    [HttpGet("product-id/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("insert")]
    public async Task<IActionResult> Create([FromForm] ProductDto dto)
    {
        string? fileName = null;

        var command = new CreateProductCommand(dto);

        var success = await _mediator.Send(command);

        if (dto.ImageFile is not null)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            int idx = dto.ImageFile.FileName.LastIndexOf('.');
            fileName = success.Id.ToString() + "_" + dto.ImageFile.FileName.Substring(0, idx) + Path.GetExtension(dto.ImageFile.FileName);

            var filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await dto.ImageFile.CopyToAsync(stream);
        }
        return Ok(success);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] ProductDto dto)
    {
        string? fileName = null;

        if (dto.ImageFile is not null)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            int idx = dto.ImageFile.FileName.LastIndexOf('.');
            fileName = id.ToString() + "_" + dto.ImageFile.FileName.Substring(0, idx) + Path.GetExtension(dto.ImageFile.FileName);

            var filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await dto.ImageFile.CopyToAsync(stream);
        }

        var command = new UpdateProductCommand(id, dto);

        var success = await _mediator.Send(command);

        return success ? NoContent() : NotFound();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _mediator.Send(new DeleteProductCommand(id));
        return success ? NoContent() : NotFound();
    }
}
