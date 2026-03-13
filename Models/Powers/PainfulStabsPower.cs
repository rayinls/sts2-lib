using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200066A RID: 1642
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PainfulStabsPower : PowerModel
	{
		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x0600543F RID: 21567 RVA: 0x002242FB File Offset: 0x002224FB
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x06005440 RID: 21568 RVA: 0x002242FE File Offset: 0x002224FE
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x06005441 RID: 21569 RVA: 0x00224301 File Offset: 0x00222501
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromCardWithCardHoverTips<Wound>(false);
			}
		}

		// Token: 0x06005442 RID: 21570 RVA: 0x00224309 File Offset: 0x00222509
		public override bool ShouldPowerBeRemovedAfterOwnerDeath()
		{
			return false;
		}

		// Token: 0x06005443 RID: 21571 RVA: 0x0022430C File Offset: 0x0022250C
		public override bool ShouldCreatureBeRemovedFromCombatAfterDeath(Creature creature)
		{
			return creature != base.Owner;
		}

		// Token: 0x06005444 RID: 21572 RVA: 0x0022431C File Offset: 0x0022251C
		public override async Task AfterAttack(AttackCommand command)
		{
			if (command.Attacker == base.Owner)
			{
				if (command.TargetSide != base.Owner.Side)
				{
					if (command.DamageProps.IsPoweredAttack())
					{
						if (command.Results.Any((DamageResult r) => r.UnblockedDamage > 0))
						{
							Dictionary<Creature, List<DamageResult>> damageResultsByCreature = new Dictionary<Creature, List<DamageResult>>();
							foreach (DamageResult damageResult in command.Results)
							{
								if (damageResult.Receiver.IsPlayer)
								{
									if (!damageResultsByCreature.ContainsKey(damageResult.Receiver))
									{
										damageResultsByCreature.Add(damageResult.Receiver, new List<DamageResult>());
									}
									damageResultsByCreature[damageResult.Receiver].Add(damageResult);
								}
							}
							bool anyWoundApplied = false;
							foreach (Creature creature in damageResultsByCreature.Keys)
							{
								int num = damageResultsByCreature[creature].Count((DamageResult r) => r.UnblockedDamage > 0);
								anyWoundApplied |= num > 0;
								await CardPileCmd.AddToCombatAndPreview<Wound>(creature, PileType.Discard, base.Amount * num, false, CardPilePosition.Bottom);
							}
							Dictionary<Creature, List<DamageResult>>.KeyCollection.Enumerator enumerator2 = default(Dictionary<Creature, List<DamageResult>>.KeyCollection.Enumerator);
							if (anyWoundApplied)
							{
								base.Flash();
							}
						}
					}
				}
			}
		}
	}
}
