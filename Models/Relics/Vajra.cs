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
	// Token: 0x020005B7 RID: 1463
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Vajra : RelicModel
	{
		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x06005058 RID: 20568 RVA: 0x0021CD46 File Offset: 0x0021AF46
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x06005059 RID: 20569 RVA: 0x0021CD49 File Offset: 0x0021AF49
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<StrengthPower>(1m));
			}
		}

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x0600505A RID: 20570 RVA: 0x0021CD5A File Offset: 0x0021AF5A
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x0600505B RID: 20571 RVA: 0x0021CD68 File Offset: 0x0021AF68
		public override async Task AfterRoomEntered(AbstractRoom room)
		{
			if (room is CombatRoom)
			{
				base.Flash();
				await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, base.DynamicVars.Strength.BaseValue, base.Owner.Creature, null, false);
			}
		}
	}
}
