using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace docwallet.indexer.Logger
{
    public class ConsoleLogger : ILogger
    {
        public void LogError(string exceptionMessage, string callerName = null, string correlationId = null)
        {
            Console.WriteLine($"{DateTime.Now}: {callerName} : {exceptionMessage}");
        }

        public void LogException(Exception ex, string callerName = null, string correlationId = null)
        {
            this.LogError(ex.ToString(), callerName, correlationId);
        }

        public void LogTrace(string message,
                                    string callerName = null,
                                    string correlationId = null)
        {
            Console.WriteLine($"{DateTime.Now}: {correlationId} : {message} ");
        }
    }
}