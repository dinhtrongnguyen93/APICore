using ApiCore.Models.DAL.IService;
using ApiCore.Models.Database.Entities;
using ApiCore.Models.Database.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCore.Models.DAL.Service
{
    public class CustomerService: ICustomerService
    {
        private IUnitOfWork _unitOfWork;
        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await _unitOfWork.CustomerRepository.GetByFieldAsync("CustomerID", id);
        }

        public Task<IEnumerable<Customer>> GetCustomerPaging()
        {
            throw new NotImplementedException();
        }
    }
}
