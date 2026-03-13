using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.TestSupport;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200079C RID: 1948
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Tunneler : MonsterModel
	{
		// Token: 0x17001798 RID: 6040
		// (get) Token: 0x06005FE1 RID: 24545 RVA: 0x00240DBB File Offset: 0x0023EFBB
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 92, 87);
			}
		}

		// Token: 0x17001799 RID: 6041
		// (get) Token: 0x06005FE2 RID: 24546 RVA: 0x00240DC7 File Offset: 0x0023EFC7
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x1700179A RID: 6042
		// (get) Token: 0x06005FE3 RID: 24547 RVA: 0x00240DCF File Offset: 0x0023EFCF
		public override string TakeDamageSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/burrowing_bug/burrowing_bug_hurt";
			}
		}

		// Token: 0x1700179B RID: 6043
		// (get) Token: 0x06005FE4 RID: 24548 RVA: 0x00240DD6 File Offset: 0x0023EFD6
		private int BiteDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 15, 13);
			}
		}

		// Token: 0x1700179C RID: 6044
		// (get) Token: 0x06005FE5 RID: 24549 RVA: 0x00240DE3 File Offset: 0x0023EFE3
		private int BlockGain
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 37, 32);
			}
		}

		// Token: 0x1700179D RID: 6045
		// (get) Token: 0x06005FE6 RID: 24550 RVA: 0x00240DEF File Offset: 0x0023EFEF
		private int BelowDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 26, 23);
			}
		}

		// Token: 0x06005FE7 RID: 24551 RVA: 0x00240DFC File Offset: 0x0023EFFC
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("BITE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BiteMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.BiteDamage)
			});
			MoveState moveState2 = new MoveState("BURROW_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BurrowMove), new AbstractIntent[]
			{
				new BuffIntent(),
				new DefendIntent()
			});
			MoveState moveState3 = new MoveState("BELOW_MOVE_1", new Func<IReadOnlyList<Creature>, Task>(this.BelowMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.BelowDamage)
			});
			MoveState moveState4 = new MoveState("DIZZY_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.StillDizzyMove), new AbstractIntent[]
			{
				new StunIntent()
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState3;
			moveState4.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(moveState4);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005FE8 RID: 24552 RVA: 0x00240EFC File Offset: 0x0023F0FC
		private async Task BiteMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.BiteDamage).FromMonster(this).WithAttackerAnim("Attack", 0.25f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005FE9 RID: 24553 RVA: 0x00240F40 File Offset: 0x0023F140
		private async Task BurrowMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/burrowing_bug/burrowing_bug_burrow", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Burrow", 0.25f);
			await PowerCmd.Apply<BurrowedPower>(base.Creature, 1m, base.Creature, null, false);
			await CreatureCmd.GainBlock(base.Creature, this.BlockGain, ValueProp.Move, null, false);
		}

		// Token: 0x06005FEA RID: 24554 RVA: 0x00240F84 File Offset: 0x0023F184
		private async Task BelowMove(IReadOnlyList<Creature> targets)
		{
			if (TestMode.IsOff)
			{
				NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(base.Creature);
				Node2D specialNode = creatureNode.GetSpecialNode<Node2D>("Visuals/SpineBoneNode");
				if (specialNode != null)
				{
					specialNode.Position = Vector2.Right * (NCombatRoom.Instance.GetCreatureNode(targets[0]).GlobalPosition.X - creatureNode.GlobalPosition.X) * 3f;
				}
				SfxCmd.Play("event:/sfx/enemy/enemy_attacks/burrowing_bug/burrowing_bug_hidden_attack", 1f);
				await CreatureCmd.TriggerAnim(base.Creature, "BurrowAttack", 0.25f);
				await Cmd.Wait(1f, false);
			}
			await DamageCmd.Attack(this.BelowDamage).FromMonster(this).WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005FEB RID: 24555 RVA: 0x00240FCF File Offset: 0x0023F1CF
		private Task StillDizzyMove(IReadOnlyList<Creature> targets)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06005FEC RID: 24556 RVA: 0x00240FD8 File Offset: 0x0023F1D8
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("die", false);
			AnimState animState3 = new AnimState("hurt", false);
			AnimState animState4 = new AnimState("attack", false);
			AnimState animState5 = new AnimState("burrow", false);
			AnimState animState6 = new AnimState("hidden_loop", true);
			AnimState animState7 = new AnimState("hidden_attack", false);
			AnimState animState8 = new AnimState("hidden_die", false);
			AnimState animState9 = new AnimState("unburrow_attack", false);
			animState5.NextState = animState6;
			animState7.NextState = animState6;
			animState4.NextState = animState;
			animState9.NextState = animState;
			animState3.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("UnburrowAttack", animState9, null);
			creatureAnimator.AddAnyState("Hit", animState3, () => !base.Creature.HasPower<BurrowedPower>());
			creatureAnimator.AddAnyState("Dead", animState2, () => !base.Creature.HasPower<BurrowedPower>());
			creatureAnimator.AddAnyState("Dead", animState8, () => base.Creature.HasPower<BurrowedPower>());
			creatureAnimator.AddAnyState("Attack", animState4, () => !base.Creature.HasPower<BurrowedPower>());
			creatureAnimator.AddAnyState("BurrowAttack", animState7, () => base.Creature.HasPower<BurrowedPower>());
			creatureAnimator.AddAnyState("Burrow", animState5, null);
			return creatureAnimator;
		}

		// Token: 0x0400241C RID: 9244
		public const string biteMoveId = "BITE_MOVE";

		// Token: 0x0400241D RID: 9245
		public const string stillDizzyMoveId = "DIZZY_MOVE";

		// Token: 0x0400241E RID: 9246
		private const string _burrowedAttackTrigger = "BurrowAttack";

		// Token: 0x0400241F RID: 9247
		public const string unburrowAttackTrigger = "UnburrowAttack";

		// Token: 0x04002420 RID: 9248
		private const string _burrowTrigger = "Burrow";

		// Token: 0x04002421 RID: 9249
		private const string _burrowSfx = "event:/sfx/enemy/enemy_attacks/burrowing_bug/burrowing_bug_burrow";

		// Token: 0x04002422 RID: 9250
		private const string _hiddenBurrowAttackSfx = "event:/sfx/enemy/enemy_attacks/burrowing_bug/burrowing_bug_hidden_attack";

		// Token: 0x04002423 RID: 9251
		private const string _unburrowSfx = "event:/sfx/enemy/enemy_attacks/burrowing_bug/burrowing_bug_unburrow_attack";
	}
}
