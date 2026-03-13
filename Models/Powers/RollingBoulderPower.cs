using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.TestSupport;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000687 RID: 1671
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RollingBoulderPower : PowerModel
	{
		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x060054F7 RID: 21751 RVA: 0x002259E7 File Offset: 0x00223BE7
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x060054F8 RID: 21752 RVA: 0x002259EA File Offset: 0x00223BEA
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x060054F9 RID: 21753 RVA: 0x002259ED File Offset: 0x00223BED
		public override bool IsInstanced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x060054FA RID: 21754 RVA: 0x002259F0 File Offset: 0x00223BF0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(5m, ValueProp.Unpowered));
			}
		}

		// Token: 0x060054FB RID: 21755 RVA: 0x00225A04 File Offset: 0x00223C04
		public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Owner.Player)
			{
				base.Flash();
				if (TestMode.IsOn)
				{
					await this.DoDamage(choiceContext, base.CombatState.HittableEnemies);
				}
				else
				{
					List<Task> damageTasks = new List<Task>();
					NRollingBoulderVfx vfx = NRollingBoulderVfx.Create(base.CombatState.HittableEnemies, base.Amount, null);
					vfx.Connect(NRollingBoulderVfx.SignalName.HitCreature, Callable.From<NCreature>(delegate(NCreature c)
					{
						damageTasks.Add(this.DoDamage(choiceContext, new <>z__ReadOnlySingleElementList<Creature>(c.Entity)));
					}), 0U);
					Callable.From(delegate
					{
						NCombatRoom instance = NCombatRoom.Instance;
						if (instance != null)
						{
							instance.CombatVfxContainer.AddChildSafely(vfx);
						}
						if (!vfx.IsInsideTree())
						{
							throw new InvalidOperationException("VFX is not inside tree after adding it to combat room!");
						}
					}).CallDeferred(Array.Empty<Variant>());
					await vfx.ToSignal(vfx, Node.SignalName.TreeExiting);
					await Task.WhenAll(damageTasks);
				}
				base.Amount += base.DynamicVars.Damage.IntValue;
				base.InvokeDisplayAmountChanged();
			}
		}

		// Token: 0x060054FC RID: 21756 RVA: 0x00225A57 File Offset: 0x00223C57
		private Task<IEnumerable<DamageResult>> DoDamage(PlayerChoiceContext choiceContext, IEnumerable<Creature> targets)
		{
			return CreatureCmd.Damage(choiceContext, targets, base.Amount, ValueProp.Unpowered, base.Owner);
		}
	}
}
