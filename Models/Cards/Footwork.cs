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
	// Token: 0x0200095C RID: 2396
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Footwork : CardModel
	{
		// Token: 0x06006B0F RID: 27407 RVA: 0x0025C605 File Offset: 0x0025A805
		public Footwork()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001C71 RID: 7281
		// (get) Token: 0x06006B10 RID: 27408 RVA: 0x0025C612 File Offset: 0x0025A812
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<DexterityPower>(2m));
			}
		}

		// Token: 0x17001C72 RID: 7282
		// (get) Token: 0x06006B11 RID: 27409 RVA: 0x0025C624 File Offset: 0x0025A824
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DexterityPower>());
			}
		}

		// Token: 0x06006B12 RID: 27410 RVA: 0x0025C630 File Offset: 0x0025A830
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<DexterityPower>(base.Owner.Creature, base.DynamicVars.Dexterity.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006B13 RID: 27411 RVA: 0x0025C673 File Offset: 0x0025A873
		protected override void OnUpgrade()
		{
			base.DynamicVars.Dexterity.UpgradeValueBy(1m);
		}
	}
}
