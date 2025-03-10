using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPISampleProjectUsingVS2022.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Hello from Demo Controller";
        }

        [HttpGet]
        public string GetUserCount(int userCount)
        {
            return "Hello from Demo User Count Controller"+"User count is.."+userCount;
        }
    }
}
