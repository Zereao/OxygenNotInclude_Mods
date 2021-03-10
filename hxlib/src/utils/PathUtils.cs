// @author Zereao
// @date 2021-03-10 21:57
// @Steam https://steamcommunity/id/hexaiolun/

using System.IO;
using KMod;

namespace HXLib.utils
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
    }
}