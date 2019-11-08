using Microsoft.AspNetCore.Mvc;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/v1/home")]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        public string GetCategory()
        {
            return "alo";
        }
    }
}