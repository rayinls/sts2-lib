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
	// Token: 0x0200099A RID: 2458
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Impervious : CardModel
	{
		// Token: 0x06006C5A RID: 27738 RVA: 0x0025EFEB File Offset: 0x0025D1EB
		public Impervious()
			: base(2, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001CF9 RID: 7417
		// (get) Token: 0x06006C5B RID: 27739 RVA: 0x0025EFF8 File Offset: 0x0025D1F8
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001CFA RID: 7418
		// (get) Token: 0x06006C5C RID: 27740 RVA: 0x0025EFFB File Offset: 0x0025D1FB
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(30m, ValueProp.Move));
			}
		}

		// Token: 0x17001CFB RID: 7419
		// (get) Token: 0x06006C5D RID: 27741 RVA: 0x0025F00F File Offset: 0x0025D20F
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x06006C5E RID: 27742 RVA: 0x0025F018 File Offset: 0x0025D218
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
		}

		// Token: 0x06006C5F RID: 27743 RVA: 0x0025F063 File Offset: 0x0025D263
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(10m);
		}
	}
}
