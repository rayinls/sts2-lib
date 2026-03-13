using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008A4 RID: 2212
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BattleTrance : CardModel
	{
		// Token: 0x06006748 RID: 26440 RVA: 0x00255120 File Offset: 0x00253320
		public BattleTrance()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001ACF RID: 6863
		// (get) Token: 0x06006749 RID: 26441 RVA: 0x0025512D File Offset: 0x0025332D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x0600674A RID: 26442 RVA: 0x0025513C File Offset: 0x0025333C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
			await PowerCmd.Apply<NoDrawPower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x0600674B RID: 26443 RVA: 0x00255187 File Offset: 0x00253387
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
