using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000957 RID: 2391
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Flechettes : CardModel
	{
		// Token: 0x06006AF4 RID: 27380 RVA: 0x0025C241 File Offset: 0x0025A441
		public Flechettes()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001C64 RID: 7268
		// (get) Token: 0x06006AF5 RID: 27381 RVA: 0x0025C250 File Offset: 0x0025A450
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[4];
				array[0] = new DamageVar(5m, ValueProp.Move);
				array[1] = new CalculationBaseVar(0m);
				array[2] = new CalculationExtraVar(1m);
				array[3] = new CalculatedVar("CalculatedHits").WithMultiplier((CardModel card, [Nullable(2)] Creature _) => PileType.Hand.GetPile(card.Owner).Cards.Count((CardModel c) => c.Type == CardType.Skill));
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x06006AF6 RID: 27382 RVA: 0x0025C2C4 File Offset: 0x0025A4C4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount((int)((CalculatedVar)base.DynamicVars["CalculatedHits"]).Calculate(cardPlay.Target)).FromCard(this)
				.Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, "dagger_throw.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x06006AF7 RID: 27383 RVA: 0x0025C317 File Offset: 0x0025A517
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
		}

		// Token: 0x04002580 RID: 9600
		private const string _calculatedHitsKey = "CalculatedHits";
	}
}
