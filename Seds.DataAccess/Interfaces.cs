using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seds.DataAccess
{
    /// <summary>
    /// Interface for the ISedsRepositories
    /// </summary>
    public interface ISedsRepositories
    {
        // Save pending changes to the data store.
        void SaveChanges();

        // Repositories
        IRepository<Certificate> Certificates { get; }
        IRepository<Student> Students { get; }
        IRepository<Study> Studies { get; }
        IRepository<User> Users { get; }
        //IRepository<Role> Roles { get; }
    }

    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
    }
}
