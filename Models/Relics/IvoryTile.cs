using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200051C RID: 1308
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class IvoryTile : RelicModel
	{
		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06004C4D RID: 19533 RVA: 0x0021558B File Offset: 0x0021378B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06004C4E RID: 19534 RVA: 0x0021558E File Offset: 0x0021378E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new EnergyVar(1),
					new EnergyVar("EnergyThreshold", 3)
				});
			}
		}

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06004C4F RID: 19535 RVA: 0x002155B2 File Offset: 0x002137B2
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x06004C50 RID: 19536 RVA: 0x002155C0 File Offset: 0x002137C0
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner)
			{
				if (cardPlay.Resources.EnergyValue >= base.DynamicVars["EnergyThreshold"].IntValue)
				{
					base.Flash();
					await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
				}
			}
		}

		// Token: 0x040021C6 RID: 8646
		private const string _energyThresholdKey = "EnergyThreshold";
	}
}
