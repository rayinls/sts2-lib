using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200051A RID: 1306
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class IntimidatingHelmet : RelicModel
	{
		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x06004C3B RID: 19515 RVA: 0x00215387 File Offset: 0x00213587
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x06004C3C RID: 19516 RVA: 0x0021538A File Offset: 0x0021358A
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(4m, ValueProp.Unpowered),
					new EnergyVar(2)
				});
			}
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x06004C3D RID: 19517 RVA: 0x002153AF File Offset: 0x002135AF
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06004C3E RID: 19518 RVA: 0x002153C4 File Offset: 0x002135C4
		public override async Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner)
			{
				if (cardPlay.Resources.EnergyValue >= base.DynamicVars.Energy.IntValue)
				{
					base.Flash();
					await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, null, false);
				}
			}
		}
	}
}
