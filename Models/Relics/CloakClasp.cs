using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004D9 RID: 1241
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CloakClasp : RelicModel
	{
		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x06004AC1 RID: 19137 RVA: 0x002128F0 File Offset: 0x00210AF0
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x06004AC2 RID: 19138 RVA: 0x002128F3 File Offset: 0x00210AF3
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(1m, ValueProp.Unpowered));
			}
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x06004AC3 RID: 19139 RVA: 0x00212905 File Offset: 0x00210B05
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06004AC4 RID: 19140 RVA: 0x00212918 File Offset: 0x00210B18
		public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Creature.Side)
			{
				IReadOnlyList<CardModel> cards = PileType.Hand.GetPile(base.Owner).Cards;
				if (cards.Count != 0)
				{
					int num = (int)(cards.Count * base.DynamicVars.Block.BaseValue);
					base.Flash();
					await CreatureCmd.GainBlock(base.Owner.Creature, num, ValueProp.Unpowered, null, false);
				}
			}
		}
	}
}
