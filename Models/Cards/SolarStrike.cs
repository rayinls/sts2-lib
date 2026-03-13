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
	// Token: 0x02000A5A RID: 2650
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SolarStrike : CardModel
	{
		// Token: 0x0600705F RID: 28767 RVA: 0x00266FBF File Offset: 0x002651BF
		public SolarStrike()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001EA7 RID: 7847
		// (get) Token: 0x06007060 RID: 28768 RVA: 0x00266FCC File Offset: 0x002651CC
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Strike };
			}
		}

		// Token: 0x17001EA8 RID: 7848
		// (get) Token: 0x06007061 RID: 28769 RVA: 0x00266FDB File Offset: 0x002651DB
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(8m, ValueProp.Move),
					new StarsVar(1)
				});
			}
		}

		// Token: 0x06007062 RID: 28770 RVA: 0x00267000 File Offset: 0x00265200
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			await PlayerCmd.GainStars(base.DynamicVars.Stars.BaseValue, base.Owner);
		}

		// Token: 0x06007063 RID: 28771 RVA: 0x00267053 File Offset: 0x00265253
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(1m);
			base.DynamicVars.Stars.UpgradeValueBy(1m);
		}
	}
}
