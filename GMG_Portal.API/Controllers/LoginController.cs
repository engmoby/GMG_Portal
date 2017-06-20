using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GMG_Portal.API.Controllers
{
    [RoutePrefix("SystemParameters/Login")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        [HttpPost]
        public bool Login(string userName, string password)
        {
            return true;
        }
    }
}
