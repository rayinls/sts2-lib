using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A37 RID: 2615
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SculptingStrike : CardModel
	{
		// Token: 0x06006FA1 RID: 28577 RVA: 0x002659F4 File Offset: 0x00263BF4
		public SculptingStrike()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001E52 RID: 7762
		// (get) Token: 0x06006FA2 RID: 28578 RVA: 0x00265A01 File Offset: 0x00263C01
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Ethereal));
			}
		}

		// Token: 0x17001E53 RID: 7763
		// (get) Token: 0x06006FA3 RID: 28579 RVA: 0x00265A0E File Offset: 0x00263C0E
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Strike };
			}
		}

		// Token: 0x17001E54 RID: 7764
		// (get) Token: 0x06006FA4 RID: 28580 RVA: 0x00265A1D File Offset: 0x00263C1D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(8m, ValueProp.Move));
			}
		}

		// Token: 0x06006FA5 RID: 28581 RVA: 0x00265A30 File Offset: 0x00263C30
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.SelectionScreenPrompt, 1);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHand(choiceContext, base.Owner, cardSelectorPrefs, (CardModel c) => !c.Keywords.Contains(CardKeyword.Ethereal), this);
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				CardCmd.ApplyKeyword(cardModel, new CardKeyword[] { CardKeyword.Ethereal });
			}
		}

		// Token: 0x06006FA6 RID: 28582 RVA: 0x00265A83 File Offset: 0x00263C83
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
