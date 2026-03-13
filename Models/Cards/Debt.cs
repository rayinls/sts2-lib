using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200090C RID: 2316
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Debt : CardModel
	{
		// Token: 0x06006953 RID: 26963 RVA: 0x00259248 File Offset: 0x00257448
		public Debt()
			: base(-1, CardType.Curse, CardRarity.Curse, TargetType.None, true)
		{
		}

		// Token: 0x17001BA5 RID: 7077
		// (get) Token: 0x06006954 RID: 26964 RVA: 0x00259256 File Offset: 0x00257456
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001BA6 RID: 7078
		// (get) Token: 0x06006955 RID: 26965 RVA: 0x00259259 File Offset: 0x00257459
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Unplayable);
			}
		}

		// Token: 0x17001BA7 RID: 7079
		// (get) Token: 0x06006956 RID: 26966 RVA: 0x00259261 File Offset: 0x00257461
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new GoldVar(10));
			}
		}

		// Token: 0x17001BA8 RID: 7080
		// (get) Token: 0x06006957 RID: 26967 RVA: 0x0025926F File Offset: 0x0025746F
		public override bool HasTurnEndInHandEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006958 RID: 26968 RVA: 0x00259274 File Offset: 0x00257474
		public override async Task OnTurnEndInHand(PlayerChoiceContext choiceContext)
		{
			int num = Mathf.Min(base.DynamicVars.Gold.IntValue, base.Owner.Gold);
			await PlayerCmd.LoseGold(num, base.Owner, GoldLossType.Lost);
		}
	}
}
