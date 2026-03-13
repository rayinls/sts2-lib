using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Audio;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x020007A3 RID: 1955
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WaterfallGiant : MonsterModel
	{
		// Token: 0x170017C1 RID: 6081
		// (get) Token: 0x06006047 RID: 24647 RVA: 0x00242337 File Offset: 0x00240537
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 260, 250);
			}
		}

		// Token: 0x170017C2 RID: 6082
		// (get) Token: 0x06006048 RID: 24648 RVA: 0x00242349 File Offset: 0x00240549
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x170017C3 RID: 6083
		// (get) Token: 0x06006049 RID: 24649 RVA: 0x00242351 File Offset: 0x00240551
		private int PressurizeAmount
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 20, 15);
			}
		}

		// Token: 0x170017C4 RID: 6084
		// (get) Token: 0x0600604A RID: 24650 RVA: 0x0024235E File Offset: 0x0024055E
		private int StompDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 16, 15);
			}
		}

		// Token: 0x170017C5 RID: 6085
		// (get) Token: 0x0600604B RID: 24651 RVA: 0x0024236B File Offset: 0x0024056B
		private int RamDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 11, 10);
			}
		}

		// Token: 0x170017C6 RID: 6086
		// (get) Token: 0x0600604C RID: 24652 RVA: 0x00242378 File Offset: 0x00240578
		private int PressureUpDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 14, 13);
			}
		}

		// Token: 0x170017C7 RID: 6087
		// (get) Token: 0x0600604D RID: 24653 RVA: 0x00242385 File Offset: 0x00240585
		private int BasePressureGunDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 23, 20);
			}
		}

		// Token: 0x170017C8 RID: 6088
		// (get) Token: 0x0600604E RID: 24654 RVA: 0x00242392 File Offset: 0x00240592
		public override bool ShouldDisappearFromDoom
		{
			get
			{
				return !base.Creature.HasPower<SteamEruptionPower>();
			}
		}

		// Token: 0x170017C9 RID: 6089
		// (get) Token: 0x0600604F RID: 24655 RVA: 0x002423A2 File Offset: 0x002405A2
		private int PressureGunIncrease
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x170017CA RID: 6090
		// (get) Token: 0x06006050 RID: 24656 RVA: 0x002423A5 File Offset: 0x002405A5
		private int SiphonHeal
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x170017CB RID: 6091
		// (get) Token: 0x06006051 RID: 24657 RVA: 0x002423A9 File Offset: 0x002405A9
		// (set) Token: 0x06006052 RID: 24658 RVA: 0x002423B1 File Offset: 0x002405B1
		private int CurrentPressureGunDamage
		{
			get
			{
				return this._currentPressureGunDamage;
			}
			set
			{
				base.AssertMutable();
				this._currentPressureGunDamage = value;
			}
		}

		// Token: 0x170017CC RID: 6092
		// (get) Token: 0x06006053 RID: 24659 RVA: 0x002423C0 File Offset: 0x002405C0
		// (set) Token: 0x06006054 RID: 24660 RVA: 0x002423C8 File Offset: 0x002405C8
		private int SteamEruptionDamage
		{
			get
			{
				return this._steamEruptionDamage;
			}
			set
			{
				base.AssertMutable();
				this._steamEruptionDamage = value;
			}
		}

		// Token: 0x170017CD RID: 6093
		// (get) Token: 0x06006055 RID: 24661 RVA: 0x002423D7 File Offset: 0x002405D7
		// (set) Token: 0x06006056 RID: 24662 RVA: 0x002423DF File Offset: 0x002405DF
		private MoveState AboutToBlowState
		{
			get
			{
				return this._aboutToBlowState;
			}
			set
			{
				base.AssertMutable();
				this._aboutToBlowState = value;
			}
		}

		// Token: 0x170017CE RID: 6094
		// (get) Token: 0x06006057 RID: 24663 RVA: 0x002423EE File Offset: 0x002405EE
		// (set) Token: 0x06006058 RID: 24664 RVA: 0x002423F6 File Offset: 0x002405F6
		private bool IsAboutToBlow
		{
			get
			{
				return this._isAboutToBlow;
			}
			set
			{
				base.AssertMutable();
				this._isAboutToBlow = value;
			}
		}

		// Token: 0x170017CF RID: 6095
		// (get) Token: 0x06006059 RID: 24665 RVA: 0x00242405 File Offset: 0x00240605
		// (set) Token: 0x0600605A RID: 24666 RVA: 0x0024240D File Offset: 0x0024060D
		private int PressureBuildupIdx
		{
			get
			{
				return this._pressureBuildupIdx;
			}
			set
			{
				base.AssertMutable();
				this._pressureBuildupIdx = value;
			}
		}

		// Token: 0x170017D0 RID: 6096
		// (get) Token: 0x0600605B RID: 24667 RVA: 0x0024241C File Offset: 0x0024061C
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Magic;
			}
		}

		// Token: 0x170017D1 RID: 6097
		// (get) Token: 0x0600605C RID: 24668 RVA: 0x0024241F File Offset: 0x0024061F
		public override bool HasDeathSfx
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017D2 RID: 6098
		// (get) Token: 0x0600605D RID: 24669 RVA: 0x00242422 File Offset: 0x00240622
		public override bool ShouldFadeAfterDeath
		{
			get
			{
				return this.PressureBuildupIdx == 0;
			}
		}

		// Token: 0x0600605E RID: 24670 RVA: 0x00242430 File Offset: 0x00240630
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			this.CurrentPressureGunDamage = this.BasePressureGunDamage;
			SfxCmd.PlayLoop("event:/sfx/enemy/enemy_attacks/waterfall_giant/waterfall_giant_ambient", false);
			base.Creature.Died += this.AfterDeath;
		}

		// Token: 0x0600605F RID: 24671 RVA: 0x00242473 File Offset: 0x00240673
		private void AfterDeath(Creature _)
		{
			if (base.Creature.HasPower<SteamEruptionPower>())
			{
				return;
			}
			base.Creature.Died -= this.AfterDeath;
			NRunMusicController instance = NRunMusicController.Instance;
			if (instance == null)
			{
				return;
			}
			instance.UpdateMusicParameter("waterfall_giant_progress", 5f);
		}

		// Token: 0x06006060 RID: 24672 RVA: 0x002424B3 File Offset: 0x002406B3
		public override void BeforeRemovedFromRoom()
		{
			this.StopAmbientSfx();
		}

		// Token: 0x06006061 RID: 24673 RVA: 0x002424BB File Offset: 0x002406BB
		private void StopAmbientSfx()
		{
			SfxCmd.SetParam("event:/sfx/enemy/enemy_attacks/waterfall_giant/waterfall_giant_ambient", "waterfall_giant_sfx", 2f);
			SfxCmd.StopLoop("event:/sfx/enemy/enemy_attacks/waterfall_giant/waterfall_giant_ambient");
		}

		// Token: 0x06006062 RID: 24674 RVA: 0x002424DC File Offset: 0x002406DC
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("PRESSURIZE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.PressurizeMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			MoveState moveState2 = new MoveState("STOMP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.StompMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.StompDamage),
				new DebuffIntent(false),
				new BuffIntent()
			});
			MoveState moveState3 = new MoveState("RAM_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.RamMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.RamDamage),
				new BuffIntent()
			});
			MoveState moveState4 = new MoveState("SIPHON_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SiphonMove), new AbstractIntent[]
			{
				new HealIntent(),
				new BuffIntent()
			});
			MoveState moveState5 = new MoveState("PRESSURE_GUN_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.PressureGunMove), new AbstractIntent[]
			{
				new SingleAttackIntent(() => this.CurrentPressureGunDamage),
				new BuffIntent()
			});
			MoveState moveState6 = new MoveState("PRESSURE_UP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.PressureUpMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.PressureUpDamage),
				new BuffIntent()
			});
			this.AboutToBlowState = new MoveState("ABOUT_TO_BLOW_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.AboutToBlowMove), new AbstractIntent[]
			{
				new StunIntent()
			})
			{
				MustPerformOnceBeforeTransitioning = true
			};
			MoveState moveState7 = new MoveState("EXPLODE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ExplodeMove), new AbstractIntent[]
			{
				new DeathBlowIntent(() => this.SteamEruptionDamage)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState4;
			moveState4.FollowUpState = moveState5;
			moveState5.FollowUpState = moveState6;
			moveState6.FollowUpState = moveState2;
			this.AboutToBlowState.FollowUpState = moveState7;
			moveState7.FollowUpState = moveState7;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(moveState4);
			list.Add(moveState5);
			list.Add(moveState6);
			list.Add(moveState7);
			list.Add(this.AboutToBlowState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06006063 RID: 24675 RVA: 0x00242710 File Offset: 0x00240910
		private async Task PressurizeMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/waterfall_giant/waterfall_giant_eruption", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Heal", 0.8f);
			await PowerCmd.Apply<SteamEruptionPower>(base.Creature, this.PressurizeAmount, base.Creature, null, false);
			this.IncrementBuildUpAnimationTrack();
		}

		// Token: 0x06006064 RID: 24676 RVA: 0x00242754 File Offset: 0x00240954
		private async Task PressureUpMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.PressureUpDamage).FromMonster(this).WithAttackerAnim("AttackBuff", 0.15f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/waterfall_giant/waterfall_giant_attack_stomp", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			await PowerCmd.Apply<SteamEruptionPower>(base.Creature, 3m, base.Creature, null, false);
			this.IncrementBuildUpAnimationTrack();
		}

		// Token: 0x06006065 RID: 24677 RVA: 0x00242798 File Offset: 0x00240998
		private async Task StompMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.StompDamage).FromMonster(this).WithAttackerAnim("AttackDebuff", 0.3f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/waterfall_giant/waterfall_giant_attack_stomp", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			await PowerCmd.Apply<WeakPower>(targets, 1m, base.Creature, null, false);
			await PowerCmd.Apply<SteamEruptionPower>(base.Creature, 3m, base.Creature, null, false);
			this.IncrementBuildUpAnimationTrack();
		}

		// Token: 0x06006066 RID: 24678 RVA: 0x002427E4 File Offset: 0x002409E4
		private async Task RamMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.RamDamage).FromMonster(this).WithAttackerAnim("Attack", 0.3f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/waterfall_giant/waterfall_giant_attack_kick", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			await PowerCmd.Apply<SteamEruptionPower>(base.Creature, 3m, base.Creature, null, false);
			this.IncrementBuildUpAnimationTrack();
		}

		// Token: 0x06006067 RID: 24679 RVA: 0x00242828 File Offset: 0x00240A28
		private async Task SiphonMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/waterfall_giant/waterfall_giant_eruption", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Heal", 0.8f);
			await CreatureCmd.Heal(base.Creature, this.SiphonHeal * base.Creature.CombatState.Players.Count, true);
			await PowerCmd.Apply<SteamEruptionPower>(base.Creature, 3m, base.Creature, null, false);
			this.IncrementBuildUpAnimationTrack();
		}

		// Token: 0x06006068 RID: 24680 RVA: 0x0024286C File Offset: 0x00240A6C
		private async Task PressureGunMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.CurrentPressureGunDamage).FromMonster(this).WithAttackerAnim("Attack", 0.3f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/waterfall_giant/waterfall_giant_attack_kick", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			this.CurrentPressureGunDamage += this.PressureGunIncrease;
			await PowerCmd.Apply<SteamEruptionPower>(base.Creature, 3m, base.Creature, null, false);
			this.IncrementBuildUpAnimationTrack();
		}

		// Token: 0x06006069 RID: 24681 RVA: 0x002428B0 File Offset: 0x00240AB0
		private async Task AboutToBlowMove(IReadOnlyList<Creature> targets)
		{
			this.SteamEruptionDamage = base.Creature.GetPowerAmount<SteamEruptionPower>();
			await PowerCmd.Remove<SteamEruptionPower>(base.Creature);
			this.PressureBuildupIdx = 6;
			this.IncrementBuildUpAnimationTrack();
		}

		// Token: 0x0600606A RID: 24682 RVA: 0x002428F4 File Offset: 0x00240AF4
		private async Task ExplodeMove(IReadOnlyList<Creature> targets)
		{
			this.StopAmbientSfx();
			await DamageCmd.Attack(this.SteamEruptionDamage).FromMonster(this).WithAttackerAnim("Erupt", 0.1f, null)
				.WithAttackerFx(null, this.DeathSfx, null)
				.Execute(null);
			await CreatureCmd.Kill(base.Creature, false);
			NRunMusicController instance = NRunMusicController.Instance;
			if (instance != null)
			{
				instance.UpdateMusicParameter("waterfall_giant_progress", 5f);
			}
		}

		// Token: 0x0600606B RID: 24683 RVA: 0x00242938 File Offset: 0x00240B38
		public async Task TriggerAboutToBlowState()
		{
			this.IsAboutToBlow = true;
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/waterfall_giant/waterfall_giant_knockout", 1f);
			NRunMusicController instance = NRunMusicController.Instance;
			if (instance != null)
			{
				instance.UpdateMusicParameter("waterfall_giant_progress", 2f);
			}
			await CreatureCmd.SetMaxAndCurrentHp(base.Creature, 999999999m);
			base.Creature.ShowsInfiniteHp = true;
			base.SetMoveImmediate(this.AboutToBlowState, true);
			SfxCmd.SetParam("event:/sfx/enemy/enemy_attacks/waterfall_giant/waterfall_giant_ambient", "waterfall_giant_sfx", 3f);
		}

		// Token: 0x0600606C RID: 24684 RVA: 0x0024297C File Offset: 0x00240B7C
		private void IncrementBuildUpAnimationTrack()
		{
			if (!TestMode.IsOff)
			{
				return;
			}
			NRunMusicController instance = NRunMusicController.Instance;
			if (instance != null)
			{
				instance.UpdateMusicParameter("waterfall_giant_progress", 1f);
			}
			SfxCmd.SetParam("event:/sfx/enemy/enemy_attacks/waterfall_giant/waterfall_giant_ambient", "waterfall_giant_sfx", 1f);
			int pressureBuildupIdx = this.PressureBuildupIdx;
			this.PressureBuildupIdx = pressureBuildupIdx + 1;
			int num = Mathf.FloorToInt((float)this.PressureBuildupIdx * 0.5f);
			num = Mathf.Clamp(num, 1, 3);
			NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(base.Creature);
			if (creatureNode != null)
			{
				MegaAnimationState animationState = creatureNode.SpineController.GetAnimationState();
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 1);
				defaultInterpolatedStringHandler.AppendLiteral("_tracks/buildup");
				defaultInterpolatedStringHandler.AppendFormatted<int>(num);
				animationState.SetAnimation(defaultInterpolatedStringHandler.ToStringAndClear(), true, 1);
			}
		}

		// Token: 0x0600606D RID: 24685 RVA: 0x00242A38 File Offset: 0x00240C38
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("cast", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("attack_buff", false);
			AnimState animState5 = new AnimState("attack_debuff", false);
			AnimState animState6 = new AnimState("heal", false);
			AnimState animState7 = new AnimState("hurt", false);
			AnimState animState8 = new AnimState("die", false);
			AnimState animState9 = new AnimState("die_loop", true);
			AnimState animState10 = new AnimState("erupt", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			animState5.NextState = animState;
			animState6.NextState = animState;
			animState7.NextState = animState;
			animState8.NextState = animState9;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("Dead", animState8, () => !this.IsAboutToBlow);
			creatureAnimator.AddAnyState("Hit", animState7, () => !this.IsAboutToBlow);
			creatureAnimator.AddAnyState("AttackBuff", animState4, null);
			creatureAnimator.AddAnyState("AttackDebuff", animState5, null);
			creatureAnimator.AddAnyState("Heal", animState6, null);
			creatureAnimator.AddAnyState("Erupt", animState10, null);
			return creatureAnimator;
		}

		// Token: 0x0400244A RID: 9290
		private const string _waterfallGiantTrackName = "waterfall_giant_progress";

		// Token: 0x0400244B RID: 9291
		private const int _endCombatBgmFlag = 5;

		// Token: 0x0400244C RID: 9292
		private const int _maxIntensityBgmFlag = 2;

		// Token: 0x0400244D RID: 9293
		private const int _increaseIntensityBgmFlag = 1;

		// Token: 0x0400244E RID: 9294
		private const int _increaseIntensityAmbienceFlag = 1;

		// Token: 0x0400244F RID: 9295
		private const int _maxIntensityAmbienceFlag = 3;

		// Token: 0x04002450 RID: 9296
		private const int _endAmbienceFlag = 2;

		// Token: 0x04002451 RID: 9297
		private int _currentPressureGunDamage;

		// Token: 0x04002452 RID: 9298
		private int _steamEruptionDamage;

		// Token: 0x04002453 RID: 9299
		private MoveState _aboutToBlowState;

		// Token: 0x04002454 RID: 9300
		private bool _isAboutToBlow;

		// Token: 0x04002455 RID: 9301
		private int _pressureBuildupIdx;

		// Token: 0x04002456 RID: 9302
		private const int _maxPressureBuildup = 6;

		// Token: 0x04002457 RID: 9303
		private const string _attackBuffTrigger = "AttackBuff";

		// Token: 0x04002458 RID: 9304
		private const string _attackDebuffTrigger = "AttackDebuff";

		// Token: 0x04002459 RID: 9305
		private const string _healTrigger = "Heal";

		// Token: 0x0400245A RID: 9306
		private const string _eruptTrigger = "Erupt";

		// Token: 0x0400245B RID: 9307
		private const string _attackKickSfx = "event:/sfx/enemy/enemy_attacks/waterfall_giant/waterfall_giant_attack_kick";

		// Token: 0x0400245C RID: 9308
		private const string _attackStompSfx = "event:/sfx/enemy/enemy_attacks/waterfall_giant/waterfall_giant_attack_stomp";

		// Token: 0x0400245D RID: 9309
		private const string _eruptionSfx = "event:/sfx/enemy/enemy_attacks/waterfall_giant/waterfall_giant_eruption";

		// Token: 0x0400245E RID: 9310
		private const string _knockoutSfx = "event:/sfx/enemy/enemy_attacks/waterfall_giant/waterfall_giant_knockout";

		// Token: 0x0400245F RID: 9311
		private const string _ambientSfx = "event:/sfx/enemy/enemy_attacks/waterfall_giant/waterfall_giant_ambient";
	}
}
