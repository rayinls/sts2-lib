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
	// Token: 0x0200090F RID: 2319
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DefendDefect : CardModel
	{
		// Token: 0x06006965 RID: 26981 RVA: 0x002593CF File Offset: 0x002575CF
		public DefendDefect()
			: base(1, CardType.Skill, CardRarity.Basic, TargetType.Self, true)
		{
		}

		// Token: 0x17001BB0 RID: 7088
		// (get) Token: 0x06006966 RID: 26982 RVA: 0x002593DC File Offset: 0x002575DC
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001BB1 RID: 7089
		// (get) Token: 0x06006967 RID: 26983 RVA: 0x002593DF File Offset: 0x002575DF
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Defend };
			}
		}

		// Token: 0x17001BB2 RID: 7090
		// (get) Token: 0x06006968 RID: 26984 RVA: 0x002593EE File Offset: 0x002575EE
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(5m, ValueProp.Move));
			}
		}

		// Token: 0x06006969 RID: 26985 RVA: 0x00259404 File Offset: 0x00257604
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
		}

		// Token: 0x0600696A RID: 26986 RVA: 0x0025944F File Offset: 0x0025764F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}
