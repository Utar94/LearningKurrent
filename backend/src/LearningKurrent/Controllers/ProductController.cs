using LearningKurrent.Application.Models;
using LearningKurrent.Application.Products.Commands;
using LearningKurrent.Application.Products.Models;
using LearningKurrent.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearningKurrent.Controllers;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase
{
  private readonly IMediator _mediator;

  public ProductController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpPost]
  public async Task<ActionResult> CreateAsync([FromBody] ProductPayload payload, CancellationToken cancellationToken)
  {
    Guid id = Guid.NewGuid();
    await _mediator.Send(new CreateOrReplaceProductCommand(id, payload, Version: null), cancellationToken);
    return Created(GetLocation(id), new ProductResult(id));
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
  {
    await _mediator.Send(new DeleteProductCommand(id), cancellationToken);
    return NoContent();
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<ProductModel>> ReadAsync(Guid id, CancellationToken cancellationToken)
  {
    ProductModel? product = await _mediator.Send(new ReadProductQuery(id, Sku: null), cancellationToken);
    return product == null ? NotFound() : Ok(product);
  }

  [HttpGet("sku:{sku}")]
  public async Task<ActionResult<ProductModel>> ReadBySkuAsync(string sku, CancellationToken cancellationToken)
  {
    ProductModel? product = await _mediator.Send(new ReadProductQuery(Id: null, sku), cancellationToken);
    return product == null ? NotFound() : Ok(product);
  }

  [HttpPut("{id}")]
  public async Task<ActionResult> ReplaceAsync(Guid id, [FromBody] ProductPayload payload, long? version, CancellationToken cancellationToken)
  {
    bool created = await _mediator.Send(new CreateOrReplaceProductCommand(id, payload, version), cancellationToken);
    ProductResult result = new(id);
    return created ? Created(GetLocation(id), result) : Ok(result);
  }

  [HttpGet]
  public async Task<ActionResult<SearchResults<ProductModel>>> SearchAsync(CancellationToken cancellationToken)
  {
    SearchProductsPayload payload = new(); // TODO(fpion): query params
    SearchResults<ProductModel> products = await _mediator.Send(new SearchProductsQuery(payload), cancellationToken);
    return Ok(products);
  }

  [HttpPatch("{id}")]
  public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] UpdateProductPayload payload, CancellationToken cancellationToken)
  {
    await _mediator.Send(new UpdateProductCommand(id, payload), cancellationToken);
    return NoContent();
  }

  private Uri GetLocation(Guid id) => new($"https://{Request.Host}/products/{id}", UriKind.Absolute);
}

public record ProductResult(Guid Id);
