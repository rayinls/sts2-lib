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
	// Token: 0x02000739 RID: 1849
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BruteRubyRaider : MonsterModel
	{
		// Token: 0x170014E6 RID: 5350
		// (get) Token: 0x0600597A RID: 22906 RVA: 0x0022D723 File Offset: 0x0022B923
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 31, 30);
			}
		}

		// Token: 0x170014E7 RID: 5351
		// (get) Token: 0x0600597B RID: 22907 RVA: 0x0022D72F File Offset: 0x0022B92F
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 34, 33);
			}
		}

		// Token: 0x170014E8 RID: 5352
		// (get) Token: 0x0600597C RID: 22908 RVA: 0x0022D73B File Offset: 0x0022B93B
		private int BeatDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 8, 7);
			}
		}

		// Token: 0x170014E9 RID: 5353
		// (get) Token: 0x0600597D RID: 22909 RVA: 0x0022D746 File Offset: 0x0022B946
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x0600597E RID: 22910 RVA: 0x0022D74C File Offset: 0x0022B94C
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("BEAT_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BeatMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.BeatDamage)
			});
			MoveState moveState2 = new MoveState("ROAR_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.RoarMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x0600597F RID: 22911 RVA: 0x0022D7D4 File Offset: 0x0022B9D4
		private async Task BeatMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.BeatDamage).FromMonster(this).WithAttackerAnim("Attack", 0.6f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005980 RID: 22912 RVA: 0x0022D818 File Offset: 0x0022BA18
		private async Task RoarMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play(this.CastSfx, 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.6f);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 3m, base.Creature, null, false);
		}

		// Token: 0x04002298 RID: 8856
		private const int _roarStrength = 3;
	}
}
