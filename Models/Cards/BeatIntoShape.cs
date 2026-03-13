using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008A8 RID: 2216
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BeatIntoShape : CardModel
	{
		// Token: 0x0600675A RID: 26458 RVA: 0x0025534E File Offset: 0x0025354E
		public BeatIntoShape()
			: base(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001AD5 RID: 6869
		// (get) Token: 0x0600675B RID: 26459 RVA: 0x0025535C File Offset: 0x0025355C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[4];
				array[0] = new DamageVar(5m, ValueProp.Move);
				array[1] = new CalculationBaseVar(5m);
				array[2] = new CalculationExtraVar(5m);
				array[3] = new CalculatedVar("CalculatedForge").WithMultiplier((CardModel card, [Nullable(2)] Creature target) => CombatManager.Instance.History.Entries.OfType<DamageReceivedEntry>().Count((DamageReceivedEntry e) => e.Receiver == target && e.Dealer == card.Owner.Creature && e.Result.Props.IsPoweredAttack() && e.HappenedThisTurn(card.CombatState)));
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x17001AD6 RID: 6870
		// (get) Token: 0x0600675C RID: 26460 RVA: 0x002553D0 File Offset: 0x002535D0
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromForge();
			}
		}

		// Token: 0x0600675D RID: 26461 RVA: 0x002553D8 File Offset: 0x002535D8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			AttackCommand attackCommand = await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
			AttackCommand attackCommand2 = attackCommand;
			decimal num = ((CalculatedVar)base.DynamicVars["CalculatedForge"]).Calculate(cardPlay.Target);
			num -= attackCommand2.Results.Count<DamageResult>() * base.DynamicVars.CalculationExtra.BaseValue;
			await ForgeCmd.Forge(num, base.Owner, this);
		}

		// Token: 0x0600675E RID: 26462 RVA: 0x0025542C File Offset: 0x0025362C
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
			base.DynamicVars.CalculationBase.UpgradeValueBy(2m);
			base.DynamicVars.CalculationExtra.UpgradeValueBy(2m);
		}

		// Token: 0x04002559 RID: 9561
		private const string _calculatedForgeKey = "CalculatedForge";
	}
}
