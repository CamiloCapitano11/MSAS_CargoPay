using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MSAS_CargoPay.Core.DTOs;
using MSAS_CargoPay.Core.Entities.V1._0;
using MSAS_CargoPay.Core.Resources;
using MSAS_CargoPay.Infrastracture.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;

namespace MSAS_CargoPay.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DatabaseLogger _logger;

        public TestController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new DatabaseLogger(configuration);
        }

        [HttpGet]
        [Route("TestConexiones")]
        public async Task<IActionResult> TestBDAsync()
        {
            LogApi _logApi = new("MSAS_ServiciosTest", "TestConexiones", HttpContext.Connection.RemoteIpAddress.ToString(), HttpContext.Request.Path, string.Empty, User.FindFirst("aud")?.Value);
            var settingsSection = _configuration.GetSection("ConnectionStrings");

            List<VerifyDB> ListverifyDB = [];

            foreach (var section in settingsSection.GetChildren())
            {
                var connection = new SqlConnection(section.Value);
                var database = connection.Database;
                try
                {
                    using (connection)
                    {
                        var result = await connection.ExecuteScalarAsync<int>("SELECT 1");
                        if (result == 1)
                        {
                            ListverifyDB.Add(new VerifyDB
                            {
                                Name = database,
                                Status = "Conexión exitosa",
                            });
                        }
                        else
                        {
                            ListverifyDB.Add(new VerifyDB
                            {
                                Name = database,
                                Status = "Conexión fallida",
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    ListverifyDB.Add(new VerifyDB
                    {
                        Name = database,
                        Status = "Conexión fallida",
                        Detail = ex.Message
                    });
                }
            }
            var jsonResult = JsonConvert.SerializeObject(ListverifyDB, Formatting.None);
            _logger.Log(_logApi, jsonResult.ToString(), "200");

            return Ok(new { data = ListverifyDB });
        }
        [HttpGet]
        [Route("RefrescarCache")]
        public IActionResult RefrescarCache()
        {
            LogApi _logApi = new("MSAS_ServiciosTest", "RefrescarCache", HttpContext.Connection.RemoteIpAddress.ToString(), HttpContext.Request.Path, string.Empty, User.FindFirst("aud")?.Value);
            try
            {
                var jsonResult = JsonConvert.SerializeObject(new { success = true, message = "Realizado" }, Formatting.None);
                _logger.Log(_logApi, jsonResult.ToString(), "200");
                return Ok(new { success = true, message = "Realizado" });
            }
            catch (Exception ex)
            {
                var jsonResult = JsonConvert.SerializeObject(new { success = false, message = ex.Message }, Formatting.None);
                _logger.Log(_logApi, jsonResult.ToString(), "200");
                return Ok(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("version")]
        public IActionResult GetVersion()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return Ok(new { version });
        }

    }
}
