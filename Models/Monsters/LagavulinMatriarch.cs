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
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000763 RID: 1891
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LagavulinMatriarch : MonsterModel
	{
		// Token: 0x17001609 RID: 5641
		// (get) Token: 0x06005C24 RID: 23588 RVA: 0x00235A96 File Offset: 0x00233C96
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 233, 222);
			}
		}

		// Token: 0x1700160A RID: 5642
		// (get) Token: 0x06005C25 RID: 23589 RVA: 0x00235AA8 File Offset: 0x00233CA8
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x1700160B RID: 5643
		// (get) Token: 0x06005C26 RID: 23590 RVA: 0x00235AB0 File Offset: 0x00233CB0
		private int SlashDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 21, 19);
			}
		}

		// Token: 0x1700160C RID: 5644
		// (get) Token: 0x06005C27 RID: 23591 RVA: 0x00235ABD File Offset: 0x00233CBD
		private int Slash2Damage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 14, 12);
			}
		}

		// Token: 0x1700160D RID: 5645
		// (get) Token: 0x06005C28 RID: 23592 RVA: 0x00235ACA File Offset: 0x00233CCA
		private int Slash2Block
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 14, 12);
			}
		}

		// Token: 0x1700160E RID: 5646
		// (get) Token: 0x06005C29 RID: 23593 RVA: 0x00235AD6 File Offset: 0x00233CD6
		private int DisembowelDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 10, 9);
			}
		}

		// Token: 0x1700160F RID: 5647
		// (get) Token: 0x06005C2A RID: 23594 RVA: 0x00235AE3 File Offset: 0x00233CE3
		private int DisembowelRepeat
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17001610 RID: 5648
		// (get) Token: 0x06005C2B RID: 23595 RVA: 0x00235AE6 File Offset: 0x00233CE6
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.ArmorBig;
			}
		}

		// Token: 0x17001611 RID: 5649
		// (get) Token: 0x06005C2C RID: 23596 RVA: 0x00235AE9 File Offset: 0x00233CE9
		// (set) Token: 0x06005C2D RID: 23597 RVA: 0x00235AF1 File Offset: 0x00233CF1
		public bool IsAwake
		{
			get
			{
				return this._isAwake;
			}
			set
			{
				base.AssertMutable();
				this._isAwake = value;
			}
		}

		// Token: 0x17001612 RID: 5650
		// (get) Token: 0x06005C2E RID: 23598 RVA: 0x00235B00 File Offset: 0x00233D00
		// (set) Token: 0x06005C2F RID: 23599 RVA: 0x00235B08 File Offset: 0x00233D08
		[Nullable(2)]
		private NSleepingVfx SleepingVfx
		{
			[NullableContext(2)]
			get
			{
				return this._sleepingVfx;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				this._sleepingVfx = value;
			}
		}

		// Token: 0x06005C30 RID: 23600 RVA: 0x00235B17 File Offset: 0x00233D17
		public override void SetupSkins(NCreatureVisuals visuals)
		{
			visuals.SpineBody.GetAnimationState().SetAnimation("_tracks/eyes_closed_loop", true, 1);
		}

		// Token: 0x06005C31 RID: 23601 RVA: 0x00235B34 File Offset: 0x00233D34
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await CreatureCmd.TriggerAnim(base.Creature, "Sleep", 0f);
			await PowerCmd.Apply<PlatingPower>(base.Creature, 12m, base.Creature, null, false);
			await PowerCmd.Apply<AsleepPower>(base.Creature, 3m, base.Creature, null, false);
			base.Creature.Died += this.AfterDeath;
			base.Creature.CurrentHpChanged += this.AfterHpChanged;
			base.Creature.CurrentHpChanged += this.AfterWokenUp;
			NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(base.Creature);
			Marker2D marker2D = ((creatureNode != null) ? creatureNode.GetSpecialNode<Marker2D>("%SleepVfxPos") : null);
			if (marker2D != null)
			{
				this.SleepingVfx = NSleepingVfx.Create(marker2D.GlobalPosition, true);
				marker2D.AddChildSafely(this.SleepingVfx);
				this.SleepingVfx.Position = Vector2.Zero;
			}
		}

		// Token: 0x06005C32 RID: 23602 RVA: 0x00235B77 File Offset: 0x00233D77
		private void AfterWokenUp(int _, int __)
		{
			base.Creature.CurrentHpChanged -= this.AfterWokenUp;
			NSleepingVfx sleepingVfx = this.SleepingVfx;
			if (sleepingVfx != null)
			{
				sleepingVfx.Stop();
			}
			this.SleepingVfx = null;
		}

		// Token: 0x06005C33 RID: 23603 RVA: 0x00235BA8 File Offset: 0x00233DA8
		private void AfterHpChanged(int _, int __)
		{
			if (base.Creature.CurrentHp > base.Creature.MaxHp / 2)
			{
				return;
			}
			base.Creature.CurrentHpChanged -= this.AfterHpChanged;
			NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(base.Creature);
			if (creatureNode != null)
			{
				creatureNode.SpineController.GetAnimationState().SetAnimation("_tracks/eyes_open", false, 1);
			}
			if (creatureNode != null)
			{
				creatureNode.SpineController.GetAnimationState().AddAnimation("_tracks/eyes_open_loop", 0f, true, 1);
			}
		}

		// Token: 0x06005C34 RID: 23604 RVA: 0x00235C34 File Offset: 0x00233E34
		private void AfterDeath(Creature _)
		{
			base.Creature.Died -= this.AfterDeath;
			NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(base.Creature);
			if (creatureNode != null)
			{
				creatureNode.SpineController.GetAnimationState().SetAnimation("_tracks/eyes_dead", false, 1);
			}
			NSleepingVfx sleepingVfx = this.SleepingVfx;
			if (sleepingVfx != null)
			{
				sleepingVfx.Stop();
			}
			this.SleepingVfx = null;
		}

		// Token: 0x06005C35 RID: 23605 RVA: 0x00235C9C File Offset: 0x00233E9C
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("SLEEP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SleepMove), new AbstractIntent[]
			{
				new SleepIntent()
			});
			MoveState moveState2 = new MoveState("SLASH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SlashMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SlashDamage)
			});
			MoveState moveState3 = new MoveState("SLASH2_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.Slash2Move), new AbstractIntent[]
			{
				new SingleAttackIntent(this.Slash2Damage),
				new DefendIntent()
			});
			MoveState moveState4 = new MoveState("DISEMBOWEL_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.DisembowelMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.DisembowelDamage, this.DisembowelRepeat)
			});
			MoveState moveState5 = new MoveState("SOUL_SIPHON_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SoulSiphonMove), new AbstractIntent[]
			{
				new DebuffIntent(false),
				new BuffIntent()
			});
			ConditionalBranchState conditionalBranchState = new ConditionalBranchState("SLEEP_BRANCH");
			moveState.FollowUpState = conditionalBranchState;
			moveState2.FollowUpState = moveState4;
			moveState4.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState5;
			moveState5.FollowUpState = moveState2;
			conditionalBranchState.AddState(moveState, () => base.Creature.HasPower<AsleepPower>());
			conditionalBranchState.AddState(moveState2, () => !base.Creature.HasPower<AsleepPower>());
			list.Add(conditionalBranchState);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(moveState5);
			list.Add(moveState4);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005C36 RID: 23606 RVA: 0x00235E23 File Offset: 0x00234023
		private Task SleepMove(IReadOnlyList<Creature> targets)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06005C37 RID: 23607 RVA: 0x00235E2C File Offset: 0x0023402C
		public async Task WakeUpMove(IReadOnlyList<Creature> _)
		{
			if (!this._isAwake)
			{
				SfxCmd.Play("event:/sfx/enemy/enemy_attacks/lagavulin_matriarch/lagavulin_matriarch_awaken", 1f);
				await CreatureCmd.TriggerAnim(base.Creature, "Wake", 0.6f);
				NSleepingVfx sleepingVfx = this.SleepingVfx;
				if (sleepingVfx != null)
				{
					sleepingVfx.Stop();
				}
				this.SleepingVfx = null;
				this.IsAwake = true;
			}
		}

		// Token: 0x06005C38 RID: 23608 RVA: 0x00235E70 File Offset: 0x00234070
		private async Task SlashMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SlashDamage).FromMonster(this).WithAttackerAnim("AttackHeavy", 0.3f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/lagavulin_matriarch/lagavulin_matriarch_slam", null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005C39 RID: 23609 RVA: 0x00235EB4 File Offset: 0x002340B4
		private async Task Slash2Move(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.Slash2Damage).FromMonster(this).WithAttackerAnim("AttackHeavy", 0.2f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/lagavulin_matriarch/lagavulin_matriarch_slam", null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			await CreatureCmd.GainBlock(base.Creature, this.Slash2Block, ValueProp.Move, null, false);
		}

		// Token: 0x06005C3A RID: 23610 RVA: 0x00235EF8 File Offset: 0x002340F8
		private async Task DisembowelMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.DisembowelDamage).WithHitCount(this.DisembowelRepeat).FromMonster(this)
				.WithAttackerAnim("AttackDouble", 0.15f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/lagavulin_matriarch/lagavulin_matriarch_attack_stab", null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005C3B RID: 23611 RVA: 0x00235F3C File Offset: 0x0023413C
		private async Task SoulSiphonMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/lagavulin_matriarch/lagavulin_matriarch_cast", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.6f);
			await PowerCmd.Apply<StrengthPower>(targets, -2m, base.Creature, null, false);
			await PowerCmd.Apply<DexterityPower>(targets, -2m, base.Creature, null, false);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 2m, base.Creature, null, false);
		}

		// Token: 0x06005C3C RID: 23612 RVA: 0x00235F88 File Offset: 0x00234188
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("sleep_loop", true);
			AnimState animState2 = new AnimState("hurt_sleeping", false);
			AnimState animState3 = new AnimState("wake_up", false);
			AnimState animState4 = new AnimState("idle_loop", true);
			AnimState animState5 = new AnimState("cast", false);
			AnimState animState6 = new AnimState("attack_heavy", false);
			AnimState animState7 = new AnimState("attack_double", false);
			AnimState animState8 = new AnimState("hurt", false);
			AnimState animState9 = new AnimState("die", false);
			animState2.NextState = animState3;
			animState3.NextState = animState4;
			animState5.NextState = animState4;
			animState6.NextState = animState4;
			animState7.NextState = animState4;
			animState8.NextState = animState4;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState4, controller);
			creatureAnimator.AddAnyState("Sleep", animState, null);
			creatureAnimator.AddAnyState("Wake", animState3, () => !this.IsAwake);
			creatureAnimator.AddAnyState("Cast", animState5, null);
			creatureAnimator.AddAnyState("AttackHeavy", animState6, null);
			creatureAnimator.AddAnyState("AttackDouble", animState7, null);
			creatureAnimator.AddAnyState("Dead", animState9, null);
			creatureAnimator.AddAnyState("Hit", animState8, () => this.IsAwake);
			creatureAnimator.AddAnyState("Hit", animState2, () => !this.IsAwake);
			return creatureAnimator;
		}

		// Token: 0x0400233E RID: 9022
		public const string slashMoveId = "SLASH_MOVE";

		// Token: 0x0400233F RID: 9023
		private const string _sleepTrigger = "Sleep";

		// Token: 0x04002340 RID: 9024
		public const string wakeTrigger = "Wake";

		// Token: 0x04002341 RID: 9025
		private const string _attackHeavyTrigger = "AttackHeavy";

		// Token: 0x04002342 RID: 9026
		private const string _attackDoubleTrigger = "AttackDouble";

		// Token: 0x04002343 RID: 9027
		private const string _slamSfx = "event:/sfx/enemy/enemy_attacks/lagavulin_matriarch/lagavulin_matriarch_slam";

		// Token: 0x04002344 RID: 9028
		private const string _castSfx = "event:/sfx/enemy/enemy_attacks/lagavulin_matriarch/lagavulin_matriarch_cast";

		// Token: 0x04002345 RID: 9029
		public const string awakenSfx = "event:/sfx/enemy/enemy_attacks/lagavulin_matriarch/lagavulin_matriarch_awaken";

		// Token: 0x04002346 RID: 9030
		private const string _attackStabSfx = "event:/sfx/enemy/enemy_attacks/lagavulin_matriarch/lagavulin_matriarch_attack_stab";

		// Token: 0x04002347 RID: 9031
		private bool _isAwake;

		// Token: 0x04002348 RID: 9032
		[Nullable(2)]
		private NSleepingVfx _sleepingVfx;
	}
}
