// @author Zereao
// @date 2021-03-06 22:20
// @Steam https://steamcommunity/id/hexaiolun/

using System;
using System.Collections;
using System.Collections.Generic;

namespace SuperHatch.patcher
{
    public abstract class AbstractPatcher
    {
        private static Hashtable _configMapping = ConfigParser.GetConfigMapping();

        public static void DoPatch(
            ref List<Diet.Info> result,
            ref Tag poopTag,
            ref float caloriesPerKg,
            ref float producedConversionRate,
            ref string diseaseId,
            ref float diseasePerKgProduced)
        {
            var newResultList = new List<Diet.Info>();
            foreach (var dietInfo in result)
            {
                var consumedTags = dietInfo.consumedTags;
                foreach (var consumedTag in consumedTags)
                {
                    var consumeTagName = consumedTag.Name;
                    if (!_configMapping.ContainsKey(consumeTagName)) continue;
                    if (!(_configMapping[consumeTagName] is ConfigModel config)) continue;
                    var newDietInfo = BuildInfo(
                        consumedTag, consumeTagName,
                        poopTag, config.ProduceName,
                        HatchTuning.STANDARD_CALORIES_PER_CYCLE / config.EatenEachCycle, config.ConversionRate,
                        diseaseId, diseasePerKgProduced
                    );
                    newResultList.Add(newDietInfo);
                }
            }

            result = newResultList;
        }


        private static Diet.Info BuildInfo(
            Tag consumeTag, string consumeTagNameOfConfig,
            Tag produceTag, string produceTagNameOfConfig,
            float caloriesPerKg,
            float producedConversionRate,
            string diseaseId,
            float diseasePerKgProduced
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
                // 消耗食物的标签
                consumedTagSet,
                produceTag,
                caloriesPerKg,
                producedConversionRate,
                diseaseId, diseasePerKgProduced
            );
        }
    }
}