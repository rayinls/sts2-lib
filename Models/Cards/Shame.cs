using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A46 RID: 2630
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Shame : CardModel
	{
		// Token: 0x06006FED RID: 28653 RVA: 0x002662A9 File Offset: 0x002644A9
		public Shame()
			: base(-1, CardType.Curse, CardRarity.Curse, TargetType.None, true)
		{
		}

		// Token: 0x17001E71 RID: 7793
		// (get) Token: 0x06006FEE RID: 28654 RVA: 0x002662B7 File Offset: 0x002644B7
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001E72 RID: 7794
		// (get) Token: 0x06006FEF RID: 28655 RVA: 0x002662BA File Offset: 0x002644BA
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Unplayable);
			}
		}

		// Token: 0x17001E73 RID: 7795
		// (get) Token: 0x06006FF0 RID: 28656 RVA: 0x002662C2 File Offset: 0x002644C2
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<FrailPower>());
			}
		}

		// Token: 0x17001E74 RID: 7796
		// (get) Token: 0x06006FF1 RID: 28657 RVA: 0x002662CE File Offset: 0x002644CE
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Frail", 1m));
			}
		}

		// Token: 0x17001E75 RID: 7797
		// (get) Token: 0x06006FF2 RID: 28658 RVA: 0x002662E4 File Offset: 0x002644E4
		public override bool HasTurnEndInHandEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006FF3 RID: 28659 RVA: 0x002662E8 File Offset: 0x002644E8
		public override async Task OnTurnEndInHand(PlayerChoiceContext choiceContext)
		{
			bool alreadyHasFrail = base.Owner.Creature.HasPower<FrailPower>();
			FrailPower frailPower = await PowerCmd.Apply<FrailPower>(base.Owner.Creature, base.DynamicVars["Frail"].BaseValue, null, this, false);
			PowerModel powerModel = frailPower;
			if (powerModel != null && !alreadyHasFrail)
			{
				powerModel.SkipNextDurationTick = true;
			}
		}

		// Token: 0x040025CA RID: 9674
		private const string _frailKey = "Frail";
	}
}
