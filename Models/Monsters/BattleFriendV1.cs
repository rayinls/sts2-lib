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
	// Token: 0x02000731 RID: 1841
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BattleFriendV1 : MonsterModel
	{
		// Token: 0x170014C4 RID: 5316
		// (get) Token: 0x06005926 RID: 22822 RVA: 0x0022C82B File Offset: 0x0022AA2B
		public override int MinInitialHp
		{
			get
			{
				return 75;
			}
		}

		// Token: 0x170014C5 RID: 5317
		// (get) Token: 0x06005927 RID: 22823 RVA: 0x0022C82F File Offset: 0x0022AA2F
		public override int MaxInitialHp
		{
			get
			{
				return 75;
			}
		}

		// Token: 0x06005928 RID: 22824 RVA: 0x0022C834 File Offset: 0x0022AA34
		public override void SetupSkins(NCreatureVisuals visuals)
		{
			MegaSkeleton skeleton = visuals.SpineBody.GetSkeleton();
			MegaSkeletonDataResource data = skeleton.GetData();
			skeleton.SetSkin(data.FindSkin("v1"));
			skeleton.SetSlotsToSetupPose();
		}

		// Token: 0x06005929 RID: 22825 RVA: 0x0022C86C File Offset: 0x0022AA6C
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			MoveState moveState = new MoveState("NOTHING_MOVE", (IReadOnlyList<Creature> _) => Task.CompletedTask, Array.Empty<AbstractIntent>());
			moveState.FollowUpState = moveState;
			return new MonsterMoveStateMachine(new <>z__ReadOnlySingleElementList<MonsterState>(moveState), moveState);
		}

		// Token: 0x0600592A RID: 22826 RVA: 0x0022C8BC File Offset: 0x0022AABC
		public override async Task AfterAddedToRoom()
		{
			await PowerCmd.Apply<BattlewornDummyTimeLimitPower>(base.Creature, 3m, null, null, false);
		}

		// Token: 0x0600592B RID: 22827 RVA: 0x0022C900 File Offset: 0x0022AB00
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
