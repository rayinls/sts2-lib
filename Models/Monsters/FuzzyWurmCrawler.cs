using System;
using System.Collections.Generic;
using System.Linq;
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
	// Token: 0x02000757 RID: 1879
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FuzzyWurmCrawler : MonsterModel
	{
		// Token: 0x170015AC RID: 5548
		// (get) Token: 0x06005B52 RID: 23378 RVA: 0x00233216 File Offset: 0x00231416
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 58, 55);
			}
		}

		// Token: 0x170015AD RID: 5549
		// (get) Token: 0x06005B53 RID: 23379 RVA: 0x00233222 File Offset: 0x00231422
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 59, 57);
			}
		}

		// Token: 0x170015AE RID: 5550
		// (get) Token: 0x06005B54 RID: 23380 RVA: 0x0023322E File Offset: 0x0023142E
		private int AcidGoopDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 6, 4);
			}
		}

		// Token: 0x170015AF RID: 5551
		// (get) Token: 0x06005B55 RID: 23381 RVA: 0x00233239 File Offset: 0x00231439
		// (set) Token: 0x06005B56 RID: 23382 RVA: 0x00233241 File Offset: 0x00231441
		private bool IsPuffed
		{
			get
			{
				return this._isPuffed;
			}
			set
			{
				base.AssertMutable();
				this._isPuffed = value;
			}
		}

		// Token: 0x06005B57 RID: 23383 RVA: 0x00233250 File Offset: 0x00231450
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("FIRST_ACID_GOOP", new Func<IReadOnlyList<Creature>, Task>(this.AcidGoop), new AbstractIntent[]
			{
				new SingleAttackIntent(this.AcidGoopDamage)
			});
			MoveState moveState2 = new MoveState("ACID_GOOP", new Func<IReadOnlyList<Creature>, Task>(this.AcidGoop), new AbstractIntent[]
			{
				new SingleAttackIntent(this.AcidGoopDamage)
			});
			MoveState moveState3 = new MoveState("INHALE", new Func<IReadOnlyList<Creature>, Task>(this.Inhale), new AbstractIntent[]
			{
				new BuffIntent()
			});
			moveState.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005B58 RID: 23384 RVA: 0x00233310 File Offset: 0x00231510
		private async Task AcidGoop(IReadOnlyList<Creature> targets)
		{
			if (TestMode.IsOff)
			{
				NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(base.Creature);
				Node2D specialNode = creatureNode.GetSpecialNode<Node2D>("Visuals/SpineBoneNode");
				if (specialNode != null)
				{
					specialNode.Position = Vector2.Left * (creatureNode.GlobalPosition.X - NCombatRoom.Instance.GetCreatureNode(targets.First<Creature>()).GlobalPosition.X);
				}
			}
			this.IsPuffed = false;
			await DamageCmd.Attack(this.AcidGoopDamage).FromMonster(this).WithAttackerAnim("Attack", 1f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005B59 RID: 23385 RVA: 0x0023335C File Offset: 0x0023155C
		private async Task Inhale(IReadOnlyList<Creature> targets)
		{
			this.IsPuffed = true;
			SfxCmd.Play(this.CastSfx, 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Inhale", 0.6f);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 7m, base.Creature, null, false);
		}

		// Token: 0x06005B5A RID: 23386 RVA: 0x002333A0 File Offset: 0x002315A0
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("attack", false);
			AnimState animState3 = new AnimState("hurt", false);
			AnimState animState4 = new AnimState("die", false);
			AnimState animState5 = new AnimState("inhale", false);
			AnimState animState6 = new AnimState("idle_loop_puffed", true);
			AnimState animState7 = new AnimState("hurt_puffed", false);
			AnimState animState8 = new AnimState("die_puffed", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState5.NextState = animState6;
			animState7.NextState = animState6;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Inhale", animState5, null);
			creatureAnimator.AddAnyState("Attack", animState2, null);
			creatureAnimator.AddAnyState("Dead", animState4, () => !this.IsPuffed);
			creatureAnimator.AddAnyState("Dead", animState8, () => this.IsPuffed);
			creatureAnimator.AddAnyState("Hit", animState3, () => !this.IsPuffed);
			creatureAnimator.AddAnyState("Hit", animState7, () => this.IsPuffed);
			return creatureAnimator;
		}

		// Token: 0x040022FF RID: 8959
		private const string _inhaleTrigger = "Inhale";

		// Token: 0x04002300 RID: 8960
		private bool _isPuffed;
	}
}
