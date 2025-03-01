using BepInEx;
using ShadowLib;
using UnityEngine;

namespace lstwoMODS_WobblyLife_Extras
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static AssetBundle AssetBundle;
        
        private void Awake()
        {
            AssetBundle = AssetUtils.LoadFromEmbeddedResources("lstwoMODS_WobblyLife_Extras.Resources.lstwomods.wobblylife.extras.bundle");
            
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
