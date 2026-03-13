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
	// Token: 0x02000511 RID: 1297
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Gorget : RelicModel
	{
		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06004C09 RID: 19465 RVA: 0x00214E14 File Offset: 0x00213014
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06004C0A RID: 19466 RVA: 0x00214E17 File Offset: 0x00213017
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromPowerWithPowerHoverTips<PlatingPower>();
			}
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06004C0B RID: 19467 RVA: 0x00214E1E File Offset: 0x0021301E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<PlatingPower>(4m));
			}
		}

		// Token: 0x06004C0C RID: 19468 RVA: 0x00214E30 File Offset: 0x00213030
		public override async Task AfterRoomEntered(AbstractRoom room)
		{
			if (room is CombatRoom)
			{
				base.Flash();
				await PowerCmd.Apply<PlatingPower>(base.Owner.Creature, base.DynamicVars["PlatingPower"].BaseValue, base.Owner.Creature, null, false);
			}
		}
	}
}
