using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200077F RID: 1919
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ShrinkerBeetle : MonsterModel
	{
		// Token: 0x170016C5 RID: 5829
		// (get) Token: 0x06005DEA RID: 24042 RVA: 0x0023AFA1 File Offset: 0x002391A1
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 40, 38);
			}
		}

		// Token: 0x170016C6 RID: 5830
		// (get) Token: 0x06005DEB RID: 24043 RVA: 0x0023AFAD File Offset: 0x002391AD
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 42, 40);
			}
		}

		// Token: 0x170016C7 RID: 5831
		// (get) Token: 0x06005DEC RID: 24044 RVA: 0x0023AFB9 File Offset: 0x002391B9
		private int ChompDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 8, 7);
			}
		}

		// Token: 0x170016C8 RID: 5832
		// (get) Token: 0x06005DED RID: 24045 RVA: 0x0023AFC4 File Offset: 0x002391C4
		private int StompDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 14, 13);
			}
		}

		// Token: 0x170016C9 RID: 5833
		// (get) Token: 0x06005DEE RID: 24046 RVA: 0x0023AFD1 File Offset: 0x002391D1
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Insect;
			}
		}

		// Token: 0x06005DEF RID: 24047 RVA: 0x0023AFD4 File Offset: 0x002391D4
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("SHRINKER_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ShrinkMove), new AbstractIntent[]
			{
				new DebuffIntent(true)
			});
			MoveState moveState2 = new MoveState("CHOMP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ChompMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.ChompDamage)
			});
			MoveState moveState3 = new MoveState("STOMP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.StompMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.StompDamage)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState2;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005DF0 RID: 24048 RVA: 0x0023B094 File Offset: 0x00239294
		private async Task ShrinkMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play(this.CastSfx, 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.5f);
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.RadialBlur(VfxPosition.Left);
			}
			await PowerCmd.Apply<ShrinkPower>(targets, -1m, base.Creature, null, false);
		}

		// Token: 0x06005DF1 RID: 24049 RVA: 0x0023B0E0 File Offset: 0x002392E0
		private async Task ChompMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.ChompDamage).FromMonster(this).WithAttackerAnim("Attack", 0.25f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005DF2 RID: 24050 RVA: 0x0023B124 File Offset: 0x00239324
		private async Task StompMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.StompDamage).FromMonster(this).WithAttackerAnim("Attack", 0.25f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}
	}
}
