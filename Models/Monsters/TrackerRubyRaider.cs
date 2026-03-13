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
	// Token: 0x0200079B RID: 1947
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TrackerRubyRaider : MonsterModel
	{
		// Token: 0x17001793 RID: 6035
		// (get) Token: 0x06005FD8 RID: 24536 RVA: 0x00240C60 File Offset: 0x0023EE60
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 22, 21);
			}
		}

		// Token: 0x17001794 RID: 6036
		// (get) Token: 0x06005FD9 RID: 24537 RVA: 0x00240C6C File Offset: 0x0023EE6C
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 26, 25);
			}
		}

		// Token: 0x17001795 RID: 6037
		// (get) Token: 0x06005FDA RID: 24538 RVA: 0x00240C78 File Offset: 0x0023EE78
		private int HoundsDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 1, 1);
			}
		}

		// Token: 0x17001796 RID: 6038
		// (get) Token: 0x06005FDB RID: 24539 RVA: 0x00240C83 File Offset: 0x0023EE83
		private int HoundsRepeat
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 9, 8);
			}
		}

		// Token: 0x17001797 RID: 6039
		// (get) Token: 0x06005FDC RID: 24540 RVA: 0x00240C8F File Offset: 0x0023EE8F
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x06005FDD RID: 24541 RVA: 0x00240C94 File Offset: 0x0023EE94
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("TRACK_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.TrackMove), new AbstractIntent[]
			{
				new DebuffIntent(false)
			});
			MoveState moveState2 = new MoveState("HOUNDS_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.HoundsMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.HoundsDamage, this.HoundsRepeat)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState2;
			list.Add(moveState);
			list.Add(moveState2);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005FDE RID: 24542 RVA: 0x00240D24 File Offset: 0x0023EF24
		private async Task TrackMove(IReadOnlyList<Creature> targets)
		{
			await CreatureCmd.TriggerAnim(base.Creature, "Attack", 0.8f);
			VfxCmd.PlayOnCreatureCenters(targets, "vfx/vfx_attack_slash");
			await PowerCmd.Apply<FrailPower>(targets, 2m, base.Creature, null, false);
		}

		// Token: 0x06005FDF RID: 24543 RVA: 0x00240D70 File Offset: 0x0023EF70
		private async Task HoundsMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.HoundsDamage).WithHitCount(this.HoundsRepeat).FromMonster(this)
				.WithAttackerAnim("Attack", 0.5f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}
	}
}
