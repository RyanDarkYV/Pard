using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Pard.WebApi.Controllers
{
    [EnableCors("Cors")]
    [Route("api/[controller]/[action]")]
    [Authorize(Policy="Admin")]
    public class AdminController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string data = "Admin role.";
            return new OkObjectResult(JsonConvert.SerializeObject(data));
        }
    }
}