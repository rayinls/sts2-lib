using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200054B RID: 1355
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class OddlySmoothStone : RelicModel
	{
		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06004D80 RID: 19840 RVA: 0x00217884 File Offset: 0x00215A84
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06004D81 RID: 19841 RVA: 0x00217887 File Offset: 0x00215A87
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<DexterityPower>(1m));
			}
		}

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06004D82 RID: 19842 RVA: 0x00217898 File Offset: 0x00215A98
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DexterityPower>());
			}
		}

		// Token: 0x06004D83 RID: 19843 RVA: 0x002178A4 File Offset: 0x00215AA4
		public override async Task AfterRoomEntered(AbstractRoom room)
		{
			if (room is CombatRoom)
			{
				base.Flash();
				await PowerCmd.Apply<DexterityPower>(base.Owner.Creature, base.DynamicVars.Dexterity.BaseValue, base.Owner.Creature, null, false);
			}
		}
	}
}
