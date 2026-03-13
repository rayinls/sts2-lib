using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005F6 RID: 1526
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CorrosiveWavePower : PowerModel
	{
		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x060051B1 RID: 20913 RVA: 0x0021FCDE File Offset: 0x0021DEDE
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x060051B2 RID: 20914 RVA: 0x0021FCE1 File Offset: 0x0021DEE1
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x060051B3 RID: 20915 RVA: 0x0021FCE4 File Offset: 0x0021DEE4
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<PoisonPower>());
			}
		}

		// Token: 0x060051B4 RID: 20916 RVA: 0x0021FCF0 File Offset: 0x0021DEF0
		public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
		{
			if (card.Owner.Creature == base.Owner)
			{
				base.Flash();
				foreach (Creature creature in base.CombatState.HittableEnemies)
				{
					NCombatRoom instance = NCombatRoom.Instance;
					NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(creature) : null);
					if (ncreature != null)
					{
						NGaseousImpactVfx ngaseousImpactVfx = NGaseousImpactVfx.Create(ncreature.VfxSpawnPosition, new Color("83eb85"));
						NCombatRoom.Instance.CombatVfxContainer.AddChildSafely(ngaseousImpactVfx);
					}
				}
				await PowerCmd.Apply<PoisonPower>(base.CombatState.HittableEnemies, base.Amount, base.Owner, null, false);
			}
		}

		// Token: 0x060051B5 RID: 20917 RVA: 0x0021FD3C File Offset: 0x0021DF3C
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				await PowerCmd.Remove(this);
			}
		}
	}
}
