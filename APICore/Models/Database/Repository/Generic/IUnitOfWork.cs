using ApiCore.Models.Database.Entities;
using System;


namespace ApiCore.Models.Database.Repository.Generic
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepository<Customer> CustomerRepository { get; }
        void Commit();
    }
}
