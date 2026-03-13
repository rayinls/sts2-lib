using System;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.CardPools
{
	// Token: 0x02000ACD RID: 2765
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CurseCardPool : CardPoolModel
	{
		// Token: 0x17001FB1 RID: 8113
		// (get) Token: 0x060072FB RID: 29435 RVA: 0x0026C027 File Offset: 0x0026A227
		public override string Title
		{
			get
			{
				return "curse";
			}
		}

		// Token: 0x17001FB2 RID: 8114
		// (get) Token: 0x060072FC RID: 29436 RVA: 0x0026C02E File Offset: 0x0026A22E
		public override string EnergyColorName
		{
			get
			{
				return "colorless";
			}
		}

		// Token: 0x17001FB3 RID: 8115
		// (get) Token: 0x060072FD RID: 29437 RVA: 0x0026C035 File Offset: 0x0026A235
		public override string CardFrameMaterialPath
		{
			get
			{
				return "card_frame_curse";
			}
		}

		// Token: 0x17001FB4 RID: 8116
		// (get) Token: 0x060072FE RID: 29438 RVA: 0x0026C03C File Offset: 0x0026A23C
		public override Color DeckEntryCardColor
		{
			get
			{
				return new Color("585B61FF");
			}
		}

		// Token: 0x17001FB5 RID: 8117
		// (get) Token: 0x060072FF RID: 29439 RVA: 0x0026C048 File Offset: 0x0026A248
		public override bool IsColorless
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007300 RID: 29440 RVA: 0x0026C04C File Offset: 0x0026A24C
		protected override CardModel[] GenerateAllCards()
		{
			return new CardModel[]
			{
				ModelDb.Card<AscendersBane>(),
				ModelDb.Card<BadLuck>(),
				ModelDb.Card<Clumsy>(),
				ModelDb.Card<CurseOfTheBell>(),
				ModelDb.Card<Debt>(),
				ModelDb.Card<Decay>(),
				ModelDb.Card<Doubt>(),
				ModelDb.Card<Enthralled>(),
				ModelDb.Card<Folly>(),
				ModelDb.Card<Greed>(),
				ModelDb.Card<Guilty>(),
				ModelDb.Card<Injury>(),
				ModelDb.Card<Normality>(),
				ModelDb.Card<PoorSleep>(),
				ModelDb.Card<Regret>(),
				ModelDb.Card<Shame>(),
				ModelDb.Card<SporeMind>(),
				ModelDb.Card<Writhe>()
			};
		}
	}
}
