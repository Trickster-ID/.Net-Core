using API.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class myContext : IdentityDbContext
    {
        public myContext(DbContextOptions<myContext> options) : base(options) { }
        public DbSet<DivisionModel> Division { get; set; }
        public DbSet<DeptModel> Dept { get; set; }
    }
}
