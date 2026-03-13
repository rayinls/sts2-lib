using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000547 RID: 1351
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NinjaScroll : RelicModel
	{
		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06004D65 RID: 19813 RVA: 0x0021754F File Offset: 0x0021574F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06004D66 RID: 19814 RVA: 0x00217552 File Offset: 0x00215752
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Shivs", 3m));
			}
		}

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06004D67 RID: 19815 RVA: 0x00217569 File Offset: 0x00215769
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Shiv>(false));
			}
		}

		// Token: 0x06004D68 RID: 19816 RVA: 0x00217578 File Offset: 0x00215778
		public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
		{
			if (player == base.Owner)
			{
				if (combatState.RoundNumber <= 1)
				{
					base.Flash();
					await Shiv.CreateInHand(base.Owner, base.DynamicVars["Shivs"].IntValue, combatState);
				}
			}
		}

		// Token: 0x040021E3 RID: 8675
		private const string _shivKey = "Shivs";
	}
}
