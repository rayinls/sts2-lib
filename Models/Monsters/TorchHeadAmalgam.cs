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
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000799 RID: 1945
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TorchHeadAmalgam : MonsterModel
	{
		// Token: 0x17001782 RID: 6018
		// (get) Token: 0x06005FAD RID: 24493 RVA: 0x0024045C File Offset: 0x0023E65C
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 211, 199);
			}
		}

		// Token: 0x17001783 RID: 6019
		// (get) Token: 0x06005FAE RID: 24494 RVA: 0x0024046E File Offset: 0x0023E66E
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001784 RID: 6020
		// (get) Token: 0x06005FAF RID: 24495 RVA: 0x00240476 File Offset: 0x0023E676
		private int TackleDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 19, 18);
			}
		}

		// Token: 0x17001785 RID: 6021
		// (get) Token: 0x06005FB0 RID: 24496 RVA: 0x00240483 File Offset: 0x0023E683
		private int WeakTackleDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 15, 14);
			}
		}

		// Token: 0x17001786 RID: 6022
		// (get) Token: 0x06005FB1 RID: 24497 RVA: 0x00240490 File Offset: 0x0023E690
		private int SoulBeamDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 8, 8);
			}
		}

		// Token: 0x17001787 RID: 6023
		// (get) Token: 0x06005FB2 RID: 24498 RVA: 0x0024049B File Offset: 0x0023E69B
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x06005FB3 RID: 24499 RVA: 0x002404A0 File Offset: 0x0023E6A0
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<MinionPower>(base.Creature, 1m, base.Creature, null, false);
		}

		// Token: 0x06005FB4 RID: 24500 RVA: 0x002404E4 File Offset: 0x0023E6E4
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("TACKLE_1_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.TackleMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.TackleDamage)
			});
			MoveState moveState2 = new MoveState("TACKLE_2_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.TackleMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.TackleDamage)
			});
			MoveState moveState3 = new MoveState("BEAM_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SoulBeamMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.SoulBeamDamage, 3)
			});
			MoveState moveState4 = new MoveState("TACKLE_3_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.WeakTackleMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.WeakTackleDamage)
			});
			MoveState moveState5 = new MoveState("TACKLE_4_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.WeakTackleMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.WeakTackleDamage)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState4;
			moveState4.FollowUpState = moveState5;
			moveState5.FollowUpState = moveState3;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(moveState4);
			list.Add(moveState5);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005FB5 RID: 24501 RVA: 0x00240624 File Offset: 0x0023E824
		public override void OnDieToDoom()
		{
			if (TestMode.IsOff)
			{
				NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(base.Creature);
				if (creatureNode != null)
				{
					Node2D specialNode = creatureNode.GetSpecialNode<Node2D>("Visuals/torch1Slot/fire1_small_green/light_small");
					if (specialNode != null)
					{
						specialNode.SetVisible(false);
					}
					Node2D specialNode2 = creatureNode.GetSpecialNode<Node2D>("Visuals/torch2Slot/fire2_small_green/light_small");
					if (specialNode2 != null)
					{
						specialNode2.SetVisible(false);
					}
					Node2D specialNode3 = creatureNode.GetSpecialNode<Node2D>("Visuals/torch3Slot/fire3_small_green/light_small");
					if (specialNode3 == null)
					{
						return;
					}
					specialNode3.SetVisible(false);
				}
			}
		}

		// Token: 0x06005FB6 RID: 24502 RVA: 0x00240690 File Offset: 0x0023E890
		private async Task TackleMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.TackleDamage).FromMonster(this).WithAttackerAnim("Attack", 0.6f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005FB7 RID: 24503 RVA: 0x002406D4 File Offset: 0x0023E8D4
		private async Task WeakTackleMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.WeakTackleDamage).FromMonster(this).WithAttackerAnim("Attack", 0.6f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005FB8 RID: 24504 RVA: 0x00240718 File Offset: 0x0023E918
		private async Task SoulBeamMove(IReadOnlyList<Creature> targets)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(base.Creature) : null);
			if (ncreature != null)
			{
				Node2D specialNode = ncreature.GetSpecialNode<Node2D>("Visuals/LaserControlBone");
				if (specialNode != null)
				{
					NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(targets[0]);
					specialNode.Position += Vector2.Left * (creatureNode.GlobalPosition.X - ncreature.GlobalPosition.X + 3000f);
				}
			}
			await DamageCmd.Attack(this.SoulBeamDamage).WithHitCount(3).FromMonster(this)
				.WithAttackerAnim("DebuffTrigger", 0.8f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/torch_head_amalgam/torch_head_amalgam_beam", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005FB9 RID: 24505 RVA: 0x00240764 File Offset: 0x0023E964
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("debuff", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("DebuffTrigger", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("Dead", animState5, null);
			creatureAnimator.AddAnyState("Hit", animState4, null);
			return creatureAnimator;
		}

		// Token: 0x04002412 RID: 9234
		private const int _soulBeamRepeat = 3;

		// Token: 0x04002413 RID: 9235
		private const string _debuffTrigger = "DebuffTrigger";

		// Token: 0x04002414 RID: 9236
		private const string _beamSfx = "event:/sfx/enemy/enemy_attacks/torch_head_amalgam/torch_head_amalgam_beam";
	}
}
