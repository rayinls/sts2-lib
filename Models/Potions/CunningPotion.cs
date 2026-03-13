using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x020006EA RID: 1770
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CunningPotion : PotionModel
	{
		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x06005757 RID: 22359 RVA: 0x00229CFF File Offset: 0x00227EFF
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Uncommon;
			}
		}

		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x06005758 RID: 22360 RVA: 0x00229D02 File Offset: 0x00227F02
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x06005759 RID: 22361 RVA: 0x00229D05 File Offset: 0x00227F05
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x0600575A RID: 22362 RVA: 0x00229D08 File Offset: 0x00227F08
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x0600575B RID: 22363 RVA: 0x00229D15 File Offset: 0x00227F15
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Shiv>(true));
			}
		}

		// Token: 0x0600575C RID: 22364 RVA: 0x00229D24 File Offset: 0x00227F24
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			IEnumerable<CardModel> enumerable = await Shiv.CreateInHand(base.Owner, base.DynamicVars.Cards.IntValue, base.Owner.Creature.CombatState);
			IEnumerable<CardModel> enumerable2 = enumerable;
			foreach (CardModel cardModel in enumerable2)
			{
				CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
			}
		}
	}
}
