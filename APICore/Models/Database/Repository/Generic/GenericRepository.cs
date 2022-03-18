using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace ApiCore.Models.Database.Repository.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection { get { return Transaction.Connection; } }

        public GenericRepository(IDbTransaction transaction)
        {
            Transaction = transaction;
        }

        public Task<int> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
           string query = string.Format("SELECT * FROM {0}", typeof(T).Name);
           IEnumerable<T> listT = await Connection.QueryAsync<T>(
                query,
                transaction: Transaction
                );
            return listT.ToList();
        }

        public async Task<T> GetByFieldAsync(string fieldName, object value)
        {
            if (string.IsNullOrEmpty(fieldName))
                return null;
            var args = new DynamicParameters();
            args.Add(fieldName, value);
            string query = string.Format("SELECT * FROM {0} Where {1} = @{1}", typeof(T).Name, fieldName);
            var result = await Connection.QueryAsync<T>(
                 query,
                 param: args,
                 transaction: Transaction
                 );
            return result.FirstOrDefault();
        }

        public Task<int> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
