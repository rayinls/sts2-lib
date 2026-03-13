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
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.TestSupport;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000769 RID: 1897
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MagiKnight : MonsterModel
	{
		// Token: 0x17001634 RID: 5684
		// (get) Token: 0x06005C8A RID: 23690 RVA: 0x00236D17 File Offset: 0x00234F17
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 89, 82);
			}
		}

		// Token: 0x17001635 RID: 5685
		// (get) Token: 0x06005C8B RID: 23691 RVA: 0x00236D23 File Offset: 0x00234F23
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001636 RID: 5686
		// (get) Token: 0x06005C8C RID: 23692 RVA: 0x00236D2B File Offset: 0x00234F2B
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x17001637 RID: 5687
		// (get) Token: 0x06005C8D RID: 23693 RVA: 0x00236D2E File Offset: 0x00234F2E
		private int PowerShieldDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 7, 6);
			}
		}

		// Token: 0x17001638 RID: 5688
		// (get) Token: 0x06005C8E RID: 23694 RVA: 0x00236D39 File Offset: 0x00234F39
		private int PowerShieldBlock
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 9, 5);
			}
		}

		// Token: 0x17001639 RID: 5689
		// (get) Token: 0x06005C8F RID: 23695 RVA: 0x00236D44 File Offset: 0x00234F44
		private int SpearDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 11, 10);
			}
		}

		// Token: 0x1700163A RID: 5690
		// (get) Token: 0x06005C90 RID: 23696 RVA: 0x00236D51 File Offset: 0x00234F51
		private int BombDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 40, 35);
			}
		}

		// Token: 0x1700163B RID: 5691
		// (get) Token: 0x06005C91 RID: 23697 RVA: 0x00236D5E File Offset: 0x00234F5E
		public override string HurtSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/magi_knight/magi_knight_hurt";
			}
		}

		// Token: 0x06005C92 RID: 23698 RVA: 0x00236D68 File Offset: 0x00234F68
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("FIRST_POWER_SHIELD_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.PowerShieldMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.PowerShieldDamage),
				new DefendIntent()
			});
			MoveState moveState2 = new MoveState("DAMPEN_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.DampenMove), new AbstractIntent[]
			{
				new DebuffIntent(false)
			});
			MoveState moveState3 = new MoveState("PREP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.PrepMove), new AbstractIntent[]
			{
				new DefendIntent()
			});
			MoveState moveState4 = new MoveState("MAGIC_BOMB", new Func<IReadOnlyList<Creature>, Task>(this.MagicBombMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.BombDamage)
			});
			MoveState moveState5 = new MoveState("RAM_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SpearMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SpearDamage)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState5;
			moveState5.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState4;
			moveState4.FollowUpState = moveState5;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState5);
			list.Add(moveState3);
			list.Add(moveState4);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005C93 RID: 23699 RVA: 0x00236EA8 File Offset: 0x002350A8
		private async Task DampenMove(IReadOnlyList<Creature> targets)
		{
			foreach (Creature creature in targets)
			{
				DampenPower dampenPower = creature.GetPower<DampenPower>();
				bool flag = dampenPower == null;
				if (flag)
				{
					dampenPower = (DampenPower)ModelDb.Power<DampenPower>().ToMutable(0);
				}
				dampenPower.AddCaster(base.Creature);
				if (flag)
				{
					await PowerCmd.Apply(dampenPower, creature, 1m, base.Creature, null, false);
				}
			}
			IEnumerator<Creature> enumerator = null;
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.CombatVfxContainer.AddChildSafely(NSpeechBubbleVfx.Create(MagiKnight._dampenDialogue.GetFormattedText(), base.Creature, 2.0, VfxColor.White));
			}
			await Cmd.Wait(0.25f, false);
		}

		// Token: 0x06005C94 RID: 23700 RVA: 0x00236EF4 File Offset: 0x002350F4
		private async Task PowerShieldMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.PowerShieldDamage).FromMonster(this).WithAttackerAnim("ShieldAttack", 0.6f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/magi_knight/magi_knight_cast_shield", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			await CreatureCmd.GainBlock(base.Creature, this.PowerShieldBlock, ValueProp.Move, null, false);
		}

		// Token: 0x06005C95 RID: 23701 RVA: 0x00236F38 File Offset: 0x00235138
		private async Task PrepMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/magi_knight/magi_knight_cast_shield", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "ShieldAttack", 0.6f);
			await CreatureCmd.GainBlock(base.Creature, this.PowerShieldBlock, ValueProp.Move, null, false);
		}

		// Token: 0x06005C96 RID: 23702 RVA: 0x00236F7C File Offset: 0x0023517C
		private async Task MagicBombMove(IReadOnlyList<Creature> targets)
		{
			if (TestMode.IsOff)
			{
				Vector2? vector = null;
				foreach (Creature creature in targets)
				{
					NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(creature);
					if (vector == null || vector.Value.X > creatureNode.GlobalPosition.X)
					{
						vector = new Vector2?(creatureNode.GlobalPosition);
					}
				}
				NCreature creatureNode2 = NCombatRoom.Instance.GetCreatureNode(base.Creature);
				Node2D specialNode = creatureNode2.GetSpecialNode<Node2D>("Visuals/AttackDistanceControl");
				if (specialNode != null)
				{
					float x = creatureNode2.Visuals.Body.Scale.X;
					specialNode.Position = Vector2.Left * ((creatureNode2.GlobalPosition.X - vector.Value.X - 600f) / x);
				}
			}
			await DamageCmd.Attack(this.BombDamage).FromMonster(this).WithAttackerAnim("BombCast", 1.2f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/magi_knight/magi_knight_attack_bomb", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005C97 RID: 23703 RVA: 0x00236FC8 File Offset: 0x002351C8
		private async Task SpearMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SpearDamage).FromMonster(this).WithAttackerAnim("RamAttack", 1.2f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/magi_knight/magi_knight_attack_ram", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005C98 RID: 23704 RVA: 0x0023700C File Offset: 0x0023520C
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("attack_bomb", false);
			AnimState animState3 = new AnimState("attack_ram", false);
			AnimState animState4 = new AnimState("cast_shield", false);
			AnimState animState5 = new AnimState("hurt", false);
			AnimState animState6 = new AnimState("die", false);
			animState2.NextState = animState;
			animState4.NextState = animState;
			animState3.NextState = animState;
			animState5.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Dead", animState6, null);
			creatureAnimator.AddAnyState("Hit", animState5, null);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("BombCast", animState2, null);
			creatureAnimator.AddAnyState("RamAttack", animState3, null);
			creatureAnimator.AddAnyState("ShieldAttack", animState4, null);
			return creatureAnimator;
		}

		// Token: 0x04002359 RID: 9049
		private static readonly LocString _dampenDialogue = new LocString("powers", "DAMPEN_POWER.banter");

		// Token: 0x0400235A RID: 9050
		private const string _bombTrigger = "BombCast";

		// Token: 0x0400235B RID: 9051
		private const string _ramAttackTrigger = "RamAttack";

		// Token: 0x0400235C RID: 9052
		private const string _shieldTrigger = "ShieldAttack";

		// Token: 0x0400235D RID: 9053
		private const string _ramSfx = "event:/sfx/enemy/enemy_attacks/magi_knight/magi_knight_attack_ram";

		// Token: 0x0400235E RID: 9054
		private const string _bombSfx = "event:/sfx/enemy/enemy_attacks/magi_knight/magi_knight_attack_bomb";

		// Token: 0x0400235F RID: 9055
		private const string _castShieldSfx = "event:/sfx/enemy/enemy_attacks/magi_knight/magi_knight_cast_shield";
	}
}
