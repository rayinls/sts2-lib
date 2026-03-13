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
	// Token: 0x02000912 RID: 2322
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DefendRegent : CardModel
	{
		// Token: 0x06006977 RID: 26999 RVA: 0x00259597 File Offset: 0x00257797
		public DefendRegent()
			: base(1, CardType.Skill, CardRarity.Basic, TargetType.Self, true)
		{
		}

		// Token: 0x17001BB9 RID: 7097
		// (get) Token: 0x06006978 RID: 27000 RVA: 0x002595A4 File Offset: 0x002577A4
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001BBA RID: 7098
		// (get) Token: 0x06006979 RID: 27001 RVA: 0x002595A7 File Offset: 0x002577A7
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Defend };
			}
		}

		// Token: 0x17001BBB RID: 7099
		// (get) Token: 0x0600697A RID: 27002 RVA: 0x002595B6 File Offset: 0x002577B6
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(5m, ValueProp.Move));
			}
		}

		// Token: 0x0600697B RID: 27003 RVA: 0x002595CC File Offset: 0x002577CC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
		}

		// Token: 0x0600697C RID: 27004 RVA: 0x00259617 File Offset: 0x00257817
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}
