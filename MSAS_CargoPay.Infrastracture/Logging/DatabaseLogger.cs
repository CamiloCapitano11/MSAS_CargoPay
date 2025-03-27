using Dapper;
using Microsoft.Extensions.Configuration;
using MSAS_CargoPay.Core.Entities.V1._0;
using MSAS_CargoPay.Core.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAS_CargoPay.Infrastracture.Logging
{
    public class DatabaseLogger
    {
        private readonly string _connectionString;
        public DatabaseLogger(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public void Log(LogApi Log, object Response, string ResponseCode)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {

                    var parameters = new
                    {
                        Log.Application,
                        Log.IP,
                        Log.Method,
                        Log.StartTime,
                        @Endtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                        @Parameters = Log.Requests,
                        @Respuesta = JsonConvert.SerializeObject(Response),
                        Log.Server,
                        @Url = Log.RequestUrl,
                        Log.Query,
                        Log.Microservice,
                        @ResponseCode = ResponseCode
                    };

                    var rowsAffected = connection.Execute(StoreProcedures.CrearLog, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }
        }
    }
}
