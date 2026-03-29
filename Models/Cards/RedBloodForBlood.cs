using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RedBloodForBlood : CardModel
	{
		public RedBloodForBlood()
			: base(4, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new DynamicVar[] { new DamageVar(18m, ValueProp.Move) };
			}
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(4m);
		}

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
			int num = CombatManager.Instance.History.Entries.OfType<DamageReceivedEntry>().Count((DamageReceivedEntry e) => e.Receiver == base.Owner.Creature && e.Result.UnblockedDamage > 0);
			if (num > 0)
			{
				base.EnergyCost.AddThisCombat(-num, false);
			}
			return Task.CompletedTask;
		}

		public override Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			if (target != base.Owner.Creature)
			{
				return Task.CompletedTask;
			}
			if (result.UnblockedDamage <= 0m)
			{
				return Task.CompletedTask;
			}
			base.EnergyCost.AddThisCombat(-1, false);
			return Task.CompletedTask;
		}
	}
}
