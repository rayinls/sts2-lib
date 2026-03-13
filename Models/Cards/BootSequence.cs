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
	// Token: 0x020008BC RID: 2236
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BootSequence : CardModel
	{
		// Token: 0x060067C7 RID: 26567 RVA: 0x0025611F File Offset: 0x0025431F
		public BootSequence()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001B04 RID: 6916
		// (get) Token: 0x060067C8 RID: 26568 RVA: 0x0025612C File Offset: 0x0025432C
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B05 RID: 6917
		// (get) Token: 0x060067C9 RID: 26569 RVA: 0x0025612F File Offset: 0x0025432F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(10m, ValueProp.Move));
			}
		}

		// Token: 0x17001B06 RID: 6918
		// (get) Token: 0x060067CA RID: 26570 RVA: 0x00256143 File Offset: 0x00254343
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlyArray<CardKeyword>(new CardKeyword[]
				{
					CardKeyword.Innate,
					CardKeyword.Exhaust
				});
			}
		}

		// Token: 0x060067CB RID: 26571 RVA: 0x00256158 File Offset: 0x00254358
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
		}

		// Token: 0x060067CC RID: 26572 RVA: 0x002561A3 File Offset: 0x002543A3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}
