using MoreFishRarities.Patches;
using HarmonyLib;

namespace MoreFishRarities
{
    public class Patcher
    {
        [HarmonyPostfix]
        [HarmonyPatch(declaringType: typeof(AI_Fish), methodName: nameof(AI_Fish.Makefish))]
        public static void AI_FishMakefish(Thing __result, Chara c)
        {
            AI_FishPatch.MakefishPostfix(__result: __result, c: c);
        }
        
        [HarmonyPrefix]
        [HarmonyPatch(declaringType: typeof(Card), methodName: nameof(Card.IsEquipmentOrRanged), methodType: MethodType.Getter)]
        public static bool CardIsEquipmentOrRanged(Card __instance, ref bool __result)
        {
            return CardPatch.IsEquipmentOrRangedPrefix(__instance: __instance, __result: ref __result);
        }
    }
}