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
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008AA RID: 2218
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Begone : CardModel
	{
		// Token: 0x06006764 RID: 26468 RVA: 0x002554EB File Offset: 0x002536EB
		public Begone()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001ADA RID: 6874
		// (get) Token: 0x06006765 RID: 26469 RVA: 0x002554F8 File Offset: 0x002536F8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(4m, ValueProp.Move));
			}
		}

		// Token: 0x17001ADB RID: 6875
		// (get) Token: 0x06006766 RID: 26470 RVA: 0x0025550B File Offset: 0x0025370B
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<MinionDiveBomb>(base.IsUpgraded));
			}
		}

		// Token: 0x06006767 RID: 26471 RVA: 0x00255520 File Offset: 0x00253720
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.TransformSelectionPrompt, 1);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHand(choiceContext, base.Owner, cardSelectorPrefs, null, this);
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				CardModel cardModel2 = base.CombatState.CreateCard<MinionDiveBomb>(base.Owner);
				if (base.IsUpgraded)
				{
					CardCmd.Upgrade(cardModel2, CardPreviewStyle.HorizontalLayout);
				}
				await CardCmd.Transform(cardModel, cardModel2, CardPreviewStyle.HorizontalLayout);
			}
		}

		// Token: 0x06006768 RID: 26472 RVA: 0x00255573 File Offset: 0x00253773
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(1m);
		}
	}
}
