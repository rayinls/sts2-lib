using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;

namespace MegaCrit.Sts2.Core.Models.Monsters.Mocks
{
	// Token: 0x020007A7 RID: 1959
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MockAttackAndSummonMinionMonster : MonsterModel
	{
		// Token: 0x170017E0 RID: 6112
		// (get) Token: 0x06006095 RID: 24725 RVA: 0x00243026 File Offset: 0x00241226
		protected override string VisualsPath
		{
			get
			{
				return SceneHelper.GetScenePath("creature_visuals/defect");
			}
		}

		// Token: 0x170017E1 RID: 6113
		// (get) Token: 0x06006096 RID: 24726 RVA: 0x00243032 File Offset: 0x00241232
		public override int MinInitialHp
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170017E2 RID: 6114
		// (get) Token: 0x06006097 RID: 24727 RVA: 0x00243036 File Offset: 0x00241236
		public override int MaxInitialHp
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x06006098 RID: 24728 RVA: 0x0024303C File Offset: 0x0024123C
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("ATTACK_AND_SUMMON_MINION", new Func<IReadOnlyList<Creature>, Task>(this.AttackAndSummonMinionMove), new AbstractIntent[]
			{
				new SingleAttackIntent(1)
			});
			moveState.FollowUpState = moveState;
			list.Add(moveState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06006099 RID: 24729 RVA: 0x0024308C File Offset: 0x0024128C
		private async Task AttackAndSummonMinionMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(1m).FromMonster(this).Execute(null);
			Creature creature = await CreatureCmd.Add<BigDummy>(base.CombatState, null);
			await PowerCmd.Apply<MinionPower>(creature, 1m, base.Creature, null, false);
		}
	}
}
