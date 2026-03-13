using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000628 RID: 1576
	public sealed class ForegoneConclusionPower : PowerModel
	{
		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x060052C7 RID: 21191 RVA: 0x002219B8 File Offset: 0x0021FBB8
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x060052C8 RID: 21192 RVA: 0x002219BB File Offset: 0x0021FBBB
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060052C9 RID: 21193 RVA: 0x002219C0 File Offset: 0x0021FBC0
		[NullableContext(1)]
		public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
		{
			if (player == base.Owner.Player)
			{
				await CardPileCmd.ShuffleIfNecessary(choiceContext, base.Owner.Player);
				IEnumerable<CardModel> enumerable = await CardSelectCmd.FromSimpleGrid(choiceContext, (from c in PileType.Draw.GetPile(base.Owner.Player).Cards
					orderby c.Rarity, c.Id
					select c).ToList<CardModel>(), base.Owner.Player, new CardSelectorPrefs(base.SelectionScreenPrompt, base.Amount));
				await CardPileCmd.Add(enumerable, PileType.Hand, CardPilePosition.Bottom, null, false);
				await PowerCmd.Remove(this);
			}
		}
	}
}
