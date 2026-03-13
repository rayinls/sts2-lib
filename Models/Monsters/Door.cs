using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200074A RID: 1866
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Door : MonsterModel
	{
		// Token: 0x17001556 RID: 5462
		// (get) Token: 0x06005A85 RID: 23173 RVA: 0x002306F1 File Offset: 0x0022E8F1
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 165, 155);
			}
		}

		// Token: 0x17001557 RID: 5463
		// (get) Token: 0x06005A86 RID: 23174 RVA: 0x00230703 File Offset: 0x0022E903
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001558 RID: 5464
		// (get) Token: 0x06005A87 RID: 23175 RVA: 0x0023070B File Offset: 0x0022E90B
		// (set) Token: 0x06005A88 RID: 23176 RVA: 0x00230713 File Offset: 0x0022E913
		public MoveState DeadState
		{
			get
			{
				return this._deadState;
			}
			private set
			{
				base.AssertMutable();
				this._deadState = value;
			}
		}

		// Token: 0x17001559 RID: 5465
		// (get) Token: 0x06005A89 RID: 23177 RVA: 0x00230722 File Offset: 0x0022E922
		private int DramaticOpenDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 28, 25);
			}
		}

		// Token: 0x1700155A RID: 5466
		// (get) Token: 0x06005A8A RID: 23178 RVA: 0x0023072F File Offset: 0x0022E92F
		private int EnforceDamage
		{
			get
			{
				return 20;
			}
		}

		// Token: 0x1700155B RID: 5467
		// (get) Token: 0x06005A8B RID: 23179 RVA: 0x00230733 File Offset: 0x0022E933
		private int EnforceStrength
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 4, 3);
			}
		}

		// Token: 0x1700155C RID: 5468
		// (get) Token: 0x06005A8C RID: 23180 RVA: 0x0023073E File Offset: 0x0022E93E
		private int DoorSlamDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 15, 15);
			}
		}

		// Token: 0x1700155D RID: 5469
		// (get) Token: 0x06005A8D RID: 23181 RVA: 0x0023074B File Offset: 0x0022E94B
		private int DoorSlamRepeat
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700155E RID: 5470
		// (get) Token: 0x06005A8E RID: 23182 RVA: 0x0023074E File Offset: 0x0022E94E
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.ArmorBig;
			}
		}

		// Token: 0x1700155F RID: 5471
		// (get) Token: 0x06005A8F RID: 23183 RVA: 0x00230751 File Offset: 0x0022E951
		public override bool ShouldDisappearFromDoom
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001560 RID: 5472
		// (get) Token: 0x06005A90 RID: 23184 RVA: 0x00230754 File Offset: 0x0022E954
		// (set) Token: 0x06005A91 RID: 23185 RVA: 0x00230766 File Offset: 0x0022E966
		public Creature Doormaker
		{
			get
			{
				Creature doormaker = this._doormaker;
				if (doormaker == null)
				{
					throw new InvalidOperationException();
				}
				return doormaker;
			}
			private set
			{
				base.AssertMutable();
				this._doormaker = value;
			}
		}

		// Token: 0x06005A92 RID: 23186 RVA: 0x00230778 File Offset: 0x0022E978
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<DoorRevivalPower>(base.Creature, 1m, base.Creature, null, false);
			Doormaker doormaker = (Doormaker)ModelDb.Monster<Doormaker>().ToMutable();
			this.Doormaker = base.CombatState.CreateCreature(doormaker, CombatSide.Enemy, "doormaker");
		}

		// Token: 0x06005A93 RID: 23187 RVA: 0x002307BB File Offset: 0x0022E9BB
		public void PrepareForRevival()
		{
			if (this._dramaticOpenMove != null)
			{
				this.DeadState.FollowUpState = this._dramaticOpenMove;
			}
		}

		// Token: 0x06005A94 RID: 23188 RVA: 0x002307D6 File Offset: 0x0022E9D6
		public void PrepareForDeath()
		{
			this.DeadState.FollowUpState = this.DeadState;
		}

		// Token: 0x06005A95 RID: 23189 RVA: 0x002307EC File Offset: 0x0022E9EC
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			this._dramaticOpenMove = new MoveState("DRAMATIC_OPEN_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.DramaticOpenMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.DramaticOpenDamage)
			});
			MoveState dramaticOpenMove = this._dramaticOpenMove;
			MoveState moveState = new MoveState("ENFORCE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.EnforceMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.EnforceDamage),
				new BuffIntent()
			});
			MoveState moveState2 = new MoveState("DOOR_SLAM_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.DoorSlamMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.DoorSlamDamage, this.DoorSlamRepeat)
			});
			this.DeadState = new MoveState("DEAD_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.DeadMove), Array.Empty<AbstractIntent>());
			this.DeadState.FollowUpState = this.DeadState;
			dramaticOpenMove.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState;
			moveState.FollowUpState = dramaticOpenMove;
			list.Add(dramaticOpenMove);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(this.DeadState);
			return new MonsterMoveStateMachine(list, dramaticOpenMove);
		}

		// Token: 0x06005A96 RID: 23190 RVA: 0x00230909 File Offset: 0x0022EB09
		private Task DeadMove(IReadOnlyList<Creature> targets)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06005A97 RID: 23191 RVA: 0x00230910 File Offset: 0x0022EB10
		private async Task DramaticOpenMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.DramaticOpenDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005A98 RID: 23192 RVA: 0x00230954 File Offset: 0x0022EB54
		private async Task EnforceMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.EnforceDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			await PowerCmd.Apply<StrengthPower>(base.Creature, this.EnforceStrength, base.Creature, null, false);
		}

		// Token: 0x06005A99 RID: 23193 RVA: 0x00230998 File Offset: 0x0022EB98
		private async Task DoorSlamMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.DoorSlamDamage).WithHitCount(this.DoorSlamRepeat).FromMonster(this)
				.WithAttackerAnim("Attack", 0.15f, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005A9A RID: 23194 RVA: 0x002309DC File Offset: 0x0022EBDC
		public void Close()
		{
			NCombatRoom instance = NCombatRoom.Instance;
			NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(base.Creature) : null);
			if (ncreature != null)
			{
				ncreature.SetVisible(true);
			}
		}

		// Token: 0x06005A9B RID: 23195 RVA: 0x00230A0C File Offset: 0x0022EC0C
		public void Open()
		{
			NCombatRoom instance = NCombatRoom.Instance;
			NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(base.Creature) : null);
			if (ncreature != null)
			{
				ncreature.SetVisible(false);
			}
		}

		// Token: 0x040022CF RID: 8911
		public const string initialMoveId = "DRAMATIC_OPEN_MOVE";

		// Token: 0x040022D0 RID: 8912
		[Nullable(2)]
		private MoveState _dramaticOpenMove;

		// Token: 0x040022D1 RID: 8913
		private MoveState _deadState;

		// Token: 0x040022D2 RID: 8914
		[Nullable(2)]
		private Creature _doormaker;
	}
}
