using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using COREDemo.Services;
using Microsoft.AspNetCore.Cors;

namespace COREDemo.API
{
    [Produces("application/json")]
    [Route("api/Demo")]
   // [EnableCors("AppPolicy")]
    public class DemoController : Controller
    {
        public static IServices _serv;
        public DemoController(IServices serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_serv.GetCustomerList());
        }
    }
}