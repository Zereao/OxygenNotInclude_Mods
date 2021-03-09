// @author Zereao
// @date 2021-03-09 21:55
// @Steam https://steamcommunity/id/hexaiolun/

using System.IO;
using KMod;

namespace CommonTools.utils
{
    /// <summary>路径工具类</summary>
    public class PathUtils
    {
        /// <summary> 私有构造函数</summary>
        private PathUtils()
        {
        }

        /// <summary>
        /// 获取 mods 文件夹路径：C:/Users/{user_name}/Documents/Klei/OxygenNotIncluded/mods
        /// </summary>
        /// <returns>mods 文件夹路径</returns>
        public static string GetModsPath()
        {
            return Manager.GetDirectory();
        }

        /// <summary>
        /// 获取配置文件路径：C:/Users/{user_name}/Documents/Klei/OxygenNotIncluded/mods/config
        /// </summary>
        /// <returns>配置文件路径</returns>
        public static string GetConfigPath()
        {
            var modsPath = GetModsPath();
            return Path.Combine(modsPath, "config");
        }

        /// <summary>
        /// 获取某个mod的配置文件路径：C:/Users/{user_name}/Documents/Klei/OxygenNotIncluded/mods/config/{modName}
        /// </summary>
        /// <returns>某个mod的配置文件路径</returns>
        public static string GetConfigPath(string modName)
        {
            var modsPath = GetModsPath();
            return Path.Combine(modsPath, "config", modName);
        }

        /// <summary>
        /// 获取某个mod配置文件的路径：C:/Users/{user_name}/Documents/Klei/OxygenNotIncluded/mods/config/{modName}/{configName}
        /// </summary>
        /// <returns>某个mod配置文件的路径</returns>
        public static string GetConfigPath(string modName, string configName)
        {
            var modsPath = GetModsPath();
            return Path.Combine(modsPath, "config", modName, configName);
        }

        /// <summary>
        /// 获取日志文件路径：C:/Users/{user_name}/Documents/Klei/OxygenNotIncluded/mods/logs
        /// </summary>
        /// <returns>日志文件路径</returns>
        public static string GetLogPath()
        {
            var modsPath = GetModsPath();
            return Path.Combine(modsPath, "logs");
        }

        /// <summary>
        /// 获取某个mod的日志文件路径：C:/Users/{user_name}/Documents/Klei/OxygenNotIncluded/mods/logs/{modName}
        /// </summary>
        /// <returns>某个mod的日志文件路径</returns>
        public static string GetLogPath(string modName)
        {
            var modsPath = GetModsPath();
            return Path.Combine(modsPath, "logs", modName);
        }

        /// <summary>
        /// 获取某个mod的日志文件路径：C:/Users/{user_name}/Documents/Klei/OxygenNotIncluded/mods/logs/{modName}/{logFileName}
        /// </summary>
        /// <returns>获取某个mod的日志文件路径</returns>
        public static string GetLogPath(string modName, string logFileName)
        {
            var modsPath = GetModsPath();
            return Path.Combine(modsPath, "logs", modName, logFileName);
        }
    }
}