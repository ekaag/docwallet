using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace docwallet.indexer.Exceptions
{
    public class ExceptionUtility
    {
        #region Constants

        /// <summary>
        /// </summary>
        internal const string ArgIsInvalid = "argument is invalid. ";

        #endregion

        public static void ThrowIfArgInvalid(string argValue, string argName)
        {
            ThrowIfArgInvalid(argValue, argName, argName);
        }

        public static void ThrowIfArgInvalid(string argValue, string argName, string format, params object[] args)
        {
            if (String.IsNullOrEmpty(argValue))
            {
                if (format == null)
                {
                    throw new Exception($"Invalid argument : {argName}", new ArgumentException(ArgIsInvalid, argName));
                }

                string message = GetStringInvariantCulture(format, args);
                throw new Exception($"Invalid argument : {argName}", new ArgumentException(message, argName));
            }
        }

        public static void ThrowIfArgInvalid(object argValue, string argName, string message = null)
        {
            if (argValue == null)
            {
                if (message == null)
                {
                    throw new ArgumentException(ArgIsInvalid, argName);
                }

                throw new ArgumentException(message, argName);
            }
        }

        public static string GetStringInvariantCulture(string format, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, format, args);
        }
    }
}
