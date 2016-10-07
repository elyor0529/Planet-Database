using System.Reflection;
using log4net;

namespace PlanetDatabase.Helpers
{
    public static class LogHelper
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void WriteInfo(string command)
        {
            if (Logger.IsDebugEnabled)
            {
                Logger.Debug(command);
            }
            else
            {
                Logger.Info(command);
            }
        }

        public static void WriteError(string command)
        {
            if (Logger.IsDebugEnabled)
            {
                Logger.Debug(command);
            }
            else
            {
                Logger.Error(command);
            }
        }
    }
}
