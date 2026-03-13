using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000508 RID: 1288
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GamePiece : RelicModel
	{
		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06004BD7 RID: 19415 RVA: 0x002148FB File Offset: 0x00212AFB
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06004BD8 RID: 19416 RVA: 0x002148FE File Offset: 0x00212AFE
		public override string FlashSfx
		{
			get
			{
				return "event:/sfx/ui/relic_activate_draw";
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06004BD9 RID: 19417 RVA: 0x00214905 File Offset: 0x00212B05
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(1));
			}
		}

		// Token: 0x06004BDA RID: 19418 RVA: 0x00214914 File Offset: 0x00212B14
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner)
			{
				if (CombatManager.Instance.IsInProgress)
				{
					if (cardPlay.Card.Type == CardType.Power)
					{
						base.Flash();
						await CardPileCmd.Draw(context, base.DynamicVars.Cards.BaseValue, base.Owner, false);
					}
				}
			}
		}
	}
}
