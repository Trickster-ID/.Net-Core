using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CLIENT.Controllers
{
    public class EmpController : Controller
    {
        //readonly HttpClient client = new HttpClient
        //{
        //    BaseAddress = new Uri("https://localhost:44392/api/")
        //};
        //public IActionResult Index()
        //{
        //    return View(cloadEmp());
        //}
        //public JsonResult cloadEmp()
        //{
        //    IEnumerable<EmpVM> employee = null;
        //    var responseTask = client.GetAsync("Emp"); 
        //    responseTask.Wait(); 
        //    var result = responseTask.Result;
        //    if (result.IsSuccessStatusCode) 
        //    {
        //        var readTask = result.Content.ReadAsAsync<IList<EmpVM>>();
        //        readTask.Wait();
        //        employee = readTask.Result;
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, "Server Error");
        //    }
        //    return Json(employee);
        //}

        //public JsonResult Insert(EmpModel empVM)
        //{
        //    var myContent = JsonConvert.SerializeObject(empVM);
        //    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //    var byteContent = new ByteArrayContent(buffer);
        //    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //    if (empVM.Id == 0)
        //    {
        //        var result = client.PostAsync("Emp/", byteContent).Result;
        //        return Json(result);
        //    }
        //    else
        //    {
        //        var result = client.PutAsync("Emp/" + empVM.Id, byteContent).Result;
        //        return Json(result);
        //    }
        //}
        //public JsonResult GetById(int Id)
        //{
        //    IEnumerable<EmpVM> employee = null;
        //    var responseTask = client.GetAsync("Emp/" + Id); 
        //    responseTask.Wait(); 
        //    var result = responseTask.Result;
        //    if (result.IsSuccessStatusCode)
        //    {
        //        var readTask = result.Content.ReadAsAsync<IList<EmpVM>>();
        //        readTask.Wait();
        //        employee = readTask.Result;
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, "Server Error");
        //    }
        //    return Json(employee);
        //}
        //public JsonResult Delete(int Id)
        //{
        //    var result = client.DeleteAsync("Emp/" + Id).Result;
        //    return Json(result);
        //}
    }
}