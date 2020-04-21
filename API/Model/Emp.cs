using API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    [Table("Employee")]
    public class EmpModel : IEntity
    {
        [Key]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<DateTimeOffset> CreateDate { get; set; }
        public Nullable<DateTimeOffset> UpdateDate { get; set; }
        public Nullable<DateTimeOffset> DeleteDate { get; set; }

        public DeptModel DeptModel { get; set; }
        [ForeignKey("DeptModel")]
        public int DeptModelId { get; set; }

        //public EmpModel() { }
        //public EmpModel(EmpModel emp) //create
        //{
        //    this.FirstName = emp.FirstName;
        //    this.FirstName = emp.FirstName;
        //    this.CreateDate = DateTimeOffset.Now;
        //    this.IsDelete = false;
        //}

        //public void Update(EmpModel emp)
        //{
        //    this.FirstName = emp.FirstName;
        //    this.FirstName = emp.FirstName;
        //    this.UpdateDate = DateTimeOffset.Now;
        //}

        //public void Delete()
        //{
        //    this.IsDelete = true;
        //    this.DeleteDate = DateTimeOffset.Now;
        //}
    }
}
