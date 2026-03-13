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
	// Token: 0x02000733 RID: 1843
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BattleFriendV3 : MonsterModel
	{
		// Token: 0x170014C8 RID: 5320
		// (get) Token: 0x06005934 RID: 22836 RVA: 0x0022CAEC File Offset: 0x0022ACEC
		public override int MinInitialHp
		{
			get
			{
				return 300;
			}
		}

		// Token: 0x170014C9 RID: 5321
		// (get) Token: 0x06005935 RID: 22837 RVA: 0x0022CAF3 File Offset: 0x0022ACF3
		public override int MaxInitialHp
		{
			get
			{
				return 300;
			}
		}

		// Token: 0x06005936 RID: 22838 RVA: 0x0022CAFC File Offset: 0x0022ACFC
		public override void SetupSkins(NCreatureVisuals visuals)
		{
			MegaSkeleton skeleton = visuals.SpineBody.GetSkeleton();
			MegaSkeletonDataResource data = skeleton.GetData();
			skeleton.SetSkin(data.FindSkin("v3"));
			skeleton.SetSlotsToSetupPose();
		}

		// Token: 0x06005937 RID: 22839 RVA: 0x0022CB34 File Offset: 0x0022AD34
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			MoveState moveState = new MoveState("NOTHING_MOVE", (IReadOnlyList<Creature> _) => Task.CompletedTask, Array.Empty<AbstractIntent>());
			moveState.FollowUpState = moveState;
			return new MonsterMoveStateMachine(new <>z__ReadOnlySingleElementList<MonsterState>(moveState), moveState);
		}

		// Token: 0x06005938 RID: 22840 RVA: 0x0022CB84 File Offset: 0x0022AD84
		public override async Task AfterAddedToRoom()
		{
			await PowerCmd.Apply<BattlewornDummyTimeLimitPower>(base.Creature, 3m, null, null, false);
		}

		// Token: 0x06005939 RID: 22841 RVA: 0x0022CBC8 File Offset: 0x0022ADC8
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
