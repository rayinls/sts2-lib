using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A28 RID: 2600
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Restlessness : CardModel
	{
		// Token: 0x06006F51 RID: 28497 RVA: 0x0026503F File Offset: 0x0026323F
		public Restlessness()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001E31 RID: 7729
		// (get) Token: 0x06006F52 RID: 28498 RVA: 0x0026504C File Offset: 0x0026324C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(2),
					new EnergyVar(2)
				});
			}
		}

		// Token: 0x17001E32 RID: 7730
		// (get) Token: 0x06006F53 RID: 28499 RVA: 0x0026506B File Offset: 0x0026326B
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x17001E33 RID: 7731
		// (get) Token: 0x06006F54 RID: 28500 RVA: 0x00265078 File Offset: 0x00263278
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Retain);
			}
		}

		// Token: 0x17001E34 RID: 7732
		// (get) Token: 0x06006F55 RID: 28501 RVA: 0x00265080 File Offset: 0x00263280
		protected override bool ShouldGlowGoldInternal
		{
			get
			{
				return this.IsOnlyCardInHand;
			}
		}

		// Token: 0x06006F56 RID: 28502 RVA: 0x00265088 File Offset: 0x00263288
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			if (this.IsOnlyCardInHand)
			{
				for (int i = 0; i < base.DynamicVars.Cards.IntValue; i++)
				{
					await CardPileCmd.Draw(choiceContext, base.Owner);
				}
				await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);
			}
		}

		// Token: 0x06006F57 RID: 28503 RVA: 0x002650D3 File Offset: 0x002632D3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
			base.DynamicVars.Energy.UpgradeValueBy(1m);
		}

		// Token: 0x17001E35 RID: 7733
		// (get) Token: 0x06006F58 RID: 28504 RVA: 0x002650FF File Offset: 0x002632FF
		private bool IsOnlyCardInHand
		{
			get
			{
				return !PileType.Hand.GetPile(base.Owner).Cards.Except(new <>z__ReadOnlySingleElementList<CardModel>(this)).Any<CardModel>();
			}
		}
	}
}
