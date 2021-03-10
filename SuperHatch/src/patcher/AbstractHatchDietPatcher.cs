// @author Zereao
// @date 2021-03-06 22:20
// @Steam https://steamcommunity/id/hexaiolun/

using System;
using System.Collections.Generic;
using HXLib.logging;
using HXLib.utils;
using SuperHatch.common;
using SuperHatch.config;

namespace SuperHatch.patcher
{
    /// <summary>抽象 哈奇食物信息 补丁类</summary>
    public abstract class AbstractHatchDietPatcher
    {
        private static readonly Log Log = Log.GetLogger(Const.ConfigName);

        private static readonly ConfigModel Config = ConfigParser.GetConfig();

        /// <summary>
        /// 前置处理，用于加载全局配置；补丁逻辑：
        /// 如果前置配置(GlobalConfig)项为空，或不能解析成功，则保持原样；否则，加载全局配置
        /// </summary>
        /// <param name="caloriesPerKg">每千克食物能提供的卡路里数</param>
        /// <param name="producedConversionRate">转换率</param>
        public static void DoPrefixPatch(ref float caloriesPerKg, ref float producedConversionRate)
        {
            var customConfig = Config.GlobalConfig;
            var consumeEachCycle = customConfig.ConsumeEachCycle;
            var produceEachCycle = customConfig.ProduceEachCycle;
            if (consumeEachCycle <= 0.0f)
            {
                Log.InfoFormat("全局配置未加载！每周期消耗量必须大于0，否则不生效！consumeEachCycle = {0}", consumeEachCycle);
                return;
            }

            // 每千克食物能提供的卡路里数 = 哈奇每周期的卡路里需求 / 哈奇每周期需要消耗的食物千克数
            caloriesPerKg = HatchTuning.STANDARD_CALORIES_PER_CYCLE / consumeEachCycle;

            if (produceEachCycle > 0.0f)
            {
                // 消耗产出转化率 = 哈奇每周期的产物千克数 / 哈奇每周期需要消耗的食物千克数
                producedConversionRate = produceEachCycle / consumeEachCycle;
            }

            Log.InfoFormat("全局配置加载完毕！newCaloriesPerKg = {0}，newProducedConversionRate = {1}",
                caloriesPerKg, producedConversionRate);
        }

        /// <summary>
        /// 后置处理，用于加载自定义配置；补丁逻辑：
        /// 如果 customConfigMapping中有数据，则按照映射进行个性化配置
        /// </summary>
        /// <param name="result">原始处理结果</param>
        /// <param name="producedConversionRate">转换率</param>
        /// <param name="diseaseId">疾病ID，不做处理，只用作值传递</param>
        /// <param name="diseasePerKgProduced">每千克产生疾病数，不做处理，只用作值传递</param>
        public static void DoPostfixPatch(
            ref List<Diet.Info> result,
            float producedConversionRate,
            ref string diseaseId, ref float diseasePerKgProduced)
        {
            var configMapping = Config.CustomConfigMapping;
            if (CollectionUtils.IsEmpty(configMapping))
            {
                Log.Info("配置映射为空！未加载任何自定义配置！");
                return;
            }

            var newResultList = new List<Diet.Info>(configMapping.Count);
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
                    if (configMapping.ContainsKey(consumeTagName) &&
                        configMapping[consumeTagName] is CustomConfig customConfig)
                    {
                        produceTagName = customConfig.ProduceName;
                        caloriesPerKg = HatchTuning.STANDARD_CALORIES_PER_CYCLE / customConfig.ConsumeEachCycle;
                        conversionRate = customConfig.ProduceEachCycle / customConfig.ConsumeEachCycle;
                        // 下面是日志输出所需数据
                        var oldConsumeEachCycle = HatchTuning.STANDARD_CALORIES_PER_CYCLE / caloriesPerKg;
                        var oldProduceEachCycle = oldConsumeEachCycle * producedConversionRate;
                        Log.InfoFormat(
                            "为食物 = {0} 加载配置文件！\n原产物：{1}，新产物：{2}\n原每周期消耗：{3}kg，现每周期消耗：{4}kg\n原每周期产出：{5}kg，现每周期产出：{6}kg\n原转换率：{7}，新转换率：{8}",
                            consumeTagName,
                            produceTagName, customConfig.ProduceName,
                            oldConsumeEachCycle, customConfig.ConsumeEachCycle,
                            oldProduceEachCycle, customConfig.ProduceEachCycle,
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
    }
}