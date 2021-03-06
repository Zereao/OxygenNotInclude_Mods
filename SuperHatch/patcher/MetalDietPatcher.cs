// @author Zereao
// @date 2021-03-06 21:57
// @Steam https://steamcommunity/id/hexaiolun/

using System.Collections.Generic;
using Harmony;

namespace SuperHatch.patcher
{
    /// <summary>
    /// 哈奇的配置，主要位于 BaseHatchConfig 类中;
    /// 需要修改的 光滑哈奇 主要吃金属，故需要修改 MetalDiet() 方法
    /// </summary>
    [HarmonyPatch(typeof(BaseHatchConfig), "MetalDiet")]
    public class MetalDietPatcher
    {
        /// <summary>
        /// 修改哈奇每周期消费的矿石量，以及产出量
        /// 修改原理：
        ///     在 HatchTuning类中定义了下面这个变量：
        ///         // 默认情况下，哈奇每周期需要消耗 700000卡路里；
        ///         public static float STANDARD_CALORIES_PER_CYCLE = 700000f;
        ///     在 HatchMetalConfig类中定义了下面这个变量：
        ///         // 默认情况下，哈奇每个周期要吃掉 100kg 的矿石
        ///         private static float KG_ORE_EATEN_PER_CYCLE = 100f;
        /// 默认情况下：
        ///     下面的变量 caloriesPerKg = STANDARD_CALORIES_PER_CYCLE / KG_ORE_EATEN_PER_CYCLE = 7000，
        ///     即 默认情况下，每千克矿石可以提供 7000卡路里。
        ///
        /// 在下面的 Prefix中，我修改 caloriesPerKg 的值为 除以10后的值，意味着每千克矿石提供的卡路里变为了原来的 1/10，
        ///     则每个周期需要消耗的矿石变为了原来的10倍。
        /// </summary>
        /// <param name="caloriesPerKg">每千克矿石所能提供的卡路里数</param>
        /// <param name="producedConversionRate">消耗与产出的转换率</param>
        public static void Prefix(ref float caloriesPerKg, ref float producedConversionRate)
        {
            caloriesPerKg /= 10f;
            producedConversionRate = 1f;
        }

        /// <summary>
        /// 新增食谱：若哈奇吃 铁矿，则产出 钢
        /// </summary>
        /// <param name="__result">原方法的结果；Harmony框架用法，必须要叫这名</param>
        /// <param name="poopTag">产物标签</param>
        /// <param name="caloriesPerKg">每千克矿石提供的卡路里数</param>
        /// <param name="producedConversionRate">每千克矿石所能提供的卡路里数</param>
        /// <param name="diseaseId">病菌ID</param>
        /// <param name="diseasePerKgProduced">每公斤病产生病菌数</param>
        public static void Postfix(
            ref List<Diet.Info> __result,
            ref Tag poopTag,
            ref float caloriesPerKg,
            ref float producedConversionRate,
            ref string diseaseId,
            ref float diseasePerKgProduced)
        {
            var consumedTagSet = new HashSet<Tag> {SimHashes.Iron.CreateTag()};
            var dietInfo = new Diet.Info(
                // 消耗食物的标签
                consumedTagSet,
                // 产出物品的标签；如果产物是金属，则产出 钢；否则，产出原产物
                poopTag == GameTags.Metal ? SimHashes.Steel.CreateTag() : poopTag,
                caloriesPerKg,
                producedConversionRate,
                diseaseId, diseasePerKgProduced
            );
            __result.Add(dietInfo);
        }
    }
}