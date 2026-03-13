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
	// Token: 0x020008E4 RID: 2276
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CloakOfStars : CardModel
	{
		// Token: 0x0600688B RID: 26763 RVA: 0x00257952 File Offset: 0x00255B52
		public CloakOfStars()
			: base(0, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001B51 RID: 6993
		// (get) Token: 0x0600688C RID: 26764 RVA: 0x0025795F File Offset: 0x00255B5F
		public override int CanonicalStarCost
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17001B52 RID: 6994
		// (get) Token: 0x0600688D RID: 26765 RVA: 0x00257962 File Offset: 0x00255B62
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B53 RID: 6995
		// (get) Token: 0x0600688E RID: 26766 RVA: 0x00257965 File Offset: 0x00255B65
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(7m, ValueProp.Move));
			}
		}

		// Token: 0x0600688F RID: 26767 RVA: 0x00257978 File Offset: 0x00255B78
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
		}

		// Token: 0x06006890 RID: 26768 RVA: 0x002579C3 File Offset: 0x00255BC3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}
