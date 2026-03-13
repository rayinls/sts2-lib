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
	// Token: 0x02000759 RID: 1881
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GlobeHead : MonsterModel
	{
		// Token: 0x170015B7 RID: 5559
		// (get) Token: 0x06005B71 RID: 23409 RVA: 0x00233717 File Offset: 0x00231917
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 158, 148);
			}
		}

		// Token: 0x170015B8 RID: 5560
		// (get) Token: 0x06005B72 RID: 23410 RVA: 0x00233729 File Offset: 0x00231929
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x170015B9 RID: 5561
		// (get) Token: 0x06005B73 RID: 23411 RVA: 0x00233731 File Offset: 0x00231931
		private int ThunderStrikeDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 7, 6);
			}
		}

		// Token: 0x170015BA RID: 5562
		// (get) Token: 0x06005B74 RID: 23412 RVA: 0x0023373C File Offset: 0x0023193C
		private int ShockingSlapDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 14, 13);
			}
		}

		// Token: 0x170015BB RID: 5563
		// (get) Token: 0x06005B75 RID: 23413 RVA: 0x00233749 File Offset: 0x00231949
		private int GalvanicBurstDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 17, 16);
			}
		}

		// Token: 0x170015BC RID: 5564
		// (get) Token: 0x06005B76 RID: 23414 RVA: 0x00233756 File Offset: 0x00231956
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x06005B77 RID: 23415 RVA: 0x0023375C File Offset: 0x0023195C
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<GalvanicPower>(base.Creature, 6m, base.Creature, null, false);
		}

		// Token: 0x06005B78 RID: 23416 RVA: 0x002337A0 File Offset: 0x002319A0
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("THUNDER_STRIKE", new Func<IReadOnlyList<Creature>, Task>(this.ThunderStrike), new AbstractIntent[]
			{
				new MultiAttackIntent(this.ThunderStrikeDamage, 3)
			});
			MoveState moveState2 = new MoveState("SHOCKING_SLAP", new Func<IReadOnlyList<Creature>, Task>(this.ShockingSlap), new AbstractIntent[]
			{
				new SingleAttackIntent(this.ShockingSlapDamage),
				new DebuffIntent(false)
			});
			MoveState moveState3 = new MoveState("GALVANIC_BURST", new Func<IReadOnlyList<Creature>, Task>(this.GalvanicBurstMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.GalvanicBurstDamage),
				new BuffIntent()
			});
			moveState2.FollowUpState = moveState;
			moveState.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState2;
			list.Add(moveState2);
			list.Add(moveState);
			list.Add(moveState3);
			return new MonsterMoveStateMachine(list, moveState2);
		}

		// Token: 0x06005B79 RID: 23417 RVA: 0x00233878 File Offset: 0x00231A78
		private async Task ThunderStrike(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.ThunderStrikeDamage).WithHitCount(3).FromMonster(this)
				.WithAttackerAnim("Cast", 0.5f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/globe_head/globe_head_charge", null)
				.WithHitFx("vfx/vfx_attack_lightning", null, null)
				.Execute(null);
		}

		// Token: 0x06005B7A RID: 23418 RVA: 0x002338BC File Offset: 0x00231ABC
		private async Task ShockingSlap(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.ShockingSlapDamage).FromMonster(this).WithAttackerAnim("Attack", 0.5f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/globe_head/globe_head_slap", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			await PowerCmd.Apply<FrailPower>(targets, 2m, base.Creature, null, false);
		}

		// Token: 0x06005B7B RID: 23419 RVA: 0x00233908 File Offset: 0x00231B08
		private async Task GalvanicBurstMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.GalvanicBurstDamage).FromMonster(this).WithAttackerAnim("Attack", 0.5f, null)
				.WithHitFx("vfx/vfx_attack_lightning", null, "blunt_attack.mp3")
				.Execute(null);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 2m, base.Creature, null, false);
		}

		// Token: 0x04002304 RID: 8964
		private const string _chargeSfx = "event:/sfx/enemy/enemy_attacks/globe_head/globe_head_charge";

		// Token: 0x04002305 RID: 8965
		private const string _slapSfx = "event:/sfx/enemy/enemy_attacks/globe_head/globe_head_slap";

		// Token: 0x04002306 RID: 8966
		private const int _thunderStrikeRepeat = 3;

		// Token: 0x04002307 RID: 8967
		private const int _shockingSlapFrail = 2;

		// Token: 0x04002308 RID: 8968
		private const int _galvanicBurstStr = 2;
	}
}
