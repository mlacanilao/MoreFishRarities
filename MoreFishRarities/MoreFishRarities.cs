using BepInEx;
using HarmonyLib;

namespace MoreFishRarities
{
    internal static class ModInfo
    {
        internal const string Guid = "omegaplatinum.elin.morefishrarities";
        internal const string Name = "More Fish Rarities";
        internal const string Version = "1.0.0.1";
    }

    [BepInPlugin(GUID: ModInfo.Guid, Name: ModInfo.Name, Version: ModInfo.Version)]
    internal class MoreFishRarities : BaseUnityPlugin
    {
        internal static MoreFishRarities Instance { get; private set; }
        
        private void Start()
        {
            Instance = this;
            
            Harmony.CreateAndPatchAll(type: typeof(Patcher), harmonyInstanceId: ModInfo.Guid);
        }
        
        internal static void Log(object payload)
        {
            Instance.Logger.LogInfo(data: payload);
        }
    }
}