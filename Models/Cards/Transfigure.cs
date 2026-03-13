using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000AA1 RID: 2721
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Transfigure : CardModel
	{
		// Token: 0x060071E5 RID: 29157 RVA: 0x0026A139 File Offset: 0x00268339
		public Transfigure()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001F3D RID: 7997
		// (get) Token: 0x060071E6 RID: 29158 RVA: 0x0026A146 File Offset: 0x00268346
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001F3E RID: 7998
		// (get) Token: 0x060071E7 RID: 29159 RVA: 0x0026A14E File Offset: 0x0026834E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(1));
			}
		}

		// Token: 0x17001F3F RID: 7999
		// (get) Token: 0x060071E8 RID: 29160 RVA: 0x0026A15B File Offset: 0x0026835B
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					base.EnergyHoverTip,
					HoverTipFactory.Static(StaticHoverTip.ReplayStatic, Array.Empty<DynamicVar>())
				});
			}
		}

		// Token: 0x060071E9 RID: 29161 RVA: 0x0026A180 File Offset: 0x00268380
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHand(choiceContext, base.Owner, new CardSelectorPrefs(base.SelectionScreenPrompt, 1), null, this);
			IEnumerable<CardModel> enumerable2 = enumerable;
			foreach (CardModel cardModel in enumerable2)
			{
				if (!cardModel.EnergyCost.CostsX && cardModel.EnergyCost.GetWithModifiers(CostModifiers.None) >= 0)
				{
					cardModel.EnergyCost.AddThisCombat(1, false);
				}
				cardModel.BaseReplayCount++;
			}
		}

		// Token: 0x060071EA RID: 29162 RVA: 0x0026A1CB File Offset: 0x002683CB
		protected override void OnUpgrade()
		{
			base.RemoveKeyword(CardKeyword.Exhaust);
		}
	}
}
