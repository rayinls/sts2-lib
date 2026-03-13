using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Enchantments;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Saves.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models
{
	// Token: 0x02000495 RID: 1173
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class EnchantmentModel : AbstractModel
	{
		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06004703 RID: 18179 RVA: 0x00200560 File Offset: 0x001FE760
		public LocString Title
		{
			get
			{
				return new LocString("enchantments", base.Id.Entry + ".title");
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06004704 RID: 18180 RVA: 0x00200581 File Offset: 0x001FE781
		public LocString Description
		{
			get
			{
				return new LocString("enchantments", base.Id.Entry + ".description");
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06004705 RID: 18181 RVA: 0x002005A2 File Offset: 0x001FE7A2
		public LocString ExtraCardText
		{
			get
			{
				return new LocString("enchantments", base.Id.Entry + ".extraCardText");
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06004706 RID: 18182 RVA: 0x002005C3 File Offset: 0x001FE7C3
		public virtual bool HasExtraCardText
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06004707 RID: 18183 RVA: 0x002005C8 File Offset: 0x001FE7C8
		public LocString DynamicDescription
		{
			get
			{
				LocString description = this.Description;
				description.Add("Amount", this.Amount);
				DynamicVarSet dynamicVarSet = this.DynamicVars.Clone(this);
				dynamicVarSet.ClearPreview();
				CardModel card = this._card;
				if (card != null)
				{
					card.UpdateDynamicVarPreview(CardPreviewMode.None, null, dynamicVarSet);
				}
				description.Add("energyPrefix", EnergyIconHelper.GetPrefix(this));
				dynamicVarSet.AddTo(description);
				return description;
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06004708 RID: 18184 RVA: 0x00200634 File Offset: 0x001FE834
		[Nullable(2)]
		public LocString DynamicExtraCardText
		{
			[NullableContext(2)]
			get
			{
				if (!this.HasExtraCardText || this.Status == EnchantmentStatus.Disabled)
				{
					return null;
				}
				LocString extraCardText = this.ExtraCardText;
				extraCardText.Add("Amount", this.Amount);
				this.DynamicVars.AddTo(extraCardText);
				return extraCardText;
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06004709 RID: 18185 RVA: 0x0020067E File Offset: 0x001FE87E
		public static string MissingIconPath
		{
			get
			{
				return ImageHelper.GetImagePath("enchantments/missing_enchantment.png");
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x0600470A RID: 18186 RVA: 0x0020068A File Offset: 0x001FE88A
		public string IntendedIconPath
		{
			get
			{
				return ImageHelper.GetImagePath("enchantments/" + base.Id.Entry.ToLowerInvariant() + ".png");
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x0600470B RID: 18187 RVA: 0x002006B0 File Offset: 0x001FE8B0
		private string BetaIconPath
		{
			get
			{
				return ImageHelper.GetImagePath("enchantments/beta/" + base.Id.Entry.ToLowerInvariant() + ".png");
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x0600470C RID: 18188 RVA: 0x002006D8 File Offset: 0x001FE8D8
		public string IconPath
		{
			get
			{
				if (this._iconPath == null)
				{
					if (ResourceLoader.Exists(this.IntendedIconPath, ""))
					{
						this._iconPath = this.IntendedIconPath;
					}
					else if (ResourceLoader.Exists(this.BetaIconPath, ""))
					{
						this._iconPath = this.BetaIconPath;
					}
					else
					{
						this._iconPath = EnchantmentModel.MissingIconPath;
					}
				}
				return this._iconPath;
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x0600470D RID: 18189 RVA: 0x0020073E File Offset: 0x001FE93E
		public CompressedTexture2D Icon
		{
			get
			{
				return PreloadManager.Cache.GetCompressedTexture2D(this.IconPath);
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x0600470E RID: 18190 RVA: 0x00200750 File Offset: 0x001FE950
		public virtual bool ShowAmount
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x0600470F RID: 18191 RVA: 0x00200753 File Offset: 0x001FE953
		public virtual int DisplayAmount
		{
			get
			{
				return this.Amount;
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06004710 RID: 18192 RVA: 0x0020075B File Offset: 0x001FE95B
		public override bool PreviewOutsideOfCombat
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06004711 RID: 18193 RVA: 0x00200760 File Offset: 0x001FE960
		public override bool ShouldReceiveCombatHooks
		{
			get
			{
				CardModel card = this.Card;
				return card != null && card.ShouldReceiveCombatHooks;
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06004712 RID: 18194 RVA: 0x0020077F File Offset: 0x001FE97F
		public virtual bool ShouldStartAtBottomOfDrawPile
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06004713 RID: 18195 RVA: 0x00200782 File Offset: 0x001FE982
		// (set) Token: 0x06004714 RID: 18196 RVA: 0x00200790 File Offset: 0x001FE990
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
					throw new InvalidOperationException("Enchantments cannot be moved from one card to another.");
				}
				this._card = value;
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06004715 RID: 18197 RVA: 0x002007B8 File Offset: 0x001FE9B8
		public bool HasCard
		{
			get
			{
				return this._card != null;
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06004716 RID: 18198 RVA: 0x002007C3 File Offset: 0x001FE9C3
		// (set) Token: 0x06004717 RID: 18199 RVA: 0x002007CB File Offset: 0x001FE9CB
		public int Amount
		{
			get
			{
				return this._amount;
			}
			set
			{
				base.AssertMutable();
				this._amount = value;
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06004718 RID: 18200 RVA: 0x002007DA File Offset: 0x001FE9DA
		// (set) Token: 0x06004719 RID: 18201 RVA: 0x002007E2 File Offset: 0x001FE9E2
		[Nullable(2)]
		[JsonPropertyName("props")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public SavedProperties Props
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			set;
		}

		// Token: 0x0600471A RID: 18202 RVA: 0x002007EB File Offset: 0x001FE9EB
		public virtual bool CanEnchantCardType(CardType cardType)
		{
			return true;
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x0600471B RID: 18203 RVA: 0x002007EE File Offset: 0x001FE9EE
		public virtual bool IsStackable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600471C RID: 18204 RVA: 0x002007F4 File Offset: 0x001FE9F4
		public virtual bool CanEnchant(CardModel card)
		{
			CardType type = card.Type;
			bool flag = type - CardType.Status <= 2;
			if (flag)
			{
				return false;
			}
			if (!this.CanEnchantCardType(card.Type))
			{
				return false;
			}
			CardPile pile = card.Pile;
			return (pile == null || pile.Type != PileType.Deck || !card.Keywords.Contains(CardKeyword.Unplayable)) && (card.Enchantment == null || (this.IsStackable && !(card.Enchantment.GetType() != base.GetType())));
		}

		// Token: 0x0600471D RID: 18205 RVA: 0x0020087B File Offset: 0x001FEA7B
		public virtual Task OnPlay(PlayerChoiceContext choiceContext, [Nullable(2)] CardPlay cardPlay)
		{
			return Task.CompletedTask;
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x0600471E RID: 18206 RVA: 0x00200882 File Offset: 0x001FEA82
		public DynamicVarSet DynamicVars
		{
			get
			{
				if (this._dynamicVars != null)
				{
					return this._dynamicVars;
				}
				this._dynamicVars = new DynamicVarSet(this.CanonicalVars);
				this._dynamicVars.InitializeWithOwner(this);
				return this._dynamicVars;
			}
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x0600471F RID: 18207 RVA: 0x002008B6 File Offset: 0x001FEAB6
		protected virtual IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return Array.Empty<DynamicVar>();
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06004720 RID: 18208 RVA: 0x002008BD File Offset: 0x001FEABD
		// (set) Token: 0x06004721 RID: 18209 RVA: 0x002008C5 File Offset: 0x001FEAC5
		public EnchantmentStatus Status
		{
			get
			{
				return this._status;
			}
			set
			{
				base.AssertMutable();
				if (this._status == value)
				{
					return;
				}
				this._status = value;
				Action statusChanged = this.StatusChanged;
				if (statusChanged == null)
				{
					return;
				}
				statusChanged();
			}
		}

		// Token: 0x14000083 RID: 131
		// (add) Token: 0x06004722 RID: 18210 RVA: 0x002008F0 File Offset: 0x001FEAF0
		// (remove) Token: 0x06004723 RID: 18211 RVA: 0x00200928 File Offset: 0x001FEB28
		[Nullable(2)]
		[method: NullableContext(2)]
		[field: Nullable(2)]
		public event Action StatusChanged;

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06004724 RID: 18212 RVA: 0x0020095D File Offset: 0x001FEB5D
		public virtual bool ShouldGlowGold
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06004725 RID: 18213 RVA: 0x00200960 File Offset: 0x001FEB60
		public virtual bool ShouldGlowRed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06004726 RID: 18214 RVA: 0x00200963 File Offset: 0x001FEB63
		// (set) Token: 0x06004727 RID: 18215 RVA: 0x00200975 File Offset: 0x001FEB75
		public EnchantmentModel CanonicalInstance
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

		// Token: 0x06004728 RID: 18216 RVA: 0x00200984 File Offset: 0x001FEB84
		public EnchantmentModel ToMutable()
		{
			base.AssertCanonical();
			EnchantmentModel enchantmentModel = (EnchantmentModel)base.MutableClone();
			enchantmentModel.CanonicalInstance = this;
			return enchantmentModel;
		}

		// Token: 0x06004729 RID: 18217 RVA: 0x002009AB File Offset: 0x001FEBAB
		protected override void DeepCloneFields()
		{
			this._card = null;
			this.StatusChanged = null;
			this._dynamicVars = this.DynamicVars.Clone(this);
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x0600472A RID: 18218 RVA: 0x002009CD File Offset: 0x001FEBCD
		public HoverTip HoverTip
		{
			get
			{
				return new HoverTip(this.Title, this.DynamicDescription, this.Icon);
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x0600472B RID: 18219 RVA: 0x002009E6 File Offset: 0x001FEBE6
		protected virtual IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return Array.Empty<IHoverTip>();
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x0600472C RID: 18220 RVA: 0x002009F0 File Offset: 0x001FEBF0
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

		// Token: 0x0600472D RID: 18221 RVA: 0x00200A37 File Offset: 0x001FEC37
		public void ApplyInternal(CardModel card, decimal amount)
		{
			if (this.Card != null)
			{
				throw new InvalidOperationException("Can't apply an enchantment to a card when it's already been applied to a different card.");
			}
			base.AssertMutable();
			card.AssertMutable();
			this.Amount = (int)amount;
			this.Card = card;
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x00200A6B File Offset: 0x001FEC6B
		public void ClearInternal()
		{
			base.AssertMutable();
			this._card = null;
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x00200A7A File Offset: 0x001FEC7A
		public void ModifyCard()
		{
			if (this.Card == null)
			{
				throw new InvalidOperationException("Card must be set at this point.");
			}
			this.OnEnchant();
			this.RecalculateValues();
			this.Card.DynamicVars.RecalculateForUpgradeOrEnchant();
		}

		// Token: 0x06004730 RID: 18224 RVA: 0x00200AAB File Offset: 0x001FECAB
		public virtual void RecalculateValues()
		{
		}

		// Token: 0x06004731 RID: 18225 RVA: 0x00200AAD File Offset: 0x001FECAD
		public SerializableEnchantment ToSerializable()
		{
			base.AssertMutable();
			return new SerializableEnchantment
			{
				Id = base.Id,
				Props = SavedProperties.From(this),
				Amount = this.Amount
			};
		}

		// Token: 0x06004732 RID: 18226 RVA: 0x00200AE0 File Offset: 0x001FECE0
		public static EnchantmentModel FromSerializable(SerializableEnchantment save)
		{
			EnchantmentModel enchantmentModel = SaveUtil.EnchantmentOrDeprecated(save.Id).ToMutable();
			SavedProperties props = save.Props;
			if (props != null)
			{
				props.Fill(enchantmentModel);
			}
			enchantmentModel.Amount = save.Amount;
			return enchantmentModel;
		}

		// Token: 0x06004733 RID: 18227 RVA: 0x00200B1D File Offset: 0x001FED1D
		protected virtual void OnEnchant()
		{
		}

		// Token: 0x06004734 RID: 18228 RVA: 0x00200B1F File Offset: 0x001FED1F
		public virtual decimal EnchantBlockAdditive(decimal originalBlock, ValueProp props)
		{
			return 0m;
		}

		// Token: 0x06004735 RID: 18229 RVA: 0x00200B26 File Offset: 0x001FED26
		public virtual decimal EnchantBlockMultiplicative(decimal originalBlock, ValueProp props)
		{
			return 1m;
		}

		// Token: 0x06004736 RID: 18230 RVA: 0x00200B2D File Offset: 0x001FED2D
		public virtual decimal EnchantDamageAdditive(decimal originalDamage, ValueProp props)
		{
			return 0m;
		}

		// Token: 0x06004737 RID: 18231 RVA: 0x00200B34 File Offset: 0x001FED34
		public virtual decimal EnchantDamageMultiplicative(decimal originalDamage, ValueProp props)
		{
			return 1m;
		}

		// Token: 0x06004738 RID: 18232 RVA: 0x00200B3B File Offset: 0x001FED3B
		public virtual int EnchantPlayCount(int originalPlayCount)
		{
			return originalPlayCount;
		}

		// Token: 0x04001ACB RID: 6859
		public const string locTable = "enchantments";

		// Token: 0x04001ACC RID: 6860
		[Nullable(2)]
		private string _iconPath;

		// Token: 0x04001ACD RID: 6861
		[Nullable(2)]
		private CardModel _card;

		// Token: 0x04001ACE RID: 6862
		private int _amount;

		// Token: 0x04001AD0 RID: 6864
		[Nullable(2)]
		private DynamicVarSet _dynamicVars;

		// Token: 0x04001AD1 RID: 6865
		private EnchantmentStatus _status;

		// Token: 0x04001AD3 RID: 6867
		private EnchantmentModel _canonicalInstance;
	}
}
