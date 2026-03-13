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
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000764 RID: 1892
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LeafSlimeM : MonsterModel
	{
		// Token: 0x17001613 RID: 5651
		// (get) Token: 0x06005C44 RID: 23620 RVA: 0x00236120 File Offset: 0x00234320
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 33, 32);
			}
		}

		// Token: 0x17001614 RID: 5652
		// (get) Token: 0x06005C45 RID: 23621 RVA: 0x0023612C File Offset: 0x0023432C
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 36, 35);
			}
		}

		// Token: 0x17001615 RID: 5653
		// (get) Token: 0x06005C46 RID: 23622 RVA: 0x00236138 File Offset: 0x00234338
		private int ClumpDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 9, 8);
			}
		}

		// Token: 0x17001616 RID: 5654
		// (get) Token: 0x06005C47 RID: 23623 RVA: 0x00236144 File Offset: 0x00234344
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Slime;
			}
		}

		// Token: 0x06005C48 RID: 23624 RVA: 0x00236148 File Offset: 0x00234348
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("CLUMP_SHOT", new Func<IReadOnlyList<Creature>, Task>(this.ClumpShotMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.ClumpDamage)
			});
			MoveState moveState2 = new MoveState("STICKY_SHOT", new Func<IReadOnlyList<Creature>, Task>(this.StickyShotMove), new AbstractIntent[]
			{
				new StatusIntent(2)
			});
			moveState2.FollowUpState = moveState;
			moveState.FollowUpState = moveState2;
			list.Add(moveState);
			list.Add(moveState2);
			return new MonsterMoveStateMachine(list, moveState2);
		}

		// Token: 0x06005C49 RID: 23625 RVA: 0x002361D0 File Offset: 0x002343D0
		private async Task ClumpShotMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.ClumpDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_slime_impact", null, null)
				.Execute(null);
		}

		// Token: 0x06005C4A RID: 23626 RVA: 0x00236214 File Offset: 0x00234414
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
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 1f);
			VfxCmd.PlayOnCreatureCenters(targets, "vfx/vfx_slime_impact");
			await CardPileCmd.AddToCombatAndPreview<Slimed>(targets, PileType.Discard, 2, false, CardPilePosition.Bottom);
		}

		// Token: 0x04002349 RID: 9033
		private const int _stickyAmount = 2;
	}
}
