using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200058B RID: 1419
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SelfFormingClay : RelicModel
	{
		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06004F32 RID: 20274 RVA: 0x0021AB23 File Offset: 0x00218D23
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x06004F33 RID: 20275 RVA: 0x0021AB26 File Offset: 0x00218D26
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("BlockNextTurn", 3m));
			}
		}

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x06004F34 RID: 20276 RVA: 0x0021AB3D File Offset: 0x00218D3D
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06004F35 RID: 20277 RVA: 0x0021AB50 File Offset: 0x00218D50
		public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			if (target == base.Owner.Creature)
			{
				if (result.UnblockedDamage > 0)
				{
					await PowerCmd.Apply<SelfFormingClayPower>(base.Owner.Creature, base.DynamicVars["BlockNextTurn"].BaseValue, base.Owner.Creature, null, false);
				}
			}
		}

		// Token: 0x04002215 RID: 8725
		private const string _blockNextTurnKey = "BlockNextTurn";
	}
}
