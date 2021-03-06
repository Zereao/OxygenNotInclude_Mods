// @author Zereao
// @date 2021-03-06 17:31
// @Steam https://steamcommunity/id/hexaiolun/

using System;
using System.Collections;
using System.IO;
using KMod;
using Newtonsoft.Json;
using SuperHatch.utils;

namespace SuperHatch
{
    public class ConfigParser
    {
        private static readonly Logger Log = new Logger("SuperHatch");

        /// <summary>
        /// 配置文件目录路径
        /// </summary>
        private static readonly string ConfigPath = Path.Combine(Manager.GetDirectory(), "settings");

        /// <summary>
        /// 配置文件路径
        /// </summary>
        private static readonly string ConfigFilePath = Path.Combine(ConfigPath, "SuperHatch.json");

        public static Hashtable ConfigMapping;

        /// <summary>
        /// 尝试读取配置文件；若不存在配置文件，或配置文件校验出错，则使用默认配置
        /// </summary>
        /// <returns>
        /// 
        /// </returns>
        public static ArrayList GetOrDefaultConfig()
        {
            var fileNotExists = !Directory.Exists(ConfigPath) || !File.Exists(ConfigFilePath);
            if (fileNotExists)
            {
                return ConfigParser.WriteDefaultConfig();
            }

            try
            {
                using (var sr = new StreamReader(ConfigFilePath))
                {
                    var jsonStr = sr.ReadToEnd();
                    return string.IsNullOrEmpty(jsonStr)
                        ? CollectionUtils.EmptyList
                        : JsonConvert.DeserializeObject<ArrayList>(jsonStr);
                }
            }
            catch (Exception e)
            {
                Log.Warning("配置文件校验未通过！请检查配置文件是否符合JSON格式！配置文件路径：{}", ConfigFilePath, e);
                return CollectionUtils.EmptyList;
            }
        }

        /// <summary>
        /// 将默认配置文件写到本地；默认配置文件参考 ConfigModel 类的默认值
        /// </summary>
        /// <returns>默认配置</returns>
        public static ArrayList WriteDefaultConfig()
        {
            var defaultConfig = new ArrayList {new ConfigModel()};
            var defaultConfigJson = JsonConvert.SerializeObject(defaultConfig, Formatting.Indented);
            using (var writer = new StreamWriter(ConfigFilePath))
            {
                writer.WriteLine(defaultConfigJson);
            }

            return defaultConfig;
        }

        /// <summary>
        /// 将配置文件中的数据，映射为一个 HashTable
        /// </summary>
        /// <returns>key - 食物名称，value - 食物数据</returns>
        public static Hashtable GetConfigMapping()
        {
            var config = GetOrDefaultConfig();
            if (CollectionUtils.IsEmpty(config))
            {
                return CollectionUtils.EmptyMap;
            }

            var configMapping = new Hashtable(config.Count);
            foreach (ConfigModel configModel in config)
            {
                var name = configModel.ConsumeName;
                configMapping.Add(name, configModel);
            }

            // 缓存起来，后续就不需要再每次读文件了
            ConfigMapping = configMapping;
            return configMapping;
        }
    }
}