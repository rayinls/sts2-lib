using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Encounters;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.TestSupport;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200075B RID: 1883
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Guardbot : MonsterModel
	{
		// Token: 0x170015C6 RID: 5574
		// (get) Token: 0x06005B8F RID: 23439 RVA: 0x00233C65 File Offset: 0x00231E65
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 22, 21);
			}
		}

		// Token: 0x170015C7 RID: 5575
		// (get) Token: 0x06005B90 RID: 23440 RVA: 0x00233C71 File Offset: 0x00231E71
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 26, 25);
			}
		}

		// Token: 0x170015C8 RID: 5576
		// (get) Token: 0x06005B91 RID: 23441 RVA: 0x00233C7D File Offset: 0x00231E7D
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x06005B92 RID: 23442 RVA: 0x00233C80 File Offset: 0x00231E80
		public override Task AfterAddedToRoom()
		{
			base.AfterAddedToRoom();
			if (TestMode.IsOff)
			{
				NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(base.Creature);
				FabricatorNormal.SetBotFallPosition(creatureNode);
			}
			return Task.CompletedTask;
		}

		// Token: 0x06005B93 RID: 23443 RVA: 0x00233CB8 File Offset: 0x00231EB8
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("GUARD_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.GuardMove), new AbstractIntent[]
			{
				new DefendIntent()
			});
			moveState.FollowUpState = moveState;
			list.Add(moveState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005B94 RID: 23444 RVA: 0x00233D08 File Offset: 0x00231F08
		private async Task GuardMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play(this.CastSfx, 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.6f);
			List<Creature> list = base.Creature.CombatState.Enemies.Where((Creature c) => c.Monster is Fabricator).ToList<Creature>();
			foreach (Creature creature in list)
			{
				await CreatureCmd.GainBlock(creature, 15m, ValueProp.Unpowered, null, false);
			}
			List<Creature>.Enumerator enumerator = default(List<Creature>.Enumerator);
		}
	}
}
