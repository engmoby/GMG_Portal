using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GMG_Portal.API.Models.General;
using Newtonsoft.Json;

namespace GMG_Portal.UI.Controllers
{
    public class LoginController : Controller
    {
        readonly HttpClient _client;

        string url = System.Configuration.ConfigurationManager.AppSettings["ServicesURL"] + "/SystemParameters/";

        public LoginController()
        {

            _client = new HttpClient();
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }



        // GET: Login
        public ActionResult Index()
        {
            //if (Request.Cookies["Global"] != null) 
            //return RedirectToAction("Index", "Admin"); 
            //else 
            if (Request.Cookies["Global"] != null)
            {
                var c = new HttpCookie("Global");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            } 
            return View();
        }
        [HttpPost]
        [HandleError]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(API.Models.SystemParameters.Admin.Admin admin)
        {
            if (ModelState.IsValid)
            {

                HttpResponseMessage responseMessageApi = await _client.PostAsJsonAsync("Admin/Login/", admin);
                if (responseMessageApi.IsSuccessStatusCode)
                {
                    var responseData = responseMessageApi.Content.ReadAsStringAsync().Result;
                    var retunObj = JsonConvert.DeserializeObject<API.Models.SystemParameters.Admin.Admin>(responseData);
                    if (retunObj != null)
                    {
                        var cookie = new HttpCookie("Global");
                        cookie.Value = retunObj.UserName;
                        Response.Cookies.Add(cookie);
                        Response.Expires= 1;
                        TempData["UserName"] = retunObj.UserName;

                    }
                }
                return RedirectToAction("Index", "Admin");
            }

            return View(admin);
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(API.Models.SystemParameters.Admin.Admin model)
        {
            if (ModelState.IsValid)
            {
                //var user = await UserManager.FindByNameAsync(model.Email);
                //if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                //{
                //    // Don't reveal that the user does not exist or is not confirmed
                //    return View("ForgotPasswordConfirmation");
                //}

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }


    }
}