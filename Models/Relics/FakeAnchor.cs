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
	// Token: 0x020004F3 RID: 1267
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FakeAnchor : RelicModel
	{
		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x06004B43 RID: 19267 RVA: 0x002137FF File Offset: 0x002119FF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06004B44 RID: 19268 RVA: 0x00213802 File Offset: 0x00211A02
		public override int MerchantCost
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06004B45 RID: 19269 RVA: 0x00213806 File Offset: 0x00211A06
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(4m, ValueProp.Unpowered));
			}
		}

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06004B46 RID: 19270 RVA: 0x00213819 File Offset: 0x00211A19
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06004B47 RID: 19271 RVA: 0x0021382C File Offset: 0x00211A2C
		public override async Task BeforeCombatStart()
		{
			base.Flash();
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, null, false);
		}
	}
}
