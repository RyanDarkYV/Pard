using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Newtonsoft.Json;

namespace Pard.WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        protected string GetUserId()
        {
            return User.Claims.First(i => i.Type == "id").Value;
        }
    }
}