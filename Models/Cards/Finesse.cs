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
	// Token: 0x0200094F RID: 2383
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Finesse : CardModel
	{
		// Token: 0x06006AC7 RID: 27335 RVA: 0x0025BBD3 File Offset: 0x00259DD3
		public Finesse()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001C54 RID: 7252
		// (get) Token: 0x06006AC8 RID: 27336 RVA: 0x0025BBE0 File Offset: 0x00259DE0
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C55 RID: 7253
		// (get) Token: 0x06006AC9 RID: 27337 RVA: 0x0025BBE3 File Offset: 0x00259DE3
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(4m, ValueProp.Move),
					new CardsVar(1)
				});
			}
		}

		// Token: 0x06006ACA RID: 27338 RVA: 0x0025BC08 File Offset: 0x00259E08
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}

		// Token: 0x06006ACB RID: 27339 RVA: 0x0025BC20 File Offset: 0x00259E20
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}
	}
}
