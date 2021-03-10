// @author Zereao
// @date 2021-03-06 21:57
// @Steam https://steamcommunity/id/hexaiolun/

using System.Collections.Generic;
using Harmony;

namespace SuperHatch.patcher
{
    [HarmonyPatch(typeof(BaseHatchConfig))]
    public class HatchDietPatcher : AbstractHatchDietPatcher
    {
        [HarmonyPrefix]
        [HarmonyPatch("BasicRockDiet")]
        [HarmonyPatch("HardRockDiet")]
        [HarmonyPatch("MetalDiet")]
        [HarmonyPatch("VeggieDiet")]
        [HarmonyPatch("FoodDiet")]
        public static void PreHandler(ref float caloriesPerKg, ref float producedConversionRate)
        {
            DoPrefixPatch(ref caloriesPerKg, ref producedConversionRate);
        }

        [HarmonyPostfix]
        [HarmonyPatch("BasicRockDiet")]
        [HarmonyPatch("HardRockDiet")]
        [HarmonyPatch("MetalDiet")]
        [HarmonyPatch("VeggieDiet")]
        [HarmonyPatch("FoodDiet")]
        public static void PostHandler(ref List<Diet.Info> result,
            float producedConversionRate,
            ref string diseaseId, ref float diseasePerKgProduced)
        {
            DoPostfixPatch(ref result,
                producedConversionRate,
                ref diseaseId, ref diseasePerKgProduced);
        }
    }
}