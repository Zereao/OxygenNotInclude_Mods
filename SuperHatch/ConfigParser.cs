// @author Zereao
// @date 2021-03-06 17:31
// @Steam https://steamcommunity/id/hexaiolun/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using KMod;
using Newtonsoft.Json;
using SuperHatch.utils;

namespace SuperHatch
{
    public class ConfigParser
    {
        private static readonly Logger Log = new Logger(GlobalConstants.ModName);

        /// <summary>
        /// 配置文件目录路径
        /// </summary>
        private static readonly string ConfigPath =
            Path.Combine(Manager.GetDirectory(), "config", GlobalConstants.ModName);

        /// <summary>
        /// 配置文件路径
        /// </summary>
        private static readonly string ConfigFilePath = Path.Combine(ConfigPath, GlobalConstants.ModName + ".json");

        /// <summary>
        /// 尝试读取配置文件；若不存在配置文件，或配置文件校验出错，则使用默认配置
        /// </summary>
        /// <returns>
        /// 
        /// </returns>
        public static List<ConfigModel> GetOrDefaultConfig()
        {
            if (!Directory.Exists(ConfigPath))
            {
                Log.Info("本地配置文件目录不存在！创建文件目录，并将默认配置文件写入本地！配置文件路径：{}", ConfigFilePath);
                Directory.CreateDirectory(ConfigPath);
                return WriteDefaultConfig();
            }

            if (!File.Exists(ConfigFilePath))
            {
                Log.Info("本地不存在配置文件，将默认配置文件写入本地！配置文件路径：{}", ConfigFilePath);
                return WriteDefaultConfig();
            }

            try
            {
                using (var sr = new StreamReader(ConfigFilePath))
                {
                    var jsonStr = sr.ReadToEnd();
                    Log.Info("从本地配置文件中读取到配置：\n{}", jsonStr);
                    return string.IsNullOrEmpty(jsonStr)
                        ? CollectionUtils.EmptyList<ConfigModel>()
                        : JsonConvert.DeserializeObject<List<ConfigModel>>(jsonStr);
                }
            }
            catch (Exception e)
            {
                Log.Warning("配置文件校验未通过！请检查配置文件是否符合JSON格式！配置文件路径：{}", ConfigFilePath, e);
                return CollectionUtils.EmptyList<ConfigModel>();
            }
        }

        /// <summary>
        /// 将默认配置文件写到本地；默认配置文件参考 ConfigModel 类的默认值
        /// </summary>
        /// <returns>默认配置</returns>
        public static List<ConfigModel> WriteDefaultConfig()
        {
            var defaultConfig = new List<ConfigModel> {new ConfigModel()};
            var defaultConfigJson = JsonConvert.SerializeObject(defaultConfig, Formatting.Indented);
            using (var writer = new StreamWriter(ConfigFilePath))
            {
                writer.WriteLine(defaultConfigJson);
            }

            Log.Info("已将默认配置写到本地！默认配置：\n{}", defaultConfigJson);
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
                Log.Info("从配置文件中读取到的配置为空！故将配置映射为空映射关系！");
                return CollectionUtils.EmptyMap;
            }

            var configMapping = new Hashtable(config.Count);
            Log.Info("准备开始执行配置映射：");
            foreach (var configModel in config)
            {
                var name = configModel.ConsumeName;
                configMapping.Add(name, configModel);
                Log.Info("key: {}，value：{}", name, configModel.ToString());
            }

            return configMapping;
        }
    }
}