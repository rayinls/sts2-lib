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
	// Token: 0x02000A0E RID: 2574
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PullFromBelow : CardModel
	{
		// Token: 0x06006EC5 RID: 28357 RVA: 0x00263E9C File Offset: 0x0026209C
		public PullFromBelow()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001DF7 RID: 7671
		// (get) Token: 0x06006EC6 RID: 28358 RVA: 0x00263EA9 File Offset: 0x002620A9
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Ethereal));
			}
		}

		// Token: 0x17001DF8 RID: 7672
		// (get) Token: 0x06006EC7 RID: 28359 RVA: 0x00263EB8 File Offset: 0x002620B8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[4];
				array[0] = new DamageVar(5m, ValueProp.Move);
				array[1] = new CalculationBaseVar(0m);
				array[2] = new CalculationExtraVar(1m);
				array[3] = new CalculatedVar("CalculatedHits").WithMultiplier((CardModel card, [Nullable(2)] Creature _) => CombatManager.Instance.History.Entries.OfType<CardPlayFinishedEntry>().Count((CardPlayFinishedEntry e) => e.CardPlay.Card.Owner == card.Owner && e.WasEthereal));
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x06006EC8 RID: 28360 RVA: 0x00263F2C File Offset: 0x0026212C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount((int)((CalculatedVar)base.DynamicVars["CalculatedHits"]).Calculate(cardPlay.Target)).FromCard(this)
				.Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06006EC9 RID: 28361 RVA: 0x00263F7F File Offset: 0x0026217F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
		}

		// Token: 0x040025BF RID: 9663
		private const string _calculatedHitsKey = "CalculatedHits";
	}
}
