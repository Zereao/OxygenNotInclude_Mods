// @author Zereao
// @date 2021-03-06 22:20
// @Steam https://steamcommunity/id/hexaiolun/

using System;
using System.Collections;
using System.Collections.Generic;
using SuperHatch.common;
using SuperHatch.config;

namespace SuperHatch.patcher
{
    /// <summary>抽象 哈奇食物信息 补丁类</summary>
    public abstract class AbstractHatchDietInfoPatcher
    {
        private static readonly Hashtable ConfigMapping = ConfigParser.GetConfigMapping();

        private static readonly common.Logger Log = new common.Logger(GlobalConstants.ModName);

        /// <summary>
        /// 补丁逻辑：
        /// 如果配置项不能解析成功，则保持原样；否则，加载配置项
        /// </summary>
        /// <param name="result">原始处理结果</param>
        /// <param name="produceTag">产出信息标签</param>
        /// <param name="caloriesPerKg">每千克食物能够提供的卡路里数</param>
        /// <param name="conversionRate">转换率</param>
        /// <param name="diseaseId">疾病ID，不做处理，只用作值传递</param>
        /// <param name="diseasePerKgProduced">每千克产生疾病数，不做处理，只用作值传递</param>
        public static void DoPatch(
            ref List<Diet.Info> result,
            ref Tag produceTag,
            ref float caloriesPerKg, ref float conversionRate,
            ref string diseaseId, ref float diseasePerKgProduced)
        {
            Log.Info("准备开始执行补丁补丁逻辑。。。。。");
            var newResultList = new List<Diet.Info>();
            foreach (var dietInfo in result)
            {
                var consumedTags = dietInfo.consumedTags;
                foreach (var consumedTag in consumedTags)
                {
                    var consumeTagName = consumedTag.Name;
                    var produceTagName = produceTag.Name;
                    if (ConfigMapping.ContainsKey(consumeTagName) &&
                        ConfigMapping[consumeTagName] is ConfigModel config)
                    {
                        produceTagName = config.ProduceName;
                        caloriesPerKg = HatchTuning.STANDARD_CALORIES_PER_CYCLE / config.EatenEachCycle;
                        conversionRate = config.ConversionRate;
                    }

                    var newDietInfo = BuildInfo(
                        consumedTag, consumeTagName,
                        produceTag, produceTagName,
                        caloriesPerKg, conversionRate,
                        diseaseId, diseasePerKgProduced
                    );
                    newResultList.Add(newDietInfo);
                }
            }

            result = newResultList;
        }


        /// <summary>
        /// 构建食物信息
        /// </summary>
        /// <param name="consumeTag">消费材料标签</param>
        /// <param name="consumeTagNameOfConfig">配置文件中的消费材料名称</param>
        /// <param name="produceTag">产物标签</param>
        /// <param name="produceTagNameOfConfig">配置文件中的产物标签</param>
        /// <param name="caloriesPerKg">消费材料每千克能提供的卡路里数</param>
        /// <param name="producedConversionRate">产物转唤率</param>
        /// <param name="diseaseId">疾病ID，不做处理，只用于传值</param>
        /// <param name="diseasePerKgProduced">每千克产生疾病数，不做处理，只用作值传递</param>
        /// <returns>新的食物信息</returns>
        private static Diet.Info BuildInfo(
            Tag consumeTag, string consumeTagNameOfConfig,
            Tag produceTag, string produceTagNameOfConfig,
            float caloriesPerKg, float producedConversionRate,
            string diseaseId, float diseasePerKgProduced
        )
        {
            consumeTag = Enum.TryParse(consumeTagNameOfConfig, out SimHashes consumeHash)
                ? consumeHash.CreateTag()
                : consumeTag;
            produceTag = Enum.TryParse(produceTagNameOfConfig, out SimHashes produceHash)
                ? produceHash.CreateTag()
                : produceTag;
            var consumedTagSet = new HashSet<Tag> {consumeTag};
            return new Diet.Info(
                consumedTagSet, produceTag,
                caloriesPerKg, producedConversionRate,
                diseaseId, diseasePerKgProduced
            );
        }
    }
}