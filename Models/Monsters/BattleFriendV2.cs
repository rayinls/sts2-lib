using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000732 RID: 1842
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BattleFriendV2 : MonsterModel
	{
		// Token: 0x170014C6 RID: 5318
		// (get) Token: 0x0600592D RID: 22829 RVA: 0x0022C988 File Offset: 0x0022AB88
		public override int MinInitialHp
		{
			get
			{
				return 150;
			}
		}

		// Token: 0x170014C7 RID: 5319
		// (get) Token: 0x0600592E RID: 22830 RVA: 0x0022C98F File Offset: 0x0022AB8F
		public override int MaxInitialHp
		{
			get
			{
				return 150;
			}
		}

		// Token: 0x0600592F RID: 22831 RVA: 0x0022C998 File Offset: 0x0022AB98
		public override void SetupSkins(NCreatureVisuals visuals)
		{
			MegaSkeleton skeleton = visuals.SpineBody.GetSkeleton();
			MegaSkeletonDataResource data = skeleton.GetData();
			skeleton.SetSkin(data.FindSkin("v2"));
			skeleton.SetSlotsToSetupPose();
		}

		// Token: 0x06005930 RID: 22832 RVA: 0x0022C9D0 File Offset: 0x0022ABD0
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			MoveState moveState = new MoveState("NOTHING_MOVE", (IReadOnlyList<Creature> _) => Task.CompletedTask, Array.Empty<AbstractIntent>());
			moveState.FollowUpState = moveState;
			return new MonsterMoveStateMachine(new <>z__ReadOnlySingleElementList<MonsterState>(moveState), moveState);
		}

		// Token: 0x06005931 RID: 22833 RVA: 0x0022CA20 File Offset: 0x0022AC20
		public override async Task AfterAddedToRoom()
		{
			await PowerCmd.Apply<BattlewornDummyTimeLimitPower>(base.Creature, 3m, null, null, false);
		}

		// Token: 0x06005932 RID: 22834 RVA: 0x0022CA64 File Offset: 0x0022AC64
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("hurt", false);
			AnimState animState3 = new AnimState("die", false);
			AnimState animState4 = new AnimState("die_loop", true);
			animState2.NextState = animState;
			animState3.NextState = animState4;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Idle", animState, null);
			creatureAnimator.AddAnyState("Dead", animState3, null);
			creatureAnimator.AddAnyState("Hit", animState2, null);
			return creatureAnimator;
		}
	}
}
