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
	// Token: 0x02000975 RID: 2421
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Grapple : CardModel
	{
		// Token: 0x06006B9C RID: 27548 RVA: 0x0025D797 File Offset: 0x0025B997
		public Grapple()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001CAB RID: 7339
		// (get) Token: 0x06006B9D RID: 27549 RVA: 0x0025D7A4 File Offset: 0x0025B9A4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(7m, ValueProp.Move),
					new PowerVar<GrapplePower>(5m)
				});
			}
		}

		// Token: 0x06006B9E RID: 27550 RVA: 0x0025D7D0 File Offset: 0x0025B9D0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			await PowerCmd.Apply<GrapplePower>(cardPlay.Target, base.DynamicVars["GrapplePower"].IntValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006B9F RID: 27551 RVA: 0x0025D823 File Offset: 0x0025BA23
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
			base.DynamicVars["GrapplePower"].UpgradeValueBy(2m);
		}
	}
}
