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
	// Token: 0x02000A6E RID: 2670
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Stardust : CardModel
	{
		// Token: 0x060070DD RID: 28893 RVA: 0x0026809D File Offset: 0x0026629D
		public Stardust()
			: base(0, CardType.Attack, CardRarity.Uncommon, TargetType.RandomEnemy, true)
		{
		}

		// Token: 0x17001ED9 RID: 7897
		// (get) Token: 0x060070DE RID: 28894 RVA: 0x002680AA File Offset: 0x002662AA
		public override bool HasStarCostX
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001EDA RID: 7898
		// (get) Token: 0x060070DF RID: 28895 RVA: 0x002680AD File Offset: 0x002662AD
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(5m, ValueProp.Move));
			}
		}

		// Token: 0x060070E0 RID: 28896 RVA: 0x002680C0 File Offset: 0x002662C0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(base.ResolveStarXValue()).FromCard(this)
				.TargetingRandomOpponents(base.CombatState, true)
				.WithHitFx("vfx/vfx_starry_impact", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x060070E1 RID: 28897 RVA: 0x0026810B File Offset: 0x0026630B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
		}
	}
}
