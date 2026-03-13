using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;

namespace MegaCrit.Sts2.Core.Models.Monsters.Mocks
{
	// Token: 0x020007A6 RID: 1958
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MockArtifactMonster : MonsterModel
	{
		// Token: 0x170017DC RID: 6108
		// (get) Token: 0x0600608C RID: 24716 RVA: 0x00242F53 File Offset: 0x00241153
		public override LocString Title
		{
			get
			{
				return MonsterModel.L10NMonsterLookup("BIG_DUMMY.name");
			}
		}

		// Token: 0x170017DD RID: 6109
		// (get) Token: 0x0600608D RID: 24717 RVA: 0x00242F5F File Offset: 0x0024115F
		protected override string VisualsPath
		{
			get
			{
				return SceneHelper.GetScenePath("creature_visuals/defect");
			}
		}

		// Token: 0x170017DE RID: 6110
		// (get) Token: 0x0600608E RID: 24718 RVA: 0x00242F6B File Offset: 0x0024116B
		public override int MinInitialHp
		{
			get
			{
				return 9999;
			}
		}

		// Token: 0x170017DF RID: 6111
		// (get) Token: 0x0600608F RID: 24719 RVA: 0x00242F72 File Offset: 0x00241172
		public override int MaxInitialHp
		{
			get
			{
				return 9999;
			}
		}

		// Token: 0x06006090 RID: 24720 RVA: 0x00242F7C File Offset: 0x0024117C
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("NOTHING", new Func<IReadOnlyList<Creature>, Task>(this.NothingMove), new AbstractIntent[]
			{
				new HiddenIntent()
			});
			moveState.FollowUpState = moveState;
			list.Add(moveState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06006091 RID: 24721 RVA: 0x00242FCC File Offset: 0x002411CC
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<ArtifactPower>(base.Creature, 1m, base.Creature, null, false);
		}

		// Token: 0x06006092 RID: 24722 RVA: 0x0024300F File Offset: 0x0024120F
		private Task NothingMove(IReadOnlyList<Creature> targets)
		{
			return Task.CompletedTask;
		}
	}
}
