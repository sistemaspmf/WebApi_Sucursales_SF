using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi_Sucursales_SF.API.Utils;
using WebApi_Sucursales_SF.SalesForce;
using WebApi_Sucursales_SF.API.Models;

namespace WebApi_Sucursales_SF.API.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/sucursales")]
    public class SucursalesController : ApiController
    {
        [HttpGet]
        [Route("{nip}")]
        public CustomHttpActionResult<List<SucursalesModel>> getSucursales(string nip)
        {
            SalesForceQuerys<List<SucursalesModel>> query = new SalesForceQuerys<List<SucursalesModel>>();
            List<SucursalesModel> result = query.GetSucursal(nip);
            result = result.Distinct().ToList();
            
            return new CustomHttpActionResult<List<SucursalesModel>>(result, result != null && result.Count > 0 ? HttpStatusCode.OK : HttpStatusCode.NotFound, Request, Configuration);
        }
    }
}
