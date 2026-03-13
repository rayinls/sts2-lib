using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200097E RID: 2430
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HammerTime : CardModel
	{
		// Token: 0x06006BC9 RID: 27593 RVA: 0x0025DCD8 File Offset: 0x0025BED8
		public HammerTime()
			: base(2, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001CC0 RID: 7360
		// (get) Token: 0x06006BCA RID: 27594 RVA: 0x0025DCE5 File Offset: 0x0025BEE5
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.MultiplayerOnly;
			}
		}

		// Token: 0x17001CC1 RID: 7361
		// (get) Token: 0x06006BCB RID: 27595 RVA: 0x0025DCE8 File Offset: 0x0025BEE8
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromForge();
			}
		}

		// Token: 0x06006BCC RID: 27596 RVA: 0x0025DCF0 File Offset: 0x0025BEF0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<HammerTimePower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x06006BCD RID: 27597 RVA: 0x0025DD33 File Offset: 0x0025BF33
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
