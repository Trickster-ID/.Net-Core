using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Base;
using API.Model;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeptController : BaseController<DeptModel, DeptRepository>
    {
        public DeptController(DeptRepository deptRepository) : base(deptRepository)
        {

        }
    }
}