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
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000989 RID: 2441
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HelixDrill : CardModel
	{
		// Token: 0x06006C01 RID: 27649 RVA: 0x0025E433 File Offset: 0x0025C633
		public HelixDrill()
			: base(0, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001CD6 RID: 7382
		// (get) Token: 0x06006C02 RID: 27650 RVA: 0x0025E440 File Offset: 0x0025C640
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[4];
				array[0] = new DamageVar(3m, ValueProp.Move);
				array[1] = new CalculationBaseVar(0m);
				array[2] = new CalculationExtraVar(1m);
				array[3] = new CalculatedVar("CalculatedHits").WithMultiplier(delegate(CardModel card, [Nullable(2)] Creature _)
				{
					if (card.Pile == null)
					{
						return 0m;
					}
					int num = (from e in CombatManager.Instance.History.Entries.OfType<EnergySpentEntry>()
						where e.HappenedThisTurn(card.CombatState) && e.Actor.Player == card.Owner
						select e).Sum((EnergySpentEntry c) => c.Amount);
					if (card.Pile.Type == PileType.Play)
					{
						num -= card.EnergyCost.GetWithModifiers(CostModifiers.All);
					}
					return num;
				});
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x17001CD7 RID: 7383
		// (get) Token: 0x06006C03 RID: 27651 RVA: 0x0025E4B2 File Offset: 0x0025C6B2
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x06006C04 RID: 27652 RVA: 0x0025E4C0 File Offset: 0x0025C6C0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount((int)((CalculatedVar)base.DynamicVars["CalculatedHits"]).Calculate(cardPlay.Target)).FromCard(this)
				.Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06006C05 RID: 27653 RVA: 0x0025E513 File Offset: 0x0025C713
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
		}

		// Token: 0x0400258E RID: 9614
		private const string _calculatedHitsKey = "CalculatedHits";
	}
}
