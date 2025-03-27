using MSAS_CargoPay.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAS_CargoPay.Infrastracture.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        private Hashtable _repositories;
        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task<int> Complete()
        {
            return await _storeContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _storeContext.Dispose();
        }
    }
}
