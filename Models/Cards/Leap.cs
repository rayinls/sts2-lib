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
	// Token: 0x020009B2 RID: 2482
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Leap : CardModel
	{
		// Token: 0x06006CD0 RID: 27856 RVA: 0x0025FE43 File Offset: 0x0025E043
		public Leap()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001D27 RID: 7463
		// (get) Token: 0x06006CD1 RID: 27857 RVA: 0x0025FE50 File Offset: 0x0025E050
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001D28 RID: 7464
		// (get) Token: 0x06006CD2 RID: 27858 RVA: 0x0025FE53 File Offset: 0x0025E053
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(9m, ValueProp.Move));
			}
		}

		// Token: 0x06006CD3 RID: 27859 RVA: 0x0025FE68 File Offset: 0x0025E068
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
		}

		// Token: 0x06006CD4 RID: 27860 RVA: 0x0025FEB3 File Offset: 0x0025E0B3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}
