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
	// Token: 0x02000780 RID: 1920
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SingleAttackMoveMonster : MonsterModel
	{
		// Token: 0x170016CA RID: 5834
		// (get) Token: 0x06005DF4 RID: 24052 RVA: 0x0023B16F File Offset: 0x0023936F
		public override LocString Title
		{
			get
			{
				return MonsterModel.L10NMonsterLookup("BIG_DUMMY.name");
			}
		}

		// Token: 0x170016CB RID: 5835
		// (get) Token: 0x06005DF5 RID: 24053 RVA: 0x0023B17B File Offset: 0x0023937B
		public override int MinInitialHp
		{
			get
			{
				return 999;
			}
		}

		// Token: 0x170016CC RID: 5836
		// (get) Token: 0x06005DF6 RID: 24054 RVA: 0x0023B182 File Offset: 0x00239382
		public override int MaxInitialHp
		{
			get
			{
				return 999;
			}
		}

		// Token: 0x06005DF7 RID: 24055 RVA: 0x0023B18C File Offset: 0x0023938C
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("POKE", new Func<IReadOnlyList<Creature>, Task>(this.PokeMove), new AbstractIntent[]
			{
				new SingleAttackIntent(1)
			});
			moveState.FollowUpState = moveState;
			list.Add(moveState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005DF8 RID: 24056 RVA: 0x0023B1DC File Offset: 0x002393DC
		private async Task PokeMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(1m).FromMonster(this).Execute(null);
		}
	}
}
