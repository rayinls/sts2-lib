using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x020007A4 RID: 1956
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Wriggler : MonsterModel
	{
		// Token: 0x170017D3 RID: 6099
		// (get) Token: 0x06006074 RID: 24692 RVA: 0x00242BCF File Offset: 0x00240DCF
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 18, 17);
			}
		}

		// Token: 0x170017D4 RID: 6100
		// (get) Token: 0x06006075 RID: 24693 RVA: 0x00242BDB File Offset: 0x00240DDB
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 22, 21);
			}
		}

		// Token: 0x170017D5 RID: 6101
		// (get) Token: 0x06006076 RID: 24694 RVA: 0x00242BE7 File Offset: 0x00240DE7
		private int BiteDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 7, 6);
			}
		}

		// Token: 0x170017D6 RID: 6102
		// (get) Token: 0x06006077 RID: 24695 RVA: 0x00242BF2 File Offset: 0x00240DF2
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Plant;
			}
		}

		// Token: 0x170017D7 RID: 6103
		// (get) Token: 0x06006078 RID: 24696 RVA: 0x00242BF5 File Offset: 0x00240DF5
		// (set) Token: 0x06006079 RID: 24697 RVA: 0x00242BFD File Offset: 0x00240DFD
		public bool StartStunned
		{
			get
			{
				return this._startStunned;
			}
			set
			{
				base.AssertMutable();
				this._startStunned = value;
			}
		}

		// Token: 0x0600607A RID: 24698 RVA: 0x00242C0C File Offset: 0x00240E0C
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("NASTY_BITE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BiteMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.BiteDamage)
			});
			MoveState moveState2 = new MoveState("WRIGGLE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.WriggleMove), new AbstractIntent[]
			{
				new BuffIntent(),
				new StatusIntent(1)
			});
			MoveState moveState3 = new MoveState("SPAWNED_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SpawnedMove), new AbstractIntent[]
			{
				new StunIntent()
			});
			ConditionalBranchState conditionalBranchState = new ConditionalBranchState("INIT_MOVE");
			conditionalBranchState.AddState(moveState, () => base.Creature.SlotName == "wriggler1");
			conditionalBranchState.AddState(moveState2, () => base.Creature.SlotName == "wriggler2");
			conditionalBranchState.AddState(moveState, () => base.Creature.SlotName == "wriggler3");
			conditionalBranchState.AddState(moveState2, () => base.Creature.SlotName == "wriggler4");
			moveState3.FollowUpState = conditionalBranchState;
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState;
			list.Add(moveState3);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(conditionalBranchState);
			MonsterState monsterState = (this.StartStunned ? moveState3 : conditionalBranchState);
			return new MonsterMoveStateMachine(list, monsterState);
		}

		// Token: 0x0600607B RID: 24699 RVA: 0x00242D43 File Offset: 0x00240F43
		private Task SpawnedMove(IReadOnlyList<Creature> targets)
		{
			return Task.CompletedTask;
		}

		// Token: 0x0600607C RID: 24700 RVA: 0x00242D4C File Offset: 0x00240F4C
		private async Task BiteMove(IReadOnlyList<Creature> targets)
		{
			AttackCommand attackCommand = DamageCmd.Attack(this.BiteDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_bite", null, null);
			Func<Creature, Node2D> func;
			if ((func = Wriggler.<>O.<0>__Create) == null)
			{
				func = (Wriggler.<>O.<0>__Create = new Func<Creature, Node2D>(NWormyImpactVfx.Create));
			}
			await attackCommand.WithHitVfxNode(func).Execute(null);
		}

		// Token: 0x0600607D RID: 24701 RVA: 0x00242D90 File Offset: 0x00240F90
		private async Task WriggleMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play(this.AttackSfx, 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Attack", 0.15f);
			foreach (Creature creature in targets)
			{
				NWormyImpactVfx nwormyImpactVfx = NWormyImpactVfx.Create(creature);
				if (nwormyImpactVfx != null)
				{
					NCombatRoom.Instance.CombatVfxContainer.AddChildSafely(nwormyImpactVfx);
				}
			}
			await CardPileCmd.AddToCombatAndPreview<Infection>(targets, PileType.Discard, 1, false, CardPilePosition.Bottom);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 2m, base.Creature, null, false);
		}

		// Token: 0x04002460 RID: 9312
		private const string _spawnedMoveId = "SPAWNED_MOVE";

		// Token: 0x04002461 RID: 9313
		private const string _initMoveId = "INIT_MOVE";

		// Token: 0x04002462 RID: 9314
		private const int _wriggleStrength = 2;

		// Token: 0x04002463 RID: 9315
		private bool _startStunned;

		// Token: 0x02001CDD RID: 7389
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040073CF RID: 29647
			[Nullable(new byte[] { 0, 1, 2 })]
			public static Func<Creature, Node2D> <0>__Create;
		}
	}
}
