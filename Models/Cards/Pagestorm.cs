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
	// Token: 0x020009EE RID: 2542
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Pagestorm : CardModel
	{
		// Token: 0x06006E25 RID: 28197 RVA: 0x002629DF File Offset: 0x00260BDF
		public Pagestorm()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001DB9 RID: 7609
		// (get) Token: 0x06006E26 RID: 28198 RVA: 0x002629EC File Offset: 0x00260BEC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(1));
			}
		}

		// Token: 0x17001DBA RID: 7610
		// (get) Token: 0x06006E27 RID: 28199 RVA: 0x002629F9 File Offset: 0x00260BF9
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Ethereal));
			}
		}

		// Token: 0x06006E28 RID: 28200 RVA: 0x00262A08 File Offset: 0x00260C08
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<PagestormPower>(base.Owner.Creature, base.DynamicVars.Cards.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006E29 RID: 28201 RVA: 0x00262A4B File Offset: 0x00260C4B
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
