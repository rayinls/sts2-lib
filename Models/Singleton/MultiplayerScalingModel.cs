using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Singleton
{
	// Token: 0x020004A6 RID: 1190
	[NullableContext(1)]
	[Nullable(0)]
	public class MultiplayerScalingModel : SingletonModel
	{
		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06004977 RID: 18807 RVA: 0x00210064 File Offset: 0x0020E264
		public override bool ShouldReceiveCombatHooks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004978 RID: 18808 RVA: 0x00210067 File Offset: 0x0020E267
		public void Initialize(RunState state)
		{
			if (this._runState != null)
			{
				throw new InvalidOperationException("Already initialized");
			}
			this._runState = state;
		}

		// Token: 0x06004979 RID: 18809 RVA: 0x00210083 File Offset: 0x0020E283
		public void OnCombatEntered(CombatState combatState)
		{
			this._combatState = combatState;
		}

		// Token: 0x0600497A RID: 18810 RVA: 0x0021008C File Offset: 0x0020E28C
		public void OnCombatFinished()
		{
			this._combatState = null;
		}

		// Token: 0x0600497B RID: 18811 RVA: 0x00210098 File Offset: 0x0020E298
		[NullableContext(2)]
		public override decimal ModifyBlockMultiplicative([Nullable(1)] Creature target, decimal block, ValueProp props, CardModel cardSource, CardPlay cardPlay)
		{
			if (target != null && !target.IsPrimaryEnemy && !target.IsSecondaryEnemy)
			{
				return 1m;
			}
			if (!props.IsPoweredCardOrMonsterMoveBlock())
			{
				return 1m;
			}
			int count = this._runState.Players.Count;
			if (count == 1)
			{
				return 1m;
			}
			return count * MultiplayerScalingModel.GetMultiplayerScaling(this._combatState.Encounter, this._runState.CurrentActIndex);
		}

		// Token: 0x0600497C RID: 18812 RVA: 0x00210110 File Offset: 0x0020E310
		public override decimal ModifyPowerAmountGiven(PowerModel power, Creature giver, decimal amount, [Nullable(2)] Creature target, [Nullable(2)] CardModel cardSource)
		{
			if (target == null)
			{
				return amount;
			}
			if (target != null && !target.IsPrimaryEnemy && !target.IsSecondaryEnemy)
			{
				return amount;
			}
			if (!power.ShouldScaleInMultiplayer)
			{
				return amount;
			}
			int count = this._runState.Players.Count;
			if (count == 1)
			{
				return amount;
			}
			bool flag = power is ArtifactPower || power is SlipperyPower || power is PlatingPower || power is BufferPower;
			if (flag)
			{
				return ((count - 1) * 2 + 1) * amount;
			}
			return amount * count * MultiplayerScalingModel.GetMultiplayerScaling(this._combatState.Encounter, this._runState.CurrentActIndex);
		}

		// Token: 0x0600497D RID: 18813 RVA: 0x002101C4 File Offset: 0x0020E3C4
		[NullableContext(2)]
		public static decimal GetMultiplayerScaling(EncounterModel encounter, int actIndex)
		{
			if (actIndex == 0)
			{
				return 1.1m;
			}
			if (actIndex == 1)
			{
				return 1.2m;
			}
			if (actIndex != 2)
			{
				throw new ArgumentOutOfRangeException("actIndex", actIndex, "Invalid act index for HP scaling");
			}
			if (encounter != null && encounter.RoomType == RoomType.Boss)
			{
				return 1.3m;
			}
			return 1.2m;
		}

		// Token: 0x0400217D RID: 8573
		private RunState _runState;

		// Token: 0x0400217E RID: 8574
		[Nullable(2)]
		private CombatState _combatState;
	}
}
