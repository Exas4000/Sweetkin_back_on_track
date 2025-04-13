using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using I2.Loc;
using Microsoft.Extensions.Configuration;
using ShinyShoe.Logging;
using SimpleInjector;
using TrainworksReloaded.Base;
using TrainworksReloaded.Base.Card;
using TrainworksReloaded.Base.CardUpgrade;
using TrainworksReloaded.Base.Character;
using TrainworksReloaded.Base.Class;
using TrainworksReloaded.Base.Effect;
using TrainworksReloaded.Base.Localization;
using TrainworksReloaded.Base.Prefab;
using TrainworksReloaded.Base.Trait;
using TrainworksReloaded.Base.Trigger;
using TrainworksReloaded.Core;
using TrainworksReloaded.Core.Extensions;
using TrainworksReloaded.Core.Impl;
using TrainworksReloaded.Core.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SweetkinBackOnTrack.Plugin
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger = new(MyPluginInfo.PLUGIN_GUID);
        public void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;

            var builder = Railhead.GetBuilder();
            builder.Configure(
                MyPluginInfo.PLUGIN_GUID,
                c =>
                {
                    c.AddMergedJsonFile(
                        "plugin.json",
                        "cards/Sw_Expulsion.json",
                        "cards/Sw_Fullcourse.json",
                        "cards/Sw_GrumpyPatron.json",
                        "cards/Sw_Hospitality.json",
                        "cards/Sw_Leftovers.json",
                        "cards/Sw_Snakification.json",
                        "cards/Sw_Spectacle.json",
                        "cards/Sw_Veil.json",
                        "cards/Sw_Workshift.json",
                        "cards/Sw_allYouCan.json",
                        "cards/Sw_care.json",
                        "cards/Sw_dineDash.json",
                        "cards/Sw_portalMaster.json",
                        "cards/Sw_punish.json",
                        "cards/Sw_seconds.json",
                        "cards/Sw_tillLastDrop.json",
                        "champions/champion_sally.json",
                        "characters/Sw_Butler.json",
                        "characters/Sw_Delivery.json",
                        "characters/Sw_FastFood.json",
                        "characters/Sw_Lime.json",
                        "characters/Sw_Mint.json",
                        "characters/Sw_Pyretatoe.json",
                        "characters/Sw_Sherbetrus.json",
                        "characters/Sw_Sours.json",
                        "characters/Sw_SpiceCook.json",
                        "characters/Sw_Sweetling.json",
                        "characters/Sw_Teamaid.json",
                        "characters/Sw_VIPTemaki.json",
                        "characters/Sw_VIPcookie.json",
                        "characters/Sw_VIPcritic.json",
                        "characters/Sw_VIPimp.json",
                        "characters/Sw_VIPlatebloomer.json",
                        "characters/Sw_VIPnugget.json",
                        "characters/Sw_VIProsette.json",
                        "characters/Sw_VIPsocialite.json",
                        "characters/Sw_VIPvirologist.json",
                        "characters/Sw_crab.json",
                        "characters/Sw_noodles.json",
                        "relics/Sw_Dispenser.json",
                        "relics/Sw_FlavourWIP.json",
                        "relics/Sw_GoldenKernel.json",
                        "relics/Sw_MintDoughWIP.json",
                        "relics/Sw_Mucus.json",
                        "relics/Sw_Pamphlet.json",
                        "relics/Sw_Paste.json",
                        "relics/Sw_Photo.json",
                        "relics/Sw_SilverPlater.json"
                    );
                }
            );

            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
