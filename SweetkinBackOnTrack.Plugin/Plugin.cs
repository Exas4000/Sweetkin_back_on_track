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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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
                    c.AddJsonFile("plugin.json");
                }
            );

            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }
    }

    public sealed class RoomStateCapacityModifierGrav : RoomStateModifierBase, IRoomStateCapacityModifier
    {
        // Token: 0x0600284F RID: 10319 RVA: 0x0009829B File Offset: 0x0009649B
        public override void Initialize(RoomModifierData roomModifierData, ICoreGameManagers coreGameManagers)
        {
            base.Initialize(roomModifierData, coreGameManagers);
            this.capacityDelta = roomModifierData.GetParamInt();
        }

        // Token: 0x06002850 RID: 10320 RVA: 0x000982B1 File Offset: 0x000964B1
        public int GetModifiedCapacity()
        {
            return this.capacityDelta;
        }

        // Token: 0x040011F3 RID: 4595
        private int capacityDelta;
    }

    public sealed class RoomStateAddEffectPostCombatLobby : RoomStateModifierBase, IRoomStatePostCombatModifier, IRoomStateModifier, ILocalizationParamInt, ILocalizationParameterContext
    {
        // Token: 0x0600283F RID: 10303 RVA: 0x00097FB8 File Offset: 0x000961B8
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

        // Token: 0x170002DA RID: 730
        // (get) Token: 0x06002840 RID: 10304 RVA: 0x00098028 File Offset: 0x00096228
        public bool CanApplyInPreviewMode
        {
            get
            {
                return false;
            }
        }

        // Token: 0x06002841 RID: 10305 RVA: 0x0009802B File Offset: 0x0009622B
        public IEnumerator PostCombat(RoomState room, ICoreGameManagers coreGameManagers)
        {
            CombatManager combatManager = coreGameManagers.GetCombatManager();
            yield return base.ShowTriggeredVFX(room, coreGameManagers);
            yield return combatManager.ApplyEffects(_effects, room.GetRoomIndex(), null, false, null, null, null, true, null, null, false, null, null, 1, null, false, CardTriggerType.OnDiscard, null, false);
            yield break;
        }

        // Token: 0x040011EB RID: 4587
        private List<CardEffectState> _effects = new List<CardEffectState>();
    }

    public sealed class RoomStateAddEffectPostCombatKitchen : RoomStateModifierBase, IRoomStatePostCombatModifier, IRoomStateModifier, ILocalizationParamInt, ILocalizationParameterContext
    {
        // Token: 0x0600283F RID: 10303 RVA: 0x00097FB8 File Offset: 0x000961B8
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

        // Token: 0x170002DA RID: 730
        // (get) Token: 0x06002840 RID: 10304 RVA: 0x00098028 File Offset: 0x00096228
        public bool CanApplyInPreviewMode
        {
            get
            {
                return false;
            }
        }

        // Token: 0x06002841 RID: 10305 RVA: 0x0009802B File Offset: 0x0009622B
        public IEnumerator PostCombat(RoomState room, ICoreGameManagers coreGameManagers)
        {
            CombatManager combatManager = coreGameManagers.GetCombatManager();
            yield return base.ShowTriggeredVFX(room, coreGameManagers);
            yield return combatManager.ApplyEffects(_effects, room.GetRoomIndex(), null, false, null, null, null, true, null, null, false, null, null, 1, null, false, CardTriggerType.OnDiscard, null, false);
            yield break;
        }

        // Token: 0x040011EB RID: 4587
        private List<CardEffectState> _effects = new List<CardEffectState>();
    }

    public sealed class RoomStateAddEffectPostCombatSpa : RoomStateModifierBase, IRoomStatePostCombatModifier, IRoomStateModifier, ILocalizationParamInt, ILocalizationParameterContext
    {
        // Token: 0x0600283F RID: 10303 RVA: 0x00097FB8 File Offset: 0x000961B8
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

        // Token: 0x170002DA RID: 730
        // (get) Token: 0x06002840 RID: 10304 RVA: 0x00098028 File Offset: 0x00096228
        public bool CanApplyInPreviewMode
        {
            get
            {
                return false;
            }
        }

        // Token: 0x06002841 RID: 10305 RVA: 0x0009802B File Offset: 0x0009622B
        public IEnumerator PostCombat(RoomState room, ICoreGameManagers coreGameManagers)
        {
            CombatManager combatManager = coreGameManagers.GetCombatManager();
            yield return base.ShowTriggeredVFX(room, coreGameManagers);
            yield return combatManager.ApplyEffects(_effects, room.GetRoomIndex(), null, false, null, null, null, true, null, null, false, null, null, 1, null, false, CardTriggerType.OnDiscard, null, false);
            yield break;
        }

        // Token: 0x040011EB RID: 4587
        private List<CardEffectState> _effects = new List<CardEffectState>();
    }
}
