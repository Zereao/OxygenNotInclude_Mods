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
        /// <param name="producedConversionRate">转换率</param>
        /// <param name="diseaseId">疾病ID，不做处理，只用作值传递</param>
        /// <param name="diseasePerKgProduced">每千克产生疾病数，不做处理，只用作值传递</param>
        public static void DoPatch(
            ref List<Diet.Info> result,
            float producedConversionRate,
            ref string diseaseId, ref float diseasePerKgProduced)
        {
            var newResultList = new List<Diet.Info>();
            foreach (var dietInfo in result)
            {
                var consumedTags = dietInfo.consumedTags;
                var produceTag = dietInfo.producedElement;
                foreach (var consumedTag in consumedTags)
                {
                    var consumeTagName = consumedTag.Name;
                    var caloriesPerKg = dietInfo.caloriesPerKg;
                    var produceTagName = produceTag.Name;
                    var conversionRate = producedConversionRate;
                    if (ConfigMapping.ContainsKey(consumeTagName) &&
                        ConfigMapping[consumeTagName] is ConfigModel config)
                    {
                        produceTagName = config.ProduceName;
                        caloriesPerKg = HatchTuning.STANDARD_CALORIES_PER_CYCLE / config.ConsumeEachCycle;
                        conversionRate = config.ProduceEachCycle / config.ConsumeEachCycle;
                        // 下面是日志输出所需数据
                        var oldConsumeEachCycle = HatchTuning.STANDARD_CALORIES_PER_CYCLE / caloriesPerKg;
                        var oldProduceEachCycle = oldConsumeEachCycle * producedConversionRate;
                        Log.Info(
                            "为食物 = {} 加载配置文件！\n原产物：{}，新产物：{}\n原每周期消耗：{}kg，现每周期消耗：{}kg\n原每周期产出：{}kg，现每周期产出：{}kg\n原转换率：{}，新转换率：{}",
                            consumeTagName,
                            produceTagName, config.ProduceName,
                            oldConsumeEachCycle, config.ConsumeEachCycle,
                            oldProduceEachCycle, config.ProduceEachCycle,
                            producedConversionRate, conversionRate
                        );
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
            consumeTag = Enum.TryParse(consumeTagNameOfConfig, true, out SimHashes consumeHash)
                ? consumeHash.CreateTag()
                : consumeTag;
            produceTag = Enum.TryParse(produceTagNameOfConfig, true, out SimHashes produceHash)
                ? produceHash.CreateTag()
                : produceTag;
            var consumedTagSet = new HashSet<Tag> {consumeTag};
            return new Diet.Info(
                consumedTagSet, produceTag,
                caloriesPerKg, producedConversionRate,
                diseaseId, diseasePerKgProduced
            );
        }

        /// <summary>
        /// 本地Debug调试时使用
        /// </summary>
        /// <param name="result">需要输出的信息</param>
        private static void DebugToString(IEnumerable<Diet.Info> result)
        {
            Log.Info("**************************** 输出原始调试信息 ****************************");
            foreach (var dietInfo in result)
            {
                var consumedTags = dietInfo.consumedTags;
                var produceTag = dietInfo.producedElement;
                var caloriesPerKg = dietInfo.caloriesPerKg;
                var produceTagName = produceTag.Name;
                foreach (var consumedTag in consumedTags)
                {
                    Log.Info("消费：{}，产生：{}，caloriesPerKg = {}，转换率：{}",
                        consumedTag.Name, produceTagName, caloriesPerKg, dietInfo.producedConversionRate);
                }
            }

            Log.Info("************************************************************************");
        }
    }
}