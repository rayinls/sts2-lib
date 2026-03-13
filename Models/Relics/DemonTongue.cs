using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004E1 RID: 1249
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DemonTongue : RelicModel
	{
		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06004AE3 RID: 19171 RVA: 0x00212C5B File Offset: 0x00210E5B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x06004AE4 RID: 19172 RVA: 0x00212C60 File Offset: 0x00210E60
		public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			if (base.Owner.Creature.CombatState != null)
			{
				if (base.Owner.Creature.CombatState.CurrentSide == base.Owner.Creature.Side)
				{
					if (target == base.Owner.Creature)
					{
						if (result.UnblockedDamage > 0)
						{
							if (!this._triggeredThisTurn)
							{
								this._triggeredThisTurn = true;
								base.Flash();
								await CreatureCmd.Heal(base.Owner.Creature, result.UnblockedDamage, true);
							}
						}
					}
				}
			}
		}

		// Token: 0x06004AE5 RID: 19173 RVA: 0x00212CB3 File Offset: 0x00210EB3
		public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side != base.Owner.Creature.Side)
			{
				return Task.CompletedTask;
			}
			this._triggeredThisTurn = false;
			return Task.CompletedTask;
		}

		// Token: 0x040021A2 RID: 8610
		private bool _triggeredThisTurn;
	}
}
