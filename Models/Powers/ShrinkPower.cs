using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000694 RID: 1684
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ShrinkPower : PowerModel
	{
		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x06005541 RID: 21825 RVA: 0x00226213 File Offset: 0x00224413
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x06005542 RID: 21826 RVA: 0x00226216 File Offset: 0x00224416
		public override PowerStackType StackType
		{
			get
			{
				if (!this.IsInfinite)
				{
					return PowerStackType.Counter;
				}
				return PowerStackType.Single;
			}
		}

		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x06005543 RID: 21827 RVA: 0x00226223 File Offset: 0x00224423
		public override bool AllowNegative
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x06005544 RID: 21828 RVA: 0x00226226 File Offset: 0x00224426
		private bool IsInfinite
		{
			get
			{
				return base.Amount < 0;
			}
		}

		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x06005545 RID: 21829 RVA: 0x00226231 File Offset: 0x00224431
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("DamageDecrease", 30m),
					new StringVar("ApplierName", "")
				});
			}
		}

		// Token: 0x06005546 RID: 21830 RVA: 0x00226264 File Offset: 0x00224464
		[NullableContext(2)]
		[return: Nullable(1)]
		public override Task AfterApplied(Creature applier, CardModel cardSource)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				NCreature creatureNode = instance.GetCreatureNode(base.Owner);
				if (creatureNode != null)
				{
					creatureNode.ScaleTo(0.5f, 0.75f);
				}
			}
			Creature applier2 = base.Applier;
			if (applier2 != null && applier2.IsMonster)
			{
				((StringVar)base.DynamicVars["ApplierName"]).StringValue = base.Applier.Monster.Title.GetFormattedText();
			}
			return Task.CompletedTask;
		}

		// Token: 0x06005547 RID: 21831 RVA: 0x002262E2 File Offset: 0x002244E2
		public override Task AfterRemoved(Creature oldOwner)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				NCreature creatureNode = instance.GetCreatureNode(oldOwner);
				if (creatureNode != null)
				{
					creatureNode.ScaleTo(1f, 0.75f);
				}
			}
			return Task.CompletedTask;
		}

		// Token: 0x06005548 RID: 21832 RVA: 0x00226310 File Offset: 0x00224510
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (!this.IsInfinite)
			{
				if (side == base.Owner.Side)
				{
					await PowerCmd.Decrement(this);
				}
			}
		}

		// Token: 0x06005549 RID: 21833 RVA: 0x0022635C File Offset: 0x0022455C
		public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (!wasRemovalPrevented)
			{
				if (creature == base.Applier)
				{
					await PowerCmd.Remove(this);
				}
			}
		}

		// Token: 0x0600554A RID: 21834 RVA: 0x002263B0 File Offset: 0x002245B0
		[NullableContext(2)]
		public override decimal ModifyDamageMultiplicative(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (base.Owner != dealer)
			{
				return 1m;
			}
			if (!props.IsPoweredAttack())
			{
				return 1m;
			}
			return (100m - base.DynamicVars["DamageDecrease"].BaseValue) / 100m;
		}

		// Token: 0x0400226C RID: 8812
		public const decimal damageDecrease = 30m;

		// Token: 0x0400226D RID: 8813
		private const string _damageDecreaseKey = "DamageDecrease";

		// Token: 0x0400226E RID: 8814
		private const string _applierNameKey = "ApplierName";
	}
}
