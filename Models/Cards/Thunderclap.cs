using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A9B RID: 2715
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Thunderclap : CardModel
	{
		// Token: 0x060071C7 RID: 29127 RVA: 0x00269DB8 File Offset: 0x00267FB8
		public Thunderclap()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001F30 RID: 7984
		// (get) Token: 0x060071C8 RID: 29128 RVA: 0x00269DC5 File Offset: 0x00267FC5
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(4m, ValueProp.Move),
					new PowerVar<VulnerablePower>(1m)
				});
			}
		}

		// Token: 0x17001F31 RID: 7985
		// (get) Token: 0x060071C9 RID: 29129 RVA: 0x00269DEE File Offset: 0x00267FEE
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VulnerablePower>());
			}
		}

		// Token: 0x060071CA RID: 29130 RVA: 0x00269DFC File Offset: 0x00267FFC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			await PowerCmd.Apply<VulnerablePower>(base.CombatState.HittableEnemies, base.DynamicVars.Vulnerable.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060071CB RID: 29131 RVA: 0x00269E47 File Offset: 0x00268047
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
