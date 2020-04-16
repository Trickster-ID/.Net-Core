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
    public class DeptController : Controller
    {
        public IActionResult Index()
        {
            return View(cloadDepartment());
        }
        public JsonResult cloadDepartment()
        {
            DeptJson departmentVM = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44392/api/")
            };
            var responseTask = client.GetAsync("Dept");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                departmentVM = JsonConvert.DeserializeObject<DeptJson>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "server error, try after some time");
            }
            //return Json(new { data = models }, JsonRequestBehavior.AllowGet);
            return Json(departmentVM);
        }

        public JsonResult Insert(DeptVM departmentVM)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44392/api/")
            };
            var myContent = JsonConvert.SerializeObject(departmentVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (departmentVM.Id.Equals(0))
            {
                var result = client.PostAsync("Dept/", byteContent).Result;
                return Json(result);
            }
            else
            {
                var result = client.PutAsync("Dept/" + departmentVM.Id, byteContent).Result;
                return Json(result);
            }
        }
        public JsonResult GetById(int Id)
        {
            DeptVM departmentVM = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44392/api/")
            };
            var responseTask = client.GetAsync("Dept/" + Id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                departmentVM = JsonConvert.DeserializeObject<DeptVM>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "server error, try after some time");
            }
            return Json(departmentVM);
        }
        public JsonResult Delete(int Id)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44392/api/")
            };
            var result = client.DeleteAsync("Dept/" + Id).Result;
            return Json(result);
        }
        //public async Task<ActionResult> Excel()
        //{
        //    var columnHeaders = new string[]
        //    {
        //        "Name",
        //        "Ditambahkan"
        //    };

        //    byte[] result;

        //    using (var package = new ExcelPackage())
        //    {
        //        var worksheet = package.Workbook.Worksheets.Add("Department Excel");
        //        using (var cells = worksheet.Cells[1, 1, 1, 2])
        //        {
        //            cells.Style.Font.Bold = true;
        //        }

        //        for (var i = 0; i < columnHeaders.Count(); i++)
        //        {
        //            worksheet.Cells[1, i + 1].Value = columnHeaders[i];
        //        }

        //        var j = 2;
        //        HttpResponseMessage response = await client.GetAsync("dept");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var readTask = await response.Content.ReadAsAsync<IList<Department>>();
        //            foreach (var department in readTask)
        //            {
        //                worksheet.Cells["A" + j].Value = department.Name;
        //                worksheet.Cells["B" + j].Value = department.CreateDate.ToString("MM/dd/yyyy");
        //                j++;
        //            }
        //        }
        //        result = package.GetAsByteArray();
        //    }
        //    return File(result, "application/ms-excel", $"Department-{DateTime.Now.ToString("hh:mm:ss-MM/dd/yyyy")}.xlsx");
        //}
        //public async Task<ActionResult> CSV()
        //{
        //    var columnHeaders = new string[]
        //    {
        //        "Nama Department",
        //        "Tanggal Ditambahkan"
        //    };
        //    HttpResponseMessage response = await client.GetAsync("dept");
        //    var readTask = await response.Content.ReadAsAsync<IList<Department>>();
        //    var departmentRecords = from department in readTask
        //                            select new object[]{
        //            $"{department.Name}",
        //            $"\"{department.CreateDate.ToString("MM/dd/yyyy")}\""
        //    }.ToList();
        //    var departmentcsv = new StringBuilder();
        //    departmentRecords.ForEach(line =>
        //    {
        //        departmentcsv.AppendLine(string.Join(",", line));
        //    });
        //    byte[] buffer = Encoding.ASCII.GetBytes($"{string.Join(",", columnHeaders)}\r\n{departmentcsv.ToString()}");
        //    return File(buffer, "text/csv", $"Department-{DateTime.Now.ToString("hh:mm:ss-MM/dd/yyyy")}.csv");
        //}
        //public ActionResult Report(Department department)
        //{
        //    DeptReport deptreport = new DeptReport();
        //    byte[] abytes = deptreport.PrepareReport(exportToPdf());
        //    return File(abytes, "application/pdf");
        //}

        //public List<Department> exportToPdf()
        //{
        //    IEnumerable<Department> models = null;
        //    var responsTask = client.GetAsync("dept");
        //    responsTask.Wait();
        //    var result = responsTask.Result;
        //    if (result.IsSuccessStatusCode)
        //    {
        //        var readTask = result.Content.ReadAsAsync<IList<Department>>();
        //        readTask.Wait();
        //        models = readTask.Result;
        //    }
        //    else
        //    {
        //        models = Enumerable.Empty<Department>();
        //        ModelState.AddModelError(string.Empty, "server error, try later");
        //    }
        //    //return Json(new { data = models }, JsonRequestBehavior.AllowGet);
        //    return models.ToList();
        //}
    }
}