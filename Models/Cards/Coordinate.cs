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
	// Token: 0x020008F2 RID: 2290
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Coordinate : CardModel
	{
		// Token: 0x060068D2 RID: 26834 RVA: 0x00258286 File Offset: 0x00256486
		public Coordinate()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyAlly, true)
		{
		}

		// Token: 0x17001B70 RID: 7024
		// (get) Token: 0x060068D3 RID: 26835 RVA: 0x00258293 File Offset: 0x00256493
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.MultiplayerOnly;
			}
		}

		// Token: 0x17001B71 RID: 7025
		// (get) Token: 0x060068D4 RID: 26836 RVA: 0x00258296 File Offset: 0x00256496
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<StrengthPower>(5m));
			}
		}

		// Token: 0x17001B72 RID: 7026
		// (get) Token: 0x060068D5 RID: 26837 RVA: 0x002582A8 File Offset: 0x002564A8
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x060068D6 RID: 26838 RVA: 0x002582B4 File Offset: 0x002564B4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			decimal baseValue = base.DynamicVars.Strength.BaseValue;
			await PowerCmd.Apply<CoordinatePower>(cardPlay.Target, baseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060068D7 RID: 26839 RVA: 0x002582FF File Offset: 0x002564FF
		protected override void OnUpgrade()
		{
			base.DynamicVars.Strength.UpgradeValueBy(3m);
		}
	}
}
