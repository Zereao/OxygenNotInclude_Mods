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
    [HarmonyPatch(typeof(BaseHatchConfig), "BasicRockDiet")]
    public class BasicRockDietPatcher : AbstractPatcher
    {
        /// <summary>
        /// 新增食谱：若哈奇吃 铁矿，则产出 钢
        /// </summary>
        /// <param name="__result">原方法的结果；Harmony框架用法，必须要叫这名</param>
        /// <param name="poopTag">产物标签</param>
        /// <param name="diseaseId">病菌ID</param>
        /// <param name="diseasePerKgProduced">每公斤病产生病菌数</param>
        public static void Postfix(
            ref List<Diet.Info> __result,
            ref Tag poopTag,
            ref string diseaseId,
            ref float diseasePerKgProduced)
        {
            DoPatch(ref __result, ref poopTag, ref diseaseId, ref diseasePerKgProduced);
        }
    }

    /// <summary>
    /// 哈奇的配置，主要位于 BaseHatchConfig 类中;
    /// 需要修改的 光滑哈奇 主要吃金属，故需要修改 MetalDiet() 方法
    /// </summary>
    [HarmonyPatch(typeof(BaseHatchConfig), "HardRockDiet")]
    public class HardRockDietPatcher : AbstractPatcher
    {
        /// <summary>
        /// 新增食谱：若哈奇吃 铁矿，则产出 钢
        /// </summary>
        /// <param name="__result">原方法的结果；Harmony框架用法，必须要叫这名</param>
        /// <param name="poopTag">产物标签</param>
        /// <param name="diseaseId">病菌ID</param>
        /// <param name="diseasePerKgProduced">每公斤病产生病菌数</param>
        public static void Postfix(
            ref List<Diet.Info> __result,
            ref Tag poopTag,
            ref string diseaseId,
            ref float diseasePerKgProduced)
        {
            DoPatch(ref __result, ref poopTag, ref diseaseId, ref diseasePerKgProduced);
        }
    }

    /// <summary>
    /// 哈奇的配置，主要位于 BaseHatchConfig 类中;
    /// 需要修改的 光滑哈奇 主要吃金属，故需要修改 MetalDiet() 方法
    /// </summary>
    [HarmonyPatch(typeof(BaseHatchConfig), "MetalDiet")]
    public class MetalDietPatcher : AbstractPatcher
    {
        /// <summary>
        /// 新增食谱：若哈奇吃 铁矿，则产出 钢
        /// </summary>
        /// <param name="__result">原方法的结果；Harmony框架用法，必须要叫这名</param>
        /// <param name="poopTag">产物标签</param>
        /// <param name="diseaseId">病菌ID</param>
        /// <param name="diseasePerKgProduced">每公斤病产生病菌数</param>
        public static void Postfix(
            ref List<Diet.Info> __result,
            ref Tag poopTag,
            ref string diseaseId,
            ref float diseasePerKgProduced)
        {
            DoPatch(ref __result, ref poopTag, ref diseaseId, ref diseasePerKgProduced);
        }
    }

    /// <summary>
    /// 哈奇的配置，主要位于 BaseHatchConfig 类中;
    /// 需要修改的 光滑哈奇 主要吃金属，故需要修改 MetalDiet() 方法
    /// </summary>
    [HarmonyPatch(typeof(BaseHatchConfig), "VeggieDiet")]
    public class VeggieDietPatcher : AbstractPatcher
    {
        /// <summary>
        /// 新增食谱：若哈奇吃 铁矿，则产出 钢
        /// </summary>
        /// <param name="__result">原方法的结果；Harmony框架用法，必须要叫这名</param>
        /// <param name="poopTag">产物标签</param>
        /// <param name="diseaseId">病菌ID</param>
        /// <param name="diseasePerKgProduced">每公斤病产生病菌数</param>
        public static void Postfix(
            ref List<Diet.Info> __result,
            ref Tag poopTag,
            ref string diseaseId,
            ref float diseasePerKgProduced)
        {
            DoPatch(ref __result, ref poopTag, ref diseaseId, ref diseasePerKgProduced);
        }
    }

    [HarmonyPatch(typeof(BaseHatchConfig), "FoodDiet")]
    public class FoodDietPatcher : AbstractPatcher
    {
        /// <summary>
        /// 新增食谱：若哈奇吃 铁矿，则产出 钢
        /// </summary>
        /// <param name="__result">原方法的结果；Harmony框架用法，必须要叫这名</param>
        /// <param name="poopTag">产物标签</param>
        /// <param name="diseaseId">病菌ID</param>
        /// <param name="diseasePerKgProduced">每公斤病产生病菌数</param>
        public static void Postfix(
            ref List<Diet.Info> __result,
            ref Tag poopTag,
            ref string diseaseId,
            ref float diseasePerKgProduced)
        {
            DoPatch(ref __result, ref poopTag, ref diseaseId, ref diseasePerKgProduced);
        }
    }
}