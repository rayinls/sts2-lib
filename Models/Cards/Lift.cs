using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009B6 RID: 2486
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Lift : CardModel
	{
		// Token: 0x06006CE7 RID: 27879 RVA: 0x002600C5 File Offset: 0x0025E2C5
		public Lift()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyAlly, true)
		{
		}

		// Token: 0x17001D32 RID: 7474
		// (get) Token: 0x06006CE8 RID: 27880 RVA: 0x002600D2 File Offset: 0x0025E2D2
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.MultiplayerOnly;
			}
		}

		// Token: 0x17001D33 RID: 7475
		// (get) Token: 0x06006CE9 RID: 27881 RVA: 0x002600D5 File Offset: 0x0025E2D5
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(11m, ValueProp.Move));
			}
		}

		// Token: 0x17001D34 RID: 7476
		// (get) Token: 0x06006CEA RID: 27882 RVA: 0x002600E9 File Offset: 0x0025E2E9
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006CEB RID: 27883 RVA: 0x002600EC File Offset: 0x0025E2EC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.GainBlock(cardPlay.Target, base.DynamicVars.Block, cardPlay, false);
		}

		// Token: 0x06006CEC RID: 27884 RVA: 0x00260137 File Offset: 0x0025E337
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(5m);
		}
	}
}
