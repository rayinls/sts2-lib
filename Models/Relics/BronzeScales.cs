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
	// Token: 0x020004C9 RID: 1225
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BronzeScales : RelicModel
	{
		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06004A62 RID: 19042 RVA: 0x00211E73 File Offset: 0x00210073
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06004A63 RID: 19043 RVA: 0x00211E76 File Offset: 0x00210076
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<ThornsPower>(3m));
			}
		}

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06004A64 RID: 19044 RVA: 0x00211E88 File Offset: 0x00210088
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<ThornsPower>());
			}
		}

		// Token: 0x06004A65 RID: 19045 RVA: 0x00211E94 File Offset: 0x00210094
		public override async Task AfterRoomEntered(AbstractRoom room)
		{
			if (room is CombatRoom)
			{
				base.Flash();
				await PowerCmd.Apply<ThornsPower>(base.Owner.Creature, base.DynamicVars["ThornsPower"].BaseValue, base.Owner.Creature, null, false);
			}
		}
	}
}
