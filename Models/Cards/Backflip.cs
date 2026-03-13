using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200089C RID: 2204
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Backflip : CardModel
	{
		// Token: 0x0600671C RID: 26396 RVA: 0x00254AD5 File Offset: 0x00252CD5
		public Backflip()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001ABD RID: 6845
		// (get) Token: 0x0600671D RID: 26397 RVA: 0x00254AE2 File Offset: 0x00252CE2
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001ABE RID: 6846
		// (get) Token: 0x0600671E RID: 26398 RVA: 0x00254AE5 File Offset: 0x00252CE5
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(5m, ValueProp.Move),
					new CardsVar(2)
				});
			}
		}

		// Token: 0x0600671F RID: 26399 RVA: 0x00254B0C File Offset: 0x00252D0C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}

		// Token: 0x06006720 RID: 26400 RVA: 0x00254B5F File Offset: 0x00252D5F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}
