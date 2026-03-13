using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000619 RID: 1561
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EntropyPower : PowerModel
	{
		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x0600527C RID: 21116 RVA: 0x00221287 File Offset: 0x0021F487
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x0600527D RID: 21117 RVA: 0x0022128A File Offset: 0x0021F48A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x0600527E RID: 21118 RVA: 0x0022128D File Offset: 0x0021F48D
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Transform, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x0600527F RID: 21119 RVA: 0x002212A0 File Offset: 0x0021F4A0
		public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Owner.Player)
			{
				CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.TransformSelectionPrompt, base.Amount);
				IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHand(choiceContext, player, cardSelectorPrefs, null, this);
				List<CardModel> list = enumerable.ToList<CardModel>();
				foreach (CardModel cardModel in list)
				{
					await CardCmd.TransformToRandom(cardModel, player.RunState.Rng.CombatCardSelection, CardPreviewStyle.HorizontalLayout);
				}
				List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
			}
		}
	}
}
