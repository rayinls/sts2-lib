using System;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.CardPools
{
	// Token: 0x02000AD8 RID: 2776
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TokenCardPool : CardPoolModel
	{
		// Token: 0x17001FEF RID: 8175
		// (get) Token: 0x06007357 RID: 29527 RVA: 0x0026D84E File Offset: 0x0026BA4E
		public override string Title
		{
			get
			{
				return "token";
			}
		}

		// Token: 0x17001FF0 RID: 8176
		// (get) Token: 0x06007358 RID: 29528 RVA: 0x0026D855 File Offset: 0x0026BA55
		public override string EnergyColorName
		{
			get
			{
				return "colorless";
			}
		}

		// Token: 0x17001FF1 RID: 8177
		// (get) Token: 0x06007359 RID: 29529 RVA: 0x0026D85C File Offset: 0x0026BA5C
		public override string CardFrameMaterialPath
		{
			get
			{
				return "card_frame_colorless";
			}
		}

		// Token: 0x17001FF2 RID: 8178
		// (get) Token: 0x0600735A RID: 29530 RVA: 0x0026D863 File Offset: 0x0026BA63
		public override Color DeckEntryCardColor
		{
			get
			{
				return Colors.White;
			}
		}

		// Token: 0x17001FF3 RID: 8179
		// (get) Token: 0x0600735B RID: 29531 RVA: 0x0026D86A File Offset: 0x0026BA6A
		public override bool IsColorless
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600735C RID: 29532 RVA: 0x0026D870 File Offset: 0x0026BA70
		protected override CardModel[] GenerateAllCards()
		{
			return new CardModel[]
			{
				ModelDb.Card<Disintegration>(),
				ModelDb.Card<Fuel>(),
				ModelDb.Card<GiantRock>(),
				ModelDb.Card<Luminesce>(),
				ModelDb.Card<MindRot>(),
				ModelDb.Card<MinionDiveBomb>(),
				ModelDb.Card<MinionSacrifice>(),
				ModelDb.Card<MinionStrike>(),
				ModelDb.Card<Shiv>(),
				ModelDb.Card<Sloth>(),
				ModelDb.Card<Soul>(),
				ModelDb.Card<SovereignBlade>(),
				ModelDb.Card<SweepingGaze>(),
				ModelDb.Card<WasteAway>()
			};
		}
	}
}
