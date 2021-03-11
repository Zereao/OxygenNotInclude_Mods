// @author Zereao
// @date 2021-03-06 17:32
// @Steam https://steamcommunity/id/hexaiolun/

using System.Collections;
using System.Collections.Generic;
using HXLib.utils;
using Newtonsoft.Json;

namespace SuperHatch.config
{
    /// <summary>哈奇食物模型</summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class ConfigModel
    {
        /// <summary>全局哈奇食物配置</summary>
        public GlobalConfig GlobalConfig = new GlobalConfig();

        /// <summary>自定义哈奇食物配置</summary>
        public List<CustomConfig> CustomConfig = new List<CustomConfig> {new CustomConfig()};

        /// <summary>
        /// 自定义配置映射；key - 食物名称，value - 食物数据
        /// </summary>
        [JsonIgnore] public Hashtable CustomConfigMapping = CollectionUtils.EmptyMap;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    /// <summary>全局哈奇食物配置</summary>
    public class GlobalConfig
    {
        /// <summary>每周期消耗量，单位 千克</summary>
        public float ConsumeEachCycle = -1.0f;

        /// <summary>
        /// 每周期产量，单位 千克
        /// </summary>
        public float ProduceEachCycle = -1.0f;

        public override string ToString()
        {
            return
                $"GlobalConfig =【ConsumeEachCycle = {ConsumeEachCycle}, ProduceEachCycle = {ProduceEachCycle}】";
        }
    }

    /// <summary>自定义哈奇食物配置</summary>
    public class CustomConfig
    {
        /// <summary>哈奇食物</summary>
        public string ConsumeName = "Sand";

        /// <summary>哈奇产出</summary>
        public string ProduceName = "Steel";

        /// <summary>每周期消耗量，单位 千克</summary>
        public float ConsumeEachCycle = 10f;

        /// <summary>
        /// 每周期产量，单位 千克
        /// </summary>
        public float ProduceEachCycle = 1000f;

        public override string ToString()
        {
            return
                $"CustomConfig =【ConsumeName = {ConsumeName}, ProduceName = {ProduceName}, ConsumeEachCycle = {ConsumeEachCycle}, ProduceEachCycle = {ProduceEachCycle}】";
        }
    }
}