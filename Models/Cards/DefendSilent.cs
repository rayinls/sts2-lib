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
	// Token: 0x02000913 RID: 2323
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DefendSilent : CardModel
	{
		// Token: 0x0600697D RID: 27005 RVA: 0x0025962F File Offset: 0x0025782F
		public DefendSilent()
			: base(1, CardType.Skill, CardRarity.Basic, TargetType.Self, true)
		{
		}

		// Token: 0x17001BBC RID: 7100
		// (get) Token: 0x0600697E RID: 27006 RVA: 0x0025963C File Offset: 0x0025783C
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001BBD RID: 7101
		// (get) Token: 0x0600697F RID: 27007 RVA: 0x0025963F File Offset: 0x0025783F
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Defend };
			}
		}

		// Token: 0x17001BBE RID: 7102
		// (get) Token: 0x06006980 RID: 27008 RVA: 0x0025964E File Offset: 0x0025784E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(5m, ValueProp.Move));
			}
		}

		// Token: 0x06006981 RID: 27009 RVA: 0x00259664 File Offset: 0x00257864
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
		}

		// Token: 0x06006982 RID: 27010 RVA: 0x002596AF File Offset: 0x002578AF
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}
