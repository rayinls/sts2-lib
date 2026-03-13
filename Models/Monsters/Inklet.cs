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
	// Token: 0x0200075F RID: 1887
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Inklet : MonsterModel
	{
		// Token: 0x170015E3 RID: 5603
		// (get) Token: 0x06005BCA RID: 23498 RVA: 0x00234807 File Offset: 0x00232A07
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 12, 11);
			}
		}

		// Token: 0x170015E4 RID: 5604
		// (get) Token: 0x06005BCB RID: 23499 RVA: 0x00234813 File Offset: 0x00232A13
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 18, 17);
			}
		}

		// Token: 0x170015E5 RID: 5605
		// (get) Token: 0x06005BCC RID: 23500 RVA: 0x0023481F File Offset: 0x00232A1F
		private int JabDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 4, 3);
			}
		}

		// Token: 0x170015E6 RID: 5606
		// (get) Token: 0x06005BCD RID: 23501 RVA: 0x0023482A File Offset: 0x00232A2A
		private int WhirlwindDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 3, 2);
			}
		}

		// Token: 0x170015E7 RID: 5607
		// (get) Token: 0x06005BCE RID: 23502 RVA: 0x00234835 File Offset: 0x00232A35
		private int PiercingGazeDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 11, 10);
			}
		}

		// Token: 0x170015E8 RID: 5608
		// (get) Token: 0x06005BCF RID: 23503 RVA: 0x00234842 File Offset: 0x00232A42
		// (set) Token: 0x06005BD0 RID: 23504 RVA: 0x0023484A File Offset: 0x00232A4A
		public bool MiddleInklet
		{
			get
			{
				return this._middleInklet;
			}
			set
			{
				base.AssertMutable();
				this._middleInklet = value;
			}
		}

		// Token: 0x170015E9 RID: 5609
		// (get) Token: 0x06005BD1 RID: 23505 RVA: 0x00234859 File Offset: 0x00232A59
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Magic;
			}
		}

		// Token: 0x170015EA RID: 5610
		// (get) Token: 0x06005BD2 RID: 23506 RVA: 0x0023485C File Offset: 0x00232A5C
		public override string HurtSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/inklet/inklet_hurt";
			}
		}

		// Token: 0x06005BD3 RID: 23507 RVA: 0x00234864 File Offset: 0x00232A64
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<SlipperyPower>(base.Creature, 1m, base.Creature, null, false);
		}

		// Token: 0x06005BD4 RID: 23508 RVA: 0x002348A8 File Offset: 0x00232AA8
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("JAB_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.JabMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.JabDamage)
			});
			MoveState moveState2 = new MoveState("WHIRLWIND_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.WhirlwindMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.WhirlwindDamage, 3)
			});
			MoveState moveState3 = new MoveState("PIERCING_GAZE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.PiercingGazeMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.PiercingGazeDamage)
			});
			RandomBranchState randomBranchState = new RandomBranchState("INIT_RAND");
			RandomBranchState randomBranchState2 = new RandomBranchState("RAND");
			moveState.FollowUpState = randomBranchState2;
			moveState3.FollowUpState = randomBranchState2;
			moveState2.FollowUpState = randomBranchState2;
			randomBranchState.AddBranch(moveState, 2, 1f);
			randomBranchState.AddBranch(moveState2, MoveRepeatType.CannotRepeat, 1f);
			randomBranchState2.AddBranch(moveState3, MoveRepeatType.CannotRepeat, 1f);
			randomBranchState2.AddBranch(moveState2, MoveRepeatType.CannotRepeat, 1f);
			moveState.FollowUpState = randomBranchState2;
			moveState2.FollowUpState = moveState;
			moveState3.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState3);
			list.Add(moveState2);
			list.Add(randomBranchState2);
			MoveState moveState4 = (this._middleInklet ? moveState2 : moveState);
			return new MonsterMoveStateMachine(list, moveState4);
		}

		// Token: 0x06005BD5 RID: 23509 RVA: 0x002349F0 File Offset: 0x00232BF0
		private async Task JabMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.JabDamage).FromMonster(this).WithAttackerAnim("Attack", 0.75f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005BD6 RID: 23510 RVA: 0x00234A34 File Offset: 0x00232C34
		private async Task WhirlwindMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.WhirlwindDamage).WithHitCount(3).FromMonster(this)
				.WithAttackerAnim("TRIPLE_ATTACK", 0.3f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/inklet/inklet_attack_triple", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005BD7 RID: 23511 RVA: 0x00234A78 File Offset: 0x00232C78
		private async Task PiercingGazeMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.PiercingGazeDamage).FromMonster(this).WithAttackerAnim("Attack", 0.75f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005BD8 RID: 23512 RVA: 0x00234ABC File Offset: 0x00232CBC
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("cast", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("attack_fast", false);
			AnimState animState5 = new AnimState("hurt", false);
			AnimState animState6 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState5.NextState = animState;
			animState4.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("TRIPLE_ATTACK", animState4, null);
			creatureAnimator.AddAnyState("Dead", animState6, null);
			creatureAnimator.AddAnyState("Hit", animState5, null);
			return creatureAnimator;
		}

		// Token: 0x04002314 RID: 8980
		private const string _attackTripleTrigger = "TRIPLE_ATTACK";

		// Token: 0x04002315 RID: 8981
		private const int _whirlwindRepeat = 3;

		// Token: 0x04002316 RID: 8982
		private bool _middleInklet;

		// Token: 0x04002317 RID: 8983
		private const string _attackTripleSfx = "event:/sfx/enemy/enemy_attacks/inklet/inklet_attack_triple";
	}
}
