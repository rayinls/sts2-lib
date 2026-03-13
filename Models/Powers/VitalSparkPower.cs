using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006CF RID: 1743
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class VitalSparkPower : PowerModel
	{
		// Token: 0x17001350 RID: 4944
		// (get) Token: 0x060056B7 RID: 22199 RVA: 0x00228F77 File Offset: 0x00227177
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001351 RID: 4945
		// (get) Token: 0x060056B8 RID: 22200 RVA: 0x00228F7A File Offset: 0x0022717A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001352 RID: 4946
		// (get) Token: 0x060056B9 RID: 22201 RVA: 0x00228F7D File Offset: 0x0022717D
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x17001353 RID: 4947
		// (get) Token: 0x060056BA RID: 22202 RVA: 0x00228F8A File Offset: 0x0022718A
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(1));
			}
		}

		// Token: 0x060056BB RID: 22203 RVA: 0x00228F97 File Offset: 0x00227197
		protected override object InitInternalData()
		{
			return new VitalSparkPower.Data();
		}

		// Token: 0x060056BC RID: 22204 RVA: 0x00228FA0 File Offset: 0x002271A0
		public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			if (target == base.Owner)
			{
				if (dealer != null)
				{
					if (props.IsPoweredAttack())
					{
						if (!result.WasFullyBlocked)
						{
							Creature creature = dealer;
							if (dealer.Monster is Osty)
							{
								creature = dealer.PetOwner.Creature;
							}
							if (creature.Player != null)
							{
								if (!base.GetInternalData<VitalSparkPower.Data>().playersTriggeredThisTurn.Contains(creature.Player))
								{
									base.GetInternalData<VitalSparkPower.Data>().playersTriggeredThisTurn.Add(creature.Player);
									base.Flash();
									await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, creature.Player);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060056BD RID: 22205 RVA: 0x00229005 File Offset: 0x00227205
		public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side != CombatSide.Enemy)
			{
				return Task.CompletedTask;
			}
			base.GetInternalData<VitalSparkPower.Data>().playersTriggeredThisTurn.Clear();
			return Task.CompletedTask;
		}

		// Token: 0x02001AD8 RID: 6872
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x04006A27 RID: 27175
			[Nullable(1)]
			public readonly HashSet<Player> playersTriggeredThisTurn = new HashSet<Player>();
		}
	}
}
