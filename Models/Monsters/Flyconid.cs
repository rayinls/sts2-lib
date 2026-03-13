using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
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
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000753 RID: 1875
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Flyconid : MonsterModel
	{
		// Token: 0x17001593 RID: 5523
		// (get) Token: 0x06005B1A RID: 23322 RVA: 0x002324E3 File Offset: 0x002306E3
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 51, 47);
			}
		}

		// Token: 0x17001594 RID: 5524
		// (get) Token: 0x06005B1B RID: 23323 RVA: 0x002324EF File Offset: 0x002306EF
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 53, 49);
			}
		}

		// Token: 0x17001595 RID: 5525
		// (get) Token: 0x06005B1C RID: 23324 RVA: 0x002324FB File Offset: 0x002306FB
		private int SmashDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 12, 11);
			}
		}

		// Token: 0x17001596 RID: 5526
		// (get) Token: 0x06005B1D RID: 23325 RVA: 0x00232508 File Offset: 0x00230708
		private int SporeDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 9, 8);
			}
		}

		// Token: 0x06005B1E RID: 23326 RVA: 0x00232514 File Offset: 0x00230714
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("VULNERABLE_SPORES_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.VulnerableSporesMove), new AbstractIntent[]
			{
				new DebuffIntent(false)
			});
			MoveState moveState2 = new MoveState("FRAIL_SPORES_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.FrailSporesMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SporeDamage),
				new DebuffIntent(false)
			});
			MoveState moveState3 = new MoveState("SMASH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SmashMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SmashDamage)
			});
			RandomBranchState randomBranchState = new RandomBranchState("RAND");
			RandomBranchState randomBranchState2 = new RandomBranchState("INITIAL");
			moveState.FollowUpState = randomBranchState;
			moveState2.FollowUpState = randomBranchState;
			moveState3.FollowUpState = randomBranchState;
			randomBranchState.AddBranch(moveState, 3, MoveRepeatType.CannotRepeat);
			randomBranchState.AddBranch(moveState2, 2, MoveRepeatType.CannotRepeat);
			randomBranchState.AddBranch(moveState3, MoveRepeatType.CannotRepeat);
			randomBranchState2.AddBranch(moveState2, 2, MoveRepeatType.CannotRepeat);
			randomBranchState2.AddBranch(moveState3, MoveRepeatType.CannotRepeat);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(randomBranchState);
			list.Add(randomBranchState2);
			return new MonsterMoveStateMachine(list, randomBranchState2);
		}

		// Token: 0x06005B1F RID: 23327 RVA: 0x0023263C File Offset: 0x0023083C
		private async Task VulnerableSporesMove(IReadOnlyList<Creature> targets)
		{
			if (TestMode.IsOff)
			{
				NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(base.Creature);
				NFlyconidSporesVfx node = creatureNode.Visuals.Body.GetNode<NFlyconidSporesVfx>("%VfxController");
				node.SetSporeTypeIsVulnerable(true);
			}
			SfxCmd.Play(this.CastSfx, 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.5f);
			await PowerCmd.Apply<VulnerablePower>(targets, 2m, base.Creature, null, false);
		}

		// Token: 0x06005B20 RID: 23328 RVA: 0x00232688 File Offset: 0x00230888
		private async Task FrailSporesMove(IReadOnlyList<Creature> targets)
		{
			if (TestMode.IsOff)
			{
				NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(base.Creature);
				NFlyconidSporesVfx node = creatureNode.Visuals.Body.GetNode<NFlyconidSporesVfx>("%VfxController");
				node.SetSporeTypeIsVulnerable(false);
			}
			await DamageCmd.Attack(this.SporeDamage).FromMonster(this).WithAttackerAnim("Cast", 0.5f, null)
				.WithAttackerFx(null, this.CastSfx, null)
				.WithWaitBeforeHit(0f, 0.6f)
				.WithHitVfxNode((Creature t) => NSporeImpactVfx.Create(t, new Color("8aad7d")))
				.Execute(null);
			await PowerCmd.Apply<FrailPower>(targets, 2m, base.Creature, null, false);
		}

		// Token: 0x06005B21 RID: 23329 RVA: 0x002326D4 File Offset: 0x002308D4
		private async Task SmashMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SmashDamage).FromMonster(this).WithAttackerAnim("Attack", 0.3f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}
	}
}
