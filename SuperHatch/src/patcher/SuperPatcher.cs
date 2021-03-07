// @author Zereao
// @date 2021-03-06 21:57
// @Steam https://steamcommunity/id/hexaiolun/

using System.Collections.Generic;
using Harmony;
using SuperHatch.common;

namespace SuperHatch.patcher
{
    [HarmonyPatch(typeof(BaseHatchConfig), "BasicRockDiet")]
    public class BasicRockDietPatcher : AbstractHatchDietInfoPatcher
    {
        private static readonly common.Logger Log = new common.Logger(GlobalConstants.ModName);

        /// <summary>
        /// 基础食物补丁
        /// </summary>
        /// <param name="__result">原方法的结果；Harmony框架用法，必须要叫这名</param>
        /// <param name="producedConversionRate">产物转化率</param>
        /// <param name="diseaseId">病菌ID</param>
        /// <param name="diseasePerKgProduced">每公斤病产生病菌数</param>
        public static void Postfix(
            ref List<Diet.Info> __result,
            ref float producedConversionRate,
            ref string diseaseId, ref float diseasePerKgProduced)
        {
            Log.Info("准备开始执行【BasicRockDiet】补丁逻辑。");
            DoPatch(ref __result,
                producedConversionRate,
                ref diseaseId, ref diseasePerKgProduced);
        }
    }

    [HarmonyPatch(typeof(BaseHatchConfig), "HardRockDiet")]
    public class HardRockDietPatcher : AbstractHatchDietInfoPatcher
    {
        private static readonly common.Logger Log = new common.Logger(GlobalConstants.ModName);

        /// <summary>
        /// 硬质食物补丁
        /// </summary>
        /// <param name="__result">原方法的结果；Harmony框架用法，必须要叫这名</param>
        /// <param name="producedConversionRate">产物转化率</param>
        /// <param name="diseaseId">病菌ID</param>
        /// <param name="diseasePerKgProduced">每公斤病产生病菌数</param>
        public static void Postfix(
            ref List<Diet.Info> __result,
            ref float producedConversionRate,
            ref string diseaseId, ref float diseasePerKgProduced)
        {
            Log.Info("准备开始执行【HardRockDiet】补丁逻辑。");
            DoPatch(ref __result,
                producedConversionRate,
                ref diseaseId, ref diseasePerKgProduced);
        }
    }

    [HarmonyPatch(typeof(BaseHatchConfig), "MetalDiet")]
    public class MetalDietPatcher : AbstractHatchDietInfoPatcher
    {
        private static readonly common.Logger Log = new common.Logger(GlobalConstants.ModName);

        /// <summary>
        /// 金属食物补丁
        /// </summary>
        /// <param name="__result">原方法的结果；Harmony框架用法，必须要叫这名</param>
        /// <param name="producedConversionRate">产物转化率</param>
        /// <param name="diseaseId">病菌ID</param>
        /// <param name="diseasePerKgProduced">每公斤病产生病菌数</param>
        public static void Postfix(
            ref List<Diet.Info> __result,
            ref float producedConversionRate,
            ref string diseaseId, ref float diseasePerKgProduced)
        {
            Log.Info("准备开始执行【MetalDiet】补丁逻辑。");
            DoPatch(ref __result,
                producedConversionRate,
                ref diseaseId, ref diseasePerKgProduced);
        }
    }

    [HarmonyPatch(typeof(BaseHatchConfig), "VeggieDiet")]
    public class VeggieDietPatcher : AbstractHatchDietInfoPatcher
    {
        private static readonly common.Logger Log = new common.Logger(GlobalConstants.ModName);

        /// <summary>
        /// 素食补丁
        /// </summary>
        /// <param name="__result">原方法的结果；Harmony框架用法，必须要叫这名</param>
        /// <param name="producedConversionRate">产物转化率</param>
        /// <param name="diseaseId">病菌ID</param>
        /// <param name="diseasePerKgProduced">每公斤病产生病菌数</param>
        public static void Postfix(
            ref List<Diet.Info> __result,
            ref float producedConversionRate,
            ref string diseaseId, ref float diseasePerKgProduced)
        {
            Log.Info("准备开始执行【VeggieDiet】补丁逻辑。");
            DoPatch(ref __result,
                producedConversionRate,
                ref diseaseId, ref diseasePerKgProduced);
        }
    }

    [HarmonyPatch(typeof(BaseHatchConfig), "FoodDiet")]
    public class FoodDietPatcher : AbstractHatchDietInfoPatcher
    {
        private static readonly common.Logger Log = new common.Logger(GlobalConstants.ModName);

        /// <summary>
        /// 制造食物补丁
        /// </summary>
        /// <param name="__result">原方法的结果；Harmony框架用法，必须要叫这名</param>
        /// <param name="producedConversionRate">产物转化率</param>
        /// <param name="diseaseId">病菌ID</param>
        /// <param name="diseasePerKgProduced">每公斤病产生病菌数</param>
        public static void Postfix(
            ref List<Diet.Info> __result,
            ref float producedConversionRate,
            ref string diseaseId, ref float diseasePerKgProduced)
        {
            Log.Info("准备开始执行【FoodDiet】补丁逻辑。");
            DoPatch(ref __result,
                producedConversionRate,
                ref diseaseId, ref diseasePerKgProduced);
        }
    }
}