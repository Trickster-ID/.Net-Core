using API.Context;
using API.Model;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.EntityFrameworkCore;

namespace API.Repository.Data
{
    public class EmpRepository : GeneralRepository<EmpModel, myContext>
    {
        DynamicParameters parameters = new DynamicParameters();
        IConfiguration _configuration { get; }
        private readonly myContext _myContext;
        public EmpRepository(myContext mycontexts, IConfiguration configuration) : base(mycontexts)
        {
            _configuration = configuration;
            _myContext = mycontexts;
        }
        public async Task<EmpModel> Get(string email)
        {
            return await _myContext.Set<EmpModel>().FindAsync(email);
        }
        public async Task<IEnumerable<EmpVM>> GetAll()
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_viewEmp";
                var employees = await conn.QueryAsync<EmpVM>(procName, commandType: CommandType.StoredProcedure);
                return employees;
            }
        }
        public async Task<IEnumerable<EmpVM>> GetById(string Email)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var spName = "SP_viewEmpById";
                parameters.Add("@Email", Email);
                var data = await connection.QueryAsync<EmpVM>(spName, parameters, commandType: CommandType.StoredProcedure);
                return data;
            }
        }
        public async Task<IEnumerable<RegisterVM>> Create2(RegisterVM employee)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_InsertEmployee";
                parameters.Add("@firstname", employee.FirstName);
                parameters.Add("@lastname", employee.LastName);
                parameters.Add("@email", employee.Email);
                parameters.Add("@birthdate", employee.BirthDate);
                parameters.Add("@phonenumber", employee.PhoneNumber);
                parameters.Add("@address", employee.Address);
                parameters.Add("@department_id", employee.DeptModelId);
                var datas = await connection.QueryAsync<RegisterVM>(procName, parameters, commandType: CommandType.StoredProcedure);
                return datas;
            }
        }
        public async Task<EmpModel> Delete2(string email)
        {
            var entity = await Get(email);
            if (entity == null)
            {
                return entity;
            }
            entity.DeleteDate = DateTimeOffset.Now;
            entity.IsDelete = true;
            _myContext.Entry(entity).State = EntityState.Modified;
            await _myContext.SaveChangesAsync();
            return entity;
        }
        public async Task<IEnumerable<Chartmodel>> GetCount()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_GetCountDept";
                var data = await connection.QueryAsync<Chartmodel>(procName, commandType: CommandType.StoredProcedure);
                return data;
            }
        }
    }
}
