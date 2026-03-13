using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A1D RID: 2589
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Reave : CardModel
	{
		// Token: 0x06006F13 RID: 28435 RVA: 0x002648C8 File Offset: 0x00262AC8
		public Reave()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001E16 RID: 7702
		// (get) Token: 0x06006F14 RID: 28436 RVA: 0x002648D5 File Offset: 0x00262AD5
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(9m, ValueProp.Move),
					new CardsVar(1)
				});
			}
		}

		// Token: 0x17001E17 RID: 7703
		// (get) Token: 0x06006F15 RID: 28437 RVA: 0x002648FB File Offset: 0x00262AFB
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Soul>(base.IsUpgraded));
			}
		}

		// Token: 0x06006F16 RID: 28438 RVA: 0x00264910 File Offset: 0x00262B10
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			IEnumerable<Soul> enumerable = Soul.Create(base.Owner, base.DynamicVars.Cards.IntValue, base.CombatState);
			if (base.IsUpgraded)
			{
				foreach (Soul soul in enumerable)
				{
					CardCmd.Upgrade(soul, CardPreviewStyle.HorizontalLayout);
				}
			}
			IReadOnlyList<CardPileAddResult> readOnlyList = await CardPileCmd.AddGeneratedCardsToCombat(enumerable, PileType.Draw, true, CardPilePosition.Bottom);
			CardCmd.PreviewCardPileAdd(readOnlyList, 1.2f, CardPreviewStyle.HorizontalLayout);
		}

		// Token: 0x06006F17 RID: 28439 RVA: 0x00264963 File Offset: 0x00262B63
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
		}
	}
}
