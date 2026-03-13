using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000645 RID: 1605
	public sealed class ImprovementPower : PowerModel
	{
		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x0600536D RID: 21357 RVA: 0x00222C7B File Offset: 0x00220E7B
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x0600536E RID: 21358 RVA: 0x00222C7E File Offset: 0x00220E7E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x0600536F RID: 21359 RVA: 0x00222C84 File Offset: 0x00220E84
		[NullableContext(1)]
		public override Task AfterCombatEnd(CombatRoom room)
		{
			List<CardModel> list = PileType.Deck.GetPile(base.Owner.Player).Cards.Where((CardModel c) => c.IsUpgradable).ToList<CardModel>();
			int num = 0;
			while (num < base.Amount && list.Count != 0)
			{
				CardModel cardModel = base.Owner.Player.RunState.Rng.CombatCardSelection.NextItem<CardModel>(list);
				list.Remove(cardModel);
				CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
				num++;
			}
			return Task.CompletedTask;
		}
	}
}
