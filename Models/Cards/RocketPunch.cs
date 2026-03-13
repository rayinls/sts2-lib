using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A2C RID: 2604
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RocketPunch : CardModel
	{
		// Token: 0x06006F6B RID: 28523 RVA: 0x00265363 File Offset: 0x00263563
		public RocketPunch()
			: base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001E3E RID: 7742
		// (get) Token: 0x06006F6C RID: 28524 RVA: 0x00265370 File Offset: 0x00263570
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(13m, ValueProp.Move),
					new CardsVar(1)
				});
			}
		}

		// Token: 0x06006F6D RID: 28525 RVA: 0x00265398 File Offset: 0x00263598
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}

		// Token: 0x06006F6E RID: 28526 RVA: 0x002653EB File Offset: 0x002635EB
		public override Task AfterCardGeneratedForCombat(CardModel card, bool addedByPlayer)
		{
			if (card.Owner != base.Owner)
			{
				return Task.CompletedTask;
			}
			if (card.Type != CardType.Status)
			{
				return Task.CompletedTask;
			}
			base.EnergyCost.SetThisTurnOrUntilPlayed(0, false);
			return Task.CompletedTask;
		}

		// Token: 0x06006F6F RID: 28527 RVA: 0x00265422 File Offset: 0x00263622
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(1m);
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
