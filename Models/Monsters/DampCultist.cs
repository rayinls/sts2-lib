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
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000744 RID: 1860
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DampCultist : MonsterModel
	{
		// Token: 0x17001539 RID: 5433
		// (get) Token: 0x06005A46 RID: 23110 RVA: 0x0022FD13 File Offset: 0x0022DF13
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 52, 51);
			}
		}

		// Token: 0x1700153A RID: 5434
		// (get) Token: 0x06005A47 RID: 23111 RVA: 0x0022FD1F File Offset: 0x0022DF1F
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 54, 53);
			}
		}

		// Token: 0x1700153B RID: 5435
		// (get) Token: 0x06005A48 RID: 23112 RVA: 0x0022FD2B File Offset: 0x0022DF2B
		private int DarkStrikeDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 3, 1);
			}
		}

		// Token: 0x1700153C RID: 5436
		// (get) Token: 0x06005A49 RID: 23113 RVA: 0x0022FD36 File Offset: 0x0022DF36
		private int IncantationAmount
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 6, 5);
			}
		}

		// Token: 0x1700153D RID: 5437
		// (get) Token: 0x06005A4A RID: 23114 RVA: 0x0022FD41 File Offset: 0x0022DF41
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Fur;
			}
		}

		// Token: 0x1700153E RID: 5438
		// (get) Token: 0x06005A4B RID: 23115 RVA: 0x0022FD44 File Offset: 0x0022DF44
		public override Vector2 ExtraDeathVfxPadding
		{
			get
			{
				return new Vector2(1.5f, 1.2f);
			}
		}

		// Token: 0x1700153F RID: 5439
		// (get) Token: 0x06005A4C RID: 23116 RVA: 0x0022FD55 File Offset: 0x0022DF55
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/cultists/cultists_die_damp";
			}
		}

		// Token: 0x17001540 RID: 5440
		// (get) Token: 0x06005A4D RID: 23117 RVA: 0x0022FD5C File Offset: 0x0022DF5C
		// (set) Token: 0x06005A4E RID: 23118 RVA: 0x0022FD64 File Offset: 0x0022DF64
		private float AttackSfxStrength
		{
			get
			{
				return this._attackSfxStrength;
			}
			set
			{
				base.AssertMutable();
				this._attackSfxStrength = value;
			}
		}

		// Token: 0x17001541 RID: 5441
		// (get) Token: 0x06005A4F RID: 23119 RVA: 0x0022FD73 File Offset: 0x0022DF73
		protected override string AttackSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/cultists/cultists_attack";
			}
		}

		// Token: 0x06005A50 RID: 23120 RVA: 0x0022FD7C File Offset: 0x0022DF7C
		public override void SetupSkins(NCreatureVisuals visuals)
		{
			MegaSkeleton skeleton = visuals.SpineBody.GetSkeleton();
			MegaSkin megaSkin = visuals.SpineBody.NewSkin("custom-skin");
			MegaSkeletonDataResource data = skeleton.GetData();
			megaSkin.AddSkin(data.FindSkin("slug"));
			skeleton.SetSkin(megaSkin);
			skeleton.SetSlotsToSetupPose();
		}

		// Token: 0x06005A51 RID: 23121 RVA: 0x0022FDCC File Offset: 0x0022DFCC
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("INCANTATION_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.IncantationMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			MoveState moveState2 = new MoveState("DARK_STRIKE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.DarkStrikeMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.DarkStrikeDamage)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState2;
			list.Add(moveState);
			list.Add(moveState2);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005A52 RID: 23122 RVA: 0x0022FE54 File Offset: 0x0022E054
		private async Task IncantationMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/cultists/cultists_buff_damp", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.6f);
			TalkCmd.Play(DampCultist._cawCawDialogue, base.Creature, -1.0, VfxColor.White);
			await PowerCmd.Apply<RitualPower>(base.Creature, this.IncantationAmount, base.Creature, null, false);
		}

		// Token: 0x06005A53 RID: 23123 RVA: 0x0022FE98 File Offset: 0x0022E098
		private async Task DarkStrikeMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.DarkStrikeDamage).FromMonster(this).WithAttackerAnim("Attack", 0.2f, null)
				.BeforeDamage(new Func<Task>(this.PlayAttackSfx))
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005A54 RID: 23124 RVA: 0x0022FEDC File Offset: 0x0022E0DC
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("buff", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("Dead", animState5, null);
			creatureAnimator.AddAnyState("Hit", animState4, null);
			return creatureAnimator;
		}

		// Token: 0x06005A55 RID: 23125 RVA: 0x0022FF7F File Offset: 0x0022E17F
		private Task PlayAttackSfx()
		{
			SfxCmd.Play(this.AttackSfx, "enemy_strength", this.AttackSfxStrength, 1f);
			this.AttackSfxStrength += 0.2f;
			return Task.CompletedTask;
		}

		// Token: 0x040022C3 RID: 8899
		private static readonly LocString _cawCawDialogue = new LocString("monsters", "DAMP_CULTIST.moves.INCANTATION.banter");

		// Token: 0x040022C4 RID: 8900
		private float _attackSfxStrength;

		// Token: 0x040022C5 RID: 8901
		private const string _buffSfx = "event:/sfx/enemy/enemy_attacks/cultists/cultists_buff_damp";
	}
}
