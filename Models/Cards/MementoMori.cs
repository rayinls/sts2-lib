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
	// Token: 0x020009C6 RID: 2502
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MementoMori : CardModel
	{
		// Token: 0x06006D50 RID: 27984 RVA: 0x00260F70 File Offset: 0x0025F170
		public MementoMori()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D5B RID: 7515
		// (get) Token: 0x06006D51 RID: 27985 RVA: 0x00260F80 File Offset: 0x0025F180
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(8m);
				array[1] = new ExtraDamageVar(4m);
				array[2] = new CalculatedDamageVar(ValueProp.Move).WithMultiplier((CardModel card, [Nullable(2)] Creature _) => CombatManager.Instance.History.Entries.OfType<CardDiscardedEntry>().Count((CardDiscardedEntry e) => e.HappenedThisTurn(card.CombatState) && e.Card.Owner == card.Owner));
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x06006D52 RID: 27986 RVA: 0x00260FE4 File Offset: 0x0025F1E4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.CalculatedDamage).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06006D53 RID: 27987 RVA: 0x00261037 File Offset: 0x0025F237
		protected override void OnUpgrade()
		{
			base.DynamicVars.CalculationBase.UpgradeValueBy(2m);
			base.DynamicVars.ExtraDamage.UpgradeValueBy(1m);
		}
	}
}
