// @author Zereao
// @date 2021-03-06 21:57
// @Steam https://steamcommunity/id/hexaiolun/

using System.Collections.Generic;
using Harmony;
using HXLib.logging;
using SuperHatch.common;

namespace SuperHatch.patcher
{
    [HarmonyPatch(typeof(BaseHatchConfig))]
    [HarmonyPatch("BasicRockDiet")]
    [HarmonyPatch("HardRockDiet")]
    [HarmonyPatch("MetalDiet")]
    [HarmonyPatch("VeggieDiet")]
    [HarmonyPatch("FoodDiet")]
    public class HatchDietPatcher : AbstractHatchDietPatcher
    {
        private static readonly Log Log = Log.GetLogger(Const.ConfigName);

        /// <summary>初始化配置</summary>
        public static void OnLoad()
        {
            Log.Info("Mod开始加载，开始预加载配置文件。");
            var init = Config;
        }

        [HarmonyPrefix]
        public static void PreHandler(ref float caloriesPerKg, ref float producedConversionRate)
        {
            DoPrefixPatch(ref caloriesPerKg, ref producedConversionRate);
        }

        [HarmonyPostfix]
        public static void PostHandler(ref List<Diet.Info> __result,
            float producedConversionRate,
            ref string diseaseId, ref float diseasePerKgProduced)
        {
            DoPostfixPatch(ref __result,
                producedConversionRate,
                ref diseaseId, ref diseasePerKgProduced);
        }
    }
}