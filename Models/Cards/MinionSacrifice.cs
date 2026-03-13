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
	// Token: 0x020009CE RID: 2510
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MinionSacrifice : CardModel
	{
		// Token: 0x06006D7C RID: 28028 RVA: 0x00261527 File Offset: 0x0025F727
		public MinionSacrifice()
			: base(0, CardType.Skill, CardRarity.Token, TargetType.Self, true)
		{
		}

		// Token: 0x17001D70 RID: 7536
		// (get) Token: 0x06006D7D RID: 28029 RVA: 0x00261534 File Offset: 0x0025F734
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001D71 RID: 7537
		// (get) Token: 0x06006D7E RID: 28030 RVA: 0x00261537 File Offset: 0x0025F737
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Minion };
			}
		}

		// Token: 0x17001D72 RID: 7538
		// (get) Token: 0x06006D7F RID: 28031 RVA: 0x00261546 File Offset: 0x0025F746
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001D73 RID: 7539
		// (get) Token: 0x06006D80 RID: 28032 RVA: 0x0026154E File Offset: 0x0025F74E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(9m, ValueProp.Move));
			}
		}

		// Token: 0x06006D81 RID: 28033 RVA: 0x00261564 File Offset: 0x0025F764
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
		}

		// Token: 0x06006D82 RID: 28034 RVA: 0x002615AF File Offset: 0x0025F7AF
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}
