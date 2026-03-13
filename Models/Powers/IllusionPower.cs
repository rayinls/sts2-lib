using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000643 RID: 1603
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class IllusionPower : PowerModel
	{
		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x0600535A RID: 21338 RVA: 0x00222AA2 File Offset: 0x00220CA2
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x0600535B RID: 21339 RVA: 0x00222AA5 File Offset: 0x00220CA5
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x0600535C RID: 21340 RVA: 0x00222AA8 File Offset: 0x00220CA8
		public override bool ShouldPlayVfx
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x0600535D RID: 21341 RVA: 0x00222AAB File Offset: 0x00220CAB
		// (set) Token: 0x0600535E RID: 21342 RVA: 0x00222AB3 File Offset: 0x00220CB3
		[Nullable(2)]
		public string FollowUpStateId
		{
			[NullableContext(2)]
			get
			{
				return this._followUpStateId;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				this._followUpStateId = value;
			}
		}

		// Token: 0x0600535F RID: 21343 RVA: 0x00222AC2 File Offset: 0x00220CC2
		protected override object InitInternalData()
		{
			return new IllusionPower.Data();
		}

		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x06005360 RID: 21344 RVA: 0x00222AC9 File Offset: 0x00220CC9
		public bool IsReviving
		{
			get
			{
				return base.GetInternalData<IllusionPower.Data>().isReviving;
			}
		}

		// Token: 0x06005361 RID: 21345 RVA: 0x00222AD6 File Offset: 0x00220CD6
		public override bool ShouldPowerBeRemovedOnDeath(PowerModel power)
		{
			return power.Type == PowerType.Debuff;
		}

		// Token: 0x06005362 RID: 21346 RVA: 0x00222AE1 File Offset: 0x00220CE1
		[NullableContext(2)]
		[return: Nullable(1)]
		public override Task AfterApplied(Creature applier, CardModel cardSource)
		{
			if (base.Owner.HasPower<MinionPower>())
			{
				return Task.CompletedTask;
			}
			return PowerCmd.Apply<MinionPower>(base.Owner, 1m, null, null, false);
		}

		// Token: 0x06005363 RID: 21347 RVA: 0x00222B0C File Offset: 0x00220D0C
		public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (!wasRemovalPrevented)
			{
				if (creature == base.Owner)
				{
					await CreatureCmd.TriggerAnim(base.Owner, "StunTrigger", 0f);
					base.GetInternalData<IllusionPower.Data>().isReviving = true;
					MoveState moveState = new MoveState("REVIVE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ReviveMove), new AbstractIntent[]
					{
						new HealIntent()
					})
					{
						FollowUpStateId = (this.FollowUpStateId ?? base.Owner.Monster.MoveStateMachine.StateLog.Last<MonsterState>().Id),
						MustPerformOnceBeforeTransitioning = true
					};
					base.Owner.Monster.SetMoveImmediate(moveState, false);
				}
			}
		}

		// Token: 0x06005364 RID: 21348 RVA: 0x00222B5F File Offset: 0x00220D5F
		public override bool ShouldAllowHitting(Creature creature)
		{
			return creature != base.Owner || !this.IsReviving;
		}

		// Token: 0x06005365 RID: 21349 RVA: 0x00222B77 File Offset: 0x00220D77
		public override bool ShouldCreatureBeRemovedFromCombatAfterDeath(Creature creature)
		{
			return creature != base.Owner;
		}

		// Token: 0x06005366 RID: 21350 RVA: 0x00222B88 File Offset: 0x00220D88
		public override async Task AfterCombatEnd(CombatRoom room)
		{
			if (!base.Owner.IsAlive)
			{
				await CreatureCmd.TriggerAnim(base.Owner, "Dead", 0.1f);
			}
		}

		// Token: 0x06005367 RID: 21351 RVA: 0x00222BCC File Offset: 0x00220DCC
		private async Task ReviveMove(IReadOnlyList<Creature> targets)
		{
			await CreatureCmd.TriggerAnim(base.Owner, "WakeUpTrigger", 0f);
			base.GetInternalData<IllusionPower.Data>().isReviving = false;
			await CreatureCmd.Heal(base.Owner, base.Owner.MaxHp - base.Owner.CurrentHp, true);
			if (base.Owner.Monster is Parafright)
			{
				SfxCmd.Play("event:/sfx/enemy/enemy_attacks/obscura/obscura_hologram_heal", 1f);
			}
		}

		// Token: 0x04002256 RID: 8790
		public const string stunTrigger = "StunTrigger";

		// Token: 0x04002257 RID: 8791
		public const string wakeUpTrigger = "WakeUpTrigger";

		// Token: 0x04002258 RID: 8792
		[Nullable(2)]
		private string _followUpStateId;

		// Token: 0x02001A1B RID: 6683
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x04006645 RID: 26181
			public bool isReviving;
		}
	}
}
