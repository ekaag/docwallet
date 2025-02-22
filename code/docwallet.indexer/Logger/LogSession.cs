using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using System;
using System.Text;

using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Web;

namespace docwallet.indexer.Logger
{
    /// <summary>
    /// Log Session class that encapsulates all operations for a Log session
    /// </summary>
    public class LogSession : IDisposable
    {
        /// <summary>
        /// Is Disposed bool flag
        /// </summary>
        private bool _isDisposed;

        /// <summary>
        /// Method name
        /// </summary>
        private readonly string _methodName;

        /// <summary>
        /// ILogger object instance
        /// </summary>
        private readonly ILogger _logger;

        private readonly string _correlationId;

        /// <summary>
        /// Session name
        /// </summary>
        private string SessionName { get; }

        /// <summary>
        /// Gets the Dispose session name that will be used in outputting the final dispose message
        /// </summary>
        private string DisposeSessionName => $"{SessionName}_Dispose";

        /// <summary>
        /// Stopwatch that keeps track of time elapsed
        /// </summary>
        private Stopwatch SwTimer { get; }

        /// <summary>
        /// Log session .cctor that takes in a ILogger object and logs its messages to it.
        /// This class encapsulates the pattern of start watch, stop watch and Trace time taken log messages
        /// </summary>
        /// <param name="logger">ILogger object instance</param>
        /// <param name="sessionName">Session name for which Log session will be established</param>
        /// <param name="callerName">Compiler attribute that indicates the caller method name</param>
        public LogSession(ILogger logger, string sessionName, [CallerMemberName] string callerName = null, string correlationId = null)
        {
            SwTimer = Stopwatch.StartNew();
            SessionName = sessionName;
            _methodName = callerName;
            _logger = logger;
            _correlationId = correlationId;
            _logger.LogTrace($"start:  CallerMethod : {_methodName} : Session {SessionName}", correlationId: _correlationId);
        }

        public TimeSpan TimeElapsed => this.SwTimer.Elapsed;

        /// <summary>
        /// This method logs an intermediate message to ILogger with TimeElapsed in the session
        /// </summary>
        /// <param name="message">Message to be logged</param>
        /// <param name="callerName">Compiler attribute that indicates the caller method name</param>
        ///  ToDo : Upon an exception log to a fallback place that HttpContext.Current threw an exception
        public void LogElapsedMessage(string message, [CallerMemberName] string callerName = null)
        {
            _logger.LogTrace($"{message} and Elapsed Time :{(int)SwTimer.ElapsedMilliseconds} milli seconds", callerName, _correlationId);
        }

        /// <summary>
        /// Dispose implementation for the IDisposable interface
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Dispose method which is thread safe to handle for GC collected versus Dispose triggered as part of being IDisposable
        /// The method as part ending the session - outputs the final time taken message for the entire session
        /// </summary>
        /// <param name="disposing">If the object is being disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;
            if (disposing)
            {
                if (SwTimer.IsRunning)
                {
                    SwTimer.Stop();
                }

                string message = string.Format(CultureInfo.InvariantCulture, $"end:  Session '{SessionName}': TimeElapsed : {this.TimeElapsed} ({this.TimeElapsed.TotalMilliseconds} milliseconds)");
                _logger.LogTrace(message, this._methodName);
            }
        }
    }
}