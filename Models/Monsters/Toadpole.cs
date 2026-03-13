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
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Random;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000798 RID: 1944
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Toadpole : MonsterModel
	{
		// Token: 0x1700177A RID: 6010
		// (get) Token: 0x06005F92 RID: 24466 RVA: 0x0023FF2D File Offset: 0x0023E12D
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 22, 21);
			}
		}

		// Token: 0x1700177B RID: 6011
		// (get) Token: 0x06005F93 RID: 24467 RVA: 0x0023FF39 File Offset: 0x0023E139
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 26, 25);
			}
		}

		// Token: 0x1700177C RID: 6012
		// (get) Token: 0x06005F94 RID: 24468 RVA: 0x0023FF45 File Offset: 0x0023E145
		// (set) Token: 0x06005F95 RID: 24469 RVA: 0x0023FF4D File Offset: 0x0023E14D
		public bool IsFront
		{
			get
			{
				return this._isFront;
			}
			set
			{
				base.AssertMutable();
				this._isFront = value;
			}
		}

		// Token: 0x1700177D RID: 6013
		// (get) Token: 0x06005F96 RID: 24470 RVA: 0x0023FF5C File Offset: 0x0023E15C
		private int SpikeSpitDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 4, 3);
			}
		}

		// Token: 0x1700177E RID: 6014
		// (get) Token: 0x06005F97 RID: 24471 RVA: 0x0023FF67 File Offset: 0x0023E167
		private int SpikeSpitRepeat
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700177F RID: 6015
		// (get) Token: 0x06005F98 RID: 24472 RVA: 0x0023FF6A File Offset: 0x0023E16A
		private int WhirlDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 8, 7);
			}
		}

		// Token: 0x17001780 RID: 6016
		// (get) Token: 0x06005F99 RID: 24473 RVA: 0x0023FF75 File Offset: 0x0023E175
		private int SpikenAmount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17001781 RID: 6017
		// (get) Token: 0x06005F9A RID: 24474 RVA: 0x0023FF78 File Offset: 0x0023E178
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Slime;
			}
		}

		// Token: 0x06005F9B RID: 24475 RVA: 0x0023FF7C File Offset: 0x0023E17C
		public override void SetupSkins(NCreatureVisuals visuals)
		{
			MegaSkeleton skeleton = visuals.SpineBody.GetSkeleton();
			MegaSkin megaSkin = visuals.SpineBody.NewSkin("custom-skin");
			MegaSkeletonDataResource data = skeleton.GetData();
			megaSkin.AddSkin(data.FindSkin(Rng.Chaotic.NextItem<string>(Toadpole._eyeOptions)));
			megaSkin.AddSkin(data.FindSkin(Rng.Chaotic.NextItem<string>(Toadpole._patternOptions)));
			skeleton.SetSkin(megaSkin);
			skeleton.SetSlotsToSetupPose();
		}

		// Token: 0x06005F9C RID: 24476 RVA: 0x0023FFF0 File Offset: 0x0023E1F0
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("SPIKE_SPIT_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SpikeSpitMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.SpikeSpitDamage, this.SpikeSpitRepeat)
			});
			MoveState moveState2 = new MoveState("WHIRL_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.WhirlMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.WhirlDamage)
			});
			MoveState moveState3 = new MoveState("SPIKEN_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SpikenMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			ConditionalBranchState conditionalBranchState = new ConditionalBranchState("INIT_MOVE");
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState;
			moveState.FollowUpState = moveState2;
			conditionalBranchState.AddState(moveState2, () => !((Toadpole)base.Creature.Monster).IsFront);
			conditionalBranchState.AddState(moveState3, () => ((Toadpole)base.Creature.Monster).IsFront);
			list.Add(conditionalBranchState);
			list.Add(moveState3);
			list.Add(moveState);
			list.Add(moveState2);
			return new MonsterMoveStateMachine(list, conditionalBranchState);
		}

		// Token: 0x06005F9D RID: 24477 RVA: 0x002400F4 File Offset: 0x0023E2F4
		private async Task SpikeSpitMove(IReadOnlyList<Creature> targets)
		{
			await PowerCmd.Apply<ThornsPower>(base.Creature, -this.SpikenAmount, base.Creature, null, false);
			await DamageCmd.Attack(this.SpikeSpitDamage).WithHitCount(this.SpikeSpitRepeat).FromMonster(this)
				.WithAttackerAnim("AttackTriple", 0.3f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/toadpole/toadpole_attack_spin", null)
				.WithHitFx("vfx/vfx_attack_blunt", this.AttackSfx, null)
				.Execute(null);
		}

		// Token: 0x06005F9E RID: 24478 RVA: 0x00240138 File Offset: 0x0023E338
		private async Task WhirlMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.WhirlDamage).FromMonster(this).WithAttackerAnim("AttackSingle", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", this.AttackSfx, null)
				.Execute(null);
		}

		// Token: 0x06005F9F RID: 24479 RVA: 0x0024017C File Offset: 0x0023E37C
		private async Task SpikenMove(IReadOnlyList<Creature> targets)
		{
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.2f);
			await PowerCmd.Apply<ThornsPower>(base.Creature, this.SpikenAmount, base.Creature, null, false);
		}

		// Token: 0x06005FA0 RID: 24480 RVA: 0x002401C0 File Offset: 0x0023E3C0
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("buff", false);
			AnimState animState3 = new AnimState("attack_single", false);
			AnimState animState4 = new AnimState("attack_triple", false);
			AnimState animState5 = new AnimState("hurt", false);
			AnimState animState6 = new AnimState("die", false);
			AnimState animState7 = new AnimState("idle_loop_buffed", true);
			AnimState animState8 = new AnimState("attack_single_buffed", false);
			AnimState animState9 = new AnimState("attack_triple", false);
			AnimState animState10 = new AnimState("hurt_buffed", false);
			AnimState animState11 = new AnimState("die_buffed", false);
			animState2.NextState = animState7;
			animState3.NextState = animState;
			animState4.NextState = animState;
			animState5.NextState = animState;
			animState8.NextState = animState7;
			animState9.NextState = animState;
			animState10.NextState = animState7;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("AttackSingle", animState3, () => !base.Creature.HasPower<ThornsPower>());
			creatureAnimator.AddAnyState("AttackTriple", animState4, () => !base.Creature.HasPower<ThornsPower>());
			creatureAnimator.AddAnyState("Dead", animState6, () => !base.Creature.HasPower<ThornsPower>());
			creatureAnimator.AddAnyState("Hit", animState5, () => !base.Creature.HasPower<ThornsPower>());
			creatureAnimator.AddAnyState("AttackSingle", animState8, () => base.Creature.HasPower<ThornsPower>());
			creatureAnimator.AddAnyState("AttackTriple", animState9, () => base.Creature.HasPower<ThornsPower>());
			creatureAnimator.AddAnyState("Dead", animState11, () => base.Creature.HasPower<ThornsPower>());
			creatureAnimator.AddAnyState("Hit", animState10, () => base.Creature.HasPower<ThornsPower>());
			return creatureAnimator;
		}

		// Token: 0x0400240C RID: 9228
		private static readonly string[] _eyeOptions = new string[] { "eye1", "eye2" };

		// Token: 0x0400240D RID: 9229
		private static readonly string[] _patternOptions = new string[] { "pattern1", "pattern2" };

		// Token: 0x0400240E RID: 9230
		private bool _isFront;

		// Token: 0x0400240F RID: 9231
		private const string _attackSingleTrigger = "AttackSingle";

		// Token: 0x04002410 RID: 9232
		private const string _attackTripleTrigger = "AttackTriple";

		// Token: 0x04002411 RID: 9233
		private const string _spinAttackSfx = "event:/sfx/enemy/enemy_attacks/toadpole/toadpole_attack_spin";
	}
}
