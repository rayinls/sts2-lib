using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200078A RID: 1930
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SpectralKnight : MonsterModel
	{
		// Token: 0x1700170E RID: 5902
		// (get) Token: 0x06005E8D RID: 24205 RVA: 0x0023CE27 File Offset: 0x0023B027
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 97, 93);
			}
		}

		// Token: 0x1700170F RID: 5903
		// (get) Token: 0x06005E8E RID: 24206 RVA: 0x0023CE33 File Offset: 0x0023B033
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001710 RID: 5904
		// (get) Token: 0x06005E8F RID: 24207 RVA: 0x0023CE3B File Offset: 0x0023B03B
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x17001711 RID: 5905
		// (get) Token: 0x06005E90 RID: 24208 RVA: 0x0023CE3E File Offset: 0x0023B03E
		private int SoulSlashDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 17, 15);
			}
		}

		// Token: 0x17001712 RID: 5906
		// (get) Token: 0x06005E91 RID: 24209 RVA: 0x0023CE4B File Offset: 0x0023B04B
		private int SoulFlameDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 4, 3);
			}
		}

		// Token: 0x06005E92 RID: 24210 RVA: 0x0023CE58 File Offset: 0x0023B058
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("HEX", new Func<IReadOnlyList<Creature>, Task>(this.HexMove), new AbstractIntent[]
			{
				new DebuffIntent(false)
			});
			MoveState moveState2 = new MoveState("SOUL_SLASH", new Func<IReadOnlyList<Creature>, Task>(this.SoulSlashMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SoulSlashDamage)
			});
			MoveState moveState3 = new MoveState("SOUL_FLAME", new Func<IReadOnlyList<Creature>, Task>(this.SoulFlameMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.SoulFlameDamage, 3)
			});
			RandomBranchState randomBranchState = new RandomBranchState("RAND");
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = randomBranchState;
			moveState3.FollowUpState = randomBranchState;
			randomBranchState.AddBranch(moveState2, 2);
			randomBranchState.AddBranch(moveState3, MoveRepeatType.CannotRepeat);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(randomBranchState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005E93 RID: 24211 RVA: 0x0023CF44 File Offset: 0x0023B144
		private async Task HexMove(IReadOnlyList<Creature> targets)
		{
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.3f);
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/spectral_knight/spectral_knight_hex", 1f);
			foreach (Creature creature in targets)
			{
				await PowerCmd.Apply<HexPower>(creature, 2m, base.Creature, null, false);
			}
			IEnumerator<Creature> enumerator = null;
		}

		// Token: 0x06005E94 RID: 24212 RVA: 0x0023CF90 File Offset: 0x0023B190
		private async Task SoulSlashMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SoulSlashDamage).FromMonster(this).WithAttackerAnim("AttackSword", 0.25f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/spectral_knight/spectral_knight_soul_slash", null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005E95 RID: 24213 RVA: 0x0023CFD4 File Offset: 0x0023B1D4
		private async Task SoulFlameMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SoulFlameDamage).WithHitCount(3).FromMonster(this)
				.OnlyPlayAnimOnce()
				.WithAttackerAnim("AttackFlame", 0.25f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/spectral_knight/spectral_knight_soul_flame", null)
				.Execute(null);
		}

		// Token: 0x06005E96 RID: 24214 RVA: 0x0023D018 File Offset: 0x0023B218
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("debuff", false);
			AnimState animState3 = new AnimState("attack_sword", false);
			AnimState animState4 = new AnimState("attack_flame", false);
			AnimState animState5 = new AnimState("hurt", false);
			AnimState animState6 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			animState5.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("AttackSword", animState3, null);
			creatureAnimator.AddAnyState("AttackFlame", animState4, null);
			creatureAnimator.AddAnyState("Dead", animState6, null);
			creatureAnimator.AddAnyState("Hit", animState5, null);
			return creatureAnimator;
		}

		// Token: 0x040023CD RID: 9165
		private const int _soulFlameRepeat = 3;

		// Token: 0x040023CE RID: 9166
		private const string _attackFlameTrigger = "AttackFlame";

		// Token: 0x040023CF RID: 9167
		private const string _attackSwordTrigger = "AttackSword";

		// Token: 0x040023D0 RID: 9168
		private const string _hexSfx = "event:/sfx/enemy/enemy_attacks/spectral_knight/spectral_knight_hex";

		// Token: 0x040023D1 RID: 9169
		private const string _soulFlameSfx = "event:/sfx/enemy/enemy_attacks/spectral_knight/spectral_knight_soul_flame";

		// Token: 0x040023D2 RID: 9170
		private const string _soulSlashSfx = "event:/sfx/enemy/enemy_attacks/spectral_knight/spectral_knight_soul_slash";
	}
}
