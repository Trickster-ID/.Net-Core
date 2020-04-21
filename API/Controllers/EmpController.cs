using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Base;
using API.Model;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmpController : BaseController<EmpModel, EmpRepository>
    {
        private readonly EmpRepository _repository;
        public EmpController(EmpRepository empRepository) : base(empRepository)
        {
            this._repository = empRepository;
        }
        [HttpPost]
        public async Task<ActionResult> Post(EmpModel entity)
        {
            await _repository.Post(entity);
            return CreatedAtAction("Get", new { Email = entity.Email }, entity);
        }
        [HttpGet]
        public async Task<IEnumerable<EmpVM>> Get()
        {
            return await _repository.GetAll();
        }
        [HttpGet("{email}")]
        public async Task<ActionResult<EmpVM>> Get(string email)
        {
            var get = await _repository.GetById(email);
            if (get == null)
            {
                return NotFound();
            }
            return Ok(get);
        }
        [HttpPut("{email}")]
        public async Task<ActionResult> Put(string email, EmpModel entity)
        {
            var put = await _repository.Get(email);
            if (put == null)
            {
                return NotFound();
            }
            put.FirstName = entity.FirstName;
            put.LastName = entity.LastName;
            put.DeptModelId = entity.DeptModelId;
            put.Address = entity.Address;
            put.PhoneNumber = entity.PhoneNumber;
            put.BirthDate = entity.BirthDate;
            put.UpdateDate = DateTimeOffset.Now;
            await _repository.Put(put);
            return Ok("Update Successfully");
        }
        [HttpDelete("{email}")]
        public async Task<ActionResult> Delete(string email)
        {
            var delete = await _repository.Delete2(email);
            if (delete == null)
            {
                return NotFound();
            }
            return Ok(delete);
        }
    }
}