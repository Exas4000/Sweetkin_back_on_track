using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Collections;
using TrainworksReloaded.Core;
using TrainworksReloaded.Core.Extensions;

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
                        // Clan definition, subtypes, and banners
                        "json/plugin.json",
                        // Champions
                        "json/champions/champion_sally.json",
                        "json/champions/champion_evergreen.json",
                        // Spells
                        "json/cards/Sw_allYouCan.json",
                        "json/cards/Sw_care.json",
                        "json/cards/Sw_dineDash.json",
                        "json/cards/Sw_Expulsion.json",
                        "json/cards/Sw_Fullcourse.json",
                        "json/cards/Sw_GrumpyPatron.json",
                        "json/cards/Sw_Hospitality.json",
                        "json/cards/Sw_Leftovers.json",
                        "json/cards/Sw_portalMaster.json",
                        "json/cards/Sw_punish.json",
                        "json/cards/Sw_seconds.json",
                        "json/cards/Sw_Snakification.json",
                        "json/cards/Sw_Spectacle.json",
                        "json/cards/Sw_SummerWildfire.json",
                        "json/cards/Sw_tillLastDrop.json",
                        "json/cards/Sw_Veil.json",
                        "json/cards/Sw_WinterSnowstorms.json",
                        "json/cards/Sw_Workshift.json",
                        // Monsters
                        "json/characters/Sw_Butler.json",
                        "json/characters/Sw_crab.json",
                        "json/characters/Sw_Delivery.json",
                        "json/characters/Sw_EvergreenSprout.json",
                        "json/characters/Sw_FastFood.json",
                        "json/characters/Sw_FrontClerk.json",
                        "json/characters/Sw_Lime.json",
                        "json/characters/Sw_Mint.json",
                        "json/characters/Sw_noodles.json",
                        "json/characters/Sw_Pyretatoe.json",
                        "json/characters/Sw_Sherbetrus.json",
                        "json/characters/Sw_Sours.json",
                        "json/characters/Sw_SpiceCook.json",
                        "json/characters/Sw_Sweetling.json",
                        "json/characters/Sw_Teamaid.json",
                        "json/characters/Sw_VIPcookie.json",
                        "json/characters/Sw_VIPcritic.json",
                        "json/characters/Sw_VIPimp.json",
                        "json/characters/Sw_VIPlatebloomer.json",
                        "json/characters/Sw_VIPnugget.json",
                        "json/characters/Sw_VIProsette.json",
                        "json/characters/Sw_VIPsocialite.json",
                        "json/characters/Sw_VIPTemaki.json",
                        "json/characters/Sw_VIPvirologist.json",
                        // Equipment
                        "json/equipments/Sw_BookCook.json",
                        "json/equipments/Sw_BookMint.json",
                        "json/equipments/Sw_ButlerSuit.json",
                        "json/equipments/Sw_CombatKnife.json",
                        "json/equipments/Sw_CrabBib.json",
                        "json/equipments/Sw_Dovelivery.json",
                        "json/equipments/Sw_LilSpoon.json",
                        "json/equipments/Sw_LimeBook.json",
                        "json/equipments/Sw_MaidSuit.json",
                        "json/equipments/Sw_Sparelimb.json",
                        "json/equipments/Sw_TuningFork.json",
                        // Rooms
                        "json/rooms/Sw_RoomGrav.json",
                        "json/rooms/Sw_RoomKitchen.json",
                        "json/rooms/Sw_RoomLobby.json",
                        "json/rooms/Sw_RoomRestaurant.json",
                        "json/rooms/Sw_RoomSpa.json",
                        // Artifacts
                        "json/relics/Sw_Dispenser.json",
                        "json/relics/Sw_FirstClass.json",
                        "json/relics/Sw_Flavour.json",
                        "json/relics/Sw_GoldenKernel.json",
                        "json/relics/Sw_MintDough.json",
                        "json/relics/Sw_Mucus.json",
                        "json/relics/Sw_Pamphlet.json",
                        "json/relics/Sw_Paste.json",
                        "json/relics/Sw_Photo.json",
                        "json/relics/Sw_SilverPlater.json",
                        "json/relics/Sw_Tailor.json"
                    );
                }
            );

            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }
    }

    public sealed class RoomStateCapacityModifierGrav : RoomStateModifierBase, IRoomStateCapacityModifier
    {
        public override void Initialize(RoomModifierData roomModifierData, ICoreGameManagers coreGameManagers)
        {
            base.Initialize(roomModifierData, coreGameManagers);
            this.capacityDelta = roomModifierData.GetParamInt();
        }
        public int GetModifiedCapacity()
        {
            return this.capacityDelta;
        }
        private int capacityDelta;
    }

    public sealed class RoomStateAddEffectPostCombatLobby : RoomStateModifierBase, IRoomStatePostCombatModifier, IRoomStateModifier, ILocalizationParamInt, ILocalizationParameterContext
    {
        public override void Initialize(RoomModifierData roomModifierData, ICoreGameManagers coreGameManagers)
        {
            base.Initialize(roomModifierData, coreGameManagers);
            foreach (CardEffectData cardEffectData in roomModifierData.GetParamCardEffectData())
            {
                CardEffectState cardEffectState = Activator.CreateInstance<CardEffectState>();
                cardEffectState.Setup(cardEffectData, null, coreGameManagers.GetSaveManager());
                _effects.Add(cardEffectState);
            }
        }

        public bool CanApplyInPreviewMode
        {
            get
            {
                return false;
            }
        }

        public IEnumerator PostCombat(RoomState room, ICoreGameManagers coreGameManagers)
        {
            CombatManager combatManager = coreGameManagers.GetCombatManager();
            yield return base.ShowTriggeredVFX(room, coreGameManagers);
            yield return combatManager.ApplyEffects(_effects, room.GetRoomIndex());
        }

        private List<CardEffectState> _effects = [];
    }

    public sealed class RoomStateAddEffectPostCombatKitchen : RoomStateModifierBase, IRoomStatePostCombatModifier, IRoomStateModifier, ILocalizationParamInt, ILocalizationParameterContext
    {
        public override void Initialize(RoomModifierData roomModifierData, ICoreGameManagers coreGameManagers)
        {
            base.Initialize(roomModifierData, coreGameManagers);
            foreach (CardEffectData cardEffectData in roomModifierData.GetParamCardEffectData())
            {
                CardEffectState cardEffectState = Activator.CreateInstance<CardEffectState>();
                cardEffectState.Setup(cardEffectData, null, coreGameManagers.GetSaveManager());
                _effects.Add(cardEffectState);
            }
        }

        public bool CanApplyInPreviewMode
        {
            get
            {
                return false;
            }
        }

        public IEnumerator PostCombat(RoomState room, ICoreGameManagers coreGameManagers)
        {
            CombatManager combatManager = coreGameManagers.GetCombatManager();
            yield return base.ShowTriggeredVFX(room, coreGameManagers);
            yield return combatManager.ApplyEffects(_effects, room.GetRoomIndex());
        }

        private List<CardEffectState> _effects = [];
    }

    public sealed class RoomStateAddEffectPostCombatSpa : RoomStateModifierBase, IRoomStatePostCombatModifier, IRoomStateModifier, ILocalizationParamInt, ILocalizationParameterContext
    {
        public override void Initialize(RoomModifierData roomModifierData, ICoreGameManagers coreGameManagers)
        {
            base.Initialize(roomModifierData, coreGameManagers);
            foreach (CardEffectData cardEffectData in roomModifierData.GetParamCardEffectData())
            {
                CardEffectState cardEffectState = Activator.CreateInstance<CardEffectState>();
                cardEffectState.Setup(cardEffectData, null, coreGameManagers.GetSaveManager());
                _effects.Add(cardEffectState);
            }
        }

        public bool CanApplyInPreviewMode
        {
            get
            {
                return false;
            }
        }

        public IEnumerator PostCombat(RoomState room, ICoreGameManagers coreGameManagers)
        {
            CombatManager combatManager = coreGameManagers.GetCombatManager();
            yield return base.ShowTriggeredVFX(room, coreGameManagers);
            yield return combatManager.ApplyEffects(_effects, room.GetRoomIndex());
        }

        private List<CardEffectState> _effects = [];
    }
}
