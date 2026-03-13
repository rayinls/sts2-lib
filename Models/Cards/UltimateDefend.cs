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
	// Token: 0x02000AA8 RID: 2728
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class UltimateDefend : CardModel
	{
		// Token: 0x06007208 RID: 29192 RVA: 0x0026A504 File Offset: 0x00268704
		public UltimateDefend()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001F4B RID: 8011
		// (get) Token: 0x06007209 RID: 29193 RVA: 0x0026A511 File Offset: 0x00268711
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001F4C RID: 8012
		// (get) Token: 0x0600720A RID: 29194 RVA: 0x0026A514 File Offset: 0x00268714
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Defend };
			}
		}

		// Token: 0x17001F4D RID: 8013
		// (get) Token: 0x0600720B RID: 29195 RVA: 0x0026A523 File Offset: 0x00268723
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(11m, ValueProp.Move));
			}
		}

		// Token: 0x0600720C RID: 29196 RVA: 0x0026A537 File Offset: 0x00268737
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(4m);
		}

		// Token: 0x0600720D RID: 29197 RVA: 0x0026A550 File Offset: 0x00268750
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
		}
	}
}
