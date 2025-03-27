using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAS_CargoPay.Core.Interfaces
{
    public interface ILoggerService
    {
        Task<object> LogAsyncJWT(string token, string message, DateTime date);
    }
}
