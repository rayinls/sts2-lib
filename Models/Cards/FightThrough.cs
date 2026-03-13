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
	// Token: 0x0200094E RID: 2382
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FightThrough : CardModel
	{
		// Token: 0x06006AC1 RID: 27329 RVA: 0x0025BB3F File Offset: 0x00259D3F
		public FightThrough()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001C51 RID: 7249
		// (get) Token: 0x06006AC2 RID: 27330 RVA: 0x0025BB4C File Offset: 0x00259D4C
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C52 RID: 7250
		// (get) Token: 0x06006AC3 RID: 27331 RVA: 0x0025BB4F File Offset: 0x00259D4F
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Wound>(false));
			}
		}

		// Token: 0x17001C53 RID: 7251
		// (get) Token: 0x06006AC4 RID: 27332 RVA: 0x0025BB5C File Offset: 0x00259D5C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(13m, ValueProp.Move));
			}
		}

		// Token: 0x06006AC5 RID: 27333 RVA: 0x0025BB70 File Offset: 0x00259D70
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			for (int i = 0; i < 2; i++)
			{
				CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(base.CombatState.CreateCard<Wound>(base.Owner), PileType.Discard, true, CardPilePosition.Bottom), 1.2f, CardPreviewStyle.HorizontalLayout);
			}
		}

		// Token: 0x06006AC6 RID: 27334 RVA: 0x0025BBBB File Offset: 0x00259DBB
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(4m);
		}
	}
}
