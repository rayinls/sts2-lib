using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200073B RID: 1851
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Byrdonis : MonsterModel
	{
		// Token: 0x170014EE RID: 5358
		// (get) Token: 0x0600598F RID: 22927 RVA: 0x0022DB6C File Offset: 0x0022BD6C
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 99, 91);
			}
		}

		// Token: 0x170014EF RID: 5359
		// (get) Token: 0x06005990 RID: 22928 RVA: 0x0022DB78 File Offset: 0x0022BD78
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 99, 94);
			}
		}

		// Token: 0x170014F0 RID: 5360
		// (get) Token: 0x06005991 RID: 22929 RVA: 0x0022DB84 File Offset: 0x0022BD84
		private static int PeckDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 4, 3);
			}
		}

		// Token: 0x170014F1 RID: 5361
		// (get) Token: 0x06005992 RID: 22930 RVA: 0x0022DB8F File Offset: 0x0022BD8F
		private static int PeckRepeat
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 3, 3);
			}
		}

		// Token: 0x170014F2 RID: 5362
		// (get) Token: 0x06005993 RID: 22931 RVA: 0x0022DB9A File Offset: 0x0022BD9A
		private static int SwoopDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 18, 16);
			}
		}

		// Token: 0x170014F3 RID: 5363
		// (get) Token: 0x06005994 RID: 22932 RVA: 0x0022DBA7 File Offset: 0x0022BDA7
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/byrdonis/byrdonis_die";
			}
		}

		// Token: 0x170014F4 RID: 5364
		// (get) Token: 0x06005995 RID: 22933 RVA: 0x0022DBAE File Offset: 0x0022BDAE
		public override string TakeDamageSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/byrdonis/byrdonis_hurt";
			}
		}

		// Token: 0x06005996 RID: 22934 RVA: 0x0022DBB8 File Offset: 0x0022BDB8
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<TerritorialPower>(base.Creature, 1m, base.Creature, null, false);
		}

		// Token: 0x06005997 RID: 22935 RVA: 0x0022DBFC File Offset: 0x0022BDFC
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("PECK_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.PeckMove), new AbstractIntent[]
			{
				new MultiAttackIntent(Byrdonis.PeckDamage, Byrdonis.PeckRepeat)
			});
			MoveState moveState2 = new MoveState("SWOOP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SwoopMove), new AbstractIntent[]
			{
				new SingleAttackIntent(Byrdonis.SwoopDamage)
			});
			moveState2.FollowUpState = moveState;
			moveState.FollowUpState = moveState2;
			list.Add(moveState2);
			list.Add(moveState);
			return new MonsterMoveStateMachine(list, moveState2);
		}

		// Token: 0x06005998 RID: 22936 RVA: 0x0022DC8C File Offset: 0x0022BE8C
		private async Task PeckMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(Byrdonis.PeckDamage).WithHitCount(Byrdonis.PeckRepeat).FromMonster(this)
				.WithAttackerAnim("Attack", 0.4f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005999 RID: 22937 RVA: 0x0022DCD0 File Offset: 0x0022BED0
		private async Task SwoopMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(Byrdonis.SwoopDamage).FromMonster(this).WithAttackerAnim("Attack", 0.4f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x0600599A RID: 22938 RVA: 0x0022DD14 File Offset: 0x0022BF14
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("hurt", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("die", false);
			AnimState animState5 = new AnimState("get_angry", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState5.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Angry", animState5, null);
			creatureAnimator.AddAnyState("Dead", animState4, null);
			creatureAnimator.AddAnyState("Hit", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			return creatureAnimator;
		}

		// Token: 0x04002299 RID: 8857
		private const string _angryTrigger = "Angry";
	}
}
