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
	// Token: 0x020008CD RID: 2253
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Bury : CardModel
	{
		// Token: 0x0600681D RID: 26653 RVA: 0x00256C13 File Offset: 0x00254E13
		public Bury()
			: base(4, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001B28 RID: 6952
		// (get) Token: 0x0600681E RID: 26654 RVA: 0x00256C20 File Offset: 0x00254E20
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(52m, ValueProp.Move));
			}
		}

		// Token: 0x0600681F RID: 26655 RVA: 0x00256C34 File Offset: 0x00254E34
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x06006820 RID: 26656 RVA: 0x00256C87 File Offset: 0x00254E87
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(11m);
		}
	}
}
