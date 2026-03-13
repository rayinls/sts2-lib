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
	// Token: 0x020007AA RID: 1962
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MockPlatingMonster : MonsterModel
	{
		// Token: 0x170017EA RID: 6122
		// (get) Token: 0x060060A9 RID: 24745 RVA: 0x0024328A File Offset: 0x0024148A
		public override LocString Title
		{
			get
			{
				return MonsterModel.L10NMonsterLookup("BIG_DUMMY.name");
			}
		}

		// Token: 0x170017EB RID: 6123
		// (get) Token: 0x060060AA RID: 24746 RVA: 0x00243296 File Offset: 0x00241496
		protected override string VisualsPath
		{
			get
			{
				return SceneHelper.GetScenePath("creature_visuals/defect");
			}
		}

		// Token: 0x170017EC RID: 6124
		// (get) Token: 0x060060AB RID: 24747 RVA: 0x002432A2 File Offset: 0x002414A2
		public override int MinInitialHp
		{
			get
			{
				return 9999;
			}
		}

		// Token: 0x170017ED RID: 6125
		// (get) Token: 0x060060AC RID: 24748 RVA: 0x002432A9 File Offset: 0x002414A9
		public override int MaxInitialHp
		{
			get
			{
				return 9999;
			}
		}

		// Token: 0x170017EE RID: 6126
		// (get) Token: 0x060060AD RID: 24749 RVA: 0x002432B0 File Offset: 0x002414B0
		// (set) Token: 0x060060AE RID: 24750 RVA: 0x002432B8 File Offset: 0x002414B8
		public int PlatingAmount
		{
			get
			{
				return this._platingAmount;
			}
			set
			{
				base.AssertMutable();
				this._platingAmount = value;
			}
		}

		// Token: 0x060060AF RID: 24751 RVA: 0x002432C8 File Offset: 0x002414C8
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

		// Token: 0x060060B0 RID: 24752 RVA: 0x00243318 File Offset: 0x00241518
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<PlatingPower>(base.Creature, this.PlatingAmount, base.Creature, null, false);
		}

		// Token: 0x060060B1 RID: 24753 RVA: 0x0024335B File Offset: 0x0024155B
		private Task NothingMove(IReadOnlyList<Creature> targets)
		{
			return Task.CompletedTask;
		}

		// Token: 0x04002464 RID: 9316
		private int _platingAmount;
	}
}
