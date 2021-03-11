// @author Zereao
// @date 2021-03-06 17:31
// @Steam https://steamcommunity/id/hexaiolun/

using System;
using System.Collections;
using System.IO;
using HXLib.logging;
using HXLib.utils;
using Newtonsoft.Json;
using SuperHatch.common;

namespace SuperHatch.config
{
    public class ConfigParser
    {
        private static readonly Log Log = Log.GetLogger(Const.ModName);

        /// <summary>配置文件目录路径</summary>
        private static readonly string ConfigPath = PathUtils.GetConfigPath(Const.ModName);

        /// <summary>配置文件路径</summary>
        private static readonly string ConfigFilePath = PathUtils.GetConfigPath(Const.ModName, Const.ConfigName);

        /// <summary>尝试读取配置文件；若不存在配置文件，或配置文件校验出错，则使用默认配置</summary>
        /// <returns>解析出的配置</returns>
        public static ConfigModel GetOrDefaultConfig()
        {
            if (!Directory.Exists(ConfigPath))
            {
                Log.InfoFormat("本地配置文件目录不存在！创建文件目录，并将默认配置文件写入本地！配置文件路径：{0}", ConfigFilePath);
                Directory.CreateDirectory(ConfigPath);
                return WriteDefaultConfig();
            }

            if (!File.Exists(ConfigFilePath))
            {
                Log.InfoFormat("本地不存在配置文件，将默认配置文件写入本地！配置文件路径：{0}", ConfigFilePath);
                return WriteDefaultConfig();
            }

            try
            {
                using var sr = new StreamReader(ConfigFilePath);
                var jsonStr = sr.ReadToEnd();
                Log.InfoFormat("从本地配置文件中读取到配置：{0}", jsonStr);
                return string.IsNullOrEmpty(jsonStr)
                    ? null
                    : JsonConvert.DeserializeObject<ConfigModel>(jsonStr,
                        new JsonSerializerSettings {ObjectCreationHandling = ObjectCreationHandling.Replace});
            }
            catch (Exception e)
            {
                Log.Error($"配置文件校验未通过！请检查配置文件是否符合JSON格式！配置文件路径：{ConfigFilePath}");
                Log.Error(e);
                return null;
            }
        }

        /// <summary>将默认配置文件写到本地；默认配置文件参考 ConfigModel 类的默认值</summary>
        /// <returns>默认配置</returns>
        public static ConfigModel WriteDefaultConfig()
        {
            var defaultConfig = new ConfigModel();
            var defaultConfigJson = JsonConvert.SerializeObject(defaultConfig, Formatting.Indented);
            using var writer = new StreamWriter(ConfigFilePath);
            writer.WriteLine(defaultConfigJson);
            Log.InfoFormat("已将默认配置写到本地！默认配置：{0}", defaultConfigJson);
            return defaultConfig;
        }

        /// <summary>将配置文件中的数据，映射为一个 HashTable，并获取最终组装后的ConfigModel</summary>
        /// <returns>最终组装后的ConfigModel</returns>
        public static ConfigModel GetConfig()
        {
            var configModel = GetOrDefaultConfig();
            if (configModel == null)
            {
                Log.Info("从配置文件中读取到的配置为空！故将配置映射为空映射关系！");
                return null;
            }

            var customConfig = configModel.CustomConfig;
            if (CollectionUtils.IsEmpty(customConfig))
            {
                Log.Info("从配置文件中读取到的自定义配置(CustomConfig)为空！");
                return configModel;
            }

            var customConfigMapping = new Hashtable(customConfig.Count);
            Log.Info("准备开始执行配置映射：");
            foreach (var config in customConfig)
            {
                var name = config.ConsumeName;
                if (customConfigMapping.ContainsKey(name))
                {
                    Log.InfoFormat("当前食物【{0}】的映射关系已经存在！忽略配置！", name);
                    continue;
                }

                customConfigMapping.Add(name, config);
                Log.InfoFormat("配置映射关系：key: {0}，value：{1}", name, config);
            }

            configModel.CustomConfigMapping = customConfigMapping;
            return configModel;
        }
    }
}