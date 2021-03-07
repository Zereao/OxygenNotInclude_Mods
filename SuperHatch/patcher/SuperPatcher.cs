// @author Zereao
// @date 2021-03-06 21:57
// @Steam https://steamcommunity/id/hexaiolun/

using System.Collections.Generic;
using Harmony;

namespace SuperHatch.patcher
{
    [HarmonyPatch(typeof(BaseHatchConfig), "BasicRockDiet")]
    public class BasicRockDietPatcher : AbstractHatchDietInfoPatcher
    {
        /// <summary>
        /// 基础食物补丁
        /// </summary>
        /// <param name="__result">原方法的结果；Harmony框架用法，必须要叫这名</param>
        /// <param name="poopTag">产物标签</param>
        /// <param name="caloriesPerKg">每千克食物能够提供的卡路里数</param>
        /// <param name="producedConversionRate"></param>
        /// <param name="diseaseId">病菌ID</param>
        /// <param name="diseasePerKgProduced">每公斤病产生病菌数</param>
        public static void Postfix(
            ref List<Diet.Info> __result,
            ref Tag poopTag,
            ref float caloriesPerKg, ref float producedConversionRate,
            ref string diseaseId, ref float diseasePerKgProduced)
        {
            DoPatch(ref __result, ref poopTag,
                ref caloriesPerKg, ref producedConversionRate,
                ref diseaseId, ref diseasePerKgProduced);
        }
    }

    [HarmonyPatch(typeof(BaseHatchConfig), "HardRockDiet")]
    public class HardRockDietPatcher : AbstractHatchDietInfoPatcher
    {
        /// <summary>
        /// 硬质食物补丁
        /// </summary>
        /// <param name="__result">原方法的结果；Harmony框架用法，必须要叫这名</param>
        /// <param name="poopTag">产物标签</param>
        /// <param name="caloriesPerKg">每千克食物能够提供的卡路里数</param>
        /// <param name="producedConversionRate"></param>
        /// <param name="diseaseId">病菌ID</param>
        /// <param name="diseasePerKgProduced">每公斤病产生病菌数</param>
        public static void Postfix(
            ref List<Diet.Info> __result,
            ref Tag poopTag,
            ref float caloriesPerKg, ref float producedConversionRate,
            ref string diseaseId, ref float diseasePerKgProduced)
        {
            DoPatch(ref __result, ref poopTag, ref caloriesPerKg,
                ref producedConversionRate, ref diseaseId,
                ref diseasePerKgProduced);
        }
    }

    [HarmonyPatch(typeof(BaseHatchConfig), "MetalDiet")]
    public class MetalDietPatcher : AbstractHatchDietInfoPatcher
    {
        /// <summary>
        /// 金属食物补丁
        /// </summary>
        /// <param name="__result">原方法的结果；Harmony框架用法，必须要叫这名</param>
        /// <param name="poopTag">产物标签</param>
        /// <param name="caloriesPerKg">每千克食物能够提供的卡路里数</param>
        /// <param name="producedConversionRate"></param>
        /// <param name="diseaseId">病菌ID</param>
        /// <param name="diseasePerKgProduced">每公斤病产生病菌数</param>
        public static void Postfix(
            ref List<Diet.Info> __result,
            ref Tag poopTag,
            ref float caloriesPerKg, ref float producedConversionRate,
            ref string diseaseId, ref float diseasePerKgProduced)
        {
            DoPatch(ref __result, ref poopTag,
                ref caloriesPerKg, ref producedConversionRate,
                ref diseaseId, ref diseasePerKgProduced);
        }
    }

    [HarmonyPatch(typeof(BaseHatchConfig), "VeggieDiet")]
    public class VeggieDietPatcher : AbstractHatchDietInfoPatcher
    {
        /// <summary>
        /// 素食补丁
        /// </summary>
        /// <param name="__result">原方法的结果；Harmony框架用法，必须要叫这名</param>
        /// <param name="poopTag">产物标签</param>
        /// <param name="caloriesPerKg">每千克食物能够提供的卡路里数</param>
        /// <param name="producedConversionRate"></param>
        /// <param name="diseaseId">病菌ID</param>
        /// <param name="diseasePerKgProduced">每公斤病产生病菌数</param>
        public static void Postfix(
            ref List<Diet.Info> __result,
            ref Tag poopTag,
            ref float caloriesPerKg, ref float producedConversionRate,
            ref string diseaseId, ref float diseasePerKgProduced)
        {
            DoPatch(ref __result, ref poopTag,
                ref caloriesPerKg, ref producedConversionRate,
                ref diseaseId, ref diseasePerKgProduced);
        }
    }

    [HarmonyPatch(typeof(BaseHatchConfig), "FoodDiet")]
    public class FoodDietPatcher : AbstractHatchDietInfoPatcher
    {
        /// <summary>
        /// 制造食物补丁
        /// </summary>
        /// <param name="__result">原方法的结果；Harmony框架用法，必须要叫这名</param>
        /// <param name="poopTag">产物标签</param>
        /// <param name="caloriesPerKg">每千克食物能够提供的卡路里数</param>
        /// <param name="producedConversionRate"></param>
        /// <param name="diseaseId">病菌ID</param>
        /// <param name="diseasePerKgProduced">每公斤病产生病菌数</param>
        public static void Postfix(
            ref List<Diet.Info> __result,
            ref Tag poopTag,
            ref float caloriesPerKg, ref float producedConversionRate,
            ref string diseaseId, ref float diseasePerKgProduced)
        {
            DoPatch(ref __result, ref poopTag,
                ref caloriesPerKg, ref producedConversionRate,
                ref diseaseId, ref diseasePerKgProduced);
        }
    }
}