using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAS_CargoPay.Infrastracture.Logging
{
    public class FileLogger
    {
        private const string LogFilePath = "log.txt";

        public void LogError(string errorMessage)
        {
            var logEntry = $"{DateTime.Now} - Error: {errorMessage}";

            using (var writer = new StreamWriter(LogFilePath, true))
            {
                writer.WriteLine(logEntry);
            }
        }
    }
}
