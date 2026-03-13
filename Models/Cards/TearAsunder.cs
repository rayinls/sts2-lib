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
	// Token: 0x02000A8D RID: 2701
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TearAsunder : CardModel
	{
		// Token: 0x06007177 RID: 29047 RVA: 0x00269437 File Offset: 0x00267637
		public TearAsunder()
			: base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001F12 RID: 7954
		// (get) Token: 0x06007178 RID: 29048 RVA: 0x00269444 File Offset: 0x00267644
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[5];
				array[0] = new DamageVar(5m, ValueProp.Move);
				array[1] = new RepeatVar(1);
				array[2] = new CalculationBaseVar(0m);
				array[3] = new CalculationExtraVar(1m);
				array[4] = new CalculatedVar("CalculatedHits").WithMultiplier((CardModel card, [Nullable(2)] Creature _) => 1 + CombatManager.Instance.History.Entries.OfType<DamageReceivedEntry>().Count((DamageReceivedEntry e) => e.Receiver == card.Owner.Creature && e.Result.UnblockedDamage > 0));
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x06007179 RID: 29049 RVA: 0x002694C0 File Offset: 0x002676C0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount((int)((CalculatedVar)base.DynamicVars["CalculatedHits"]).Calculate(cardPlay.Target)).FromCard(this)
				.Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x0600717A RID: 29050 RVA: 0x00269513 File Offset: 0x00267713
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
		}

		// Token: 0x040025D9 RID: 9689
		private const string _calculatedHitsKey = "CalculatedHits";
	}
}
