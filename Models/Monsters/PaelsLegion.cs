using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000775 RID: 1909
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PaelsLegion : MonsterModel
	{
		// Token: 0x17001679 RID: 5753
		// (get) Token: 0x06005D34 RID: 23860 RVA: 0x00238DCB File Offset: 0x00236FCB
		public override int MinInitialHp
		{
			get
			{
				return 9999;
			}
		}

		// Token: 0x1700167A RID: 5754
		// (get) Token: 0x06005D35 RID: 23861 RVA: 0x00238DD2 File Offset: 0x00236FD2
		public override int MaxInitialHp
		{
			get
			{
				return 9999;
			}
		}

		// Token: 0x1700167B RID: 5755
		// (get) Token: 0x06005D36 RID: 23862 RVA: 0x00238DD9 File Offset: 0x00236FD9
		public override bool IsHealthBarVisible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005D37 RID: 23863 RVA: 0x00238DDC File Offset: 0x00236FDC
		public override void SetupSkins(NCreatureVisuals visuals)
		{
			string text;
			if (base.IsMutable)
			{
				text = base.Creature.PetOwner.GetRelic<PaelsLegion>().Skin;
			}
			else
			{
				text = PaelsLegion.SkinOptions[0];
			}
			MegaSkeleton skeleton = visuals.SpineBody.GetSkeleton();
			MegaSkeletonDataResource data = skeleton.GetData();
			skeleton.SetSkin(data.FindSkin(text));
			skeleton.SetSlotsToSetupPose();
		}

		// Token: 0x06005D38 RID: 23864 RVA: 0x00238E38 File Offset: 0x00237038
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			MoveState moveState = new MoveState("NOTHING_MOVE", (IReadOnlyList<Creature> _) => Task.CompletedTask, Array.Empty<AbstractIntent>());
			moveState.FollowUpState = moveState;
			return new MonsterMoveStateMachine(new <>z__ReadOnlySingleElementList<MonsterState>(moveState), moveState);
		}

		// Token: 0x06005D39 RID: 23865 RVA: 0x00238E88 File Offset: 0x00237088
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("block", false);
			AnimState animState3 = new AnimState("block_loop", false);
			AnimState animState4 = new AnimState("sleep", false);
			AnimState animState5 = new AnimState("wake_up", false);
			animState5.NextState = animState;
			animState2.NextState = animState3;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Idle", animState, null);
			creatureAnimator.AddAnyState("BlockTrigger", animState2, null);
			creatureAnimator.AddAnyState("SleepTrigger", animState4, null);
			creatureAnimator.AddAnyState("WakeUpTrigger", animState5, null);
			return creatureAnimator;
		}

		// Token: 0x04002382 RID: 9090
		public const string blockTrigger = "BlockTrigger";

		// Token: 0x04002383 RID: 9091
		public const string sleepTrigger = "SleepTrigger";

		// Token: 0x04002384 RID: 9092
		public const string wakeUpTrigger = "WakeUpTrigger";
	}
}
