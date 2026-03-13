using System;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.CardPools
{
	// Token: 0x02000AD0 RID: 2768
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EventCardPool : CardPoolModel
	{
		// Token: 0x17001FC1 RID: 8129
		// (get) Token: 0x06007312 RID: 29458 RVA: 0x0026C536 File Offset: 0x0026A736
		public override string Title
		{
			get
			{
				return "event";
			}
		}

		// Token: 0x17001FC2 RID: 8130
		// (get) Token: 0x06007313 RID: 29459 RVA: 0x0026C53D File Offset: 0x0026A73D
		public override string EnergyColorName
		{
			get
			{
				return "colorless";
			}
		}

		// Token: 0x17001FC3 RID: 8131
		// (get) Token: 0x06007314 RID: 29460 RVA: 0x0026C544 File Offset: 0x0026A744
		public override string CardFrameMaterialPath
		{
			get
			{
				return "card_frame_colorless";
			}
		}

		// Token: 0x17001FC4 RID: 8132
		// (get) Token: 0x06007315 RID: 29461 RVA: 0x0026C54B File Offset: 0x0026A74B
		public override Color DeckEntryCardColor
		{
			get
			{
				return new Color("A3A3A3FF");
			}
		}

		// Token: 0x17001FC5 RID: 8133
		// (get) Token: 0x06007316 RID: 29462 RVA: 0x0026C557 File Offset: 0x0026A757
		public override bool IsColorless
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007317 RID: 29463 RVA: 0x0026C55C File Offset: 0x0026A75C
		protected override CardModel[] GenerateAllCards()
		{
			return new CardModel[]
			{
				ModelDb.Card<Apotheosis>(),
				ModelDb.Card<Apparition>(),
				ModelDb.Card<BrightestFlame>(),
				ModelDb.Card<ByrdSwoop>(),
				ModelDb.Card<Caltrops>(),
				ModelDb.Card<Clash>(),
				ModelDb.Card<Distraction>(),
				ModelDb.Card<DualWield>(),
				ModelDb.Card<Enlightenment>(),
				ModelDb.Card<Entrench>(),
				ModelDb.Card<Exterminate>(),
				ModelDb.Card<FeedingFrenzy>(),
				ModelDb.Card<HelloWorld>(),
				ModelDb.Card<MadScience>(),
				ModelDb.Card<Maul>(),
				ModelDb.Card<Metamorphosis>(),
				ModelDb.Card<NeowsFury>(),
				ModelDb.Card<Outmaneuver>(),
				ModelDb.Card<Peck>(),
				ModelDb.Card<Rebound>(),
				ModelDb.Card<Relax>(),
				ModelDb.Card<RipAndTear>(),
				ModelDb.Card<Squash>(),
				ModelDb.Card<Stack>(),
				ModelDb.Card<ToricToughness>(),
				ModelDb.Card<Wish>(),
				ModelDb.Card<Whistle>()
			};
		}
	}
}
