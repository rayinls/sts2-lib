using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A02 RID: 2562
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Pounce : CardModel
	{
		// Token: 0x06006E8D RID: 28301 RVA: 0x0026371C File Offset: 0x0026191C
		public Pounce()
			: base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001DE2 RID: 7650
		// (get) Token: 0x06006E8E RID: 28302 RVA: 0x00263729 File Offset: 0x00261929
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(12m, ValueProp.Move));
			}
		}

		// Token: 0x06006E8F RID: 28303 RVA: 0x00263740 File Offset: 0x00261940
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			await PowerCmd.Apply<FreeSkillPower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x06006E90 RID: 28304 RVA: 0x00263793 File Offset: 0x00261993
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(6m);
		}
	}
}
