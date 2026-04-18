using HarmonyLib;
using System.Collections.Generic;

namespace MoreFishRarities;

internal static class Patcher
{
    [HarmonyPostfix]
    [HarmonyPatch(declaringType: typeof(AI_Fish), methodName: nameof(AI_Fish.Makefish))]
    internal static void AI_FishMakefish(Thing __result, Chara c)
    {
        AI_FishPatch.MakefishPostfix(__result: __result, c: c);
    }

    [HarmonyPostfix]
    [HarmonyPatch(declaringType: typeof(ButtonGrid), methodName: nameof(ButtonGrid.SetCard))]
    internal static void ButtonGridSetCard(ButtonGrid __instance, Card c, ButtonGrid.Mode mode)
    {
        ButtonGridPatch.SetCardPostfix(__instance: __instance, card: c, mode: mode);
    }

    [HarmonyPostfix]
    [HarmonyPatch(declaringType: typeof(ButtonGrid), methodName: nameof(ButtonGrid.Reset))]
    internal static void ButtonGridReset(ButtonGrid __instance)
    {
        ButtonGridPatch.ResetPostfix(__instance: __instance);
    }

    [HarmonyTranspiler]
    [HarmonyPatch(declaringType: typeof(Thing), methodName: nameof(Thing.GetName))]
    internal static IEnumerable<CodeInstruction> ThingGetName(IEnumerable<CodeInstruction> instructions)
    {
        return ThingPatch.GetNameTranspiler(instructions: instructions);
    }
}
