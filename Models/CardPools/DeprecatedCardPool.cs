using System;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.CardPools
{
	// Token: 0x02000ACF RID: 2767
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DeprecatedCardPool : CardPoolModel
	{
		// Token: 0x17001FBC RID: 8124
		// (get) Token: 0x0600730B RID: 29451 RVA: 0x0026C4FF File Offset: 0x0026A6FF
		public override string Title
		{
			get
			{
				return "token";
			}
		}

		// Token: 0x17001FBD RID: 8125
		// (get) Token: 0x0600730C RID: 29452 RVA: 0x0026C506 File Offset: 0x0026A706
		public override string EnergyColorName
		{
			get
			{
				return "colorless";
			}
		}

		// Token: 0x17001FBE RID: 8126
		// (get) Token: 0x0600730D RID: 29453 RVA: 0x0026C50D File Offset: 0x0026A70D
		public override string CardFrameMaterialPath
		{
			get
			{
				return "card_frame_colorless";
			}
		}

		// Token: 0x17001FBF RID: 8127
		// (get) Token: 0x0600730E RID: 29454 RVA: 0x0026C514 File Offset: 0x0026A714
		public override Color DeckEntryCardColor
		{
			get
			{
				return Colors.White;
			}
		}

		// Token: 0x17001FC0 RID: 8128
		// (get) Token: 0x0600730F RID: 29455 RVA: 0x0026C51B File Offset: 0x0026A71B
		public override bool IsColorless
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007310 RID: 29456 RVA: 0x0026C51E File Offset: 0x0026A71E
		protected override CardModel[] GenerateAllCards()
		{
			return new CardModel[] { ModelDb.Card<DeprecatedCard>() };
		}
	}
}
