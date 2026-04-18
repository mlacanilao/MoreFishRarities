using System.Runtime.CompilerServices;
using BepInEx;
using HarmonyLib;

namespace MoreFishRarities;

internal static class ModInfo
{
    internal const string Guid = "omegaplatinum.elin.morefishrarities";
    internal const string Name = "More Fish Rarities";
    internal const string Version = "2.0.0";
}

[BepInPlugin(GUID: ModInfo.Guid, Name: ModInfo.Name, Version: ModInfo.Version)]
internal class MoreFishRarities : BaseUnityPlugin
{
    internal static MoreFishRarities? Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        Harmony.CreateAndPatchAll(type: typeof(Patcher), harmonyInstanceId: ModInfo.Guid);
    }

    internal static void LogDebug(object message, [CallerMemberName] string caller = "")
    {
        Instance?.Logger.LogDebug(data: $"[{caller}] {message}");
    }

    internal static void LogInfo(object message)
    {
        Instance?.Logger.LogInfo(data: message);
    }

    internal static void LogError(object message)
    {
        Instance?.Logger.LogError(data: message);
    }
}
