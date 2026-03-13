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
	// Token: 0x020009AC RID: 2476
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Knockdown : CardModel
	{
		// Token: 0x06006CB3 RID: 27827 RVA: 0x0025FAAF File Offset: 0x0025DCAF
		public Knockdown()
			: base(3, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D1B RID: 7451
		// (get) Token: 0x06006CB4 RID: 27828 RVA: 0x0025FABC File Offset: 0x0025DCBC
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.MultiplayerOnly;
			}
		}

		// Token: 0x17001D1C RID: 7452
		// (get) Token: 0x06006CB5 RID: 27829 RVA: 0x0025FABF File Offset: 0x0025DCBF
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(10m, ValueProp.Move),
					new PowerVar<KnockdownPower>(2m)
				});
			}
		}

		// Token: 0x06006CB6 RID: 27830 RVA: 0x0025FAEC File Offset: 0x0025DCEC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			await PowerCmd.Apply<KnockdownPower>(cardPlay.Target, base.DynamicVars["KnockdownPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006CB7 RID: 27831 RVA: 0x0025FB3F File Offset: 0x0025DD3F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(4m);
			base.DynamicVars["KnockdownPower"].UpgradeValueBy(1m);
		}
	}
}
