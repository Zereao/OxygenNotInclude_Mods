// @author Zereao
// @date 2021-03-09 19:13
// @Steam https://steamcommunity/id/hexaiolun/

using log4net.Appender;
using log4net.Config;
using log4net.Layout;

namespace CommonTools.logging
{
    public class LoggerConfig
    {
        /// <summary>ConsoleAppender，只加载一次</summary>
        private static bool _isConsoleAppenderInitialed;

        /// <summary>全局唯一的Layout</summary>
        private static readonly ILayout Layout =
            new PatternLayout("%date %-5level %logger:%method() - %message%newline");

        /// <summary>初始化操作</summary>
        /// <param name="filePath"></param>
        public static void Init(string filePath)
        {
            if (!_isConsoleAppenderInitialed)
            {
                BasicConfigurator.Configure(GetConsoleAppender());
                _isConsoleAppenderInitialed = true;
            }

            BasicConfigurator.Configure(GetRollingFileAppender(filePath));
        }

        /// <summary>获取ConsoleAppender</summary>
        /// <returns>ConsoleAppender</returns>
        private static IAppender GetConsoleAppender()
        {
            var consoleAppender = new ConsoleAppender {Name = "ConsoleAppender", Layout = Layout};
            consoleAppender.ActivateOptions();
            return consoleAppender;
        }

        /// <summary>获取RollingFileAppender</summary>
        /// <param name="filePath">日志路径</param>
        /// <returns>RollingFileAppender</returns>
        private static IAppender GetRollingFileAppender(string filePath)
        {
            var rollingFileAppender = new RollingFileAppender
            {
                File = filePath,
                AppendToFile = true,
                RollingStyle = RollingFileAppender.RollingMode.Date,
                DatePattern = "yyyy-MM-dd",
                Layout = Layout
            };
            rollingFileAppender.ActivateOptions();
            return rollingFileAppender;
        }
    }
}