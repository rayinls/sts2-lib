using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000654 RID: 1620
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MagicBombPower : PowerModel
	{
		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x060053C2 RID: 21442 RVA: 0x00223714 File Offset: 0x00221914
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x060053C3 RID: 21443 RVA: 0x00223717 File Offset: 0x00221917
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x060053C4 RID: 21444 RVA: 0x0022371A File Offset: 0x0022191A
		public override bool IsInstanced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060053C5 RID: 21445 RVA: 0x00223720 File Offset: 0x00221920
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				if (base.Applier != null && !base.Applier.IsDead)
				{
					base.Flash();
					await Cmd.Wait(0.25f, false);
					await CreatureCmd.Damage(choiceContext, base.Owner, base.Amount, ValueProp.Unpowered, base.Owner, null);
					await PowerCmd.Remove(this);
				}
			}
		}

		// Token: 0x060053C6 RID: 21446 RVA: 0x00223774 File Offset: 0x00221974
		public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (!wasRemovalPrevented)
			{
				if (creature == base.Applier)
				{
					await PowerCmd.Remove(this);
				}
			}
		}
	}
}
