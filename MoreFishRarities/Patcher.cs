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
        
        [HarmonyPostfix]
        [HarmonyPatch(declaringType: typeof(ButtonGrid), methodName: nameof(ButtonGrid.SetCard))]
        public static void ButtonGridSetCard(ButtonGrid __instance, Card c)
        {
            ButtonGridPatch.SetCardPostfix(__instance: __instance, c: c);
        }
    }
}