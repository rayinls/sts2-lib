using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models
{
	// Token: 0x0200048F RID: 1167
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class AfflictionModel : AbstractModel
	{
		// Token: 0x14000078 RID: 120
		// (add) Token: 0x06004586 RID: 17798 RVA: 0x001FCB0C File Offset: 0x001FAD0C
		// (remove) Token: 0x06004587 RID: 17799 RVA: 0x001FCB44 File Offset: 0x001FAD44
		[Nullable(2)]
		[method: NullableContext(2)]
		[field: Nullable(2)]
		public event Action<int, int> AmountChanged;

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06004588 RID: 17800 RVA: 0x001FCB79 File Offset: 0x001FAD79
		public LocString Title
		{
			get
			{
				return new LocString("afflictions", base.Id.Entry + ".title");
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06004589 RID: 17801 RVA: 0x001FCB9A File Offset: 0x001FAD9A
		public LocString Description
		{
			get
			{
				return new LocString("afflictions", base.Id.Entry + ".description");
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x0600458A RID: 17802 RVA: 0x001FCBBB File Offset: 0x001FADBB
		public LocString ExtraCardText
		{
			get
			{
				return new LocString("afflictions", base.Id.Entry + ".extraCardText");
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x0600458B RID: 17803 RVA: 0x001FCBDC File Offset: 0x001FADDC
		public virtual bool HasExtraCardText
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x0600458C RID: 17804 RVA: 0x001FCBE0 File Offset: 0x001FADE0
		public LocString DynamicDescription
		{
			get
			{
				LocString description = this.Description;
				description.Add("Amount", this.Amount);
				return description;
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x0600458D RID: 17805 RVA: 0x001FCC0C File Offset: 0x001FAE0C
		[Nullable(2)]
		public LocString DynamicExtraCardText
		{
			[NullableContext(2)]
			get
			{
				if (!this.HasExtraCardText)
				{
					return null;
				}
				LocString extraCardText = this.ExtraCardText;
				extraCardText.Add("Amount", this.Amount);
				return extraCardText;
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x0600458E RID: 17806 RVA: 0x001FCC41 File Offset: 0x001FAE41
		public string OverlayPath
		{
			get
			{
				return SceneHelper.GetScenePath("cards/overlays/afflictions/" + base.Id.Entry.ToLowerInvariant());
			}
		}

		// Token: 0x0600458F RID: 17807 RVA: 0x001FCC62 File Offset: 0x001FAE62
		public Control CreateOverlay()
		{
			return PreloadManager.Cache.GetScene(this.OverlayPath).Instantiate<Control>(PackedScene.GenEditState.Disabled);
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06004590 RID: 17808 RVA: 0x001FCC7B File Offset: 0x001FAE7B
		public bool HasOverlay
		{
			get
			{
				return ResourceLoader.Exists(this.OverlayPath, "");
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06004591 RID: 17809 RVA: 0x001FCC8D File Offset: 0x001FAE8D
		// (set) Token: 0x06004592 RID: 17810 RVA: 0x001FCC9B File Offset: 0x001FAE9B
		public CardModel Card
		{
			get
			{
				base.AssertMutable();
				return this._card;
			}
			set
			{
				base.AssertMutable();
				value.AssertMutable();
				if (this._card != null)
				{
					throw new InvalidOperationException("Afflictions cannot be moved from one card to another.");
				}
				this._card = value;
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06004593 RID: 17811 RVA: 0x001FCCC3 File Offset: 0x001FAEC3
		public bool HasCard
		{
			get
			{
				return this._card != null;
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06004594 RID: 17812 RVA: 0x001FCCCE File Offset: 0x001FAECE
		// (set) Token: 0x06004595 RID: 17813 RVA: 0x001FCCD8 File Offset: 0x001FAED8
		public int Amount
		{
			get
			{
				return this._amount;
			}
			set
			{
				base.AssertMutable();
				if (this._amount == value)
				{
					return;
				}
				int amount = this._amount;
				this._amount = value;
				if (this._card != null)
				{
					this._card.Owner.PlayerCombatState.RecalculateCardValues();
				}
				Action<int, int> amountChanged = this.AmountChanged;
				if (amountChanged == null)
				{
					return;
				}
				amountChanged(amount, this._amount);
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06004596 RID: 17814 RVA: 0x001FCD37 File Offset: 0x001FAF37
		public CombatState CombatState
		{
			get
			{
				return this.Card.CombatState;
			}
		}

		// Token: 0x06004597 RID: 17815 RVA: 0x001FCD44 File Offset: 0x001FAF44
		public virtual bool CanAfflictCardType(CardType cardType)
		{
			return true;
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06004598 RID: 17816 RVA: 0x001FCD47 File Offset: 0x001FAF47
		public virtual bool CanAfflictUnplayableCards
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06004599 RID: 17817 RVA: 0x001FCD4A File Offset: 0x001FAF4A
		public virtual bool IsStackable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600459A RID: 17818 RVA: 0x001FCD50 File Offset: 0x001FAF50
		public virtual bool CanAfflict(CardModel card)
		{
			return this.CanAfflictCardType(card.Type) && (!card.Keywords.Contains(CardKeyword.Unplayable) || this.CanAfflictUnplayableCards) && (card.Affliction == null || (this.IsStackable && !(card.Affliction.GetType() != base.GetType())));
		}

		// Token: 0x0600459B RID: 17819 RVA: 0x001FCDB0 File Offset: 0x001FAFB0
		public virtual void AfterApplied()
		{
		}

		// Token: 0x0600459C RID: 17820 RVA: 0x001FCDB2 File Offset: 0x001FAFB2
		public virtual void BeforeRemoved()
		{
		}

		// Token: 0x0600459D RID: 17821 RVA: 0x001FCDB4 File Offset: 0x001FAFB4
		public virtual Task OnPlay(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			return Task.CompletedTask;
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x0600459E RID: 17822 RVA: 0x001FCDBB File Offset: 0x001FAFBB
		// (set) Token: 0x0600459F RID: 17823 RVA: 0x001FCDCD File Offset: 0x001FAFCD
		public AfflictionModel CanonicalInstance
		{
			get
			{
				if (!base.IsMutable)
				{
					return this;
				}
				return this._canonicalInstance;
			}
			private set
			{
				base.AssertMutable();
				this._canonicalInstance = value;
			}
		}

		// Token: 0x060045A0 RID: 17824 RVA: 0x001FCDDC File Offset: 0x001FAFDC
		public AfflictionModel ToMutable()
		{
			base.AssertCanonical();
			AfflictionModel afflictionModel = (AfflictionModel)base.MutableClone();
			afflictionModel.CanonicalInstance = this;
			return afflictionModel;
		}

		// Token: 0x060045A1 RID: 17825 RVA: 0x001FCE03 File Offset: 0x001FB003
		protected override void AfterCloned()
		{
			base.AfterCloned();
			this.AmountChanged = null;
			this._card = null;
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x060045A2 RID: 17826 RVA: 0x001FCE19 File Offset: 0x001FB019
		public override bool ShouldReceiveCombatHooks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x060045A3 RID: 17827 RVA: 0x001FCE1C File Offset: 0x001FB01C
		protected virtual IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return Array.Empty<IHoverTip>();
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x060045A4 RID: 17828 RVA: 0x001FCE23 File Offset: 0x001FB023
		public HoverTip HoverTip
		{
			get
			{
				return new HoverTip(this, this.DynamicDescription);
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x060045A5 RID: 17829 RVA: 0x001FCE34 File Offset: 0x001FB034
		public unsafe IEnumerable<IHoverTip> HoverTips
		{
			get
			{
				int num = 1;
				List<IHoverTip> list = new List<IHoverTip>(num);
				CollectionsMarshal.SetCount<IHoverTip>(list, num);
				Span<IHoverTip> span = CollectionsMarshal.AsSpan<IHoverTip>(list);
				int num2 = 0;
				*span[num2] = this.HoverTip;
				List<IHoverTip> list2 = list;
				list2.AddRange(this.ExtraHoverTips);
				return list2;
			}
		}

		// Token: 0x060045A6 RID: 17830 RVA: 0x001FCE7C File Offset: 0x001FB07C
		public IReadOnlyList<CardModel> PickRandomTargets(RunRngSet rngSet, IEnumerable<CardModel> cards, int count)
		{
			List<CardModel> list = cards.Where(new Func<CardModel, bool>(this.CanAfflict)).ToList<CardModel>().UnstableShuffle(rngSet.CombatCardGeneration);
			list.RemoveRange(Math.Clamp(list.Count - 1, 0, count), Math.Max(0, list.Count - count));
			return list;
		}

		// Token: 0x060045A7 RID: 17831 RVA: 0x001FCED1 File Offset: 0x001FB0D1
		public void ClearInternal()
		{
			this.BeforeRemoved();
			this._card = null;
		}

		// Token: 0x04001A83 RID: 6787
		public const string locTable = "afflictions";

		// Token: 0x04001A85 RID: 6789
		[Nullable(2)]
		private CardModel _card;

		// Token: 0x04001A86 RID: 6790
		private int _amount;

		// Token: 0x04001A87 RID: 6791
		private AfflictionModel _canonicalInstance;
	}
}
