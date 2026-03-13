using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Orbs;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A16 RID: 2582
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Rainbow : CardModel
	{
		// Token: 0x06006EEC RID: 28396 RVA: 0x00264400 File Offset: 0x00262600
		public Rainbow()
			: base(2, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001E06 RID: 7686
		// (get) Token: 0x06006EED RID: 28397 RVA: 0x0026440D File Offset: 0x0026260D
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001E07 RID: 7687
		// (get) Token: 0x06006EEE RID: 28398 RVA: 0x00264415 File Offset: 0x00262615
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<LightningOrb>(),
					HoverTipFactory.FromOrb<FrostOrb>(),
					HoverTipFactory.FromOrb<DarkOrb>()
				});
			}
		}

		// Token: 0x06006EEF RID: 28399 RVA: 0x00264448 File Offset: 0x00262648
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await OrbCmd.Channel<LightningOrb>(choiceContext, base.Owner);
			await OrbCmd.Channel<FrostOrb>(choiceContext, base.Owner);
			await OrbCmd.Channel<DarkOrb>(choiceContext, base.Owner);
		}

		// Token: 0x06006EF0 RID: 28400 RVA: 0x00264493 File Offset: 0x00262693
		protected override void OnUpgrade()
		{
			base.RemoveKeyword(CardKeyword.Exhaust);
		}
	}
}
