using BepInEx;
using BepInEx.Logging;
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
                    "cards/Sw_allYouCan.json",
                    "cards/Sw_care.json",
                    "cards/Sw_dineDash.json",
                    "cards/Sw_Expulsion.json",
                    "cards/Sw_Fullcourse.json",
                    "cards/Sw_GrumpyPatron.json",
                    "cards/Sw_Hospitality.json",
                    "cards/Sw_Leftovers.json",
                    "cards/Sw_portalMaster.json",
                    "cards/Sw_punish.json",
                    "cards/Sw_seconds.json",
                    "cards/Sw_Snakification.json",
                    "cards/Sw_Spectacle.json",
                    "cards/Sw_SummerWildfire.json",
                    "cards/Sw_tillLastDrop.json",
                    "cards/Sw_Veil.json",
                    "cards/Sw_WinterSnowstorms.json",
                    "cards/Sw_Workshift.json",
                    "champions/champion_sally.json",
                    "champions/champion_evergreen.json",
                    "characters/Sw_Butler.json",
                    "characters/Sw_crab.json",
                    "characters/Sw_Delivery.json",
                    "characters/Sw_EvergreenSprout.json",
                    "characters/Sw_FastFood.json",
                    "characters/Sw_FrontClerk.json",
                    "characters/Sw_Lime.json",
                    "characters/Sw_Mint.json",
                    "characters/Sw_noodles.json",
                    "characters/Sw_Pyretatoe.json",
                    "characters/Sw_Sherbetrus.json",
                    "characters/Sw_Sours.json",
                    "characters/Sw_SpiceCook.json",
                    "characters/Sw_Sweetling.json",
                    "characters/Sw_Teamaid.json",
                    "characters/Sw_VIPcookie.json",
                    "characters/Sw_VIPcritic.json",
                    "characters/Sw_VIPimp.json",
                    "characters/Sw_VIPlatebloomer.json",
                    "characters/Sw_VIPnugget.json",
                    "characters/Sw_VIProsette.json",
                    "characters/Sw_VIPsocialite.json",
                    "characters/Sw_VIPTemaki.json",
                    "characters/Sw_VIPvirologist.json",
                    "equipments/Sw_BookCook.json",
                    "equipments/Sw_BookMint.json",
                    "equipments/Sw_ButlerSuit.json",
                    "equipments/Sw_CombatKnife.json",
                    "equipments/Sw_CrabBib.json",
                    "equipments/Sw_Dovelivery.json",
                    "equipments/Sw_LilSpoon.json",
                    "equipments/Sw_LimeBook.json",
                    "equipments/Sw_MaidSuit.json",
                    "equipments/Sw_Sparelimb.json",
                    "equipments/Sw_TuningFork.json",
                    "plugin.json",
                    "relics/Sw_Dispenser.json",
                    "relics/Sw_FirstClass.json",
                    "relics/Sw_Flavour.json",
                    "relics/Sw_GoldenKernel.json",
                    "relics/Sw_MintDough.json",
                    "relics/Sw_Mucus.json",
                    "relics/Sw_Pamphlet.json",
                    "relics/Sw_Paste.json",
                    "relics/Sw_Photo.json",
                    "relics/Sw_SilverPlater.json",
                    "relics/Sw_Tailor.json",
                    "rooms/Sw_RoomGrav.json",
                    "rooms/Sw_RoomKitchen.json",
                    "rooms/Sw_RoomLobby.json",
                    "rooms/Sw_RoomRestaurant.json",
                    "rooms/Sw_RoomSpa.json"
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
                cardEffectState.Setup(cardEffectData, null, null);
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
            yield return combatManager.ApplyEffects(_effects, room.GetRoomIndex(), null, false, null, null, null, true, null, null, false, null, null, 1, null, false, CardTriggerType.OnDiscard, null, false);
            yield break;
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
                cardEffectState.Setup(cardEffectData, null, null);
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
            yield return combatManager.ApplyEffects(_effects, room.GetRoomIndex(), null, false, null, null, null, true, null, null, false, null, null, 1, null, false, CardTriggerType.OnDiscard, null, false);
            yield break;
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
                cardEffectState.Setup(cardEffectData, null, null);
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
            yield return combatManager.ApplyEffects(_effects, room.GetRoomIndex(), null, false, null, null, null, true, null, null, false, null, null, 1, null, false, CardTriggerType.OnDiscard, null, false);
            yield break;
        }

        private List<CardEffectState> _effects = [];
    }
}
