namespace MoreFishRarities
{
    internal static class AI_FishPatch
    {
        public static void MakefishPostfix(Thing __result, Chara c)
        {
            if (c?.IsPC == false)
            {
                return;
            }

            if (__result?.source?._origin != "fish")
            {
                return;
            }
            
            if (Dice.Roll(num: 1, sides: 1000, bonus: 0, card: c) == 1000)
            {
                __result.rarity = Rarity.Artifact; // 1/1000
            }
            else if (Dice.Roll(num: 1, sides: 250, bonus: 0, card: c) == 250)
            {
                __result.rarity = Rarity.Mythical; // 1/250
            }
            else if (Dice.Roll(num: 1, sides: 80, bonus: 0, card: c) == 80)
            {
                __result.rarity = Rarity.Legendary; // 1/80
            }
            else if (Dice.Roll(num: 1, sides: 10, bonus: 0, card: c) == 10)
            {
                __result.rarity = Rarity.Superior; // 1/10
            }
        }
    }
}