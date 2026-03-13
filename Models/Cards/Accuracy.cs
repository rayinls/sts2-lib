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
	// Token: 0x02000886 RID: 2182
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Accuracy : CardModel
	{
		// Token: 0x060066B0 RID: 26288 RVA: 0x00253EA3 File Offset: 0x002520A3
		public Accuracy()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001A91 RID: 6801
		// (get) Token: 0x060066B1 RID: 26289 RVA: 0x00253EB0 File Offset: 0x002520B0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<AccuracyPower>(4m));
			}
		}

		// Token: 0x17001A92 RID: 6802
		// (get) Token: 0x060066B2 RID: 26290 RVA: 0x00253EC2 File Offset: 0x002520C2
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Shiv>(false));
			}
		}

		// Token: 0x060066B3 RID: 26291 RVA: 0x00253ED0 File Offset: 0x002520D0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<AccuracyPower>(base.Owner.Creature, base.DynamicVars["AccuracyPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060066B4 RID: 26292 RVA: 0x00253F13 File Offset: 0x00252113
		protected override void OnUpgrade()
		{
			base.DynamicVars["AccuracyPower"].UpgradeValueBy(2m);
		}
	}
}
