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
	// Token: 0x02000938 RID: 2360
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Entropy : CardModel
	{
		// Token: 0x06006A44 RID: 27204 RVA: 0x0025AB85 File Offset: 0x00258D85
		public Entropy()
			: base(1, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001C19 RID: 7193
		// (get) Token: 0x06006A45 RID: 27205 RVA: 0x0025AB92 File Offset: 0x00258D92
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Transform, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x17001C1A RID: 7194
		// (get) Token: 0x06006A46 RID: 27206 RVA: 0x0025ABA4 File Offset: 0x00258DA4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(1));
			}
		}

		// Token: 0x06006A47 RID: 27207 RVA: 0x0025ABB4 File Offset: 0x00258DB4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<EntropyPower>(base.Owner.Creature, base.DynamicVars.Cards.IntValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006A48 RID: 27208 RVA: 0x0025ABF7 File Offset: 0x00258DF7
		protected override void OnUpgrade()
		{
			base.AddKeyword(CardKeyword.Innate);
		}
	}
}
