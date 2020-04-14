﻿using API.Context;
using API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class DivisionRepository : GeneralRepository<DivisionModel, myContext>
    {
        public DivisionRepository(myContext mycontexts) : base(mycontexts)
        {

        }
    }
}
