using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000571 RID: 1393
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PrismaticGem : RelicModel
	{
		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06004E97 RID: 20119 RVA: 0x0021978F File Offset: 0x0021798F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06004E98 RID: 20120 RVA: 0x00219792 File Offset: 0x00217992
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(1));
			}
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06004E99 RID: 20121 RVA: 0x0021979F File Offset: 0x0021799F
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x06004E9A RID: 20122 RVA: 0x002197AC File Offset: 0x002179AC
		public override decimal ModifyMaxEnergy(Player player, decimal amount)
		{
			if (player != base.Owner)
			{
				return amount;
			}
			return amount + base.DynamicVars.Energy.IntValue;
		}

		// Token: 0x06004E9B RID: 20123 RVA: 0x002197D4 File Offset: 0x002179D4
		public override CardCreationOptions ModifyCardRewardCreationOptions(Player player, CardCreationOptions options)
		{
			if (base.Owner != player)
			{
				return options;
			}
			if (options.Flags.HasFlag(CardCreationFlags.NoCardPoolModifications))
			{
				return options;
			}
			if (options.CustomCardPool != null)
			{
				return options;
			}
			if (options.CardPools.All((CardPoolModel p) => p.IsColorless))
			{
				return options;
			}
			IEnumerable<CardPoolModel> enumerable = player.UnlockState.CharacterCardPools.Union(options.CardPools);
			return options.WithCardPools(enumerable, options.CardPoolFilter);
		}
	}
}
