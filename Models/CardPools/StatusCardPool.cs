using System;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.CardPools
{
	// Token: 0x02000AD7 RID: 2775
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class StatusCardPool : CardPoolModel
	{
		// Token: 0x17001FEA RID: 8170
		// (get) Token: 0x06007350 RID: 29520 RVA: 0x0026D7B7 File Offset: 0x0026B9B7
		public override string Title
		{
			get
			{
				return "status";
			}
		}

		// Token: 0x17001FEB RID: 8171
		// (get) Token: 0x06007351 RID: 29521 RVA: 0x0026D7BE File Offset: 0x0026B9BE
		public override string EnergyColorName
		{
			get
			{
				return "colorless";
			}
		}

		// Token: 0x17001FEC RID: 8172
		// (get) Token: 0x06007352 RID: 29522 RVA: 0x0026D7C5 File Offset: 0x0026B9C5
		public override string CardFrameMaterialPath
		{
			get
			{
				return "card_frame_colorless";
			}
		}

		// Token: 0x17001FED RID: 8173
		// (get) Token: 0x06007353 RID: 29523 RVA: 0x0026D7CC File Offset: 0x0026B9CC
		public override Color DeckEntryCardColor
		{
			get
			{
				return Colors.White;
			}
		}

		// Token: 0x17001FEE RID: 8174
		// (get) Token: 0x06007354 RID: 29524 RVA: 0x0026D7D3 File Offset: 0x0026B9D3
		public override bool IsColorless
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007355 RID: 29525 RVA: 0x0026D7D8 File Offset: 0x0026B9D8
		protected override CardModel[] GenerateAllCards()
		{
			return new CardModel[]
			{
				ModelDb.Card<Beckon>(),
				ModelDb.Card<Burn>(),
				ModelDb.Card<Dazed>(),
				ModelDb.Card<Debris>(),
				ModelDb.Card<FranticEscape>(),
				ModelDb.Card<Infection>(),
				ModelDb.Card<Slimed>(),
				ModelDb.Card<Soot>(),
				ModelDb.Card<Toxic>(),
				ModelDb.Card<Void>(),
				ModelDb.Card<Wound>()
			};
		}
	}
}
