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
	// Token: 0x02000980 RID: 2432
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HandTrick : CardModel
	{
		// Token: 0x06006BD4 RID: 27604 RVA: 0x0025DE1B File Offset: 0x0025C01B
		public HandTrick()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001CC5 RID: 7365
		// (get) Token: 0x06006BD5 RID: 27605 RVA: 0x0025DE28 File Offset: 0x0025C028
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001CC6 RID: 7366
		// (get) Token: 0x06006BD6 RID: 27606 RVA: 0x0025DE2B File Offset: 0x0025C02B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(7m, ValueProp.Move));
			}
		}

		// Token: 0x17001CC7 RID: 7367
		// (get) Token: 0x06006BD7 RID: 27607 RVA: 0x0025DE3E File Offset: 0x0025C03E
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Sly));
			}
		}

		// Token: 0x06006BD8 RID: 27608 RVA: 0x0025DE4C File Offset: 0x0025C04C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.SelectionScreenPrompt, 1);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHand(choiceContext, base.Owner, cardSelectorPrefs, (CardModel card) => card.Type == CardType.Skill && !card.IsSlyThisTurn, this);
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				CardCmd.ApplySingleTurnSly(cardModel);
			}
		}

		// Token: 0x06006BD9 RID: 27609 RVA: 0x0025DE9F File Offset: 0x0025C09F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}
