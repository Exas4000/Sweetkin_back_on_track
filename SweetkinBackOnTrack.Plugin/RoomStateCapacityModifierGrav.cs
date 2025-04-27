using System;
using System.Runtime.CompilerServices;

// Token: 0x020004A8 RID: 1192
public sealed class RoomStateCapacityModifierGrav : RoomStateModifierBase, IRoomStateCapacityModifier
{
	// Token: 0x0600284F RID: 10319 RVA: 0x0009829B File Offset: 0x0009649B
	[NullableContext(1)]
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