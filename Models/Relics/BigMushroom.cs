using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004B6 RID: 1206
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BigMushroom : RelicModel
	{
		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x060049E9 RID: 18921 RVA: 0x00210F57 File Offset: 0x0020F157
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x060049EA RID: 18922 RVA: 0x00210F5A File Offset: 0x0020F15A
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x060049EB RID: 18923 RVA: 0x00210F5D File Offset: 0x0020F15D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new MaxHpVar(20m),
					new CardsVar(2)
				});
			}
		}

		// Token: 0x060049EC RID: 18924 RVA: 0x00210F84 File Offset: 0x0020F184
		public override async Task AfterObtained()
		{
			await CreatureCmd.GainMaxHp(base.Owner.Creature, base.DynamicVars.MaxHp.BaseValue);
			this.Grow();
		}

		// Token: 0x060049ED RID: 18925 RVA: 0x00210FC7 File Offset: 0x0020F1C7
		public override Task AfterRoomEntered(AbstractRoom _)
		{
			this.Grow();
			return Task.CompletedTask;
		}

		// Token: 0x060049EE RID: 18926 RVA: 0x00210FD4 File Offset: 0x0020F1D4
		public override decimal ModifyHandDraw(Player player, decimal cardsToDraw)
		{
			if (player != base.Owner)
			{
				return cardsToDraw;
			}
			if (player.Creature.CombatState.RoundNumber != 1)
			{
				return cardsToDraw;
			}
			return cardsToDraw - base.DynamicVars.Cards.IntValue;
		}

		// Token: 0x060049EF RID: 18927 RVA: 0x00211011 File Offset: 0x0020F211
		private void Grow()
		{
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance == null)
			{
				return;
			}
			NCreature creatureNode = instance.GetCreatureNode(base.Owner.Creature);
			if (creatureNode == null)
			{
				return;
			}
			creatureNode.ScaleTo(1.5f, 0f);
		}
	}
}
