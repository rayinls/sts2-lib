using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200072D RID: 1837
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Architect : MonsterModel
	{
		// Token: 0x170014B1 RID: 5297
		// (get) Token: 0x060058FF RID: 22783 RVA: 0x0022C0B7 File Offset: 0x0022A2B7
		public override int MinInitialHp
		{
			get
			{
				return 9999;
			}
		}

		// Token: 0x170014B2 RID: 5298
		// (get) Token: 0x06005900 RID: 22784 RVA: 0x0022C0BE File Offset: 0x0022A2BE
		public override int MaxInitialHp
		{
			get
			{
				return 9999;
			}
		}

		// Token: 0x06005901 RID: 22785 RVA: 0x0022C0C8 File Offset: 0x0022A2C8
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("NOTHING", (IReadOnlyList<Creature> _) => Task.CompletedTask, new AbstractIntent[]
			{
				new HiddenIntent()
			});
			moveState.FollowUpState = moveState;
			list.Add(moveState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005902 RID: 22786 RVA: 0x0022C128 File Offset: 0x0022A328
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("attack", false);
			AnimState animState3 = new AnimState("hurt", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Idle", animState, null);
			creatureAnimator.AddAnyState("Attack", animState2, null);
			creatureAnimator.AddAnyState("Hit", animState3, null);
			return creatureAnimator;
		}
	}
}
