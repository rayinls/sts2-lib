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
	// Token: 0x02000A49 RID: 2633
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ShiningStrike : CardModel
	{
		// Token: 0x06007000 RID: 28672 RVA: 0x00266483 File Offset: 0x00264683
		public ShiningStrike()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001E7C RID: 7804
		// (get) Token: 0x06007001 RID: 28673 RVA: 0x00266490 File Offset: 0x00264690
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Strike };
			}
		}

		// Token: 0x17001E7D RID: 7805
		// (get) Token: 0x06007002 RID: 28674 RVA: 0x0026649F File Offset: 0x0026469F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(8m, ValueProp.Move),
					new StarsVar(2)
				});
			}
		}

		// Token: 0x06007003 RID: 28675 RVA: 0x002664C4 File Offset: 0x002646C4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_starry_impact", null, null)
				.Execute(choiceContext);
			await PlayerCmd.GainStars(base.DynamicVars.Stars.BaseValue, base.Owner);
			if (!base.Keywords.Contains(CardKeyword.Exhaust) && !base.ExhaustOnNextPlay)
			{
				await CardPileCmd.Add(this, PileType.Draw, CardPilePosition.Top, null, false);
			}
		}

		// Token: 0x06007004 RID: 28676 RVA: 0x00266517 File Offset: 0x00264717
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
