using System;
using System.Collections.Generic;
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
	// Token: 0x02000AAA RID: 2730
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Undeath : CardModel
	{
		// Token: 0x06007213 RID: 29203 RVA: 0x0026A637 File Offset: 0x00268837
		public Undeath()
			: base(0, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001F50 RID: 8016
		// (get) Token: 0x06007214 RID: 29204 RVA: 0x0026A644 File Offset: 0x00268844
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001F51 RID: 8017
		// (get) Token: 0x06007215 RID: 29205 RVA: 0x0026A647 File Offset: 0x00268847
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(7m, ValueProp.Move));
			}
		}

		// Token: 0x06007216 RID: 29206 RVA: 0x0026A65C File Offset: 0x0026885C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			CardModel cardModel = base.CreateClone();
			CardPileAddResult cardPileAddResult = await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Discard, true, CardPilePosition.Bottom);
			CardCmd.PreviewCardPileAdd(cardPileAddResult, 2.2f, CardPreviewStyle.HorizontalLayout);
		}

		// Token: 0x06007217 RID: 29207 RVA: 0x0026A6A7 File Offset: 0x002688A7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(2m);
		}
	}
}
