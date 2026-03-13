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
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000767 RID: 1895
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LivingShield : MonsterModel
	{
		// Token: 0x17001624 RID: 5668
		// (get) Token: 0x06005C67 RID: 23655 RVA: 0x00236708 File Offset: 0x00234908
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 65, 55);
			}
		}

		// Token: 0x17001625 RID: 5669
		// (get) Token: 0x06005C68 RID: 23656 RVA: 0x00236714 File Offset: 0x00234914
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001626 RID: 5670
		// (get) Token: 0x06005C69 RID: 23657 RVA: 0x0023671C File Offset: 0x0023491C
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x17001627 RID: 5671
		// (get) Token: 0x06005C6A RID: 23658 RVA: 0x0023671F File Offset: 0x0023491F
		public override bool HasDeathSfx
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001628 RID: 5672
		// (get) Token: 0x06005C6B RID: 23659 RVA: 0x00236722 File Offset: 0x00234922
		private int ShieldSlamDamage
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x17001629 RID: 5673
		// (get) Token: 0x06005C6C RID: 23660 RVA: 0x00236725 File Offset: 0x00234925
		private int SmashDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 18, 16);
			}
		}

		// Token: 0x1700162A RID: 5674
		// (get) Token: 0x06005C6D RID: 23661 RVA: 0x00236732 File Offset: 0x00234932
		private int EnrageStr
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06005C6E RID: 23662 RVA: 0x00236738 File Offset: 0x00234938
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<RampartPower>(base.Creature, 25m, base.Creature, null, false);
		}

		// Token: 0x06005C6F RID: 23663 RVA: 0x0023677C File Offset: 0x0023497C
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("SHIELD_SLAM_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ShieldSlamMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.ShieldSlamDamage)
			});
			ConditionalBranchState conditionalBranchState = new ConditionalBranchState("SHIELD_SLAM_BRANCH");
			MoveState moveState2 = new MoveState("SMASH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SmashMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SmashDamage),
				new BuffIntent()
			});
			moveState.FollowUpState = conditionalBranchState;
			conditionalBranchState.AddState(moveState, () => this.GetAllyCount() > 0);
			conditionalBranchState.AddState(moveState2, () => this.GetAllyCount() == 0);
			moveState2.FollowUpState = moveState2;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(conditionalBranchState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005C70 RID: 23664 RVA: 0x00236848 File Offset: 0x00234A48
		private async Task ShieldSlamMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.ShieldSlamDamage).FromMonster(this).WithAttackerAnim("Attack", 0.3f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005C71 RID: 23665 RVA: 0x0023688C File Offset: 0x00234A8C
		private async Task SmashMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SmashDamage).FromMonster(this).WithAttackerAnim("Attack", 0.3f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			await PowerCmd.Apply<StrengthPower>(base.Creature, this.EnrageStr, base.Creature, null, false);
		}

		// Token: 0x06005C72 RID: 23666 RVA: 0x002368CF File Offset: 0x00234ACF
		private int GetAllyCount()
		{
			return base.Creature.CombatState.GetTeammatesOf(base.Creature).Count((Creature c) => c.IsAlive && c != base.Creature);
		}
	}
}
