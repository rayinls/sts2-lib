using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000956 RID: 2390
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Flatten : CardModel
	{
		// Token: 0x06006AE8 RID: 27368 RVA: 0x0025C0DF File Offset: 0x0025A2DF
		public Flatten()
			: base(2, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001C5F RID: 7263
		// (get) Token: 0x06006AE9 RID: 27369 RVA: 0x0025C0EC File Offset: 0x0025A2EC
		protected override bool ShouldGlowGoldInternal
		{
			get
			{
				return this.HasOstyAttackedThisTurn;
			}
		}

		// Token: 0x17001C60 RID: 7264
		// (get) Token: 0x06006AEA RID: 27370 RVA: 0x0025C0F4 File Offset: 0x0025A2F4
		protected override bool ShouldGlowRedInternal
		{
			get
			{
				return base.Owner.IsOstyMissing;
			}
		}

		// Token: 0x17001C61 RID: 7265
		// (get) Token: 0x06006AEB RID: 27371 RVA: 0x0025C101 File Offset: 0x0025A301
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.OstyAttack };
			}
		}

		// Token: 0x17001C62 RID: 7266
		// (get) Token: 0x06006AEC RID: 27372 RVA: 0x0025C110 File Offset: 0x0025A310
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new OstyDamageVar(12m, ValueProp.Move));
			}
		}

		// Token: 0x06006AED RID: 27373 RVA: 0x0025C124 File Offset: 0x0025A324
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			if (!Osty.CheckMissingWithAnim(base.Owner))
			{
				await DamageCmd.Attack(base.DynamicVars.OstyDamage.BaseValue).FromOsty(base.Owner.Osty, this).Targeting(cardPlay.Target)
					.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
					.Execute(choiceContext);
			}
		}

		// Token: 0x06006AEE RID: 27374 RVA: 0x0025C177 File Offset: 0x0025A377
		protected override void OnUpgrade()
		{
			base.DynamicVars.OstyDamage.UpgradeValueBy(4m);
		}

		// Token: 0x06006AEF RID: 27375 RVA: 0x0025C18F File Offset: 0x0025A38F
		public override Task AfterCardEnteredCombat(CardModel card)
		{
			if (card != this)
			{
				return Task.CompletedTask;
			}
			if (!this.HasOstyAttackedThisTurn)
			{
				return Task.CompletedTask;
			}
			this.ReduceCost();
			return Task.CompletedTask;
		}

		// Token: 0x06006AF0 RID: 27376 RVA: 0x0025C1B4 File Offset: 0x0025A3B4
		public override Task AfterAttack(AttackCommand command)
		{
			if (command.Attacker == null)
			{
				return Task.CompletedTask;
			}
			if (command.Attacker != base.Owner.Osty)
			{
				return Task.CompletedTask;
			}
			this.ReduceCost();
			return Task.CompletedTask;
		}

		// Token: 0x06006AF1 RID: 27377 RVA: 0x0025C1E8 File Offset: 0x0025A3E8
		private void ReduceCost()
		{
			base.EnergyCost.SetThisTurn(0, false);
		}

		// Token: 0x17001C63 RID: 7267
		// (get) Token: 0x06006AF2 RID: 27378 RVA: 0x0025C1F7 File Offset: 0x0025A3F7
		private bool HasOstyAttackedThisTurn
		{
			get
			{
				return CombatManager.Instance.History.Entries.OfType<CreatureAttackedEntry>().Any((CreatureAttackedEntry e) => e.Actor == base.Owner.Osty && e.HappenedThisTurn(base.CombatState));
			}
		}
	}
}
