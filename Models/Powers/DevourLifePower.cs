using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000609 RID: 1545
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DevourLifePower : PowerModel
	{
		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x0600521E RID: 21022 RVA: 0x00220843 File Offset: 0x0021EA43
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x0600521F RID: 21023 RVA: 0x00220846 File Offset: 0x0021EA46
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x06005220 RID: 21024 RVA: 0x00220849 File Offset: 0x0021EA49
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromCard<Soul>(false),
					HoverTipFactory.Static(StaticHoverTip.SummonStatic, Array.Empty<DynamicVar>())
				});
			}
		}

		// Token: 0x06005221 RID: 21025 RVA: 0x00220870 File Offset: 0x0021EA70
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card is Soul)
			{
				if (cardPlay.Card.Owner.Creature == base.Owner)
				{
					await OstyCmd.Summon(context, cardPlay.Card.Owner, base.Amount, this);
				}
			}
		}
	}
}
