using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200073C RID: 1852
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Byrdpip : MonsterModel
	{
		// Token: 0x170014F5 RID: 5365
		// (get) Token: 0x0600599D RID: 22941 RVA: 0x0022DDC8 File Offset: 0x0022BFC8
		public override int MinInitialHp
		{
			get
			{
				return 9999;
			}
		}

		// Token: 0x170014F6 RID: 5366
		// (get) Token: 0x0600599E RID: 22942 RVA: 0x0022DDCF File Offset: 0x0022BFCF
		public override int MaxInitialHp
		{
			get
			{
				return 9999;
			}
		}

		// Token: 0x170014F7 RID: 5367
		// (get) Token: 0x0600599F RID: 22943 RVA: 0x0022DDD6 File Offset: 0x0022BFD6
		public override bool IsHealthBarVisible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060059A0 RID: 22944 RVA: 0x0022DDDC File Offset: 0x0022BFDC
		public override void SetupSkins(NCreatureVisuals visuals)
		{
			string text;
			if (base.IsMutable)
			{
				text = base.Creature.PetOwner.GetRelic<Byrdpip>().Skin;
			}
			else
			{
				text = Byrdpip.SkinOptions[0];
			}
			MegaSkeleton skeleton = visuals.SpineBody.GetSkeleton();
			MegaSkeletonDataResource data = skeleton.GetData();
			skeleton.SetSkin(data.FindSkin(text));
			skeleton.SetSlotsToSetupPose();
		}

		// Token: 0x060059A1 RID: 22945 RVA: 0x0022DE38 File Offset: 0x0022C038
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			MoveState moveState = new MoveState("NOTHING_MOVE", (IReadOnlyList<Creature> _) => Task.CompletedTask, Array.Empty<AbstractIntent>());
			moveState.FollowUpState = moveState;
			return new MonsterMoveStateMachine(new <>z__ReadOnlySingleElementList<MonsterState>(moveState), moveState);
		}
	}
}
