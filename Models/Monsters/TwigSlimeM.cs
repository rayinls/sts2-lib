using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.MonsterMoves;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200079E RID: 1950
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TwigSlimeM : MonsterModel
	{
		// Token: 0x170017A3 RID: 6051
		// (get) Token: 0x06005FFD RID: 24573 RVA: 0x0024139B File Offset: 0x0023F59B
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 27, 26);
			}
		}

		// Token: 0x170017A4 RID: 6052
		// (get) Token: 0x06005FFE RID: 24574 RVA: 0x002413A7 File Offset: 0x0023F5A7
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 29, 28);
			}
		}

		// Token: 0x170017A5 RID: 6053
		// (get) Token: 0x06005FFF RID: 24575 RVA: 0x002413B3 File Offset: 0x0023F5B3
		private int ClumpDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 12, 11);
			}
		}

		// Token: 0x170017A6 RID: 6054
		// (get) Token: 0x06006000 RID: 24576 RVA: 0x002413C0 File Offset: 0x0023F5C0
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Slime;
			}
		}

		// Token: 0x06006001 RID: 24577 RVA: 0x002413C4 File Offset: 0x0023F5C4
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("CLUMP_SHOT_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ClumpShotMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.ClumpDamage)
			});
			MoveState moveState2 = new MoveState("STICKY_SHOT_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.StickyShotMove), new AbstractIntent[]
			{
				new StatusIntent(1)
			});
			RandomBranchState randomBranchState = new RandomBranchState("RAND");
			moveState.FollowUpState = randomBranchState;
			moveState2.FollowUpState = randomBranchState;
			randomBranchState.AddBranch(moveState, 2);
			randomBranchState.AddBranch(moveState2, MoveRepeatType.CannotRepeat);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(randomBranchState);
			return new MonsterMoveStateMachine(list, moveState2);
		}

		// Token: 0x06006002 RID: 24578 RVA: 0x00241470 File Offset: 0x0023F670
		private async Task ClumpShotMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.ClumpDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_slime_impact", null, null)
				.Execute(null);
		}

		// Token: 0x06006003 RID: 24579 RVA: 0x002414B4 File Offset: 0x0023F6B4
		private async Task StickyShotMove(IReadOnlyList<Creature> targets)
		{
			if (TestMode.IsOff)
			{
				Vector2? vector = null;
				foreach (Creature creature in targets)
				{
					NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(creature);
					if (creatureNode != null && (vector == null || vector.Value.X > creatureNode.GlobalPosition.X))
					{
						vector = new Vector2?(creatureNode.VfxSpawnPosition);
					}
				}
				NCreature creatureNode2 = NCombatRoom.Instance.GetCreatureNode(base.Creature);
				Node2D node2D = ((creatureNode2 != null) ? creatureNode2.GetSpecialNode<Node2D>("Visuals/SpitTarget") : null);
				if (node2D != null)
				{
					node2D.GlobalPosition = vector.Value;
				}
			}
			SfxCmd.Play(this.CastSfx, 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.75f);
			VfxCmd.PlayOnCreatureCenters(targets, "vfx/vfx_slime_impact");
			await CardPileCmd.AddToCombatAndPreview<Slimed>(targets, PileType.Discard, 1, false, CardPilePosition.Bottom);
		}

		// Token: 0x04002427 RID: 9255
		private const int _stickyAmount = 1;
	}
}
