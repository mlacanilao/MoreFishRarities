using UnityEngine;

namespace MoreFishRarities
{
    internal static class ButtonGridPatch
    {
        internal static void SetCardPostfix(ButtonGrid __instance, Card c)
        {
            CoreRef.ButtonAssets refs = EClass.core.refs.buttonAssets;
            
            if (c == null)
            {
                __instance.image.sprite = refs.bgDefault;
            }
            
            if (c?.Thing?.source?._origin != "fish")
            {
                return;
            }
            
            Sprite sprite = refs.bgDefault;

            if (c.rarity >= Rarity.Superior && c.IsIdentified)
            {
                sprite = (c.rarity == Rarity.Artifact) ? refs.bgArtifact :
                    (c.rarity == Rarity.Mythical) ? refs.bgMythical :
                    (c.rarity == Rarity.Legendary) ? refs.bgLegendary :
                    (c.rarity == Rarity.Superior) ? refs.bgSuperior :
                    refs.bgDefault;
            }

            __instance.image.sprite = sprite;
        }
    }
}