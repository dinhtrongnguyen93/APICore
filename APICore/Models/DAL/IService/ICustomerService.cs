using ApiCore.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCore.Models.DAL.IService
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetCustomerPaging();

        Task<Customer> GetCustomer(int id);

    }
}
