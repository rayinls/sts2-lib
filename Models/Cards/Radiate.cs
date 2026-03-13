using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A14 RID: 2580
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Radiate : CardModel
	{
		// Token: 0x06006EE3 RID: 28387 RVA: 0x00264277 File Offset: 0x00262477
		public Radiate()
			: base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001E03 RID: 7683
		// (get) Token: 0x06006EE4 RID: 28388 RVA: 0x00264284 File Offset: 0x00262484
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[5];
				array[0] = new DamageVar(3m, ValueProp.Move);
				array[1] = new StarsVar(1);
				array[2] = new CalculationBaseVar(0m);
				array[3] = new CalculationExtraVar(1m);
				array[4] = new CalculatedVar("CalculatedHits").WithMultiplier((CardModel card, [Nullable(2)] Creature _) => (from e in CombatManager.Instance.History.Entries.OfType<StarsModifiedEntry>()
					where e.HappenedThisTurn(card.CombatState) && e.Amount > 0
					select e).Sum((StarsModifiedEntry e) => e.Amount));
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x06006EE5 RID: 28389 RVA: 0x00264300 File Offset: 0x00262500
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount((int)((CalculatedVar)base.DynamicVars["CalculatedHits"]).Calculate(cardPlay.Target)).FromCard(this)
				.TargetingAllOpponents(base.CombatState)
				.WithHitFx("vfx/vfx_starry_impact", null, "slash_attack.mp3")
				.SpawningHitVfxOnEachCreature()
				.Execute(choiceContext);
		}

		// Token: 0x06006EE6 RID: 28390 RVA: 0x00264353 File Offset: 0x00262553
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(1m);
		}

		// Token: 0x040025C1 RID: 9665
		private const string _calculatedHitsKey = "CalculatedHits";
	}
}
