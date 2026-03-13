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
	// Token: 0x0200089A RID: 2202
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class AstralPulse : CardModel
	{
		// Token: 0x06006712 RID: 26386 RVA: 0x002549D4 File Offset: 0x00252BD4
		public AstralPulse()
			: base(0, CardType.Attack, CardRarity.Common, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001AB9 RID: 6841
		// (get) Token: 0x06006713 RID: 26387 RVA: 0x002549E1 File Offset: 0x00252BE1
		public override int CanonicalStarCost
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17001ABA RID: 6842
		// (get) Token: 0x06006714 RID: 26388 RVA: 0x002549E4 File Offset: 0x00252BE4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(14m, ValueProp.Move));
			}
		}

		// Token: 0x06006715 RID: 26389 RVA: 0x002549F8 File Offset: 0x00252BF8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
				.WithHitFx("vfx/vfx_starry_impact", null, null)
				.SpawningHitVfxOnEachCreature()
				.Execute(choiceContext);
		}

		// Token: 0x06006716 RID: 26390 RVA: 0x00254A43 File Offset: 0x00252C43
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(4m);
		}
	}
}
