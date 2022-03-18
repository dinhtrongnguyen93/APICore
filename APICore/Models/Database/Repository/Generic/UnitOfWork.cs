using ApiCore.Models.Database.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Data;


namespace ApiCore.Models.Database.Repository.Generic
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IGenericRepository<Customer> _customerRepository;
        private bool _dispose;

        // ctor + tab tab
        public UnitOfWork(IOptions<UnitOfWorkOption> option)
        {
            // SqlConnection için ctrl + . yapıp gerekli kütüphaneyi indirmeniz gerekiyor. =>  using System.Data.SqlClient;
            UnitOfWorkOption unitOfWorkOption = option.Value;
            _connection = new SqlConnection(unitOfWorkOption.DataBaseConnectString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();

        }

        public IGenericRepository<Customer> CustomerRepository
        {
            get { return _customerRepository ?? (_customerRepository = new GenericRepository<Customer>(_transaction)); }
        }

        public void Commit()
        {
            try
            {
                // burda hata yaptık commit yapması gerekirken sürekli hataya düşecek burası yorgunluk böyle birşey :)))  hiçbir türlü commit olmaz düzeltiyoruz :)))

                _transaction.Commit();  // olması gereken bu 
            }
            catch
            {
                // yapılanları geri al 
                _transaction.Rollback();
                throw;
            }
            finally
            {
                // her ne olursa olsun bu işlemleri yap anlamında => ister try a girsib ister catch'e 
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            // repositoryleri temizliyoruz.
            _customerRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        // burada çok fazla if kullandık ilerleyen süreçlerde buralarında üzerinden geçmeliyiz. ne kadar az if if o kadar iyi :) s
        private void dispose(bool disposing)
        {
            if (!_dispose)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _dispose = true;
            }
        }
        ~UnitOfWork()
        {
            dispose(false);
        }


    }
}
