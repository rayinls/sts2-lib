using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200067F RID: 1663
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ReattachPower : PowerModel
	{
		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x060054BF RID: 21695 RVA: 0x002251DD File Offset: 0x002233DD
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x060054C0 RID: 21696 RVA: 0x002251E0 File Offset: 0x002233E0
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x060054C1 RID: 21697 RVA: 0x002251E3 File Offset: 0x002233E3
		public override bool ShouldScaleInMultiplayer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060054C2 RID: 21698 RVA: 0x002251E6 File Offset: 0x002233E6
		protected override object InitInternalData()
		{
			return new ReattachPower.Data();
		}

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x060054C3 RID: 21699 RVA: 0x002251ED File Offset: 0x002233ED
		private bool IsReviving
		{
			get
			{
				return base.GetInternalData<ReattachPower.Data>().isReviving;
			}
		}

		// Token: 0x060054C4 RID: 21700 RVA: 0x002251FC File Offset: 0x002233FC
		public async Task DoReattach()
		{
			if (!this.AreAllOtherSegmentsDead())
			{
				NCombatRoom instance = NCombatRoom.Instance;
				NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(base.Owner) : null);
				if (ncreature != null)
				{
					NDecimillipedeSegmentVfx specialNode = ncreature.GetSpecialNode<NDecimillipedeSegmentVfx>("%NDecimillipedeSegmentVfx");
					if (specialNode != null)
					{
						specialNode.Regenerate();
					}
				}
				base.GetInternalData<ReattachPower.Data>().isReviving = false;
				NCombatRoom instance2 = NCombatRoom.Instance;
				if (instance2 != null)
				{
					instance2.SetCreatureIsInteractable(base.Owner, true);
				}
				await CreatureCmd.Heal(base.Owner, base.Amount, true);
			}
		}

		// Token: 0x060054C5 RID: 21701 RVA: 0x00225240 File Offset: 0x00223440
		public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (!wasRemovalPrevented)
			{
				if (base.Owner == creature)
				{
					if (!this.AreAllOtherSegmentsDead() || !base.Owner.IsDead)
					{
						base.GetInternalData<ReattachPower.Data>().isReviving = true;
						DecimillipedeSegment decimillipedeSegment = creature.Monster as DecimillipedeSegment;
						if (decimillipedeSegment != null)
						{
							base.Owner.Monster.SetMoveImmediate(decimillipedeSegment.DeadState, false);
						}
						NCombatRoom instance = NCombatRoom.Instance;
						if (instance != null)
						{
							instance.SetCreatureIsInteractable(base.Owner, false);
						}
					}
					else
					{
						await Cmd.Wait(0.25f, true);
						this.DoFadeOutOnAllSegments();
					}
				}
			}
		}

		// Token: 0x060054C6 RID: 21702 RVA: 0x00225293 File Offset: 0x00223493
		public override bool ShouldAllowHitting(Creature creature)
		{
			return creature != base.Owner || !this.IsReviving;
		}

		// Token: 0x060054C7 RID: 21703 RVA: 0x002252AB File Offset: 0x002234AB
		public override bool ShouldCreatureBeRemovedFromCombatAfterDeath(Creature creature)
		{
			return creature != base.Owner;
		}

		// Token: 0x060054C8 RID: 21704 RVA: 0x002252B9 File Offset: 0x002234B9
		public override bool ShouldPowerBeRemovedAfterOwnerDeath()
		{
			return false;
		}

		// Token: 0x060054C9 RID: 21705 RVA: 0x002252BC File Offset: 0x002234BC
		public override bool ShouldOwnerDeathTriggerFatal()
		{
			return this.AreAllOtherSegmentsDead();
		}

		// Token: 0x060054CA RID: 21706 RVA: 0x002252C4 File Offset: 0x002234C4
		private void DoFadeOutOnAllSegments()
		{
			float num = 0f;
			List<NCreature> list = new List<NCreature>();
			foreach (Creature creature in base.CombatState.Enemies)
			{
				NCombatRoom instance = NCombatRoom.Instance;
				NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(creature) : null);
				if (ncreature != null)
				{
					ncreature.AnimHideIntent(0f);
					num = Math.Max(num, ncreature.GetCurrentAnimationLength());
					list.Add(ncreature);
				}
			}
			NMonsterDeathVfx nmonsterDeathVfx = NMonsterDeathVfx.Create(list);
			if (nmonsterDeathVfx != null && list.Count > 0)
			{
				Node parent = list[0].GetParent();
				parent.AddChildSafely(nmonsterDeathVfx);
				parent.MoveChild(nmonsterDeathVfx, list[0].GetIndex(false));
				Task task = TaskHelper.RunSafely(this.PlayVfxAndThenRemoveNodes(nmonsterDeathVfx, list));
				foreach (NCreature ncreature2 in list)
				{
					ncreature2.DeathAnimationTask = task;
					NCombatRoom instance2 = NCombatRoom.Instance;
					if (instance2 != null)
					{
						instance2.RemoveCreatureNode(ncreature2);
					}
				}
			}
		}

		// Token: 0x060054CB RID: 21707 RVA: 0x00225400 File Offset: 0x00223600
		private async Task PlayVfxAndThenRemoveNodes(NMonsterDeathVfx vfx, List<NCreature> creatures)
		{
			await Cmd.Wait(0.25f, true);
			await vfx.PlayVfx();
			foreach (NCreature ncreature in creatures)
			{
				ncreature.QueueFreeSafely();
			}
		}

		// Token: 0x060054CC RID: 21708 RVA: 0x0022544C File Offset: 0x0022364C
		private IEnumerable<Creature> GetOtherSegments()
		{
			return from c in base.Owner.CombatState.GetTeammatesOf(base.Owner).Except(new <>z__ReadOnlySingleElementList<Creature>(base.Owner))
				where c.HasPower<ReattachPower>()
				select c;
		}

		// Token: 0x060054CD RID: 21709 RVA: 0x002254A3 File Offset: 0x002236A3
		private bool AreAllOtherSegmentsDead()
		{
			return this.GetOtherSegments().All((Creature s) => s.IsDead);
		}

		// Token: 0x02001A64 RID: 6756
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x040067B8 RID: 26552
			public bool isReviving;
		}
	}
}
