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
	// Token: 0x02000968 RID: 2408
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GatherLight : CardModel
	{
		// Token: 0x06006B52 RID: 27474 RVA: 0x0025CE6F File Offset: 0x0025B06F
		public GatherLight()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001C8D RID: 7309
		// (get) Token: 0x06006B53 RID: 27475 RVA: 0x0025CE7C File Offset: 0x0025B07C
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C8E RID: 7310
		// (get) Token: 0x06006B54 RID: 27476 RVA: 0x0025CE7F File Offset: 0x0025B07F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(7m, ValueProp.Move),
					new StarsVar(1)
				});
			}
		}

		// Token: 0x06006B55 RID: 27477 RVA: 0x0025CEA4 File Offset: 0x0025B0A4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await PlayerCmd.GainStars(base.DynamicVars.Stars.BaseValue, base.Owner);
		}

		// Token: 0x06006B56 RID: 27478 RVA: 0x0025CEEF File Offset: 0x0025B0EF
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}
