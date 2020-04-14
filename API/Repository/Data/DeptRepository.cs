using API.Model;
using API.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class DeptRepository : GeneralRepository<DeptModel, myContext>
    {
        public DeptRepository(myContext mycontexts) : base(mycontexts)
        {

        }
    }
}
