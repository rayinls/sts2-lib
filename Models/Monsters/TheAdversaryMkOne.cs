using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000790 RID: 1936
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TheAdversaryMkOne : MonsterModel
	{
		// Token: 0x17001741 RID: 5953
		// (get) Token: 0x06005F0C RID: 24332 RVA: 0x0023E595 File Offset: 0x0023C795
		public override int MinInitialHp
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x17001742 RID: 5954
		// (get) Token: 0x06005F0D RID: 24333 RVA: 0x0023E599 File Offset: 0x0023C799
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001743 RID: 5955
		// (get) Token: 0x06005F0E RID: 24334 RVA: 0x0023E5A1 File Offset: 0x0023C7A1
		private int SmashDamage
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17001744 RID: 5956
		// (get) Token: 0x06005F0F RID: 24335 RVA: 0x0023E5A5 File Offset: 0x0023C7A5
		private int BeamDamage
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x17001745 RID: 5957
		// (get) Token: 0x06005F10 RID: 24336 RVA: 0x0023E5A9 File Offset: 0x0023C7A9
		private int BarrageDamage
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x17001746 RID: 5958
		// (get) Token: 0x06005F11 RID: 24337 RVA: 0x0023E5AC File Offset: 0x0023C7AC
		private int BarrageRepeat
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17001747 RID: 5959
		// (get) Token: 0x06005F12 RID: 24338 RVA: 0x0023E5AF File Offset: 0x0023C7AF
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x06005F13 RID: 24339 RVA: 0x0023E5B4 File Offset: 0x0023C7B4
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<ArtifactPower>(base.Creature, 0m, base.Creature, null, false);
		}

		// Token: 0x06005F14 RID: 24340 RVA: 0x0023E5F8 File Offset: 0x0023C7F8
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("SMASH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SmashMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SmashDamage)
			});
			MoveState moveState2 = new MoveState("BEAM_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BeamMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.BeamDamage)
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

		// Token: 0x06005F15 RID: 24341 RVA: 0x0023E6CC File Offset: 0x0023C8CC
		private async Task SmashMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SmashDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005F16 RID: 24342 RVA: 0x0023E710 File Offset: 0x0023C910
		private async Task BeamMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.BeamDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005F17 RID: 24343 RVA: 0x0023E754 File Offset: 0x0023C954
		private async Task BarrageMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.BarrageDamage).WithHitCount(this.BarrageRepeat).FromMonster(this)
				.WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 2m, base.Creature, null, false);
		}
	}
}
