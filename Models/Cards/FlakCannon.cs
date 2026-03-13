using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000952 RID: 2386
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FlakCannon : CardModel
	{
		// Token: 0x06006AD5 RID: 27349 RVA: 0x0025BDEF File Offset: 0x00259FEF
		public FlakCannon()
			: base(2, CardType.Attack, CardRarity.Rare, TargetType.RandomEnemy, true)
		{
		}

		// Token: 0x17001C59 RID: 7257
		// (get) Token: 0x06006AD6 RID: 27350 RVA: 0x0025BDFC File Offset: 0x00259FFC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[4];
				array[0] = new DamageVar(8m, ValueProp.Move);
				array[1] = new CalculationBaseVar(0m);
				array[2] = new CalculationExtraVar(1m);
				array[3] = new CalculatedVar("CalculatedHits").WithMultiplier((CardModel card, [Nullable(2)] Creature _) => FlakCannon.GetStatuses(card.Owner).Count<CardModel>());
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x17001C5A RID: 7258
		// (get) Token: 0x06006AD7 RID: 27351 RVA: 0x0025BE6E File Offset: 0x0025A06E
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x06006AD8 RID: 27352 RVA: 0x0025BE7C File Offset: 0x0025A07C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			List<CardModel> list = FlakCannon.GetStatuses(base.Owner).ToList<CardModel>();
			int statusCount = (int)((CalculatedVar)base.DynamicVars["CalculatedHits"]).Calculate(cardPlay.Target);
			foreach (CardModel cardModel in list)
			{
				await CardCmd.Exhaust(choiceContext, cardModel, false, false);
			}
			List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(statusCount).FromCard(this)
				.TargetingRandomOpponents(base.CombatState, true)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x06006AD9 RID: 27353 RVA: 0x0025BECF File Offset: 0x0025A0CF
		private static IEnumerable<CardModel> GetStatuses(Player owner)
		{
			return owner.PlayerCombatState.AllCards.Where((CardModel c) => c.Type == CardType.Status && c.Pile.Type != PileType.Exhaust);
		}

		// Token: 0x06006ADA RID: 27354 RVA: 0x0025BF00 File Offset: 0x0025A100
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}

		// Token: 0x0400257E RID: 9598
		private const string _calculatedHitsKey = "CalculatedHits";
	}
}
