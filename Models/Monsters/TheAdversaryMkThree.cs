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
	// Token: 0x02000791 RID: 1937
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TheAdversaryMkThree : MonsterModel
	{
		// Token: 0x17001748 RID: 5960
		// (get) Token: 0x06005F1A RID: 24346 RVA: 0x0023E7A7 File Offset: 0x0023C9A7
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 300, 300);
			}
		}

		// Token: 0x17001749 RID: 5961
		// (get) Token: 0x06005F1B RID: 24347 RVA: 0x0023E7B9 File Offset: 0x0023C9B9
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x1700174A RID: 5962
		// (get) Token: 0x06005F1C RID: 24348 RVA: 0x0023E7C1 File Offset: 0x0023C9C1
		private int CrashDamage
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x1700174B RID: 5963
		// (get) Token: 0x06005F1D RID: 24349 RVA: 0x0023E7C5 File Offset: 0x0023C9C5
		private int FlameBeamDamage
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x1700174C RID: 5964
		// (get) Token: 0x06005F1E RID: 24350 RVA: 0x0023E7C9 File Offset: 0x0023C9C9
		private int BarrageDamage
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700174D RID: 5965
		// (get) Token: 0x06005F1F RID: 24351 RVA: 0x0023E7CD File Offset: 0x0023C9CD
		private int BarrageRepeat
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700174E RID: 5966
		// (get) Token: 0x06005F20 RID: 24352 RVA: 0x0023E7D0 File Offset: 0x0023C9D0
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x06005F21 RID: 24353 RVA: 0x0023E7D4 File Offset: 0x0023C9D4
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<ArtifactPower>(base.Creature, 2m, base.Creature, null, false);
		}

		// Token: 0x06005F22 RID: 24354 RVA: 0x0023E818 File Offset: 0x0023CA18
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("CRASH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.CrashMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.CrashDamage)
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

		// Token: 0x06005F23 RID: 24355 RVA: 0x0023E8EC File Offset: 0x0023CAEC
		private async Task CrashMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.CrashDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005F24 RID: 24356 RVA: 0x0023E930 File Offset: 0x0023CB30
		private async Task FlameBeamMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.FlameBeamDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005F25 RID: 24357 RVA: 0x0023E974 File Offset: 0x0023CB74
		private async Task BarrageMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.BarrageDamage).WithHitCount(this.BarrageRepeat).FromMonster(this)
				.WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 4m, base.Creature, null, false);
		}
	}
}
