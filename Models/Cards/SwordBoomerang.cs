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
	// Token: 0x02000A85 RID: 2693
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SwordBoomerang : CardModel
	{
		// Token: 0x0600714F RID: 29007 RVA: 0x00268F83 File Offset: 0x00267183
		public SwordBoomerang()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.RandomEnemy, true)
		{
		}

		// Token: 0x17001F02 RID: 7938
		// (get) Token: 0x06007150 RID: 29008 RVA: 0x00268F90 File Offset: 0x00267190
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(3m, ValueProp.Move),
					new RepeatVar(3)
				});
			}
		}

		// Token: 0x06007151 RID: 29009 RVA: 0x00268FB8 File Offset: 0x002671B8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(base.DynamicVars.Repeat.IntValue).FromCard(this)
				.TargetingRandomOpponents(base.CombatState, true)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06007152 RID: 29010 RVA: 0x00269003 File Offset: 0x00267203
		protected override void OnUpgrade()
		{
			base.DynamicVars.Repeat.UpgradeValueBy(1m);
		}
	}
}
