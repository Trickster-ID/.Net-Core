﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API.Base;
using API.Model;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class DeptController : BaseController<DeptModel, DeptRepository>
    {
        private readonly DeptRepository _repository;
        public DeptController(DeptRepository deptRepository) : base(deptRepository)
        {
            this._repository = deptRepository;
        }
        
        [HttpPut("{Id}")]
        public async Task<ActionResult> Put(int id, DeptModel entity)
        {
            var put = await _repository.Get(id);
            if (put == null)
            {
                return NotFound();
            }
            put.Name = entity.Name;
            put.UpdateDate = DateTimeOffset.Now;
            await _repository.Put(put);
            return Ok("Update Successfully");
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var get = await _repository.Get();
            return Ok(new { data = get });
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult> Get(int Id)
        {
            var get = await _repository.Get(Id);
            if (get == null)
            {
                return NotFound();
            }
            return Ok(get);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var delete = await _repository.Delete(id);
            if (delete == null)
            {
                return NotFound();
            }
            return Ok(delete);
        }
        [HttpPost]
        public async Task<ActionResult> Post(DeptModel entity)
        {
            await _repository.Post(entity);
            return CreatedAtAction("Get", new { Id = entity.Id }, entity);
        }
    }
}