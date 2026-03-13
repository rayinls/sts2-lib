using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Audio;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Nodes.Vfx.Utilities;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x020007A1 RID: 1953
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Vantom : MonsterModel
	{
		// Token: 0x170017B5 RID: 6069
		// (get) Token: 0x0600602A RID: 24618 RVA: 0x00241C31 File Offset: 0x0023FE31
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 183, 173);
			}
		}

		// Token: 0x170017B6 RID: 6070
		// (get) Token: 0x0600602B RID: 24619 RVA: 0x00241C43 File Offset: 0x0023FE43
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x170017B7 RID: 6071
		// (get) Token: 0x0600602C RID: 24620 RVA: 0x00241C4B File Offset: 0x0023FE4B
		private int InkBlotDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 8, 7);
			}
		}

		// Token: 0x170017B8 RID: 6072
		// (get) Token: 0x0600602D RID: 24621 RVA: 0x00241C56 File Offset: 0x0023FE56
		private int InkyLanceDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 7, 6);
			}
		}

		// Token: 0x170017B9 RID: 6073
		// (get) Token: 0x0600602E RID: 24622 RVA: 0x00241C61 File Offset: 0x0023FE61
		private int DismemberDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 30, 27);
			}
		}

		// Token: 0x170017BA RID: 6074
		// (get) Token: 0x0600602F RID: 24623 RVA: 0x00241C6E File Offset: 0x0023FE6E
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Magic;
			}
		}

		// Token: 0x170017BB RID: 6075
		// (get) Token: 0x06006030 RID: 24624 RVA: 0x00241C71 File Offset: 0x0023FE71
		public override bool ShouldDisappearFromDoom
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006031 RID: 24625 RVA: 0x00241C74 File Offset: 0x0023FE74
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<SlipperyPower>(base.Creature, 9m, base.Creature, null, false);
			base.Creature.Died += this.AfterDeath;
		}

		// Token: 0x06006032 RID: 24626 RVA: 0x00241CB7 File Offset: 0x0023FEB7
		private void AfterDeath(Creature _)
		{
			base.Creature.Died -= this.AfterDeath;
			NRunMusicController instance = NRunMusicController.Instance;
			if (instance == null)
			{
				return;
			}
			instance.UpdateMusicParameter("vantom_progress", 5f);
		}

		// Token: 0x06006033 RID: 24627 RVA: 0x00241CEC File Offset: 0x0023FEEC
		public override void SetupSkins(NCreatureVisuals visuals)
		{
			MegaAnimationState animationState = visuals.SpineBody.GetAnimationState();
			animationState.SetAnimation("_tracks/charge_up_1", false, 1);
			animationState.AddAnimation("_tracks/charged_1", 0f, true, 1);
		}

		// Token: 0x06006034 RID: 24628 RVA: 0x00241D28 File Offset: 0x0023FF28
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("INK_BLOT_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.InkBlotMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.InkBlotDamage)
			});
			MoveState moveState2 = new MoveState("INKY_LANCE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.InkyLanceMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.InkyLanceDamage, 2)
			});
			MoveState moveState3 = new MoveState("DISMEMBER_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.DismemberMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.DismemberDamage),
				new StatusIntent(3)
			});
			MoveState moveState4 = new MoveState("PREPARE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.PrepareMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState4;
			moveState4.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(moveState4);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06006035 RID: 24629 RVA: 0x00241E30 File Offset: 0x00240030
		private async Task InkBlotMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.InkBlotDamage).FromMonster(this).WithAttackerAnim("Attack", 0.35f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/vantom/vantom_inky_lance", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			if (TestMode.IsOff && base.Creature.IsAlive)
			{
				await Cmd.CustomScaledWait(1f, 1f, false, default(CancellationToken));
				NRunMusicController instance = NRunMusicController.Instance;
				if (instance != null)
				{
					instance.UpdateMusicParameter("vantom_progress", 1f);
				}
				SfxCmd.Play("event:/sfx/enemy/enemy_attacks/vantom/vantom_extend_2", 1f);
				await CreatureCmd.TriggerAnim(base.Creature, "CHARGE_UP", 0.15f);
				NCombatRoom instance2 = NCombatRoom.Instance;
				NCreature ncreature = ((instance2 != null) ? instance2.GetCreatureNode(base.Creature) : null);
				MegaAnimationState megaAnimationState;
				if (ncreature == null)
				{
					megaAnimationState = null;
				}
				else
				{
					MegaSprite spineController = ncreature.SpineController;
					megaAnimationState = ((spineController != null) ? spineController.GetAnimationState() : null);
				}
				MegaAnimationState megaAnimationState2 = megaAnimationState;
				if (megaAnimationState2 != null)
				{
					megaAnimationState2.SetAnimation("_tracks/charge_up_2", false, 1);
				}
				if (megaAnimationState2 != null)
				{
					megaAnimationState2.AddAnimation("_tracks/charged_2", 0f, true, 1);
				}
			}
		}

		// Token: 0x06006036 RID: 24630 RVA: 0x00241E74 File Offset: 0x00240074
		private async Task InkyLanceMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.InkyLanceDamage).WithHitCount(2).FromMonster(this)
				.WithAttackerAnim("DEBUFF", 0.4f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/vantom/vantom_inky_lance", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			if (TestMode.IsOff && base.Creature.IsAlive)
			{
				NRunMusicController instance = NRunMusicController.Instance;
				if (instance != null)
				{
					instance.UpdateMusicParameter("vantom_progress", 2f);
				}
				await Cmd.CustomScaledWait(1f, 1f, false, default(CancellationToken));
				SfxCmd.Play("event:/sfx/enemy/enemy_attacks/vantom/vantom_extend_2", 1f);
				NCombatRoom instance2 = NCombatRoom.Instance;
				NCreature ncreature = ((instance2 != null) ? instance2.GetCreatureNode(base.Creature) : null);
				MegaAnimationState megaAnimationState;
				if (ncreature == null)
				{
					megaAnimationState = null;
				}
				else
				{
					MegaSprite spineController = ncreature.SpineController;
					megaAnimationState = ((spineController != null) ? spineController.GetAnimationState() : null);
				}
				MegaAnimationState megaAnimationState2 = megaAnimationState;
				if (megaAnimationState2 != null)
				{
					megaAnimationState2.SetAnimation("_tracks/charge_up_3", false, 1);
				}
				if (megaAnimationState2 != null)
				{
					megaAnimationState2.AddAnimation("_tracks/charged_3", 0f, true, 1);
				}
				await CreatureCmd.TriggerAnim(base.Creature, "CHARGE_UP", 0.15f);
			}
		}

		// Token: 0x06006037 RID: 24631 RVA: 0x00241EB8 File Offset: 0x002400B8
		private async Task DismemberMove(IReadOnlyList<Creature> targets)
		{
			if (TestMode.IsOff && base.Creature.IsAlive)
			{
				NCombatRoom instance = NCombatRoom.Instance;
				NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(base.Creature) : null);
				MegaAnimationState megaAnimationState;
				if (ncreature == null)
				{
					megaAnimationState = null;
				}
				else
				{
					MegaSprite spineController = ncreature.SpineController;
					megaAnimationState = ((spineController != null) ? spineController.GetAnimationState() : null);
				}
				MegaAnimationState megaAnimationState2 = megaAnimationState;
				if (megaAnimationState2 != null)
				{
					megaAnimationState2.SetAnimation("_tracks/attack_heavy", false, 1);
				}
				if (megaAnimationState2 != null)
				{
					megaAnimationState2.AddAnimation("_tracks/charged_0", 0f, true, 1);
				}
			}
			NRunMusicController instance2 = NRunMusicController.Instance;
			if (instance2 != null)
			{
				instance2.UpdateMusicParameter("vantom_progress", 3f);
			}
			await CreatureCmd.TriggerAnim(base.Creature, "ATTACK_HEAVY", 0f);
			await Cmd.Wait(0.25f, false);
			NCombatRoom instance3 = NCombatRoom.Instance;
			if (instance3 != null)
			{
				instance3.RadialBlur(VfxPosition.Left);
			}
			NGame instance4 = NGame.Instance;
			if (instance4 != null)
			{
				instance4.ScreenShake(ShakeStrength.Strong, ShakeDuration.Normal, 180f + Rng.Chaotic.NextFloat(-10f, 10f));
			}
			await DamageCmd.Attack(this.DismemberDamage).FromMonster(this).WithNoAttackerAnim()
				.WithHitFx("vfx/vfx_giant_horizontal_slash", "event:/sfx/enemy/enemy_attacks/vantom/vantom_dismember", null)
				.Execute(null);
			NGame.Instance.DoHitStop(ShakeStrength.Weak, ShakeDuration.Short);
			await Cmd.Wait(0.5f, false);
			await CardPileCmd.AddToCombatAndPreview<Wound>(targets, PileType.Discard, 3, false, CardPilePosition.Bottom);
		}

		// Token: 0x06006038 RID: 24632 RVA: 0x00241F04 File Offset: 0x00240104
		private async Task PrepareMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/vantom/vantom_buff", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "BUFF", 0.6f);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 2m, base.Creature, null, false);
			if (TestMode.IsOff && base.Creature.IsAlive)
			{
				await Cmd.CustomScaledWait(1f, 1f, false, default(CancellationToken));
				SfxCmd.Play("event:/sfx/enemy/enemy_attacks/vantom/vantom_extend_1", 1f);
				NCombatRoom instance = NCombatRoom.Instance;
				NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(base.Creature) : null);
				MegaAnimationState megaAnimationState;
				if (ncreature == null)
				{
					megaAnimationState = null;
				}
				else
				{
					MegaSprite spineController = ncreature.SpineController;
					megaAnimationState = ((spineController != null) ? spineController.GetAnimationState() : null);
				}
				MegaAnimationState megaAnimationState2 = megaAnimationState;
				if (megaAnimationState2 != null)
				{
					megaAnimationState2.SetAnimation("_tracks/charge_up_1", false, 1);
				}
				if (megaAnimationState2 != null)
				{
					megaAnimationState2.AddAnimation("_tracks/charged_1", 0f, true, 1);
				}
				await CreatureCmd.TriggerAnim(base.Creature, "CHARGE_UP", 0.25f);
				NRunMusicController instance2 = NRunMusicController.Instance;
				if (instance2 != null)
				{
					instance2.UpdateMusicParameter("vantom_progress", 1f);
				}
			}
		}

		// Token: 0x06006039 RID: 24633 RVA: 0x00241F48 File Offset: 0x00240148
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("buff", false);
			AnimState animState3 = new AnimState("debuff", false);
			AnimState animState4 = new AnimState("attack", false);
			AnimState animState5 = new AnimState("hurt", false);
			AnimState animState6 = new AnimState("die", false);
			AnimState animState7 = new AnimState("charge_up", false);
			AnimState animState8 = new AnimState("attack_heavy", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			animState5.NextState = animState;
			animState7.NextState = animState;
			animState8.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("CHARGE_UP", animState7, null);
			creatureAnimator.AddAnyState("ATTACK_HEAVY", animState8, null);
			creatureAnimator.AddAnyState("BUFF", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState4, null);
			creatureAnimator.AddAnyState("Dead", animState6, null);
			creatureAnimator.AddAnyState("Hit", animState5, null);
			creatureAnimator.AddAnyState("DEBUFF", animState3, null);
			return creatureAnimator;
		}

		// Token: 0x04002434 RID: 9268
		private const string _vantomCustomTrackName = "vantom_progress";

		// Token: 0x04002435 RID: 9269
		private const int _inkyLanceRepeat = 2;

		// Token: 0x04002436 RID: 9270
		private const int _dismemberWounds = 3;

		// Token: 0x04002437 RID: 9271
		private const int _prepareStrength = 2;

		// Token: 0x04002438 RID: 9272
		private const string _chargeUpTrigger = "CHARGE_UP";

		// Token: 0x04002439 RID: 9273
		private const string _buffTrigger = "BUFF";

		// Token: 0x0400243A RID: 9274
		private const string _debuffTrigger = "DEBUFF";

		// Token: 0x0400243B RID: 9275
		private const string _heavyAttackTrigger = "ATTACK_HEAVY";

		// Token: 0x0400243C RID: 9276
		private const string _buffSfx = "event:/sfx/enemy/enemy_attacks/vantom/vantom_buff";

		// Token: 0x0400243D RID: 9277
		private const string _dismemberSfx = "event:/sfx/enemy/enemy_attacks/vantom/vantom_dismember";

		// Token: 0x0400243E RID: 9278
		private const string _extend1Sfx = "event:/sfx/enemy/enemy_attacks/vantom/vantom_extend_1";

		// Token: 0x0400243F RID: 9279
		private const string _extend2Sfx = "event:/sfx/enemy/enemy_attacks/vantom/vantom_extend_2";

		// Token: 0x04002440 RID: 9280
		private const string _extend3Sfx = "event:/sfx/enemy/enemy_attacks/vantom/vantom_extend_2";

		// Token: 0x04002441 RID: 9281
		private const string _inkyLanceSfx = "event:/sfx/enemy/enemy_attacks/vantom/vantom_inky_lance";
	}
}
