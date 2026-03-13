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
	// Token: 0x02000A4D RID: 2637
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ShrugItOff : CardModel
	{
		// Token: 0x0600701B RID: 28699 RVA: 0x0026685E File Offset: 0x00264A5E
		public ShrugItOff()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001E88 RID: 7816
		// (get) Token: 0x0600701C RID: 28700 RVA: 0x0026686B File Offset: 0x00264A6B
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001E89 RID: 7817
		// (get) Token: 0x0600701D RID: 28701 RVA: 0x0026686E File Offset: 0x00264A6E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(8m, ValueProp.Move),
					new CardsVar(1)
				});
			}
		}

		// Token: 0x0600701E RID: 28702 RVA: 0x00266894 File Offset: 0x00264A94
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}

		// Token: 0x0600701F RID: 28703 RVA: 0x002668E7 File Offset: 0x00264AE7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}
