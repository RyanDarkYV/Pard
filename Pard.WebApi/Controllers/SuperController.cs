using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Pard.WebApi.Controllers
{
    [EnableCors("Cors")]
    [Authorize(Roles="Superadmin")]
    [Route("api/[controller]/[action]")]
    public class SuperController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string data = "Superadmin role.";
            return new OkObjectResult(JsonConvert.SerializeObject(data));
        }
    }
}