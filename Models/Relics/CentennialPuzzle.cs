using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004D1 RID: 1233
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CentennialPuzzle : RelicModel
	{
		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x06004A97 RID: 19095 RVA: 0x002124A0 File Offset: 0x002106A0
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x06004A98 RID: 19096 RVA: 0x002124A3 File Offset: 0x002106A3
		public override string FlashSfx
		{
			get
			{
				return "event:/sfx/ui/relic_activate_draw";
			}
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x06004A99 RID: 19097 RVA: 0x002124AA File Offset: 0x002106AA
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x06004A9A RID: 19098 RVA: 0x002124B7 File Offset: 0x002106B7
		// (set) Token: 0x06004A9B RID: 19099 RVA: 0x002124BF File Offset: 0x002106BF
		public bool UsedThisCombat
		{
			get
			{
				return this._usedThisCombat;
			}
			private set
			{
				if (this._usedThisCombat == value)
				{
					return;
				}
				base.AssertMutable();
				this._usedThisCombat = value;
			}
		}

		// Token: 0x06004A9C RID: 19100 RVA: 0x002124D8 File Offset: 0x002106D8
		public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			if (CombatManager.Instance.IsInProgress)
			{
				if (target == base.Owner.Creature)
				{
					if (result.UnblockedDamage > 0)
					{
						if (!this.UsedThisCombat)
						{
							base.Flash();
							this.UsedThisCombat = true;
							int i = 0;
							while (i < base.DynamicVars.Cards.BaseValue)
							{
								await CardPileCmd.Draw(choiceContext, base.Owner);
								i++;
							}
						}
					}
				}
			}
		}

		// Token: 0x06004A9D RID: 19101 RVA: 0x00212533 File Offset: 0x00210733
		public override Task AfterCombatEnd(CombatRoom _)
		{
			this.UsedThisCombat = false;
			return Task.CompletedTask;
		}

		// Token: 0x0400219E RID: 8606
		private bool _usedThisCombat;
	}
}
