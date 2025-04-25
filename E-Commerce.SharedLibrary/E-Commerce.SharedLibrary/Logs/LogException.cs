using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace E_Commerce.SharedLibrary.Logs
{
    public static class LogException
    {
        public static void LogExceptions(Exception exception)
        {
        
            LogToFile(exception.Message);
            LogToConsole(exception.Message);
            LogToDebugger(exception.Message);
            LogToError(exception.Message);


        }

        public static void LogToDebugger(string message)
        {
            Log.Information(message);
        }

        public static void LogToConsole(string message)
        {
          Log.Warning(message);
        }

        public static void LogToFile(string message)
        {
          Log.Debug(message);
        }
        public static void LogToError(string message)
        {
            Log.Error(message);
        }

    }
}
