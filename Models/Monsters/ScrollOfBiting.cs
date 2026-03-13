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
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Random;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200077C RID: 1916
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ScrollOfBiting : MonsterModel
	{
		// Token: 0x170016B0 RID: 5808
		// (get) Token: 0x06005DBB RID: 23995 RVA: 0x0023A6A3 File Offset: 0x002388A3
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 32, 31);
			}
		}

		// Token: 0x170016B1 RID: 5809
		// (get) Token: 0x06005DBC RID: 23996 RVA: 0x0023A6AF File Offset: 0x002388AF
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 39, 38);
			}
		}

		// Token: 0x170016B2 RID: 5810
		// (get) Token: 0x06005DBD RID: 23997 RVA: 0x0023A6BB File Offset: 0x002388BB
		private int ChompDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 16, 14);
			}
		}

		// Token: 0x170016B3 RID: 5811
		// (get) Token: 0x06005DBE RID: 23998 RVA: 0x0023A6C8 File Offset: 0x002388C8
		private int ChewDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 6, 5);
			}
		}

		// Token: 0x170016B4 RID: 5812
		// (get) Token: 0x06005DBF RID: 23999 RVA: 0x0023A6D3 File Offset: 0x002388D3
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Magic;
			}
		}

		// Token: 0x170016B5 RID: 5813
		// (get) Token: 0x06005DC0 RID: 24000 RVA: 0x0023A6D6 File Offset: 0x002388D6
		public override bool HasDeathSfx
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170016B6 RID: 5814
		// (get) Token: 0x06005DC1 RID: 24001 RVA: 0x0023A6D9 File Offset: 0x002388D9
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/scroll_of_biting/scroll_of_biting_die";
			}
		}

		// Token: 0x170016B7 RID: 5815
		// (get) Token: 0x06005DC2 RID: 24002 RVA: 0x0023A6E0 File Offset: 0x002388E0
		// (set) Token: 0x06005DC3 RID: 24003 RVA: 0x0023A6E8 File Offset: 0x002388E8
		public int StarterMoveIdx
		{
			get
			{
				return this._starterMoveIdx;
			}
			set
			{
				base.AssertMutable();
				this._starterMoveIdx = value;
			}
		}

		// Token: 0x06005DC4 RID: 24004 RVA: 0x0023A6F8 File Offset: 0x002388F8
		public override void SetupSkins(NCreatureVisuals visuals)
		{
			MegaSkeleton skeleton = visuals.SpineBody.GetSkeleton();
			MegaSkin megaSkin = visuals.SpineBody.NewSkin("custom-skin");
			MegaSkeletonDataResource data = skeleton.GetData();
			megaSkin.AddSkin(data.FindSkin(Rng.Chaotic.NextItem<string>(ScrollOfBiting._skinOptions)));
			skeleton.SetSkin(megaSkin);
			skeleton.SetSlotsToSetupPose();
		}

		// Token: 0x06005DC5 RID: 24005 RVA: 0x0023A754 File Offset: 0x00238954
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<PaperCutsPower>(base.Creature, 2m, base.Creature, null, false);
		}

		// Token: 0x06005DC6 RID: 24006 RVA: 0x0023A798 File Offset: 0x00238998
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("CHOMP", new Func<IReadOnlyList<Creature>, Task>(this.ChompMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.ChompDamage)
			});
			MoveState moveState2 = new MoveState("CHEW", new Func<IReadOnlyList<Creature>, Task>(this.ChewState), new AbstractIntent[]
			{
				new MultiAttackIntent(this.ChewDamage, 2)
			});
			MoveState moveState3 = new MoveState("MORE_TEETH", new Func<IReadOnlyList<Creature>, Task>(this.MoreTeethMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			RandomBranchState randomBranchState = new RandomBranchState("rand");
			moveState.FollowUpState = moveState3;
			moveState2.FollowUpState = randomBranchState;
			moveState3.FollowUpState = moveState2;
			randomBranchState.AddBranch(moveState, MoveRepeatType.CannotRepeat);
			randomBranchState.AddBranch(moveState2, 2);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(randomBranchState);
			int num = this.StarterMoveIdx % 3;
			if (num == 0)
			{
				return new MonsterMoveStateMachine(list, moveState);
			}
			if (num != 1)
			{
				return new MonsterMoveStateMachine(list, moveState3);
			}
			return new MonsterMoveStateMachine(list, moveState2);
		}

		// Token: 0x06005DC7 RID: 24007 RVA: 0x0023A8A4 File Offset: 0x00238AA4
		private async Task ChompMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.ChompDamage).FromMonster(this).WithAttackerAnim("Attack", 0.2f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/scroll_of_biting/scroll_of_biting_bite", null)
				.WithHitFx("vfx/vfx_bite", null, null)
				.Execute(null);
		}

		// Token: 0x06005DC8 RID: 24008 RVA: 0x0023A8E8 File Offset: 0x00238AE8
		private async Task ChewState(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.ChewDamage).WithHitCount(2).FromMonster(this)
				.WithAttackerAnim("ATTACK_DOUBLE", 0.2f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/scroll_of_biting/scroll_of_biting_bite_double", null)
				.WithHitFx("vfx/vfx_bite", null, null)
				.Execute(null);
		}

		// Token: 0x06005DC9 RID: 24009 RVA: 0x0023A92C File Offset: 0x00238B2C
		private async Task MoreTeethMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/scroll_of_biting/scroll_of_biting_buff", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.8f);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 2m, base.Creature, null, false);
		}

		// Token: 0x06005DCA RID: 24010 RVA: 0x0023A970 File Offset: 0x00238B70
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("buff", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("attack_double", false);
			AnimState animState5 = new AnimState("hurt", false);
			AnimState animState6 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState5.NextState = animState;
			animState4.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("Dead", animState6, null);
			creatureAnimator.AddAnyState("Hit", animState5, null);
			creatureAnimator.AddAnyState("ATTACK_DOUBLE", animState4, null);
			return creatureAnimator;
		}

		// Token: 0x0400239E RID: 9118
		private static readonly string[] _skinOptions = new string[] { "skin1", "skin2" };

		// Token: 0x0400239F RID: 9119
		private const int _chewRepeat = 2;

		// Token: 0x040023A0 RID: 9120
		private const int _buffAmt = 2;

		// Token: 0x040023A1 RID: 9121
		private int _starterMoveIdx;

		// Token: 0x040023A2 RID: 9122
		private const string _attackDoubleTrigger = "ATTACK_DOUBLE";

		// Token: 0x040023A3 RID: 9123
		public const string biteSfx = "event:/sfx/enemy/enemy_attacks/scroll_of_biting/scroll_of_biting_bite";

		// Token: 0x040023A4 RID: 9124
		public const string biteDoubleSfx = "event:/sfx/enemy/enemy_attacks/scroll_of_biting/scroll_of_biting_bite_double";

		// Token: 0x040023A5 RID: 9125
		public const string buffSfx = "event:/sfx/enemy/enemy_attacks/scroll_of_biting/scroll_of_biting_buff";
	}
}
