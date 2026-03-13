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
	// Token: 0x020008A5 RID: 2213
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BeaconOfHope : CardModel
	{
		// Token: 0x0600674C RID: 26444 RVA: 0x0025519E File Offset: 0x0025339E
		public BeaconOfHope()
			: base(1, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001AD0 RID: 6864
		// (get) Token: 0x0600674D RID: 26445 RVA: 0x002551AB File Offset: 0x002533AB
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.MultiplayerOnly;
			}
		}

		// Token: 0x17001AD1 RID: 6865
		// (get) Token: 0x0600674E RID: 26446 RVA: 0x002551AE File Offset: 0x002533AE
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x0600674F RID: 26447 RVA: 0x002551C0 File Offset: 0x002533C0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<BeaconOfHopePower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x06006750 RID: 26448 RVA: 0x00255203 File Offset: 0x00253403
		protected override void OnUpgrade()
		{
			base.AddKeyword(CardKeyword.Innate);
		}
	}
}
