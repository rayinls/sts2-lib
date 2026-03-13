using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009A6 RID: 2470
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Jackpot : CardModel
	{
		// Token: 0x06006C96 RID: 27798 RVA: 0x0025F64F File Offset: 0x0025D84F
		public Jackpot()
			: base(3, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D13 RID: 7443
		// (get) Token: 0x06006C97 RID: 27799 RVA: 0x0025F65C File Offset: 0x0025D85C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(25m, ValueProp.Move),
					new CardsVar(3)
				});
			}
		}

		// Token: 0x06006C98 RID: 27800 RVA: 0x0025F684 File Offset: 0x0025D884
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			IEnumerable<CardModel> forCombat = CardFactory.GetForCombat(base.Owner, base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint).Where(delegate(CardModel c)
			{
				CardEnergyCost energyCost = c.EnergyCost;
				return energyCost != null && energyCost.Canonical == 0 && !energyCost.CostsX;
			}), base.DynamicVars.Cards.IntValue, base.Owner.RunState.Rng.CombatCardGeneration);
			foreach (CardModel cardModel in forCombat)
			{
				if (base.IsUpgraded)
				{
					CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
				}
				await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, true, CardPilePosition.Bottom);
			}
			IEnumerator<CardModel> enumerator = null;
		}

		// Token: 0x06006C99 RID: 27801 RVA: 0x0025F6D7 File Offset: 0x0025D8D7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(5m);
		}
	}
}
