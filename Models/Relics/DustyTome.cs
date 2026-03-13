using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004EC RID: 1260
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DustyTome : RelicModel
	{
		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x06004B16 RID: 19222 RVA: 0x002132C0 File Offset: 0x002114C0
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x06004B17 RID: 19223 RVA: 0x002132C3 File Offset: 0x002114C3
		// (set) Token: 0x06004B18 RID: 19224 RVA: 0x002132CC File Offset: 0x002114CC
		[Nullable(2)]
		[SavedProperty]
		public ModelId AncientCard
		{
			[NullableContext(2)]
			get
			{
				return this._ancientCard;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				this._ancientCard = value;
				if (this._ancientCard != null)
				{
					CardModel cardModel = SaveUtil.CardOrDeprecated(this._ancientCard);
					this._extraHoverTips = cardModel.HoverTips.Concat(new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard(cardModel, true)));
					((StringVar)base.DynamicVars["AncientCard"]).StringValue = cardModel.Title;
				}
			}
		}

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x06004B19 RID: 19225 RVA: 0x0021333D File Offset: 0x0021153D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new StringVar("AncientCard", ""));
			}
		}

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x06004B1A RID: 19226 RVA: 0x00213353 File Offset: 0x00211553
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return this._extraHoverTips;
			}
		}

		// Token: 0x06004B1B RID: 19227 RVA: 0x0021335C File Offset: 0x0021155C
		public void SetupForPlayer(Player player)
		{
			IEnumerable<CardModel> enumerable = from c in player.Character.CardPool.GetUnlockedCards(player.UnlockState, player.RunState.CardMultiplayerConstraint)
				where c.Rarity == CardRarity.Ancient && !ArchaicTooth.TranscendenceCards.Contains(c)
				select c;
			this.AncientCard = player.PlayerRng.Rewards.NextItem<CardModel>(enumerable).Id;
		}

		// Token: 0x06004B1C RID: 19228 RVA: 0x002133CC File Offset: 0x002115CC
		public override async Task AfterObtained()
		{
			CardModel cardModel = base.Owner.RunState.CreateCard(ModelDb.GetById<CardModel>(this.AncientCard), base.Owner);
			CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
			CardPileAddResult cardPileAddResult = await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, null, false);
			CardPileAddResult cardPileAddResult2 = cardPileAddResult;
			CardCmd.PreviewCardPileAdd(cardPileAddResult2, 2f, CardPreviewStyle.HorizontalLayout);
		}

		// Token: 0x040021A6 RID: 8614
		private const string _ancientCardKey = "AncientCard";

		// Token: 0x040021A7 RID: 8615
		[Nullable(2)]
		private ModelId _ancientCard;

		// Token: 0x040021A8 RID: 8616
		private IEnumerable<IHoverTip> _extraHoverTips = Array.Empty<IHoverTip>();
	}
}
