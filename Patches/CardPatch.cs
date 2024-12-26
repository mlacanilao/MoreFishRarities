namespace MoreFishRarities.Patches
{
    public static class CardPatch
    {
        public static bool IsEquipmentOrRangedPrefix(Card __instance, ref bool __result)
        {
            if (__instance?.category?.id == "fish")
            {
                __result = true;
                return false;
            }

            return true;
        }
    }
}