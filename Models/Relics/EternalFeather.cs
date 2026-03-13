using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004F2 RID: 1266
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EternalFeather : RelicModel
	{
		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x06004B3F RID: 19263 RVA: 0x00213783 File Offset: 0x00211983
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x06004B40 RID: 19264 RVA: 0x00213786 File Offset: 0x00211986
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(5),
					new HealVar(3m)
				});
			}
		}

		// Token: 0x06004B41 RID: 19265 RVA: 0x002137AC File Offset: 0x002119AC
		public override async Task AfterRoomEntered(AbstractRoom room)
		{
			if (room is RestSiteRoom)
			{
				base.Flash();
				int num = PileType.Deck.GetPile(base.Owner).Cards.Count / base.DynamicVars.Cards.IntValue;
				decimal healAmount = base.DynamicVars.Heal.BaseValue * num;
				await CreatureCmd.Heal(base.Owner.Creature, healAmount, true);
				if (LocalContext.IsMe(base.Owner))
				{
					PlayerFullscreenHealVfx.Play(base.Owner, healAmount, NRestSiteRoom.Instance);
				}
			}
		}
	}
}
