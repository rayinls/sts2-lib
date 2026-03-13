using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000664 RID: 1636
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NoxiousFumesPower : PowerModel
	{
		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x06005417 RID: 21527 RVA: 0x00223F1C File Offset: 0x0022211C
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06005418 RID: 21528 RVA: 0x00223F1F File Offset: 0x0022211F
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06005419 RID: 21529 RVA: 0x00223F22 File Offset: 0x00222122
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<PoisonPower>());
			}
		}

		// Token: 0x0600541A RID: 21530 RVA: 0x00223F30 File Offset: 0x00222130
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Side)
			{
				base.Flash();
				await Cmd.CustomScaledWait(0.2f, 0.4f, false, default(CancellationToken));
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
	}
}
