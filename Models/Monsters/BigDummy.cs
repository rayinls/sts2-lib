using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000734 RID: 1844
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BigDummy : MonsterModel
	{
		// Token: 0x170014CA RID: 5322
		// (get) Token: 0x0600593B RID: 22843 RVA: 0x0022CC50 File Offset: 0x0022AE50
		public override LocString Title
		{
			get
			{
				return MonsterModel.L10NMonsterLookup("BIG_DUMMY.name");
			}
		}

		// Token: 0x170014CB RID: 5323
		// (get) Token: 0x0600593C RID: 22844 RVA: 0x0022CC5C File Offset: 0x0022AE5C
		protected override string VisualsPath
		{
			get
			{
				return SceneHelper.GetScenePath("creature_visuals/defect");
			}
		}

		// Token: 0x170014CC RID: 5324
		// (get) Token: 0x0600593D RID: 22845 RVA: 0x0022CC68 File Offset: 0x0022AE68
		public override int MinInitialHp
		{
			get
			{
				return 9999;
			}
		}

		// Token: 0x170014CD RID: 5325
		// (get) Token: 0x0600593E RID: 22846 RVA: 0x0022CC6F File Offset: 0x0022AE6F
		public override int MaxInitialHp
		{
			get
			{
				return 9999;
			}
		}

		// Token: 0x0600593F RID: 22847 RVA: 0x0022CC78 File Offset: 0x0022AE78
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

		// Token: 0x06005940 RID: 22848 RVA: 0x0022CCC5 File Offset: 0x0022AEC5
		private Task NothingMove(IReadOnlyList<Creature> targets)
		{
			return Task.CompletedTask;
		}
	}
}
