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
	// Token: 0x02000AA9 RID: 2729
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class UltimateStrike : CardModel
	{
		// Token: 0x0600720E RID: 29198 RVA: 0x0026A59B File Offset: 0x0026879B
		public UltimateStrike()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001F4E RID: 8014
		// (get) Token: 0x0600720F RID: 29199 RVA: 0x0026A5A8 File Offset: 0x002687A8
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Strike };
			}
		}

		// Token: 0x17001F4F RID: 8015
		// (get) Token: 0x06007210 RID: 29200 RVA: 0x0026A5B7 File Offset: 0x002687B7
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(14m, ValueProp.Move));
			}
		}

		// Token: 0x06007211 RID: 29201 RVA: 0x0026A5CC File Offset: 0x002687CC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06007212 RID: 29202 RVA: 0x0026A61F File Offset: 0x0026881F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(6m);
		}
	}
}
