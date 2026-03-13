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
	// Token: 0x0200096B RID: 2411
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GiantRock : CardModel
	{
		// Token: 0x06006B68 RID: 27496 RVA: 0x0025D0BD File Offset: 0x0025B2BD
		public GiantRock()
			: base(1, CardType.Attack, CardRarity.Token, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001C95 RID: 7317
		// (get) Token: 0x06006B69 RID: 27497 RVA: 0x0025D0CA File Offset: 0x0025B2CA
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(16m, ValueProp.Move));
			}
		}

		// Token: 0x06006B6A RID: 27498 RVA: 0x0025D0E0 File Offset: 0x0025B2E0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_rock_shatter", null, "blunt_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x06006B6B RID: 27499 RVA: 0x0025D133 File Offset: 0x0025B333
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(4m);
		}
	}
}
