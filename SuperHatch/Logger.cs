// @author Zereao
// @date 2021-03-06 18:49
// @Steam https://steamcommunity/id/hexaiolun/

using System;
using System.Text;
using SuperHatch.utils;

namespace SuperHatch
{
    /// <summary>
    /// 日志工具类，还是习惯Slf4J的打日志方式
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// MOD名称标识，打印日志的时候加上标识，方便问题排查
        /// </summary>
        private readonly string _modName;

        /// <summary>
        /// 默认的占位符，Slf4j风格的 {} 占位符
        /// </summary>
        private static readonly string[] Separator = {"{}"};

        public Logger(string modName)
        {
            _modName = $"********【{modName}】********";
        }

        /// <summary>
        /// 打印一条INFO日志
        /// </summary>
        /// <param name="msg">消息</param>
        public void Info(string msg)
        {
            Debug.LogFormat(this.ConvertMsg(msg));
        }

        /// <summary>
        /// 打印INFO日志，支持 {} 占位符
        /// </summary>
        /// <param name="msg">消息模板，使用{}做占位符</param>
        /// <param name="args">参数</param>
        public void Info(string msg, params object[] args)
        {
            if (CollectionUtils.IsEmpty(args))
            {
                this.Info(this.ConvertMsg(msg));
                return;
            }

            Debug.LogFormat(this.ConvertFormatter(msg), args);
        }

        /// <summary>
        /// 打印 WARNING级别日志
        /// </summary>
        /// <param name="msg">消息内容</param>
        public void Warning(string msg)
        {
            Debug.LogWarning(this.ConvertMsg(msg));
        }

        /// <summary>
        /// 打印WARNING日志，支持 {} 占位符
        /// </summary>
        /// <param name="msg">消息模板，使用{}做占位符</param>
        /// <param name="args">参数</param>
        public void Warning(string msg, params object[] args)
        {
            if (CollectionUtils.IsEmpty(args))
            {
                this.Warning(this.ConvertMsg(msg));
                return;
            }

            var tailIndex = args.Length - 1;
            if (args[tailIndex] is Exception e)
            {
                args[tailIndex] = e.ToString();
                msg += " \n{}";
            }

            Debug.LogWarningFormat(this.ConvertFormatter(msg), args);
        }

        /// <summary>
        /// 消息转换，加上Mod名称声明
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <returns>转换后的消息，加上了Mod名称声明</returns>
        private string ConvertMsg(string msg) => $"{_modName} {msg}";

        /// <summary>
        /// 格式化字符串转换，将 {} {} 转换为 {0} {1} 这样的占位符
        /// </summary>
        /// <param name="oldFormatter">原始格式化字符，使用{}做占位符</param>
        /// <returns>C#占位符，使用{0},{1}做占位符</returns>
        /// <exception cref="ApplicationException">原始格式化字符串为空时，直接抛出异常</exception>
        private string ConvertFormatter(string oldFormatter)
        {
            if (string.IsNullOrEmpty(oldFormatter))
            {
                const string errorMsg = "The log formatter can't be empty!";
                throw new ApplicationException(this.ConvertMsg(errorMsg));
            }

            oldFormatter = $"{_modName} {oldFormatter}";
            if (!oldFormatter.Contains("{}"))
            {
                return oldFormatter;
            }

            var parts = oldFormatter.Split(Separator, StringSplitOptions.None);
            var sb = new StringBuilder();
            var index = 0;
            for (; index < parts.Length - 1; index++)
            {
                sb.Append(parts[index]).Append("{").Append(index).Append("}");
            }

            sb.Append(parts[index]);
            return sb.ToString();
        }
    }
}