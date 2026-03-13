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
	// Token: 0x02000908 RID: 2312
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DeathMarch : CardModel
	{
		// Token: 0x0600693D RID: 26941 RVA: 0x00258F5B File Offset: 0x0025715B
		public DeathMarch()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001B9B RID: 7067
		// (get) Token: 0x0600693E RID: 26942 RVA: 0x00258F68 File Offset: 0x00257168
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(8m);
				array[1] = new ExtraDamageVar(3m);
				array[2] = new CalculatedDamageVar(ValueProp.Move).WithMultiplier((CardModel card, [Nullable(2)] Creature _) => CombatManager.Instance.History.Entries.OfType<CardDrawnEntry>().Count((CardDrawnEntry e) => e.HappenedThisTurn(card.CombatState) && e.Actor == card.Owner.Creature && !e.FromHandDraw));
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x0600693F RID: 26943 RVA: 0x00258FCC File Offset: 0x002571CC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.CalculatedDamage).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06006940 RID: 26944 RVA: 0x0025901F File Offset: 0x0025721F
		protected override void OnUpgrade()
		{
			base.DynamicVars.CalculationBase.UpgradeValueBy(1m);
			base.DynamicVars.ExtraDamage.UpgradeValueBy(1m);
		}
	}
}
