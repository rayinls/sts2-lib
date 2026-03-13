using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009B9 RID: 2489
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Luminesce : CardModel
	{
		// Token: 0x06006CF7 RID: 27895 RVA: 0x00260293 File Offset: 0x0025E493
		public Luminesce()
			: base(0, CardType.Skill, CardRarity.Token, TargetType.Self, true)
		{
		}

		// Token: 0x17001D39 RID: 7481
		// (get) Token: 0x06006CF8 RID: 27896 RVA: 0x002602A0 File Offset: 0x0025E4A0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(2));
			}
		}

		// Token: 0x17001D3A RID: 7482
		// (get) Token: 0x06006CF9 RID: 27897 RVA: 0x002602AD File Offset: 0x0025E4AD
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x17001D3B RID: 7483
		// (get) Token: 0x06006CFA RID: 27898 RVA: 0x002602BA File Offset: 0x0025E4BA
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlyArray<CardKeyword>(new CardKeyword[]
				{
					CardKeyword.Exhaust,
					CardKeyword.Retain
				});
			}
		}

		// Token: 0x06006CFB RID: 27899 RVA: 0x002602D0 File Offset: 0x0025E4D0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
		}

		// Token: 0x06006CFC RID: 27900 RVA: 0x00260313 File Offset: 0x0025E513
		protected override void OnUpgrade()
		{
			base.DynamicVars.Energy.UpgradeValueBy(1m);
		}
	}
}
