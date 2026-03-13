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
	// Token: 0x020009FD RID: 2557
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Pinpoint : CardModel
	{
		// Token: 0x06006E71 RID: 28273 RVA: 0x0026335E File Offset: 0x0026155E
		public Pinpoint()
			: base(3, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001DD7 RID: 7639
		// (get) Token: 0x06006E72 RID: 28274 RVA: 0x0026336B File Offset: 0x0026156B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(17m, ValueProp.Move));
			}
		}

		// Token: 0x06006E73 RID: 28275 RVA: 0x00263380 File Offset: 0x00261580
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06006E74 RID: 28276 RVA: 0x002633D3 File Offset: 0x002615D3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(5m);
		}

		// Token: 0x06006E75 RID: 28277 RVA: 0x002633EC File Offset: 0x002615EC
		public override Task AfterCardEnteredCombat(CardModel card)
		{
			if (card != this)
			{
				return Task.CompletedTask;
			}
			if (base.IsClone)
			{
				return Task.CompletedTask;
			}
			int num = CombatManager.Instance.History.CardPlaysFinished.Count((CardPlayFinishedEntry e) => e.CardPlay.Card.Type == CardType.Skill && e.CardPlay.Card.Owner == base.Owner && e.HappenedThisTurn(base.CombatState));
			this.ReduceCostBy(num);
			return Task.CompletedTask;
		}

		// Token: 0x06006E76 RID: 28278 RVA: 0x0026343E File Offset: 0x0026163E
		public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner != base.Owner)
			{
				return Task.CompletedTask;
			}
			if (cardPlay.Card.Type != CardType.Skill)
			{
				return Task.CompletedTask;
			}
			this.ReduceCostBy(1);
			return Task.CompletedTask;
		}

		// Token: 0x06006E77 RID: 28279 RVA: 0x00263479 File Offset: 0x00261679
		private void ReduceCostBy(int amount)
		{
			base.EnergyCost.AddThisTurn(-amount, false);
		}
	}
}
