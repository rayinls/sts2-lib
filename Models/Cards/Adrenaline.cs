using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000889 RID: 2185
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Adrenaline : CardModel
	{
		// Token: 0x060066BE RID: 26302 RVA: 0x0025404B File Offset: 0x0025224B
		public Adrenaline()
			: base(0, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001A96 RID: 6806
		// (get) Token: 0x060066BF RID: 26303 RVA: 0x00254058 File Offset: 0x00252258
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new EnergyVar(1),
					new CardsVar(2)
				});
			}
		}

		// Token: 0x17001A97 RID: 6807
		// (get) Token: 0x060066C0 RID: 26304 RVA: 0x00254077 File Offset: 0x00252277
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x17001A98 RID: 6808
		// (get) Token: 0x060066C1 RID: 26305 RVA: 0x00254084 File Offset: 0x00252284
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x060066C2 RID: 26306 RVA: 0x0025408C File Offset: 0x0025228C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			if (LocalContext.IsMe(base.Owner))
			{
				VfxCmd.PlayFullScreenInCombat("vfx/vfx_adrenaline");
			}
			await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}

		// Token: 0x060066C3 RID: 26307 RVA: 0x002540D7 File Offset: 0x002522D7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Energy.UpgradeValueBy(1m);
		}
	}
}
