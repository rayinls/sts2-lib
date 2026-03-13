using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200076C RID: 1900
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MultiAttackMoveMonster : MonsterModel
	{
		// Token: 0x17001647 RID: 5703
		// (get) Token: 0x06005CC8 RID: 23752 RVA: 0x00237A0E File Offset: 0x00235C0E
		public override LocString Title
		{
			get
			{
				return MonsterModel.L10NMonsterLookup("BIG_DUMMY.name");
			}
		}

		// Token: 0x17001648 RID: 5704
		// (get) Token: 0x06005CC9 RID: 23753 RVA: 0x00237A1A File Offset: 0x00235C1A
		public override int MinInitialHp
		{
			get
			{
				return 999;
			}
		}

		// Token: 0x17001649 RID: 5705
		// (get) Token: 0x06005CCA RID: 23754 RVA: 0x00237A21 File Offset: 0x00235C21
		public override int MaxInitialHp
		{
			get
			{
				return 999;
			}
		}

		// Token: 0x06005CCB RID: 23755 RVA: 0x00237A28 File Offset: 0x00235C28
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("POKE", new Func<IReadOnlyList<Creature>, Task>(this.PokeMove), new AbstractIntent[]
			{
				new MultiAttackIntent(1, 5)
			});
			moveState.FollowUpState = moveState;
			list.Add(moveState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005CCC RID: 23756 RVA: 0x00237A78 File Offset: 0x00235C78
		private async Task PokeMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(1m).WithHitCount(5).FromMonster(this)
				.Execute(null);
		}

		// Token: 0x0400236B RID: 9067
		public const int repeat = 5;
	}
}
