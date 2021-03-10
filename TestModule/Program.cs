using CommonTools.logging;
using log4net;

namespace TestModule
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            LoggerConfig.Init("test.log");
            ILog logger = LogManager.GetLogger(typeof(Program));
            logger.Info("test sdsadad");
        }
    }
}