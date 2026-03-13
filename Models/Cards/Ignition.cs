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
	// Token: 0x02000998 RID: 2456
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Ignition : CardModel
	{
		// Token: 0x06006C4F RID: 27727 RVA: 0x0025EE9F File Offset: 0x0025D09F
		public Ignition()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.AnyAlly, true)
		{
		}

		// Token: 0x17001CF4 RID: 7412
		// (get) Token: 0x06006C50 RID: 27728 RVA: 0x0025EEAC File Offset: 0x0025D0AC
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.MultiplayerOnly;
			}
		}

		// Token: 0x17001CF5 RID: 7413
		// (get) Token: 0x06006C51 RID: 27729 RVA: 0x0025EEAF File Offset: 0x0025D0AF
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001CF6 RID: 7414
		// (get) Token: 0x06006C52 RID: 27730 RVA: 0x0025EEB7 File Offset: 0x0025D0B7
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<PlasmaOrb>()
				});
			}
		}

		// Token: 0x06006C53 RID: 27731 RVA: 0x0025EEDC File Offset: 0x0025D0DC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await OrbCmd.Channel<PlasmaOrb>(choiceContext, cardPlay.Target.Player);
		}

		// Token: 0x06006C54 RID: 27732 RVA: 0x0025EF2F File Offset: 0x0025D12F
		protected override void OnUpgrade()
		{
			base.RemoveKeyword(CardKeyword.Exhaust);
		}
	}
}
