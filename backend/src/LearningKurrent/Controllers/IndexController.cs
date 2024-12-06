using LearningKurrent.Models.Index;
using Microsoft.AspNetCore.Mvc;

namespace LearningKurrent.Controllers;

[ApiController]
[Route("")]
public class IndexController : ControllerBase
{
  [HttpGet]
  public ActionResult<ApiVersion> Get() => Ok(ApiVersion.Current);
}
