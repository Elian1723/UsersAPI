using Microsoft.AspNetCore.Mvc;

namespace UsersAPI.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IActionResult Get()
        {
            throw new NotImplementedException("This method is not implemented yet.");
        }
    }
}
