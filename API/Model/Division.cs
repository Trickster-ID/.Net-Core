using API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    [Table("Division")]
    public class DivisionModel : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int DeptId { get; set; }
        public virtual DeptModel DeptModel { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<DateTimeOffset> CreateDate { get; set; }
        public Nullable<DateTimeOffset> UpdateDate { get; set; }
        public Nullable<DateTimeOffset> DeleteDate { get; set; }

        public DivisionModel() { }
        public DivisionModel(DivisionModel divisionModel) //create
        {
            this.Name = divisionModel.Name;
            this.CreateDate = DateTimeOffset.Now;
            this.IsDelete = false;
            this.DeptId = DeptModel.Id;
        }

        public void Update(DivisionModel divisionModel)
        {
            this.Name = divisionModel.Name;
            this.UpdateDate = DateTimeOffset.Now;
            this.DeptId = DeptModel.Id;
        }

        public void Delete()
        {
            this.IsDelete = true;
            this.DeleteDate = DateTimeOffset.Now;
        }
    }
}
