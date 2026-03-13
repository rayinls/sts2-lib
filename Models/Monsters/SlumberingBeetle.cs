using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000785 RID: 1925
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SlumberingBeetle : MonsterModel
	{
		// Token: 0x170016E9 RID: 5865
		// (get) Token: 0x06005E31 RID: 24113 RVA: 0x0023BDC3 File Offset: 0x00239FC3
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 89, 86);
			}
		}

		// Token: 0x170016EA RID: 5866
		// (get) Token: 0x06005E32 RID: 24114 RVA: 0x0023BDCF File Offset: 0x00239FCF
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x170016EB RID: 5867
		// (get) Token: 0x06005E33 RID: 24115 RVA: 0x0023BDD7 File Offset: 0x00239FD7
		private int RolloutDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 18, 16);
			}
		}

		// Token: 0x170016EC RID: 5868
		// (get) Token: 0x06005E34 RID: 24116 RVA: 0x0023BDE4 File Offset: 0x00239FE4
		private int PlatingAmount
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 18, 15);
			}
		}

		// Token: 0x170016ED RID: 5869
		// (get) Token: 0x06005E35 RID: 24117 RVA: 0x0023BDF0 File Offset: 0x00239FF0
		// (set) Token: 0x06005E36 RID: 24118 RVA: 0x0023BDF8 File Offset: 0x00239FF8
		public bool IsAwake
		{
			get
			{
				return this._isAwake;
			}
			set
			{
				base.AssertMutable();
				this._isAwake = value;
			}
		}

		// Token: 0x170016EE RID: 5870
		// (get) Token: 0x06005E37 RID: 24119 RVA: 0x0023BE07 File Offset: 0x0023A007
		// (set) Token: 0x06005E38 RID: 24120 RVA: 0x0023BE0F File Offset: 0x0023A00F
		[Nullable(2)]
		private NSleepingVfx SleepingVfx
		{
			[NullableContext(2)]
			get
			{
				return this._sleepingVfx;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				this._sleepingVfx = value;
			}
		}

		// Token: 0x06005E39 RID: 24121 RVA: 0x0023BE20 File Offset: 0x0023A020
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<PlatingPower>(base.Creature, this.PlatingAmount, base.Creature, null, false);
			await PowerCmd.Apply<SlumberPower>(base.Creature, 3m, base.Creature, null, false);
			SfxCmd.PlayLoop("event:/sfx/enemy/enemy_attacks/slumbering_beetle/slumbering_beetle_sleep_loop", true);
			NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(base.Creature);
			Marker2D marker2D = ((creatureNode != null) ? creatureNode.GetSpecialNode<Marker2D>("%SleepVfxPos") : null);
			if (marker2D != null)
			{
				this.SleepingVfx = NSleepingVfx.Create(marker2D.GlobalPosition, true);
				marker2D.AddChildSafely(this.SleepingVfx);
				this.SleepingVfx.Position = Vector2.Zero;
			}
			base.Creature.Died += this.AfterDeath;
		}

		// Token: 0x06005E3A RID: 24122 RVA: 0x0023BE63 File Offset: 0x0023A063
		private void AfterDeath(Creature _)
		{
			base.Creature.Died -= this.AfterDeath;
			NSleepingVfx sleepingVfx = this.SleepingVfx;
			if (sleepingVfx != null)
			{
				sleepingVfx.Stop();
			}
			this.SleepingVfx = null;
		}

		// Token: 0x06005E3B RID: 24123 RVA: 0x0023BE94 File Offset: 0x0023A094
		public async Task WakeUpMove(IReadOnlyList<Creature> _)
		{
			SfxCmd.StopLoop("event:/sfx/enemy/enemy_attacks/slumbering_beetle/slumbering_beetle_sleep_loop");
			NSleepingVfx sleepingVfx = this.SleepingVfx;
			if (sleepingVfx != null)
			{
				sleepingVfx.Stop();
			}
			this.SleepingVfx = null;
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/slumbering_beetle/slumbering_beetle_wake_up", 1f);
			this.IsAwake = true;
			await CreatureCmd.TriggerAnim(base.Creature, "WakeUp", 0.6f);
			if (base.Creature.HasPower<PlatingPower>())
			{
				await PowerCmd.Remove(base.Creature.GetPower<PlatingPower>());
			}
		}

		// Token: 0x06005E3C RID: 24124 RVA: 0x0023BED8 File Offset: 0x0023A0D8
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("SNORE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SnoreMove), new AbstractIntent[]
			{
				new SleepIntent()
			});
			MoveState moveState2 = new MoveState("ROLL_OUT_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.RolloutMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.RolloutDamage),
				new BuffIntent()
			});
			ConditionalBranchState conditionalBranchState = new ConditionalBranchState("SNORE_NEXT");
			moveState.FollowUpState = conditionalBranchState;
			conditionalBranchState.AddState(moveState, () => base.Creature.HasPower<SlumberPower>());
			conditionalBranchState.AddState(moveState2, () => !base.Creature.HasPower<SlumberPower>());
			moveState2.FollowUpState = moveState2;
			list.Add(moveState);
			list.Add(conditionalBranchState);
			list.Add(moveState2);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005E3D RID: 24125 RVA: 0x0023BF9E File Offset: 0x0023A19E
		private Task SnoreMove(IReadOnlyList<Creature> targets)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06005E3E RID: 24126 RVA: 0x0023BFA8 File Offset: 0x0023A1A8
		private async Task RolloutMove(IReadOnlyList<Creature> targets)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(base.Creature) : null);
			if (ncreature != null)
			{
				NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(LocalContext.GetMe(base.CombatState).Creature);
				Node2D specialNode = ncreature.GetSpecialNode<Node2D>("Visuals/SpineBoneNode");
				if (specialNode != null)
				{
					specialNode.Position = Vector2.Left * (ncreature.GlobalPosition.X - creatureNode.GlobalPosition.X);
				}
			}
			await DamageCmd.Attack(this.RolloutDamage).FromMonster(this).WithAttackerAnim("Rollout", 0.5f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/slumbering_beetle/slumbering_beetle_roll", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 2m, base.Creature, null, false);
		}

		// Token: 0x06005E3F RID: 24127 RVA: 0x0023BFEC File Offset: 0x0023A1EC
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("sleep_loop", true);
			AnimState animState2 = new AnimState("wake_up", false);
			AnimState animState3 = new AnimState("idle_loop", true);
			AnimState animState4 = new AnimState("cast", false);
			AnimState animState5 = new AnimState("attack", false);
			AnimState animState6 = new AnimState("roll", false);
			AnimState animState7 = new AnimState("hurt", false);
			AnimState animState8 = new AnimState("die", false);
			animState2.NextState = animState3;
			animState5.NextState = animState3;
			animState4.NextState = animState3;
			animState6.NextState = animState3;
			animState7.NextState = animState3;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("WakeUp", animState2, null);
			creatureAnimator.AddAnyState("Dead", animState8, null);
			creatureAnimator.AddAnyState("Attack", animState5, null);
			creatureAnimator.AddAnyState("Rollout", animState6, null);
			creatureAnimator.AddAnyState("Cast", animState4, null);
			creatureAnimator.AddAnyState("Hit", animState7, () => this.IsAwake);
			return creatureAnimator;
		}

		// Token: 0x040023B7 RID: 9143
		public const string wakeUpTrigger = "WakeUp";

		// Token: 0x040023B8 RID: 9144
		private const string _rolloutTrigger = "Rollout";

		// Token: 0x040023B9 RID: 9145
		public const string rolloutMoveId = "ROLL_OUT_MOVE";

		// Token: 0x040023BA RID: 9146
		private const string _rollSfx = "event:/sfx/enemy/enemy_attacks/slumbering_beetle/slumbering_beetle_roll";

		// Token: 0x040023BB RID: 9147
		public const string wakeUp = "event:/sfx/enemy/enemy_attacks/slumbering_beetle/slumbering_beetle_wake_up";

		// Token: 0x040023BC RID: 9148
		public const string sleepLoop = "event:/sfx/enemy/enemy_attacks/slumbering_beetle/slumbering_beetle_sleep_loop";

		// Token: 0x040023BD RID: 9149
		private bool _isAwake;

		// Token: 0x040023BE RID: 9150
		[Nullable(2)]
		private NSleepingVfx _sleepingVfx;
	}
}
