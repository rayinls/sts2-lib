using System;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.CardPools
{
	// Token: 0x02000AD4 RID: 2772
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class QuestCardPool : CardPoolModel
	{
		// Token: 0x17001FD8 RID: 8152
		// (get) Token: 0x06007336 RID: 29494 RVA: 0x0026CF67 File Offset: 0x0026B167
		public override string Title
		{
			get
			{
				return "quest";
			}
		}

		// Token: 0x17001FD9 RID: 8153
		// (get) Token: 0x06007337 RID: 29495 RVA: 0x0026CF6E File Offset: 0x0026B16E
		public override string EnergyColorName
		{
			get
			{
				return "colorless";
			}
		}

		// Token: 0x17001FDA RID: 8154
		// (get) Token: 0x06007338 RID: 29496 RVA: 0x0026CF75 File Offset: 0x0026B175
		public override string CardFrameMaterialPath
		{
			get
			{
				return "card_frame_quest";
			}
		}

		// Token: 0x17001FDB RID: 8155
		// (get) Token: 0x06007339 RID: 29497 RVA: 0x0026CF7C File Offset: 0x0026B17C
		public override Color DeckEntryCardColor
		{
			get
			{
				return new Color("24476A");
			}
		}

		// Token: 0x17001FDC RID: 8156
		// (get) Token: 0x0600733A RID: 29498 RVA: 0x0026CF88 File Offset: 0x0026B188
		public override Color EnergyOutlineColor
		{
			get
			{
				return new Color("431E14");
			}
		}

		// Token: 0x17001FDD RID: 8157
		// (get) Token: 0x0600733B RID: 29499 RVA: 0x0026CF94 File Offset: 0x0026B194
		public override bool IsColorless
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600733C RID: 29500 RVA: 0x0026CF97 File Offset: 0x0026B197
		protected override CardModel[] GenerateAllCards()
		{
			return new CardModel[]
			{
				ModelDb.Card<ByrdonisEgg>(),
				ModelDb.Card<LanternKey>(),
				ModelDb.Card<SpoilsMap>()
			};
		}
	}
}
