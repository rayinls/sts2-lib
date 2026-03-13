using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Encounters;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200078C RID: 1932
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Stabbot : MonsterModel
	{
		// Token: 0x1700171B RID: 5915
		// (get) Token: 0x06005EAC RID: 24236 RVA: 0x0023D46E File Offset: 0x0023B66E
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 24, 23);
			}
		}

		// Token: 0x1700171C RID: 5916
		// (get) Token: 0x06005EAD RID: 24237 RVA: 0x0023D47A File Offset: 0x0023B67A
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 29, 28);
			}
		}

		// Token: 0x1700171D RID: 5917
		// (get) Token: 0x06005EAE RID: 24238 RVA: 0x0023D486 File Offset: 0x0023B686
		private int StabDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 12, 11);
			}
		}

		// Token: 0x1700171E RID: 5918
		// (get) Token: 0x06005EAF RID: 24239 RVA: 0x0023D493 File Offset: 0x0023B693
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x06005EB0 RID: 24240 RVA: 0x0023D498 File Offset: 0x0023B698
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

		// Token: 0x06005EB1 RID: 24241 RVA: 0x0023D4D0 File Offset: 0x0023B6D0
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("STAB_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.StabMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.StabDamage),
				new DebuffIntent(false)
			});
			moveState.FollowUpState = moveState;
			list.Add(moveState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005EB2 RID: 24242 RVA: 0x0023D52C File Offset: 0x0023B72C
		private async Task StabMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.StabDamage).FromMonster(this).WithAttackerAnim("Attack", 0.6f, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			await PowerCmd.Apply<FrailPower>(targets, 1m, base.Creature, null, false);
		}
	}
}
