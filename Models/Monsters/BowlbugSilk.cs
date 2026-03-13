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

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000738 RID: 1848
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BowlbugSilk : MonsterModel
	{
		// Token: 0x170014E2 RID: 5346
		// (get) Token: 0x06005970 RID: 22896 RVA: 0x0022D4FC File Offset: 0x0022B6FC
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 41, 40);
			}
		}

		// Token: 0x170014E3 RID: 5347
		// (get) Token: 0x06005971 RID: 22897 RVA: 0x0022D508 File Offset: 0x0022B708
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 44, 43);
			}
		}

		// Token: 0x170014E4 RID: 5348
		// (get) Token: 0x06005972 RID: 22898 RVA: 0x0022D514 File Offset: 0x0022B714
		private int ThrashDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 5, 4);
			}
		}

		// Token: 0x170014E5 RID: 5349
		// (get) Token: 0x06005973 RID: 22899 RVA: 0x0022D51F File Offset: 0x0022B71F
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/workbug_silk/workbug_silk_die";
			}
		}

		// Token: 0x06005974 RID: 22900 RVA: 0x0022D528 File Offset: 0x0022B728
		public override void SetupSkins(NCreatureVisuals visuals)
		{
			MegaSkeleton skeleton = visuals.SpineBody.GetSkeleton();
			skeleton.SetSkin(skeleton.GetData().FindSkin("web"));
			skeleton.SetSlotsToSetupPose();
		}

		// Token: 0x06005975 RID: 22901 RVA: 0x0022D560 File Offset: 0x0022B760
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("TRASH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ThrashMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.ThrashDamage, 2)
			});
			MoveState moveState2 = new MoveState("TOXIC_SPIT_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.WebMove), new AbstractIntent[]
			{
				new DebuffIntent(false)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			return new MonsterMoveStateMachine(list, moveState2);
		}

		// Token: 0x06005976 RID: 22902 RVA: 0x0022D5E8 File Offset: 0x0022B7E8
		private async Task ThrashMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.ThrashDamage).WithHitCount(2).FromMonster(this)
				.WithAttackerAnim("Attack", 0.3f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005977 RID: 22903 RVA: 0x0022D62C File Offset: 0x0022B82C
		private async Task WebMove(IReadOnlyList<Creature> targets)
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
				Node2D specialNode = creatureNode2.GetSpecialNode<Node2D>("Visuals/SpineBoneNode");
				if (specialNode != null)
				{
					specialNode.Position = Vector2.Right * (vector.Value.X - creatureNode2.GlobalPosition.X) * 4f;
				}
			}
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/workbug_silk/workbug_silk_spit", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.8f);
			await PowerCmd.Apply<WeakPower>(targets, 1m, base.Creature, null, false);
		}

		// Token: 0x06005978 RID: 22904 RVA: 0x0022D678 File Offset: 0x0022B878
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("spit", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Dead", animState5, null);
			creatureAnimator.AddAnyState("Hit", animState4, null);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			return creatureAnimator;
		}

		// Token: 0x04002295 RID: 8853
		private const int _thrashRepeat = 2;

		// Token: 0x04002296 RID: 8854
		private const string _spitSfx = "event:/sfx/enemy/enemy_attacks/workbug_silk/workbug_silk_spit";

		// Token: 0x04002297 RID: 8855
		private const string _spineSkin = "web";
	}
}
