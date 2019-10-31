using Microsoft.AspNetCore.Mvc;
using System.Linq;

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