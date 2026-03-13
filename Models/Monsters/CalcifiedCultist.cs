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
	// Token: 0x0200073D RID: 1853
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CalcifiedCultist : MonsterModel
	{
		// Token: 0x170014F8 RID: 5368
		// (get) Token: 0x060059A3 RID: 22947 RVA: 0x0022DE8F File Offset: 0x0022C08F
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 39, 38);
			}
		}

		// Token: 0x170014F9 RID: 5369
		// (get) Token: 0x060059A4 RID: 22948 RVA: 0x0022DE9B File Offset: 0x0022C09B
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 42, 41);
			}
		}

		// Token: 0x170014FA RID: 5370
		// (get) Token: 0x060059A5 RID: 22949 RVA: 0x0022DEA7 File Offset: 0x0022C0A7
		private int DarkStrikeDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 11, 9);
			}
		}

		// Token: 0x170014FB RID: 5371
		// (get) Token: 0x060059A6 RID: 22950 RVA: 0x0022DEB4 File Offset: 0x0022C0B4
		private int IncantationAmount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170014FC RID: 5372
		// (get) Token: 0x060059A7 RID: 22951 RVA: 0x0022DEB7 File Offset: 0x0022C0B7
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Fur;
			}
		}

		// Token: 0x170014FD RID: 5373
		// (get) Token: 0x060059A8 RID: 22952 RVA: 0x0022DEBA File Offset: 0x0022C0BA
		public override Vector2 ExtraDeathVfxPadding
		{
			get
			{
				return new Vector2(1.5f, 1.2f);
			}
		}

		// Token: 0x170014FE RID: 5374
		// (get) Token: 0x060059A9 RID: 22953 RVA: 0x0022DECB File Offset: 0x0022C0CB
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/cultists/cultists_die_calcified";
			}
		}

		// Token: 0x170014FF RID: 5375
		// (get) Token: 0x060059AA RID: 22954 RVA: 0x0022DED2 File Offset: 0x0022C0D2
		protected override string AttackSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/cultists/cultists_attack";
			}
		}

		// Token: 0x17001500 RID: 5376
		// (get) Token: 0x060059AB RID: 22955 RVA: 0x0022DED9 File Offset: 0x0022C0D9
		// (set) Token: 0x060059AC RID: 22956 RVA: 0x0022DEE1 File Offset: 0x0022C0E1
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

		// Token: 0x060059AD RID: 22957 RVA: 0x0022DEF0 File Offset: 0x0022C0F0
		public override void SetupSkins(NCreatureVisuals visuals)
		{
			MegaSkeleton skeleton = visuals.SpineBody.GetSkeleton();
			MegaSkin megaSkin = visuals.SpineBody.NewSkin("custom-skin");
			MegaSkeletonDataResource data = skeleton.GetData();
			megaSkin.AddSkin(data.FindSkin("coral"));
			skeleton.SetSkin(megaSkin);
			skeleton.SetSlotsToSetupPose();
		}

		// Token: 0x060059AE RID: 22958 RVA: 0x0022DF40 File Offset: 0x0022C140
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

		// Token: 0x060059AF RID: 22959 RVA: 0x0022DFC8 File Offset: 0x0022C1C8
		private async Task IncantationMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/cultists/cultists_buff_calcified", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.6f);
			TalkCmd.Play(CalcifiedCultist._cawCawDialogue, base.Creature, -1.0, VfxColor.White);
			await PowerCmd.Apply<RitualPower>(base.Creature, this.IncantationAmount, base.Creature, null, false);
		}

		// Token: 0x060059B0 RID: 22960 RVA: 0x0022E00C File Offset: 0x0022C20C
		private async Task DarkStrikeMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.DarkStrikeDamage).FromMonster(this).WithAttackerAnim("Attack", 0.2f, null)
				.BeforeDamage(new Func<Task>(this.PlayAttackSfx))
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x060059B1 RID: 22961 RVA: 0x0022E050 File Offset: 0x0022C250
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

		// Token: 0x060059B2 RID: 22962 RVA: 0x0022E0F3 File Offset: 0x0022C2F3
		private Task PlayAttackSfx()
		{
			SfxCmd.Play(this.AttackSfx, "enemy_strength", this.AttackSfxStrength, 1f);
			this.AttackSfxStrength += 0.2f;
			return Task.CompletedTask;
		}

		// Token: 0x0400229A RID: 8858
		private static readonly LocString _cawCawDialogue = new LocString("monsters", "CALCIFIED_CULTIST.moves.INCANTATION.banter");

		// Token: 0x0400229B RID: 8859
		private const string _buffSfx = "event:/sfx/enemy/enemy_attacks/cultists/cultists_buff_calcified";

		// Token: 0x0400229C RID: 8860
		private float _attackSfxStrength;
	}
}
