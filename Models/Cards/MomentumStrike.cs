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
	// Token: 0x020009D4 RID: 2516
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MomentumStrike : CardModel
	{
		// Token: 0x06006DA0 RID: 28064 RVA: 0x00261953 File Offset: 0x0025FB53
		public MomentumStrike()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D81 RID: 7553
		// (get) Token: 0x06006DA1 RID: 28065 RVA: 0x00261960 File Offset: 0x0025FB60
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Strike };
			}
		}

		// Token: 0x17001D82 RID: 7554
		// (get) Token: 0x06006DA2 RID: 28066 RVA: 0x0026196F File Offset: 0x0025FB6F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(10m, ValueProp.Move));
			}
		}

		// Token: 0x06006DA3 RID: 28067 RVA: 0x00261984 File Offset: 0x0025FB84
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			base.EnergyCost.SetThisCombat(0, false);
		}

		// Token: 0x06006DA4 RID: 28068 RVA: 0x002619D7 File Offset: 0x0025FBD7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
