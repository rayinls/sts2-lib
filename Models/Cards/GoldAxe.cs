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
	// Token: 0x02000973 RID: 2419
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GoldAxe : CardModel
	{
		// Token: 0x06006B92 RID: 27538 RVA: 0x0025D623 File Offset: 0x0025B823
		public GoldAxe()
			: base(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001CA7 RID: 7335
		// (get) Token: 0x06006B93 RID: 27539 RVA: 0x0025D630 File Offset: 0x0025B830
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(0m);
				array[1] = new ExtraDamageVar(1m);
				array[2] = new CalculatedDamageVar(ValueProp.Move).WithMultiplier((CardModel card, [Nullable(2)] Creature _) => CombatManager.Instance.History.CardPlaysFinished.Count((CardPlayFinishedEntry e) => e.CardPlay.Card.Owner == card.Owner));
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x06006B94 RID: 27540 RVA: 0x0025D690 File Offset: 0x0025B890
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.CalculatedDamage).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_dramatic_stab", null, "blunt_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x06006B95 RID: 27541 RVA: 0x0025D6E3 File Offset: 0x0025B8E3
		protected override void OnUpgrade()
		{
			base.AddKeyword(CardKeyword.Retain);
		}
	}
}
