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
	// Token: 0x02000981 RID: 2433
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Hang : CardModel
	{
		// Token: 0x06006BDA RID: 27610 RVA: 0x0025DEB7 File Offset: 0x0025C0B7
		public Hang()
			: base(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001CC8 RID: 7368
		// (get) Token: 0x06006BDB RID: 27611 RVA: 0x0025DEC4 File Offset: 0x0025C0C4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(10m, ValueProp.Move));
			}
		}

		// Token: 0x06006BDC RID: 27612 RVA: 0x0025DED8 File Offset: 0x0025C0D8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			int powerAmount = cardPlay.Target.GetPowerAmount<HangPower>();
			int num = Math.Max(2, powerAmount);
			if (powerAmount + num > 999)
			{
				num = Math.Max(0, 999 - powerAmount);
			}
			await PowerCmd.Apply<HangPower>(cardPlay.Target, num, base.Owner.Creature, this, false);
		}

		// Token: 0x06006BDD RID: 27613 RVA: 0x0025DF2B File Offset: 0x0025C12B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
