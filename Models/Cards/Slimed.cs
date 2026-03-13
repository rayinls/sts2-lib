using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A54 RID: 2644
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Slimed : CardModel
	{
		// Token: 0x0600703D RID: 28733 RVA: 0x00266C7F File Offset: 0x00264E7F
		public Slimed()
			: base(1, CardType.Status, CardRarity.Status, TargetType.None, true)
		{
		}

		// Token: 0x17001E95 RID: 7829
		// (get) Token: 0x0600703E RID: 28734 RVA: 0x00266C8C File Offset: 0x00264E8C
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001E96 RID: 7830
		// (get) Token: 0x0600703F RID: 28735 RVA: 0x00266C8F File Offset: 0x00264E8F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(1));
			}
		}

		// Token: 0x17001E97 RID: 7831
		// (get) Token: 0x06007040 RID: 28736 RVA: 0x00266C9C File Offset: 0x00264E9C
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x06007041 RID: 28737 RVA: 0x00266CA4 File Offset: 0x00264EA4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			NGoopyImpactVfx ngoopyImpactVfx = NGoopyImpactVfx.Create(base.Owner.Creature);
			if (ngoopyImpactVfx != null)
			{
				NCombatRoom instance = NCombatRoom.Instance;
				if (instance != null)
				{
					instance.CombatVfxContainer.AddChildSafely(ngoopyImpactVfx);
				}
			}
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}
	}
}
