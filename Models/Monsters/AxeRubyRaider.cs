using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000730 RID: 1840
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class AxeRubyRaider : MonsterModel
	{
		// Token: 0x170014BE RID: 5310
		// (get) Token: 0x0600591C RID: 22812 RVA: 0x0022C683 File Offset: 0x0022A883
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 21, 20);
			}
		}

		// Token: 0x170014BF RID: 5311
		// (get) Token: 0x0600591D RID: 22813 RVA: 0x0022C68F File Offset: 0x0022A88F
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 23, 22);
			}
		}

		// Token: 0x170014C0 RID: 5312
		// (get) Token: 0x0600591E RID: 22814 RVA: 0x0022C69B File Offset: 0x0022A89B
		private int SwingDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 6, 5);
			}
		}

		// Token: 0x170014C1 RID: 5313
		// (get) Token: 0x0600591F RID: 22815 RVA: 0x0022C6A6 File Offset: 0x0022A8A6
		private int SwingBlock
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 6, 5);
			}
		}

		// Token: 0x170014C2 RID: 5314
		// (get) Token: 0x06005920 RID: 22816 RVA: 0x0022C6B1 File Offset: 0x0022A8B1
		private int BigSwingDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 13, 12);
			}
		}

		// Token: 0x170014C3 RID: 5315
		// (get) Token: 0x06005921 RID: 22817 RVA: 0x0022C6BE File Offset: 0x0022A8BE
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x06005922 RID: 22818 RVA: 0x0022C6C4 File Offset: 0x0022A8C4
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("SWING_1", new Func<IReadOnlyList<Creature>, Task>(this.SwingMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SwingDamage),
				new DefendIntent()
			});
			MoveState moveState2 = new MoveState("SWING_2", new Func<IReadOnlyList<Creature>, Task>(this.SwingMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SwingDamage),
				new DefendIntent()
			});
			MoveState moveState3 = new MoveState("BIG_SWING", new Func<IReadOnlyList<Creature>, Task>(this.BigSwingMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.BigSwingDamage)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005923 RID: 22819 RVA: 0x0022C79C File Offset: 0x0022A99C
		private async Task SwingMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SwingDamage).FromMonster(this).WithAttackerAnim("Attack", 0.5f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			await CreatureCmd.GainBlock(base.Creature, this.SwingBlock, ValueProp.Move, null, false);
		}

		// Token: 0x06005924 RID: 22820 RVA: 0x0022C7E0 File Offset: 0x0022A9E0
		private async Task BigSwingMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.BigSwingDamage).FromMonster(this).WithAttackerAnim("Attack", 0.5f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}
	}
}
