using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004AA RID: 1194
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Anchor : RelicModel
	{
		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x0600498F RID: 18831 RVA: 0x002103BD File Offset: 0x0020E5BD
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06004990 RID: 18832 RVA: 0x002103C0 File Offset: 0x0020E5C0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(10m, ValueProp.Unpowered));
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06004991 RID: 18833 RVA: 0x002103D4 File Offset: 0x0020E5D4
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06004992 RID: 18834 RVA: 0x002103E8 File Offset: 0x0020E5E8
		public override async Task BeforeCombatStart()
		{
			base.Flash();
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, null, false);
		}
	}
}
