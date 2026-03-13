using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
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
	// Token: 0x02000599 RID: 1433
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class StoneCracker : RelicModel
	{
		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x06004F98 RID: 20376 RVA: 0x0021B6AF File Offset: 0x002198AF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x06004F99 RID: 20377 RVA: 0x0021B6B2 File Offset: 0x002198B2
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x06004F9A RID: 20378 RVA: 0x0021B6C0 File Offset: 0x002198C0
		public override async Task AfterRoomEntered(AbstractRoom room)
		{
			if (room.RoomType == RoomType.Boss)
			{
				base.Flash();
				List<CardModel> list = PileType.Draw.GetPile(base.Owner).Cards.Where((CardModel c) => c.IsUpgradable).ToList<CardModel>().StableShuffle(base.Owner.RunState.Rng.CombatCardSelection)
					.Take(base.DynamicVars.Cards.IntValue)
					.ToList<CardModel>();
				CardCmd.Upgrade(list, CardPreviewStyle.HorizontalLayout);
				CardCmd.Preview(list, 1.2f, CardPreviewStyle.HorizontalLayout);
				await Cmd.CustomScaledWait(0.5f, 1f, false, default(CancellationToken));
			}
		}
	}
}
