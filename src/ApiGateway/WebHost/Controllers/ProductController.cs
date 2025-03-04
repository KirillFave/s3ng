using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.IAM.Enums;

namespace WebHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = nameof(RoleType.Admin))]
    public class ProductController : ControllerBase
    {
        //TODO Refit / NSwag generation
    }
}
