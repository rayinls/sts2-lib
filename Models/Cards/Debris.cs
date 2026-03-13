using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200090B RID: 2315
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Debris : CardModel
	{
		// Token: 0x0600694F RID: 26959 RVA: 0x00259229 File Offset: 0x00257429
		public Debris()
			: base(1, CardType.Status, CardRarity.Status, TargetType.None, true)
		{
		}

		// Token: 0x17001BA3 RID: 7075
		// (get) Token: 0x06006950 RID: 26960 RVA: 0x00259236 File Offset: 0x00257436
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001BA4 RID: 7076
		// (get) Token: 0x06006951 RID: 26961 RVA: 0x00259239 File Offset: 0x00257439
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x06006952 RID: 26962 RVA: 0x00259241 File Offset: 0x00257441
		protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			return Task.CompletedTask;
		}
	}
}
