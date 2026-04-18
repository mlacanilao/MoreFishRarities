using System;
using UnityEngine;
using UnityEngine.UI;

namespace MoreFishRarities;

internal static class ButtonGridPatch
{
    private const string OverlayName = "MoreFishRarities_RarityFrame";

    internal static void SetCardPostfix(ButtonGrid __instance, Card card, ButtonGrid.Mode mode)
    {
        if (ShouldShowRarityOverlay(card: card, mode: mode) == false)
        {
            HideOverlay(grid: __instance);
            return;
        }

        Sprite? sprite = GetRaritySprite(card: card);
        if (sprite == null)
        {
            HideOverlay(grid: __instance);
            return;
        }

        Image overlay = GetOrCreateOverlay(grid: __instance);
        overlay.sprite = sprite;
        overlay.gameObject.SetActive(value: true);
    }

    internal static void ResetPostfix(ButtonGrid __instance)
    {
        HideOverlay(grid: __instance);
    }

    private static bool ShouldShowRarityOverlay(Card card, ButtonGrid.Mode mode)
    {
        if (mode != ButtonGrid.Mode.Grid &&
            mode != ButtonGrid.Mode.Search)
        {
            return false;
        }

        if (card == null || card.isDestroyed)
        {
            return false;
        }

        if (card.Thing?.source?._origin != "fish")
        {
            return false;
        }

        if (card.IsIdentified == false)
        {
            return false;
        }

        return card.rarity >= Rarity.Superior;
    }

    private static Sprite? GetRaritySprite(Card card)
    {
        CoreRef.ButtonAssets refs = EClass.core.refs.buttonAssets;

        return card.rarity switch
        {
            Rarity.Artifact => refs.bgArtifact,
            Rarity.Mythical => refs.bgMythical,
            Rarity.Legendary => refs.bgLegendary,
            Rarity.Superior => refs.bgSuperior,
            _ => null
        };
    }

    private static Image GetOrCreateOverlay(ButtonGrid grid)
    {
        Transform existingTransform = grid.transform.Find(n: OverlayName);
        if (existingTransform != null)
        {
            return existingTransform.GetComponent<Image>();
        }

        GameObject overlayObject = new(name: OverlayName);
        RectTransform overlayTransform = overlayObject.AddComponent<RectTransform>();
        Image overlay = overlayObject.AddComponent<Image>();

        overlay.raycastTarget = false;
        overlayTransform.SetParent(parent: grid.transform, worldPositionStays: false);
        overlayTransform.anchorMin = Vector2.zero;
        overlayTransform.anchorMax = Vector2.one;
        overlayTransform.offsetMin = Vector2.zero;
        overlayTransform.offsetMax = Vector2.zero;

        if (grid.icon != null)
        {
            overlayTransform.SetSiblingIndex(index: Math.Max(0, grid.icon.transform.GetSiblingIndex()));
        }
        else
        {
            overlayTransform.SetAsFirstSibling();
        }

        return overlay;
    }

    private static void HideOverlay(ButtonGrid grid)
    {
        Transform overlayTransform = grid.transform.Find(n: OverlayName);
        if (overlayTransform != null)
        {
            overlayTransform.gameObject.SetActive(value: false);
        }
    }
}
