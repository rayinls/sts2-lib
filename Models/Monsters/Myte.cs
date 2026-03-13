using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200076E RID: 1902
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Myte : MonsterModel
	{
		// Token: 0x1700164A RID: 5706
		// (get) Token: 0x06005CD1 RID: 23761 RVA: 0x00237B17 File Offset: 0x00235D17
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 64, 61);
			}
		}

		// Token: 0x1700164B RID: 5707
		// (get) Token: 0x06005CD2 RID: 23762 RVA: 0x00237B23 File Offset: 0x00235D23
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 69, 67);
			}
		}

		// Token: 0x1700164C RID: 5708
		// (get) Token: 0x06005CD3 RID: 23763 RVA: 0x00237B2F File Offset: 0x00235D2F
		private int BiteDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 15, 13);
			}
		}

		// Token: 0x1700164D RID: 5709
		// (get) Token: 0x06005CD4 RID: 23764 RVA: 0x00237B3C File Offset: 0x00235D3C
		private int SuckDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 6, 4);
			}
		}

		// Token: 0x1700164E RID: 5710
		// (get) Token: 0x06005CD5 RID: 23765 RVA: 0x00237B47 File Offset: 0x00235D47
		private int SuckStrength
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 3, 2);
			}
		}

		// Token: 0x1700164F RID: 5711
		// (get) Token: 0x06005CD6 RID: 23766 RVA: 0x00237B52 File Offset: 0x00235D52
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/mite/mite_die";
			}
		}

		// Token: 0x06005CD7 RID: 23767 RVA: 0x00237B5C File Offset: 0x00235D5C
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("TOXIC_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ToxicMove), new AbstractIntent[]
			{
				new StatusIntent(2)
			});
			MoveState moveState2 = new MoveState("BITE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BiteMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.BiteDamage)
			});
			MoveState moveState3 = new MoveState("SUCK_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SuckMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SuckDamage),
				new BuffIntent()
			});
			ConditionalBranchState conditionalBranchState = new ConditionalBranchState("INIT_MOVE");
			conditionalBranchState.AddState(moveState, () => base.Creature.SlotName == "first");
			conditionalBranchState.AddState(moveState3, () => base.Creature.SlotName == "second");
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			return new MonsterMoveStateMachine(list, conditionalBranchState);
		}

		// Token: 0x06005CD8 RID: 23768 RVA: 0x00237C5C File Offset: 0x00235E5C
		private async Task ToxicMove(IReadOnlyList<Creature> targets)
		{
			if (TestMode.IsOff)
			{
				NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(base.Creature);
				Player me = LocalContext.GetMe(base.CombatState);
				Creature creature = ((me != null) ? me.Creature : null);
				NMyteVfx specialNode = creatureNode.GetSpecialNode<NMyteVfx>("%NMyteVfx");
				if (specialNode != null)
				{
					specialNode.SetTarget(creature);
				}
			}
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/mite/mite_cast", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.6f);
			await CardPileCmd.AddToCombatAndPreview<Toxic>(targets, PileType.Hand, 2, false, CardPilePosition.Bottom);
		}

		// Token: 0x06005CD9 RID: 23769 RVA: 0x00237CA8 File Offset: 0x00235EA8
		private async Task BiteMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.BiteDamage).FromMonster(this).WithAttackerAnim("Attack", 0.5f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/mite/mite_attack", null)
				.WithHitFx("vfx/vfx_bite", null, null)
				.Execute(null);
		}

		// Token: 0x06005CDA RID: 23770 RVA: 0x00237CEC File Offset: 0x00235EEC
		private async Task SuckMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SuckDamage).FromMonster(this).WithAttackerAnim("Suck", 0.4f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/mite/mite_suck", null)
				.WithHitFx("vfx/vfx_bite", null, null)
				.Execute(null);
			await PowerCmd.Apply<StrengthPower>(base.Creature, this.SuckStrength, base.Creature, null, false);
		}

		// Token: 0x06005CDB RID: 23771 RVA: 0x00237D30 File Offset: 0x00235F30
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("cast", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("die", false);
			AnimState animState6 = new AnimState("suck", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			animState6.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("Suck", animState6, null);
			creatureAnimator.AddAnyState("Dead", animState5, null);
			creatureAnimator.AddAnyState("Hit", animState4, null);
			return creatureAnimator;
		}

		// Token: 0x0400236C RID: 9068
		private const int _toxicCount = 2;

		// Token: 0x0400236D RID: 9069
		private const string _suckTrigger = "Suck";

		// Token: 0x0400236E RID: 9070
		private const string _attackSfx = "event:/sfx/enemy/enemy_attacks/mite/mite_attack";

		// Token: 0x0400236F RID: 9071
		private const string _castSfx = "event:/sfx/enemy/enemy_attacks/mite/mite_cast";

		// Token: 0x04002370 RID: 9072
		private const string _suckSfx = "event:/sfx/enemy/enemy_attacks/mite/mite_suck";
	}
}
