using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Unlocks;

namespace MegaCrit.Sts2.Core.Models
{
	// Token: 0x02000493 RID: 1171
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class CardPoolModel : AbstractModel, IPoolModel
	{
		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x060046A7 RID: 18087
		public abstract string Title { get; }

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x060046A8 RID: 18088
		public abstract string EnergyColorName { get; }

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x060046A9 RID: 18089
		public abstract string CardFrameMaterialPath { get; }

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x060046AA RID: 18090 RVA: 0x001FFA4C File Offset: 0x001FDC4C
		public string FrameMaterialPath
		{
			get
			{
				return "res://materials/cards/frames/" + this.CardFrameMaterialPath + "_mat.tres";
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x060046AB RID: 18091 RVA: 0x001FFA63 File Offset: 0x001FDC63
		public Material FrameMaterial
		{
			get
			{
				return PreloadManager.Cache.GetMaterial(this.FrameMaterialPath);
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x060046AC RID: 18092
		public abstract Color DeckEntryCardColor { get; }

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x060046AD RID: 18093 RVA: 0x001FFA75 File Offset: 0x001FDC75
		public virtual Color EnergyOutlineColor
		{
			get
			{
				return new Color("5C5440");
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x060046AE RID: 18094 RVA: 0x001FFA81 File Offset: 0x001FDC81
		public string EnergyIconPath
		{
			get
			{
				return EnergyIconHelper.GetPath(this.EnergyColorName);
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x060046AF RID: 18095 RVA: 0x001FFA8E File Offset: 0x001FDC8E
		public virtual IEnumerable<CardModel> AllCards
		{
			get
			{
				if (this._allCards == null)
				{
					this._allCards = this.GenerateAllCards();
					this._allCards = ModHelper.ConcatModelsFromMods<CardModel>(this, this._allCards).ToArray<CardModel>();
				}
				return this._allCards;
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x060046B0 RID: 18096 RVA: 0x001FFAC4 File Offset: 0x001FDCC4
		public IEnumerable<ModelId> AllCardIds
		{
			get
			{
				HashSet<ModelId> hashSet;
				if ((hashSet = this._allCardIds) == null)
				{
					hashSet = (this._allCardIds = this.AllCards.Select((CardModel c) => c.Id).ToHashSet<ModelId>());
				}
				return hashSet;
			}
		}

		// Token: 0x060046B1 RID: 18097
		protected abstract CardModel[] GenerateAllCards();

		// Token: 0x060046B2 RID: 18098 RVA: 0x001FFB14 File Offset: 0x001FDD14
		public IEnumerable<CardModel> GetUnlockedCards(UnlockState unlockState, CardMultiplayerConstraint multiplayerConstraint)
		{
			List<CardModel> list = this.FilterThroughEpochs(unlockState, this.AllCards).ToList<CardModel>();
			if (multiplayerConstraint != CardMultiplayerConstraint.MultiplayerOnly)
			{
				if (multiplayerConstraint == CardMultiplayerConstraint.SingleplayerOnly)
				{
					list.RemoveAll((CardModel c) => c.MultiplayerConstraint == CardMultiplayerConstraint.MultiplayerOnly);
				}
			}
			else
			{
				list.RemoveAll((CardModel c) => c.MultiplayerConstraint == CardMultiplayerConstraint.SingleplayerOnly);
			}
			return list;
		}

		// Token: 0x060046B3 RID: 18099 RVA: 0x001FFB8D File Offset: 0x001FDD8D
		protected virtual IEnumerable<CardModel> FilterThroughEpochs(UnlockState unlockState, IEnumerable<CardModel> cards)
		{
			return cards.ToList<CardModel>();
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x060046B4 RID: 18100
		public abstract bool IsColorless { get; }

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x060046B5 RID: 18101 RVA: 0x001FFB95 File Offset: 0x001FDD95
		public override bool ShouldReceiveCombatHooks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060046B6 RID: 18102 RVA: 0x001FFB98 File Offset: 0x001FDD98
		public CardPoolModel ToMutable()
		{
			base.AssertCanonical();
			return (CardPoolModel)base.MutableClone();
		}

		// Token: 0x04001AC4 RID: 6852
		[Nullable(new byte[] { 2, 1 })]
		private CardModel[] _allCards;

		// Token: 0x04001AC5 RID: 6853
		[Nullable(new byte[] { 2, 1 })]
		private HashSet<ModelId> _allCardIds;
	}
}
