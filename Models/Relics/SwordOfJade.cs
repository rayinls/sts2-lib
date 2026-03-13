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
	// Token: 0x0200059F RID: 1439
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SwordOfJade : RelicModel
	{
		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x06004FB4 RID: 20404 RVA: 0x0021B9E7 File Offset: 0x00219BE7
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x06004FB5 RID: 20405 RVA: 0x0021B9EA File Offset: 0x00219BEA
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<StrengthPower>(3m));
			}
		}

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06004FB6 RID: 20406 RVA: 0x0021B9FC File Offset: 0x00219BFC
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x06004FB7 RID: 20407 RVA: 0x0021BA08 File Offset: 0x00219C08
		public override async Task AfterRoomEntered(AbstractRoom room)
		{
			if (room is CombatRoom)
			{
				await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, base.DynamicVars.Strength.BaseValue, null, null, false);
			}
		}
	}
}
