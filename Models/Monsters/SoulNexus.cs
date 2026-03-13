using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000789 RID: 1929
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SoulNexus : MonsterModel
	{
		// Token: 0x17001706 RID: 5894
		// (get) Token: 0x06005E7B RID: 24187 RVA: 0x0023CAE2 File Offset: 0x0023ACE2
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 254, 234);
			}
		}

		// Token: 0x17001707 RID: 5895
		// (get) Token: 0x06005E7C RID: 24188 RVA: 0x0023CAF4 File Offset: 0x0023ACF4
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001708 RID: 5896
		// (get) Token: 0x06005E7D RID: 24189 RVA: 0x0023CAFC File Offset: 0x0023ACFC
		private int SoulBurnDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 31, 29);
			}
		}

		// Token: 0x17001709 RID: 5897
		// (get) Token: 0x06005E7E RID: 24190 RVA: 0x0023CB09 File Offset: 0x0023AD09
		private int MaelstromDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 7, 6);
			}
		}

		// Token: 0x1700170A RID: 5898
		// (get) Token: 0x06005E7F RID: 24191 RVA: 0x0023CB14 File Offset: 0x0023AD14
		private int MaelstromRepeat
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 4, 4);
			}
		}

		// Token: 0x1700170B RID: 5899
		// (get) Token: 0x06005E80 RID: 24192 RVA: 0x0023CB1F File Offset: 0x0023AD1F
		private int DrainLifeDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 19, 18);
			}
		}

		// Token: 0x1700170C RID: 5900
		// (get) Token: 0x06005E81 RID: 24193 RVA: 0x0023CB2C File Offset: 0x0023AD2C
		public override bool ShouldFadeAfterDeath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700170D RID: 5901
		// (get) Token: 0x06005E82 RID: 24194 RVA: 0x0023CB2F File Offset: 0x0023AD2F
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Magic;
			}
		}

		// Token: 0x06005E83 RID: 24195 RVA: 0x0023CB32 File Offset: 0x0023AD32
		public override void SetupSkins(NCreatureVisuals visuals)
		{
			visuals.SpineBody.GetAnimationState().SetAnimation("tracks/writhe", true, 1);
		}

		// Token: 0x06005E84 RID: 24196 RVA: 0x0023CB4C File Offset: 0x0023AD4C
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			base.Creature.Died += this.AfterDeath;
		}

		// Token: 0x06005E85 RID: 24197 RVA: 0x0023CB90 File Offset: 0x0023AD90
		private void AfterDeath(Creature _)
		{
			base.Creature.Died -= this.AfterDeath;
			NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(base.Creature);
			if (creatureNode == null)
			{
				return;
			}
			creatureNode.SpineController.GetAnimationState().SetAnimation("tracks/empty", true, 1);
		}

		// Token: 0x06005E86 RID: 24198 RVA: 0x0023CBE0 File Offset: 0x0023ADE0
		public override void BeforeRemovedFromRoom()
		{
			if (base.CombatState.RunState.IsGameOver)
			{
				return;
			}
			NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(base.Creature);
			if (creatureNode == null)
			{
				return;
			}
			creatureNode.SpineController.GetAnimationState().SetAnimation("tracks/empty", true, 1);
		}

		// Token: 0x06005E87 RID: 24199 RVA: 0x0023CC2C File Offset: 0x0023AE2C
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("SOUL_BURN_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SoulBurnMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SoulBurnDamage)
			});
			MoveState moveState2 = new MoveState("MAELSTROM_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.MaelstromMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.MaelstromDamage, this.MaelstromRepeat)
			});
			MoveState moveState3 = new MoveState("DRAIN_LIFE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.DrainLifeMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.DrainLifeDamage),
				new DebuffIntent(true)
			});
			RandomBranchState randomBranchState = new RandomBranchState("RAND");
			moveState.FollowUpState = randomBranchState;
			moveState2.FollowUpState = randomBranchState;
			moveState3.FollowUpState = randomBranchState;
			randomBranchState.AddBranch(moveState, MoveRepeatType.CannotRepeat, 1f);
			randomBranchState.AddBranch(moveState2, MoveRepeatType.CannotRepeat, 1f);
			randomBranchState.AddBranch(moveState3, MoveRepeatType.CannotRepeat, 1f);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(randomBranchState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005E88 RID: 24200 RVA: 0x0023CD44 File Offset: 0x0023AF44
		private async Task SoulBurnMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SoulBurnDamage).FromMonster(this).WithAttackerAnim("Attack", 0.6f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005E89 RID: 24201 RVA: 0x0023CD88 File Offset: 0x0023AF88
		private async Task MaelstromMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.MaelstromDamage).WithHitCount(this.MaelstromRepeat).FromMonster(this)
				.OnlyPlayAnimOnce()
				.WithAttackerAnim("Attack", 0.2f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005E8A RID: 24202 RVA: 0x0023CDCC File Offset: 0x0023AFCC
		private async Task DrainLifeMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.DrainLifeDamage).FromMonster(this).WithAttackerAnim("Cast", 1f, null)
				.WithAttackerFx(null, this.CastSfx, null)
				.Execute(null);
			await PowerCmd.Apply<VulnerablePower>(targets, 2m, base.Creature, null, false);
			await PowerCmd.Apply<WeakPower>(targets, 2m, base.Creature, null, false);
		}
	}
}
