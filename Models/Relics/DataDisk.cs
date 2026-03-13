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
	// Token: 0x020004DE RID: 1246
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DataDisk : RelicModel
	{
		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x06004AD7 RID: 19159 RVA: 0x00212B2F File Offset: 0x00210D2F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06004AD8 RID: 19160 RVA: 0x00212B32 File Offset: 0x00210D32
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<FocusPower>(1m));
			}
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06004AD9 RID: 19161 RVA: 0x00212B43 File Offset: 0x00210D43
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<FocusPower>());
			}
		}

		// Token: 0x06004ADA RID: 19162 RVA: 0x00212B50 File Offset: 0x00210D50
		public override async Task AfterRoomEntered(AbstractRoom room)
		{
			if (room is CombatRoom)
			{
				base.Flash();
				await PowerCmd.Apply<FocusPower>(base.Owner.Creature, base.DynamicVars["FocusPower"].BaseValue, base.Owner.Creature, null, false);
			}
		}
	}
}
