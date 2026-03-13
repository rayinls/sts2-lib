using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;

namespace MegaCrit.Sts2.Core.Models.Monsters.Mocks
{
	// Token: 0x020007A8 RID: 1960
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MockAttackMonster : MonsterModel
	{
		// Token: 0x170017E3 RID: 6115
		// (get) Token: 0x0600609B RID: 24731 RVA: 0x002430D7 File Offset: 0x002412D7
		protected override string VisualsPath
		{
			get
			{
				return SceneHelper.GetScenePath("creature_visuals/defect");
			}
		}

		// Token: 0x170017E4 RID: 6116
		// (get) Token: 0x0600609C RID: 24732 RVA: 0x002430E3 File Offset: 0x002412E3
		public override int MinInitialHp
		{
			get
			{
				return 9999;
			}
		}

		// Token: 0x170017E5 RID: 6117
		// (get) Token: 0x0600609D RID: 24733 RVA: 0x002430EA File Offset: 0x002412EA
		public override int MaxInitialHp
		{
			get
			{
				return 9999;
			}
		}

		// Token: 0x0600609E RID: 24734 RVA: 0x002430F4 File Offset: 0x002412F4
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("ATTACK", new Func<IReadOnlyList<Creature>, Task>(this.AttackMove), new AbstractIntent[]
			{
				new SingleAttackIntent(1)
			});
			moveState.FollowUpState = moveState;
			list.Add(moveState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x0600609F RID: 24735 RVA: 0x00243144 File Offset: 0x00241344
		private async Task AttackMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(1m).FromMonster(this).Execute(null);
		}
	}
}
