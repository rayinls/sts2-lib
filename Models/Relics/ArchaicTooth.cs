using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004AC RID: 1196
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ArchaicTooth : RelicModel
	{
		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06004998 RID: 18840 RVA: 0x00210490 File Offset: 0x0020E690
		private static Dictionary<ModelId, CardModel> TranscendenceUpgrades
		{
			get
			{
				return new Dictionary<ModelId, CardModel>
				{
					{
						ModelDb.Card<Bash>().Id,
						ModelDb.Card<Break>()
					},
					{
						ModelDb.Card<Neutralize>().Id,
						ModelDb.Card<Suppress>()
					},
					{
						ModelDb.Card<Unleash>().Id,
						ModelDb.Card<Protector>()
					},
					{
						ModelDb.Card<FallingStar>().Id,
						ModelDb.Card<MeteorShower>()
					},
					{
						ModelDb.Card<Dualcast>().Id,
						ModelDb.Card<Quadcast>()
					}
				};
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06004999 RID: 18841 RVA: 0x0021050B File Offset: 0x0020E70B
		public static List<CardModel> TranscendenceCards
		{
			get
			{
				return ArchaicTooth.TranscendenceUpgrades.Values.ToList<CardModel>();
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x0600499A RID: 18842 RVA: 0x0021051C File Offset: 0x0020E71C
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x0600499B RID: 18843 RVA: 0x0021051F File Offset: 0x0020E71F
		// (set) Token: 0x0600499C RID: 18844 RVA: 0x00210527 File Offset: 0x0020E727
		[Nullable(2)]
		[SavedProperty]
		public SerializableCard StarterCard
		{
			[NullableContext(2)]
			get
			{
				return this._serializableStarterCard;
			}
			[NullableContext(2)]
			private set
			{
				base.AssertMutable();
				this._serializableStarterCard = value;
				this.UpdateHoverTips();
			}
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x0600499D RID: 18845 RVA: 0x0021053C File Offset: 0x0020E73C
		// (set) Token: 0x0600499E RID: 18846 RVA: 0x00210544 File Offset: 0x0020E744
		[Nullable(2)]
		[SavedProperty]
		public SerializableCard AncientCard
		{
			[NullableContext(2)]
			get
			{
				return this._serializableAncientCard;
			}
			[NullableContext(2)]
			private set
			{
				base.AssertMutable();
				this._serializableAncientCard = value;
				this.UpdateHoverTips();
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x0600499F RID: 18847 RVA: 0x00210559 File Offset: 0x0020E759
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new StringVar("StarterCard", ""),
					new StringVar("AncientCard", "")
				});
			}
		}

		// Token: 0x060049A0 RID: 18848 RVA: 0x0021058A File Offset: 0x0020E78A
		protected override void AfterCloned()
		{
			base.AfterCloned();
			this._extraHoverTips = new List<IHoverTip>();
		}

		// Token: 0x060049A1 RID: 18849 RVA: 0x002105A0 File Offset: 0x0020E7A0
		public bool SetupForPlayer(Player player)
		{
			base.AssertMutable();
			CardModel transcendenceStarterCard = this.GetTranscendenceStarterCard(player);
			if (transcendenceStarterCard != null)
			{
				this.StarterCard = transcendenceStarterCard.ToSerializable();
				this.AncientCard = this.GetTranscendenceTransformedCard(transcendenceStarterCard).ToSerializable();
				this.UpdateHoverTips();
				return true;
			}
			return false;
		}

		// Token: 0x060049A2 RID: 18850 RVA: 0x002105E5 File Offset: 0x0020E7E5
		public void SetupForTests(SerializableCard starterCard, SerializableCard ancientCard)
		{
			base.AssertMutable();
			this.StarterCard = starterCard;
			this.AncientCard = ancientCard;
			this.UpdateHoverTips();
		}

		// Token: 0x060049A3 RID: 18851 RVA: 0x00210604 File Offset: 0x0020E804
		private void UpdateHoverTips()
		{
			this._extraHoverTips.Clear();
			if (this.StarterCard != null)
			{
				CardModel cardModel = CardModel.FromSerializable(this.StarterCard);
				this._extraHoverTips.AddRange(cardModel.HoverTips);
				this._extraHoverTips.Add(HoverTipFactory.FromCard(cardModel, false));
				((StringVar)base.DynamicVars["StarterCard"]).StringValue = cardModel.Title;
			}
			if (this.AncientCard != null)
			{
				CardModel cardModel2 = CardModel.FromSerializable(this.AncientCard);
				this._extraHoverTips.AddRange(cardModel2.HoverTips);
				this._extraHoverTips.Add(HoverTipFactory.FromCard(cardModel2, false));
				((StringVar)base.DynamicVars["AncientCard"]).StringValue = cardModel2.Title;
			}
		}

		// Token: 0x060049A4 RID: 18852 RVA: 0x002106CA File Offset: 0x0020E8CA
		[return: Nullable(2)]
		private CardModel GetTranscendenceStarterCard(Player player)
		{
			return player.Deck.Cards.FirstOrDefault((CardModel c) => ArchaicTooth.TranscendenceUpgrades.ContainsKey(c.Id));
		}

		// Token: 0x060049A5 RID: 18853 RVA: 0x002106FC File Offset: 0x0020E8FC
		private CardModel GetTranscendenceTransformedCard(CardModel starterCard)
		{
			CardModel cardModel;
			if (ArchaicTooth.TranscendenceUpgrades.TryGetValue(starterCard.Id, out cardModel))
			{
				CardModel cardModel2 = starterCard.Owner.RunState.CreateCard(cardModel, starterCard.Owner);
				if (starterCard.IsUpgraded)
				{
					CardCmd.Upgrade(cardModel2, CardPreviewStyle.HorizontalLayout);
				}
				if (starterCard.Enchantment != null)
				{
					EnchantmentModel enchantmentModel = (EnchantmentModel)starterCard.Enchantment.MutableClone();
					CardCmd.Enchant(enchantmentModel, cardModel2, enchantmentModel.Amount);
				}
				return cardModel2;
			}
			return base.Owner.RunState.CreateCard<Doubt>(starterCard.Owner);
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x060049A6 RID: 18854 RVA: 0x00210788 File Offset: 0x0020E988
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return this._extraHoverTips;
			}
		}

		// Token: 0x060049A7 RID: 18855 RVA: 0x00210790 File Offset: 0x0020E990
		public override async Task AfterObtained()
		{
			CardModel transcendenceStarterCard = this.GetTranscendenceStarterCard(base.Owner);
			await CardCmd.Transform(transcendenceStarterCard, this.GetTranscendenceTransformedCard(transcendenceStarterCard), CardPreviewStyle.HorizontalLayout);
		}

		// Token: 0x04002180 RID: 8576
		private const string _starterCardKey = "StarterCard";

		// Token: 0x04002181 RID: 8577
		private const string _ancientCardKey = "AncientCard";

		// Token: 0x04002182 RID: 8578
		[Nullable(2)]
		private SerializableCard _serializableStarterCard;

		// Token: 0x04002183 RID: 8579
		[Nullable(2)]
		private SerializableCard _serializableAncientCard;

		// Token: 0x04002184 RID: 8580
		private List<IHoverTip> _extraHoverTips = new List<IHoverTip>();
	}
}
