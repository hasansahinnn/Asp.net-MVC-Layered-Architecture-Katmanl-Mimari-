using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using DAL;
using DAL.DAL;


using REPO.Interfaces;
using System.Data;
using System.Transactions;
namespace REPO
{
    public class UnitOfWork : IOperations
    {
        private bool _disposed = false;
        private DBContextUsage context = new DAL.DBContextUsage();
        private Repository<Model1> _Model1;

        public Repository<Model1> Model1
        {
            get
            {
                if (_Model1 == null)
                {
                    _Model1 = new Repository<Model1>(context);
                }
                return _Model1;
            }
        }
       
        public void Save()
        {
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                context.SaveChanges();
                scope.Complete();
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
