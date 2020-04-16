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
    public class EmpController : BaseController<EmpModel, EmpRepository>
    {
        private readonly EmpRepository _repository;
        public EmpController(EmpRepository empRepository) : base(empRepository)
        {
            this._repository = empRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<EmpVM>> Get()
        {
            return await _repository.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpVM>> Get(int id)
        {
            var get = await _repository.GetById(id);
            if (get == null)
            {
                return NotFound();
            }
            return Ok(get);
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult> Put(int id, EmpModel entity)
        {
            var put = await _repository.Get(id);
            if (put == null)
            {
                return NotFound();
            }
            put.FirstName = entity.FirstName;
            put.LastName = entity.LastName;
            put.DeptModelId = entity.DeptModelId;
            put.Email = entity.Email;
            put.Address = entity.Address;
            put.PhoneNumber = entity.PhoneNumber;
            put.BirthDate = entity.BirthDate;
            put.UpdateDate = DateTimeOffset.Now;
            await _repository.Put(put);
            return Ok("Update Successfully");


            //if (id != entity.Id)
            //{
            //    return BadRequest();
            //}
            //await _repository.Put(entity);
            //return Ok("Update Successfully");
        }
    }
}