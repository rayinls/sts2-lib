using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;

namespace MegaCrit.Sts2.Core.Models.Monsters.Mocks
{
	// Token: 0x020007A9 RID: 1961
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MockIntangibleMonster : MonsterModel
	{
		// Token: 0x170017E6 RID: 6118
		// (get) Token: 0x060060A1 RID: 24737 RVA: 0x0024318F File Offset: 0x0024138F
		public override LocString Title
		{
			get
			{
				return MonsterModel.L10NMonsterLookup("BIG_DUMMY.name");
			}
		}

		// Token: 0x170017E7 RID: 6119
		// (get) Token: 0x060060A2 RID: 24738 RVA: 0x0024319B File Offset: 0x0024139B
		protected override string VisualsPath
		{
			get
			{
				return SceneHelper.GetScenePath("creature_visuals/defect");
			}
		}

		// Token: 0x170017E8 RID: 6120
		// (get) Token: 0x060060A3 RID: 24739 RVA: 0x002431A7 File Offset: 0x002413A7
		public override int MinInitialHp
		{
			get
			{
				return 9999;
			}
		}

		// Token: 0x170017E9 RID: 6121
		// (get) Token: 0x060060A4 RID: 24740 RVA: 0x002431AE File Offset: 0x002413AE
		public override int MaxInitialHp
		{
			get
			{
				return 9999;
			}
		}

		// Token: 0x060060A5 RID: 24741 RVA: 0x002431B8 File Offset: 0x002413B8
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("INTANGIBLE", new Func<IReadOnlyList<Creature>, Task>(this.IntangibleMove), new AbstractIntent[]
			{
				new HiddenIntent()
			});
			MoveState moveState2 = new MoveState("NOTHING", new Func<IReadOnlyList<Creature>, Task>(this.NothingMove), new AbstractIntent[]
			{
				new HiddenIntent()
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x060060A6 RID: 24742 RVA: 0x00243238 File Offset: 0x00241438
		private async Task IntangibleMove(IReadOnlyList<Creature> targets)
		{
			await PowerCmd.Apply<IntangiblePower>(base.Creature, 2m, base.Creature, null, false);
		}

		// Token: 0x060060A7 RID: 24743 RVA: 0x0024327B File Offset: 0x0024147B
		private Task NothingMove(IReadOnlyList<Creature> targets)
		{
			return Task.CompletedTask;
		}
	}
}
