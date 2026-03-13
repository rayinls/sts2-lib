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

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000737 RID: 1847
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BowlbugRock : MonsterModel
	{
		// Token: 0x170014DB RID: 5339
		// (get) Token: 0x0600595C RID: 22876 RVA: 0x0022D197 File Offset: 0x0022B397
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 46, 45);
			}
		}

		// Token: 0x170014DC RID: 5340
		// (get) Token: 0x0600595D RID: 22877 RVA: 0x0022D1A3 File Offset: 0x0022B3A3
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 49, 48);
			}
		}

		// Token: 0x170014DD RID: 5341
		// (get) Token: 0x0600595E RID: 22878 RVA: 0x0022D1AF File Offset: 0x0022B3AF
		public static int HeadbuttDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 16, 15);
			}
		}

		// Token: 0x170014DE RID: 5342
		// (get) Token: 0x0600595F RID: 22879 RVA: 0x0022D1BC File Offset: 0x0022B3BC
		// (set) Token: 0x06005960 RID: 22880 RVA: 0x0022D1C4 File Offset: 0x0022B3C4
		public bool IsOffBalance
		{
			get
			{
				return this._isOffBalance;
			}
			set
			{
				base.AssertMutable();
				this._isOffBalance = value;
			}
		}

		// Token: 0x170014DF RID: 5343
		// (get) Token: 0x06005961 RID: 22881 RVA: 0x0022D1D3 File Offset: 0x0022B3D3
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/workbug_rock/workbug_rock_die";
			}
		}

		// Token: 0x170014E0 RID: 5344
		// (get) Token: 0x06005962 RID: 22882 RVA: 0x0022D1DA File Offset: 0x0022B3DA
		protected override string AttackSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/workbug_rock/workbug_rock_attack";
			}
		}

		// Token: 0x170014E1 RID: 5345
		// (get) Token: 0x06005963 RID: 22883 RVA: 0x0022D1E1 File Offset: 0x0022B3E1
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Insect;
			}
		}

		// Token: 0x06005964 RID: 22884 RVA: 0x0022D1E4 File Offset: 0x0022B3E4
		public override void SetupSkins(NCreatureVisuals visuals)
		{
			MegaSkeleton skeleton = visuals.SpineBody.GetSkeleton();
			skeleton.SetSkin(skeleton.GetData().FindSkin("rock"));
			skeleton.SetSlotsToSetupPose();
		}

		// Token: 0x06005965 RID: 22885 RVA: 0x0022D21C File Offset: 0x0022B41C
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<ImbalancedPower>(base.Creature, 1m, base.Creature, null, false);
		}

		// Token: 0x06005966 RID: 22886 RVA: 0x0022D260 File Offset: 0x0022B460
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("HEADBUTT_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.HeadbuttMove), new AbstractIntent[]
			{
				new SingleAttackIntent(BowlbugRock.HeadbuttDamage)
			});
			MoveState moveState2 = new MoveState("DIZZY_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.DizzyMove), new AbstractIntent[]
			{
				new StunIntent()
			});
			ConditionalBranchState conditionalBranchState = new ConditionalBranchState("POST_HEADBUTT");
			moveState.FollowUpState = conditionalBranchState;
			moveState2.FollowUpState = moveState;
			conditionalBranchState.AddState(moveState2, () => this.IsOffBalance);
			conditionalBranchState.AddState(moveState, () => !this.IsOffBalance);
			list.Add(moveState2);
			list.Add(conditionalBranchState);
			list.Add(moveState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005967 RID: 22887 RVA: 0x0022D320 File Offset: 0x0022B520
		private async Task HeadbuttMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(BowlbugRock.HeadbuttDamage).FromMonster(this).WithAttackerAnim("Attack", 0.3f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			if (this.IsOffBalance)
			{
				SfxCmd.Play("event:/sfx/enemy/enemy_attacks/workbug_rock/workbug_rock_stun", 1f);
				await CreatureCmd.TriggerAnim(base.Creature, "Stun", 0.6f);
			}
		}

		// Token: 0x06005968 RID: 22888 RVA: 0x0022D364 File Offset: 0x0022B564
		private async Task DizzyMove(IReadOnlyList<Creature> targets)
		{
			this.IsOffBalance = false;
			await CreatureCmd.TriggerAnim(base.Creature, "Unstun", 0.6f);
		}

		// Token: 0x06005969 RID: 22889 RVA: 0x0022D3A8 File Offset: 0x0022B5A8
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("buff", false);
			AnimState animState3 = new AnimState("headbutt", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("hurt_stunned", false);
			AnimState animState6 = new AnimState("die", false);
			AnimState animState7 = new AnimState("stun", false);
			AnimState animState8 = new AnimState("stunned_loop", true);
			animState2.NextState = animState;
			animState4.NextState = animState;
			animState3.NextState = animState;
			animState7.NextState = animState8;
			animState5.NextState = animState8;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Dead", animState6, null);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("Hit", animState4, () => !this.IsOffBalance);
			creatureAnimator.AddAnyState("Hit", animState5, () => this.IsOffBalance);
			creatureAnimator.AddAnyState("Stun", animState7, null);
			creatureAnimator.AddAnyState("Unstun", animState, null);
			return creatureAnimator;
		}

		// Token: 0x04002290 RID: 8848
		private const string _stunTrigger = "Stun";

		// Token: 0x04002291 RID: 8849
		private const string _unstunTrigger = "Unstun";

		// Token: 0x04002292 RID: 8850
		private bool _isOffBalance;

		// Token: 0x04002293 RID: 8851
		private const string _stunSfx = "event:/sfx/enemy/enemy_attacks/workbug_rock/workbug_rock_stun";

		// Token: 0x04002294 RID: 8852
		private const string _spineSkin = "rock";
	}
}
