using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Web;

namespace docwallet.indexer.Logger
{
    public interface ILogger
    {
        /// <summary>
        /// Method used to log informational events.
        /// </summary>
        /// <param name="message">Message to be logged as informational</param>
        /// <param name="context">The current HttpContext instance for the request.</param>
        /// <param name="callerName">Name of the method that is calling in</param>
        /// <param name="correlationId">Correlation Id</param>
        void LogTrace(string message,
                                        [CallerMemberName] string callerName = null,
                                        string correlationId = null);

        /// <summary>
        /// Log Error method that logs error to log provider in HttpContext mode
        /// </summary>
        /// <param name="context">The current HttpContext</param>
        /// <param name="exceptionMessage">Exception message to be logged</param>
        /// <param name="callerName">Compiler attribute that indicates the caller method name</param>
        void LogError(string exceptionMessage,
            [CallerMemberName] string callerName = null,
            string correlationId = null);

        /// <summary>
        /// Method that logs an exception to the LogProvider in HttpContext mode
        /// </summary>
        /// <param name="ex">Exception to be logged</param>
        /// <param name="callerName">Compiler attribute that indicates the caller method name</param>
        /// <param name="context">The current HttpContext</param>
        void LogException(Exception ex,
                            [CallerMemberName] string callerName = null,
                            string correlationId = null);
    }
}