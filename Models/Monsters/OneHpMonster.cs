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
	// Token: 0x02000771 RID: 1905
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class OneHpMonster : MonsterModel
	{
		// Token: 0x1700165C RID: 5724
		// (get) Token: 0x06005CFA RID: 23802 RVA: 0x0023827B File Offset: 0x0023647B
		public override LocString Title
		{
			get
			{
				return MonsterModel.L10NMonsterLookup("BIG_DUMMY.name");
			}
		}

		// Token: 0x1700165D RID: 5725
		// (get) Token: 0x06005CFB RID: 23803 RVA: 0x00238287 File Offset: 0x00236487
		public override int MinInitialHp
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700165E RID: 5726
		// (get) Token: 0x06005CFC RID: 23804 RVA: 0x0023828A File Offset: 0x0023648A
		public override int MaxInitialHp
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06005CFD RID: 23805 RVA: 0x00238290 File Offset: 0x00236490
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

		// Token: 0x06005CFE RID: 23806 RVA: 0x002382DD File Offset: 0x002364DD
		private Task NothingMove(IReadOnlyList<Creature> targets)
		{
			return Task.CompletedTask;
		}
	}
}
