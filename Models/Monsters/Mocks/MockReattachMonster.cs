using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;

namespace MegaCrit.Sts2.Core.Models.Monsters.Mocks
{
	// Token: 0x020007AB RID: 1963
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MockReattachMonster : MonsterModel
	{
		// Token: 0x170017EF RID: 6127
		// (get) Token: 0x060060B4 RID: 24756 RVA: 0x00243372 File Offset: 0x00241572
		public override LocString Title
		{
			get
			{
				return MonsterModel.L10NMonsterLookup("BIG_DUMMY.name");
			}
		}

		// Token: 0x170017F0 RID: 6128
		// (get) Token: 0x060060B5 RID: 24757 RVA: 0x0024337E File Offset: 0x0024157E
		public override int MinInitialHp
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170017F1 RID: 6129
		// (get) Token: 0x060060B6 RID: 24758 RVA: 0x00243381 File Offset: 0x00241581
		public override int MaxInitialHp
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170017F2 RID: 6130
		// (get) Token: 0x060060B7 RID: 24759 RVA: 0x00243384 File Offset: 0x00241584
		// (set) Token: 0x060060B8 RID: 24760 RVA: 0x0024338C File Offset: 0x0024158C
		public MoveState DeadState
		{
			get
			{
				return this._deadState;
			}
			private set
			{
				base.AssertMutable();
				this._deadState = value;
			}
		}

		// Token: 0x060060B9 RID: 24761 RVA: 0x0024339C File Offset: 0x0024159C
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<ReattachPower>(base.Creature, 1m, base.Creature, null, false);
		}

		// Token: 0x060060BA RID: 24762 RVA: 0x002433E0 File Offset: 0x002415E0
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("NOTHING", new Func<IReadOnlyList<Creature>, Task>(this.NothingMove), new AbstractIntent[]
			{
				new HiddenIntent()
			});
			moveState.FollowUpState = moveState;
			MoveState moveState2 = new MoveState("REATTACH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ReattachMove), new AbstractIntent[]
			{
				new HealIntent()
			})
			{
				MustPerformOnceBeforeTransitioning = true,
				FollowUpState = moveState
			};
			this.DeadState = new MoveState("DEAD_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.NothingMove), Array.Empty<AbstractIntent>())
			{
				FollowUpState = moveState2
			};
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(this.DeadState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x060060BB RID: 24763 RVA: 0x0024349B File Offset: 0x0024169B
		private Task NothingMove(IReadOnlyList<Creature> targets)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060060BC RID: 24764 RVA: 0x002434A4 File Offset: 0x002416A4
		private async Task ReattachMove(IReadOnlyList<Creature> targets)
		{
			await base.Creature.GetPower<ReattachPower>().DoReattach();
		}

		// Token: 0x04002465 RID: 9317
		private MoveState _deadState;
	}
}
