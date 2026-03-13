using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A82 RID: 2690
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Survivor : CardModel
	{
		// Token: 0x0600713E RID: 28990 RVA: 0x00268D61 File Offset: 0x00266F61
		public Survivor()
			: base(1, CardType.Skill, CardRarity.Basic, TargetType.Self, true)
		{
		}

		// Token: 0x17001EFB RID: 7931
		// (get) Token: 0x0600713F RID: 28991 RVA: 0x00268D6E File Offset: 0x00266F6E
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001EFC RID: 7932
		// (get) Token: 0x06007140 RID: 28992 RVA: 0x00268D71 File Offset: 0x00266F71
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(8m, ValueProp.Move));
			}
		}

		// Token: 0x06007141 RID: 28993 RVA: 0x00268D84 File Offset: 0x00266F84
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHandForDiscard(choiceContext, base.Owner, new CardSelectorPrefs(CardSelectorPrefs.DiscardSelectionPrompt, 1), null, this);
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				await CardCmd.Discard(choiceContext, cardModel);
			}
		}

		// Token: 0x06007142 RID: 28994 RVA: 0x00268DD7 File Offset: 0x00266FD7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}
