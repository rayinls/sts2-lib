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
	// Token: 0x02000A04 RID: 2564
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Predator : CardModel
	{
		// Token: 0x06006E95 RID: 28309 RVA: 0x00263887 File Offset: 0x00261A87
		public Predator()
			: base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001DE4 RID: 7652
		// (get) Token: 0x06006E96 RID: 28310 RVA: 0x00263894 File Offset: 0x00261A94
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(15m, ValueProp.Move));
			}
		}

		// Token: 0x06006E97 RID: 28311 RVA: 0x002638A8 File Offset: 0x00261AA8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			await PowerCmd.Apply<DrawCardsNextTurnPower>(base.Owner.Creature, 2m, base.Owner.Creature, this, false);
		}

		// Token: 0x06006E98 RID: 28312 RVA: 0x002638FB File Offset: 0x00261AFB
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(5m);
		}
	}
}
