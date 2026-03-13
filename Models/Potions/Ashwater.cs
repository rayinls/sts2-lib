using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x020006DF RID: 1759
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Ashwater : PotionModel
	{
		// Token: 0x17001379 RID: 4985
		// (get) Token: 0x06005716 RID: 22294 RVA: 0x002297BD File Offset: 0x002279BD
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Uncommon;
			}
		}

		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x06005717 RID: 22295 RVA: 0x002297C0 File Offset: 0x002279C0
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x1700137B RID: 4987
		// (get) Token: 0x06005718 RID: 22296 RVA: 0x002297C3 File Offset: 0x002279C3
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x06005719 RID: 22297 RVA: 0x002297C6 File Offset: 0x002279C6
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x0600571A RID: 22298 RVA: 0x002297D4 File Offset: 0x002279D4
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.PlaySplashVfx(base.Owner.Creature, this._tint);
			}
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.SelectionScreenPrompt, 0, 999999999);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHand(choiceContext, base.Owner, cardSelectorPrefs, null, this);
			IEnumerable<CardModel> enumerable2 = enumerable;
			foreach (CardModel cardModel in enumerable2)
			{
				await CardCmd.Exhaust(choiceContext, cardModel, false, false);
			}
			IEnumerator<CardModel> enumerator = null;
		}

		// Token: 0x0400227D RID: 8829
		private readonly Color _tint = new Color("83ebdf");
	}
}
