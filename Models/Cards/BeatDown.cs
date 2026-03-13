using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008A7 RID: 2215
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BeatDown : CardModel
	{
		// Token: 0x06006756 RID: 26454 RVA: 0x002552CF File Offset: 0x002534CF
		public BeatDown()
			: base(3, CardType.Skill, CardRarity.Rare, TargetType.RandomEnemy, true)
		{
		}

		// Token: 0x17001AD4 RID: 6868
		// (get) Token: 0x06006757 RID: 26455 RVA: 0x002552DC File Offset: 0x002534DC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x06006758 RID: 26456 RVA: 0x002552EC File Offset: 0x002534EC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			CardPile pile = PileType.Discard.GetPile(base.Owner);
			IEnumerable<CardModel> enumerable = pile.Cards.Where((CardModel c) => c.Type == CardType.Attack && !c.Keywords.Contains(CardKeyword.Unplayable)).ToList<CardModel>().StableShuffle(base.Owner.RunState.Rng.Shuffle)
				.Take(base.DynamicVars.Cards.IntValue);
			foreach (CardModel cardModel in enumerable)
			{
				if (CombatManager.Instance.IsOverOrEnding)
				{
					break;
				}
				if (cardModel.TargetType == TargetType.AnyEnemy)
				{
					Creature creature = base.Owner.RunState.Rng.CombatTargets.NextItem<Creature>(base.CombatState.HittableEnemies);
					await CardCmd.AutoPlay(choiceContext, cardModel, creature, AutoPlayType.Default, false, false);
				}
				else
				{
					await CardCmd.AutoPlay(choiceContext, cardModel, null, AutoPlayType.Default, false, false);
				}
			}
			IEnumerator<CardModel> enumerator = null;
		}

		// Token: 0x06006759 RID: 26457 RVA: 0x00255337 File Offset: 0x00253537
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
