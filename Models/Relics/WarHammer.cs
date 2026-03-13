using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005BF RID: 1471
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WarHammer : RelicModel
	{
		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x0600508E RID: 20622 RVA: 0x0021D2D4 File Offset: 0x0021B4D4
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x0600508F RID: 20623 RVA: 0x0021D2D7 File Offset: 0x0021B4D7
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(4));
			}
		}

		// Token: 0x06005090 RID: 20624 RVA: 0x0021D2E4 File Offset: 0x0021B4E4
		public override Task AfterCombatVictory(CombatRoom room)
		{
			if (room.RoomType != RoomType.Elite)
			{
				return Task.CompletedTask;
			}
			base.Flash();
			IEnumerable<CardModel> enumerable = PileType.Deck.GetPile(base.Owner).Cards.Where((CardModel c) => c.IsUpgradable).ToList<CardModel>().StableShuffle(base.Owner.RunState.Rng.Niche)
				.Take(base.DynamicVars.Cards.IntValue);
			foreach (CardModel cardModel in enumerable)
			{
				CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
			}
			return Task.CompletedTask;
		}
	}
}
