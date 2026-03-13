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
	// Token: 0x020008EB RID: 2283
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CompileDriver : CardModel
	{
		// Token: 0x060068B0 RID: 26800 RVA: 0x00257D8A File Offset: 0x00255F8A
		public CompileDriver()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001B63 RID: 7011
		// (get) Token: 0x060068B1 RID: 26801 RVA: 0x00257D98 File Offset: 0x00255F98
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[4];
				array[0] = new DamageVar(7m, ValueProp.Move);
				array[1] = new CalculationBaseVar(0m);
				array[2] = new CalculationExtraVar(1m);
				array[3] = new CalculatedVar("CalculatedCards").WithMultiplier((CardModel card, [Nullable(2)] Creature _) => (from orb in card.Owner.PlayerCombatState.OrbQueue.Orbs
					group orb by orb.Id).Count<IGrouping<ModelId, OrbModel>>());
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x060068B2 RID: 26802 RVA: 0x00257E0C File Offset: 0x0025600C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
			await CardPileCmd.Draw(choiceContext, ((CalculatedVar)base.DynamicVars["CalculatedCards"]).Calculate(cardPlay.Target), base.Owner, false);
		}

		// Token: 0x060068B3 RID: 26803 RVA: 0x00257E5F File Offset: 0x0025605F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}

		// Token: 0x04002567 RID: 9575
		private const string _calculatedCardsKey = "CalculatedCards";
	}
}
