using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200078D RID: 1933
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TenHpMonster : MonsterModel
	{
		// Token: 0x1700171F RID: 5919
		// (get) Token: 0x06005EB4 RID: 24244 RVA: 0x0023D57F File Offset: 0x0023B77F
		public override LocString Title
		{
			get
			{
				return MonsterModel.L10NMonsterLookup("BIG_DUMMY.name");
			}
		}

		// Token: 0x17001720 RID: 5920
		// (get) Token: 0x06005EB5 RID: 24245 RVA: 0x0023D58B File Offset: 0x0023B78B
		public override int MinInitialHp
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17001721 RID: 5921
		// (get) Token: 0x06005EB6 RID: 24246 RVA: 0x0023D58F File Offset: 0x0023B78F
		public override int MaxInitialHp
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x06005EB7 RID: 24247 RVA: 0x0023D594 File Offset: 0x0023B794
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("NOTHING", new Func<IReadOnlyList<Creature>, Task>(this.NothingMove), new AbstractIntent[]
			{
				new HiddenIntent()
			});
			moveState.FollowUpState = moveState;
			list.Add(moveState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005EB8 RID: 24248 RVA: 0x0023D5E1 File Offset: 0x0023B7E1
		private Task NothingMove(IReadOnlyList<Creature> targets)
		{
			return Task.CompletedTask;
		}
	}
}
