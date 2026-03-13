using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A09 RID: 2569
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Prolong : CardModel
	{
		// Token: 0x06006EAB RID: 28331 RVA: 0x00263B00 File Offset: 0x00261D00
		public Prolong()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001DEC RID: 7660
		// (get) Token: 0x06006EAC RID: 28332 RVA: 0x00263B0D File Offset: 0x00261D0D
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x17001DED RID: 7661
		// (get) Token: 0x06006EAD RID: 28333 RVA: 0x00263B1F File Offset: 0x00261D1F
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x06006EAE RID: 28334 RVA: 0x00263B28 File Offset: 0x00261D28
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			Creature creature = base.Owner.Creature;
			await PowerCmd.Apply<BlockNextTurnPower>(creature, creature.Block, creature, this, false);
		}

		// Token: 0x06006EAF RID: 28335 RVA: 0x00263B6B File Offset: 0x00261D6B
		protected override void OnUpgrade()
		{
			base.RemoveKeyword(CardKeyword.Exhaust);
		}
	}
}
