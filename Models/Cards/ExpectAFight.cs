using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200093F RID: 2367
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ExpectAFight : CardModel
	{
		// Token: 0x06006A6D RID: 27245 RVA: 0x0025AFEF File Offset: 0x002591EF
		public ExpectAFight()
			: base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001C2C RID: 7212
		// (get) Token: 0x06006A6E RID: 27246 RVA: 0x0025AFFC File Offset: 0x002591FC
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x17001C2D RID: 7213
		// (get) Token: 0x06006A6F RID: 27247 RVA: 0x0025B00C File Offset: 0x0025920C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[4];
				array[0] = new EnergyVar(0);
				array[1] = new CalculationBaseVar(0m);
				array[2] = new CalculationExtraVar(1m);
				array[3] = new CalculatedVar("CalculatedEnergy").WithMultiplier((CardModel card, [Nullable(2)] Creature _) => PileType.Hand.GetPile(card.Owner).Cards.Count((CardModel c) => c.Type == CardType.Attack));
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x06006A70 RID: 27248 RVA: 0x0025B078 File Offset: 0x00259278
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PlayerCmd.GainEnergy(((CalculatedVar)base.DynamicVars["CalculatedEnergy"]).Calculate(cardPlay.Target), base.Owner);
		}

		// Token: 0x06006A71 RID: 27249 RVA: 0x0025B0C3 File Offset: 0x002592C3
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}

		// Token: 0x04002577 RID: 9591
		private const string _calculatedEnergyKey = "CalculatedEnergy";
	}
}
