using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x020006FD RID: 1789
	public sealed class GamblersBrew : PotionModel
	{
		// Token: 0x170013F5 RID: 5109
		// (get) Token: 0x060057D0 RID: 22480 RVA: 0x0022A643 File Offset: 0x00228843
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Uncommon;
			}
		}

		// Token: 0x170013F6 RID: 5110
		// (get) Token: 0x060057D1 RID: 22481 RVA: 0x0022A646 File Offset: 0x00228846
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x170013F7 RID: 5111
		// (get) Token: 0x060057D2 RID: 22482 RVA: 0x0022A649 File Offset: 0x00228849
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x060057D3 RID: 22483 RVA: 0x0022A64C File Offset: 0x0022884C
		[NullableContext(1)]
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.PlaySplashVfx(base.Owner.Creature, new Color("a19f91"));
			}
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHandForDiscard(choiceContext, base.Owner, new CardSelectorPrefs(base.SelectionScreenPrompt, 0, 999999999), null, this);
			List<CardModel> list = enumerable.ToList<CardModel>();
			await CardCmd.DiscardAndDraw(choiceContext, list, list.Count);
		}
	}
}
