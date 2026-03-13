using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008A1 RID: 2209
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Barrage : CardModel
	{
		// Token: 0x0600673A RID: 26426 RVA: 0x00254EED File Offset: 0x002530ED
		public Barrage()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001ACA RID: 6858
		// (get) Token: 0x0600673B RID: 26427 RVA: 0x00254EFA File Offset: 0x002530FA
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x17001ACB RID: 6859
		// (get) Token: 0x0600673C RID: 26428 RVA: 0x00254F0C File Offset: 0x0025310C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[4];
				array[0] = new DamageVar(5m, ValueProp.Move);
				array[1] = new CalculationBaseVar(0m);
				array[2] = new CalculationExtraVar(1m);
				array[3] = new CalculatedVar("CalculatedHits").WithMultiplier((CardModel card, [Nullable(2)] Creature _) => card.Owner.PlayerCombatState.OrbQueue.Orbs.Count);
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x0600673D RID: 26429 RVA: 0x00254F80 File Offset: 0x00253180
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount((int)((CalculatedVar)base.DynamicVars["CalculatedHits"]).Calculate(cardPlay.Target)).FromCard(this)
				.Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x0600673E RID: 26430 RVA: 0x00254FD3 File Offset: 0x002531D3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
		}

		// Token: 0x04002558 RID: 9560
		private const string _calculatedHitsKey = "CalculatedHits";
	}
}
