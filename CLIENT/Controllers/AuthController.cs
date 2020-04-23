using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace CLIENT.Controllers
{
    public class AuthController : Controller
    {
        //public string _email;
        private HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44392/api/")
        };
        public IActionResult Login()
        {   
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            var myContent = JsonConvert.SerializeObject(model);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PostAsync("Auth/login", byteContent).Result;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsStringAsync().Result;
                var handler = new JwtSecurityTokenHandler();
                var datajson = handler.ReadJwtToken(data);

                string token = "Bearer " + data;
                string role = datajson.Claims.First(claim => claim.Type == "Role").Value;
                string email = datajson.Claims.First(claim => claim.Type == "Email").Value;

                HttpContext.Session.SetString("JWTToken", token);
                HttpContext.Session.SetString("Role", role);
                HttpContext.Session.SetString("Email", email);
                //_email = email;
                if(role == "Admin")
                {
                    return RedirectToAction("Index", "Dept");
                }
                else
                {
                    return RedirectToAction("accessEmp", "auth");
                }
            }
            return View();
        }
        public JsonResult LoadEmployee()
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            var email = HttpContext.Session.GetString("Email");
            var responseTask = client.GetAsync("Emp/" + email);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<EmpVM>>();
                readTask.Wait();
                return Json(readTask.Result[0]);
            }
            else
            {
                return Json(result);
            }
        }
        public JsonResult Edit(EmpModel model)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            var myContent = JsonConvert.SerializeObject(model);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("Emp/" + model.Email, byteContent).Result;
            return Json(result);
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("JWTToken");
            HttpContext.Session.Remove("Role");
            HttpContext.Session.Remove("Email");
            return RedirectToAction("Login", "Auth");
        }
        public IActionResult accessEmp()
        {
            var role = HttpContext.Session.GetString("Role");
            if ( role == "Employee")
            {
                return View();
            }
            else
            {
                return RedirectToAction("notfound", "Auth");
            }
        }
        public IActionResult notfound()
        {
            return View();
        }
    }
}