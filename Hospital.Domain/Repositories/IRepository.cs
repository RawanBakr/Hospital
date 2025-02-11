using Hospital.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Repositories;

public interface IRepository<T> 
{
    Task<IQueryable<T>> GetAllAsync( );
    Task<T> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task SaveAsync();



    //public IEnumerable<Patient> GetStaticPatients();


    //IEnumerable<Patient> GetAll();
    ////Patient GetById(Guid id);

    ////Patient Find(Expression<Func<Patient, bool>> predicate, string[] includes =null);
    //void Add(Patient patient);
    //void Update(Patient patient);
    //void Delete(Patient patient);
}
