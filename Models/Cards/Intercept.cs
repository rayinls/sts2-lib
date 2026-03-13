using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009A1 RID: 2465
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Intercept : CardModel
	{
		// Token: 0x06006C7D RID: 27773 RVA: 0x0025F338 File Offset: 0x0025D538
		public Intercept()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyAlly, true)
		{
		}

		// Token: 0x17001D09 RID: 7433
		// (get) Token: 0x06006C7E RID: 27774 RVA: 0x0025F345 File Offset: 0x0025D545
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.MultiplayerOnly;
			}
		}

		// Token: 0x17001D0A RID: 7434
		// (get) Token: 0x06006C7F RID: 27775 RVA: 0x0025F348 File Offset: 0x0025D548
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001D0B RID: 7435
		// (get) Token: 0x06006C80 RID: 27776 RVA: 0x0025F34B File Offset: 0x0025D54B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(9m, ValueProp.Move));
			}
		}

		// Token: 0x06006C81 RID: 27777 RVA: 0x0025F360 File Offset: 0x0025D560
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await PowerCmd.Apply<CoveredPower>(cardPlay.Target, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x06006C82 RID: 27778 RVA: 0x0025F3AB File Offset: 0x0025D5AB
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(4m);
		}
	}
}
