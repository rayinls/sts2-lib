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
	// Token: 0x02000910 RID: 2320
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DefendIronclad : CardModel
	{
		// Token: 0x0600696B RID: 26987 RVA: 0x00259467 File Offset: 0x00257667
		public DefendIronclad()
			: base(1, CardType.Skill, CardRarity.Basic, TargetType.Self, true)
		{
		}

		// Token: 0x17001BB3 RID: 7091
		// (get) Token: 0x0600696C RID: 26988 RVA: 0x00259474 File Offset: 0x00257674
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001BB4 RID: 7092
		// (get) Token: 0x0600696D RID: 26989 RVA: 0x00259477 File Offset: 0x00257677
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Defend };
			}
		}

		// Token: 0x17001BB5 RID: 7093
		// (get) Token: 0x0600696E RID: 26990 RVA: 0x00259486 File Offset: 0x00257686
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(5m, ValueProp.Move));
			}
		}

		// Token: 0x0600696F RID: 26991 RVA: 0x0025949C File Offset: 0x0025769C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
		}

		// Token: 0x06006970 RID: 26992 RVA: 0x002594E7 File Offset: 0x002576E7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}
