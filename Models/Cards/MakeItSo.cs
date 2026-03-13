using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009BD RID: 2493
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MakeItSo : CardModel
	{
		// Token: 0x06006D1E RID: 27934 RVA: 0x002608E0 File Offset: 0x0025EAE0
		public MakeItSo()
			: base(0, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D49 RID: 7497
		// (get) Token: 0x06006D1F RID: 27935 RVA: 0x002608ED File Offset: 0x0025EAED
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(6m, ValueProp.Move),
					new CardsVar(3)
				});
			}
		}

		// Token: 0x06006D20 RID: 27936 RVA: 0x00260914 File Offset: 0x0025EB14
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_starry_impact", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06006D21 RID: 27937 RVA: 0x00260968 File Offset: 0x0025EB68
		public override async Task AfterCardPlayedLate(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner)
			{
				if (cardPlay.Card.Type == CardType.Skill)
				{
					if (base.Pile.Type != PileType.Hand)
					{
						int num = CombatManager.Instance.History.CardPlaysFinished.Count((CardPlayFinishedEntry e) => e.HappenedThisTurn(base.CombatState) && e.CardPlay.Card.Type == CardType.Skill && e.CardPlay.Card.Owner == base.Owner);
						if (num % base.DynamicVars.Cards.IntValue == 0)
						{
							await CardPileCmd.Add(this, PileType.Hand, CardPilePosition.Bottom, null, false);
						}
					}
				}
			}
		}

		// Token: 0x06006D22 RID: 27938 RVA: 0x002609B3 File Offset: 0x0025EBB3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
