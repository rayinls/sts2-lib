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

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000792 RID: 1938
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TheAdversaryMkTwo : MonsterModel
	{
		// Token: 0x1700174F RID: 5967
		// (get) Token: 0x06005F28 RID: 24360 RVA: 0x0023E9C7 File Offset: 0x0023CBC7
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 200, 200);
			}
		}

		// Token: 0x17001750 RID: 5968
		// (get) Token: 0x06005F29 RID: 24361 RVA: 0x0023E9D9 File Offset: 0x0023CBD9
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001751 RID: 5969
		// (get) Token: 0x06005F2A RID: 24362 RVA: 0x0023E9E1 File Offset: 0x0023CBE1
		private int BashDamage
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x17001752 RID: 5970
		// (get) Token: 0x06005F2B RID: 24363 RVA: 0x0023E9E5 File Offset: 0x0023CBE5
		private int FlameBeamDamage
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17001753 RID: 5971
		// (get) Token: 0x06005F2C RID: 24364 RVA: 0x0023E9E9 File Offset: 0x0023CBE9
		private int FlameBeamStatusCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17001754 RID: 5972
		// (get) Token: 0x06005F2D RID: 24365 RVA: 0x0023E9EC File Offset: 0x0023CBEC
		private int BarrageDamage
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17001755 RID: 5973
		// (get) Token: 0x06005F2E RID: 24366 RVA: 0x0023E9F0 File Offset: 0x0023CBF0
		private int BarrageRepeat
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17001756 RID: 5974
		// (get) Token: 0x06005F2F RID: 24367 RVA: 0x0023E9F3 File Offset: 0x0023CBF3
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x06005F30 RID: 24368 RVA: 0x0023E9F8 File Offset: 0x0023CBF8
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<ArtifactPower>(base.Creature, 1m, base.Creature, null, false);
		}

		// Token: 0x06005F31 RID: 24369 RVA: 0x0023EA3C File Offset: 0x0023CC3C
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("BASH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BashMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.BashDamage)
			});
			MoveState moveState2 = new MoveState("FLAME_BEAM_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.FlameBeamMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.FlameBeamDamage)
			});
			MoveState moveState3 = new MoveState("BARRAGE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BarrageMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.BarrageDamage, this.BarrageRepeat),
				new BuffIntent()
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005F32 RID: 24370 RVA: 0x0023EB10 File Offset: 0x0023CD10
		private async Task BashMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.BashDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005F33 RID: 24371 RVA: 0x0023EB54 File Offset: 0x0023CD54
		private async Task FlameBeamMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.FlameBeamDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005F34 RID: 24372 RVA: 0x0023EB98 File Offset: 0x0023CD98
		private async Task BarrageMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.BarrageDamage).WithHitCount(this.BarrageRepeat).FromMonster(this)
				.WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 3m, base.Creature, null, false);
		}
	}
}
