using API.Base;
using API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<List<T>> Get();
        Task<T> Get(int Id);
        Task<T> Post(T entity);
        Task<T> Put(T entity);
        //Task<T> Put(int Id);
        Task<T> Delete(int Id);

    }
    //interface IEmpRepository
    //{
    //    IEnumerable<EmpVM> Get();
    //    Task<IEnumerable<EmpVM>> Get(int Id);
    //    int Create(EmpVM empvm);
    //    int Update(int Id, EmpVM empvm);
    //    int Delete(int Id);
    //}
}
