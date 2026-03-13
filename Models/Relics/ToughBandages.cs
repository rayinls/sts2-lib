using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005AD RID: 1453
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ToughBandages : RelicModel
	{
		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x0600500A RID: 20490 RVA: 0x0021C44A File Offset: 0x0021A64A
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x0600500B RID: 20491 RVA: 0x0021C44D File Offset: 0x0021A64D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(3m, ValueProp.Unpowered));
			}
		}

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x0600500C RID: 20492 RVA: 0x0021C460 File Offset: 0x0021A660
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x0600500D RID: 20493 RVA: 0x0021C474 File Offset: 0x0021A674
		public override async Task AfterCardDiscarded(PlayerChoiceContext choiceContext, CardModel card)
		{
			if (card.Owner == base.Owner)
			{
				if (base.Owner.Creature.Side == base.Owner.Creature.CombatState.CurrentSide)
				{
					base.Flash();
					await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, null, false);
				}
			}
		}
	}
}
