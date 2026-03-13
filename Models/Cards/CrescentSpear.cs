using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008F9 RID: 2297
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CrescentSpear : CardModel
	{
		// Token: 0x060068F5 RID: 26869 RVA: 0x0025864D File Offset: 0x0025684D
		public CrescentSpear()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001B7E RID: 7038
		// (get) Token: 0x060068F6 RID: 26870 RVA: 0x0025865A File Offset: 0x0025685A
		public override int CanonicalStarCost
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17001B7F RID: 7039
		// (get) Token: 0x060068F7 RID: 26871 RVA: 0x00258660 File Offset: 0x00256860
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(6m);
				array[1] = new ExtraDamageVar(2m);
				array[2] = new CalculatedDamageVar(ValueProp.Move).WithMultiplier((CardModel card, [Nullable(2)] Creature _) => card.Owner.PlayerCombatState.AllCards.Count((CardModel c) => c.CanonicalStarCost >= 0 || c.HasStarCostX));
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x060068F8 RID: 26872 RVA: 0x002586C4 File Offset: 0x002568C4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.CalculatedDamage).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_starry_impact", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x060068F9 RID: 26873 RVA: 0x00258717 File Offset: 0x00256917
		protected override void OnUpgrade()
		{
			base.DynamicVars.ExtraDamage.UpgradeValueBy(1m);
		}
	}
}
