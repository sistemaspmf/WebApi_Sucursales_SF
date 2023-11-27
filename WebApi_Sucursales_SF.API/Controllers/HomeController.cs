using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi_Sucursales_SF.API.Controllers
{
    public class HomeController : ApiController
    {
        [Route("")]
        public async Task<dynamic> Get() => new { message = "I am live!!!", date = DateTime.Now };
    }
}
