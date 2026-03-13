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
	// Token: 0x02000911 RID: 2321
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DefendNecrobinder : CardModel
	{
		// Token: 0x06006971 RID: 26993 RVA: 0x002594FF File Offset: 0x002576FF
		public DefendNecrobinder()
			: base(1, CardType.Skill, CardRarity.Basic, TargetType.Self, true)
		{
		}

		// Token: 0x17001BB6 RID: 7094
		// (get) Token: 0x06006972 RID: 26994 RVA: 0x0025950C File Offset: 0x0025770C
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001BB7 RID: 7095
		// (get) Token: 0x06006973 RID: 26995 RVA: 0x0025950F File Offset: 0x0025770F
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Defend };
			}
		}

		// Token: 0x17001BB8 RID: 7096
		// (get) Token: 0x06006974 RID: 26996 RVA: 0x0025951E File Offset: 0x0025771E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(5m, ValueProp.Move));
			}
		}

		// Token: 0x06006975 RID: 26997 RVA: 0x00259534 File Offset: 0x00257734
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
		}

		// Token: 0x06006976 RID: 26998 RVA: 0x0025957F File Offset: 0x0025777F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}
