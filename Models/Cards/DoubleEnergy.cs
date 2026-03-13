using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000926 RID: 2342
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DoubleEnergy : CardModel
	{
		// Token: 0x060069E5 RID: 27109 RVA: 0x0025A17C File Offset: 0x0025837C
		public DoubleEnergy()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001BED RID: 7149
		// (get) Token: 0x060069E6 RID: 27110 RVA: 0x0025A189 File Offset: 0x00258389
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001BEE RID: 7150
		// (get) Token: 0x060069E7 RID: 27111 RVA: 0x0025A191 File Offset: 0x00258391
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x060069E8 RID: 27112 RVA: 0x0025A1A0 File Offset: 0x002583A0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PlayerCmd.GainEnergy(base.Owner.PlayerCombatState.Energy, base.Owner);
		}

		// Token: 0x060069E9 RID: 27113 RVA: 0x0025A1E3 File Offset: 0x002583E3
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
