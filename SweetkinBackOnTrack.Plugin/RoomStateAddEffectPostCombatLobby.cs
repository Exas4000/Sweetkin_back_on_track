using System;
using System.Collections.Generic;
using System.Text;

namespace SweetkinBackOnTrack.Plugin.Class
{
    public sealed class RoomStateAddEffectPostCombatLobby : RoomStateModifierBase, IRoomStatePostCombatModifier, IRoomStateModifier, ILocalizationParamInt, ILocalizationParameterContext
    {
        // Token: 0x0600283F RID: 10303 RVA: 0x00097FB8 File Offset: 0x000961B8
        [NullableContext(1)]
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
        [NullableContext(1)]
        public IEnumerator PostCombat(RoomState room, ICoreGameManagers coreGameManagers)
        {
            CombatManager combatManager = coreGameManagers.GetCombatManager();
            yield return base.ShowTriggeredVFX(room, coreGameManagers);
            yield return combatManager.ApplyEffects(_effects, room.GetRoomIndex(), null, false, null, null, null, true, null, null, false, null, null, 1, null, false, CardTriggerType.OnDiscard, null, false);
            yield break;
        }

        // Token: 0x040011EB RID: 4587
        [Nullable(1)]
        private List<CardEffectState> _effects = new List<CardEffectState>();
    }
}
