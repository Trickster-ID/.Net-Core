using API.Context;
using API.Model;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace API.Repository.Data
{
    public class EmpRepository : GeneralRepository<EmpModel, myContext>
    {
        DynamicParameters parameters = new DynamicParameters();
        IConfiguration _configuration { get; }
        public EmpRepository(myContext myContexts, IConfiguration configuration) : base(myContexts)
        {
            _configuration = configuration;
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
        public async Task<IEnumerable<EmpVM>> GetById(int Id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var spName = "SP_viewEmpById";
                parameters.Add("@Id", Id);
                var data = await connection.QueryAsync<EmpVM>(spName, parameters, commandType: CommandType.StoredProcedure);
                return data;
            }
        }
    }
}
