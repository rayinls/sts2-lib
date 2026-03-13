using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000552 RID: 1362
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PaelsClaw : RelicModel
	{
		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06004DB0 RID: 19888 RVA: 0x00217D27 File Offset: 0x00215F27
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06004DB1 RID: 19889 RVA: 0x00217D2A File Offset: 0x00215F2A
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(3),
					new StringVar("EnchantmentName", ModelDb.Enchantment<Goopy>().Title.GetFormattedText())
				});
			}
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06004DB2 RID: 19890 RVA: 0x00217D5C File Offset: 0x00215F5C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromEnchantment<Goopy>(2);
			}
		}

		// Token: 0x06004DB3 RID: 19891 RVA: 0x00217D64 File Offset: 0x00215F64
		public override Task AfterObtained()
		{
			IEnumerable<CardModel> enumerable = PileType.Deck.GetPile(base.Owner).Cards.ToList<CardModel>();
			foreach (CardModel cardModel in enumerable)
			{
				if (ModelDb.Enchantment<Goopy>().CanEnchant(cardModel))
				{
					CardCmd.Enchant<Goopy>(cardModel, 1m);
					NRun instance = NRun.Instance;
					if (instance != null)
					{
						instance.GlobalUi.CardPreviewContainer.AddChildSafely(NCardEnchantVfx.Create(cardModel));
					}
				}
			}
			return Task.CompletedTask;
		}

		// Token: 0x040021E9 RID: 8681
		public const int cardsCount = 3;

		// Token: 0x040021EA RID: 8682
		private const int _enchantmentAmount = 2;
	}
}
