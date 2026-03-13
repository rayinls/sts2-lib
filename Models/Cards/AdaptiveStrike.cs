using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000888 RID: 2184
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class AdaptiveStrike : CardModel
	{
		// Token: 0x060066B9 RID: 26297 RVA: 0x00253FAE File Offset: 0x002521AE
		public AdaptiveStrike()
			: base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001A94 RID: 6804
		// (get) Token: 0x060066BA RID: 26298 RVA: 0x00253FBB File Offset: 0x002521BB
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Strike };
			}
		}

		// Token: 0x17001A95 RID: 6805
		// (get) Token: 0x060066BB RID: 26299 RVA: 0x00253FCA File Offset: 0x002521CA
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(18m, ValueProp.Move));
			}
		}

		// Token: 0x060066BC RID: 26300 RVA: 0x00253FE0 File Offset: 0x002521E0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			CardModel cardModel = base.CreateClone();
			cardModel.EnergyCost.SetThisCombat(0, false);
			CardPileAddResult cardPileAddResult = await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Discard, true, CardPilePosition.Bottom);
			CardCmd.PreviewCardPileAdd(cardPileAddResult, 1.5f, CardPreviewStyle.HorizontalLayout);
		}

		// Token: 0x060066BD RID: 26301 RVA: 0x00254033 File Offset: 0x00252233
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(5m);
		}
	}
}
