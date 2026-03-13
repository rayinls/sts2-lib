using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000895 RID: 2197
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Armaments : CardModel
	{
		// Token: 0x060066F9 RID: 26361 RVA: 0x002546D4 File Offset: 0x002528D4
		public Armaments()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001AAC RID: 6828
		// (get) Token: 0x060066FA RID: 26362 RVA: 0x002546E1 File Offset: 0x002528E1
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001AAD RID: 6829
		// (get) Token: 0x060066FB RID: 26363 RVA: 0x002546E4 File Offset: 0x002528E4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(5m, ValueProp.Move));
			}
		}

		// Token: 0x060066FC RID: 26364 RVA: 0x002546F8 File Offset: 0x002528F8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			if (base.IsUpgraded)
			{
				using (IEnumerator<CardModel> enumerator = PileType.Hand.GetPile(base.Owner).Cards.Where((CardModel c) => c.IsUpgradable).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						CardModel cardModel = enumerator.Current;
						CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
					}
					return;
				}
			}
			CardModel cardModel2 = await CardSelectCmd.FromHandForUpgrade(choiceContext, base.Owner, this);
			if (cardModel2 != null)
			{
				CardCmd.Upgrade(cardModel2, CardPreviewStyle.HorizontalLayout);
			}
		}
	}
}
