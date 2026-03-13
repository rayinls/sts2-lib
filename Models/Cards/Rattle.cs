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
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A19 RID: 2585
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Rattle : CardModel
	{
		// Token: 0x06006EFE RID: 28414 RVA: 0x00264615 File Offset: 0x00262815
		public Rattle()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001E0D RID: 7693
		// (get) Token: 0x06006EFF RID: 28415 RVA: 0x00264622 File Offset: 0x00262822
		protected override bool ShouldGlowRedInternal
		{
			get
			{
				return base.Owner.IsOstyMissing;
			}
		}

		// Token: 0x17001E0E RID: 7694
		// (get) Token: 0x06006F00 RID: 28416 RVA: 0x0026462F File Offset: 0x0026282F
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.OstyAttack };
			}
		}

		// Token: 0x17001E0F RID: 7695
		// (get) Token: 0x06006F01 RID: 28417 RVA: 0x00264640 File Offset: 0x00262840
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[4];
				array[0] = new OstyDamageVar(7m, ValueProp.Move);
				array[1] = new CalculationBaseVar(0m);
				array[2] = new CalculationExtraVar(1m);
				array[3] = new CalculatedVar("CalculatedHits").WithMultiplier((CardModel card, [Nullable(2)] Creature _) => 1 + CombatManager.Instance.History.Entries.OfType<CreatureAttackedEntry>().Count((CreatureAttackedEntry e) => e.Actor == card.Owner.Osty && e.HappenedThisTurn(card.CombatState)));
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x06006F02 RID: 28418 RVA: 0x002646B4 File Offset: 0x002628B4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			if (!Osty.CheckMissingWithAnim(base.Owner))
			{
				await DamageCmd.Attack(base.DynamicVars.OstyDamage.BaseValue).FromOsty(base.Owner.Osty, this).Targeting(cardPlay.Target)
					.WithHitCount((int)((CalculatedVar)base.DynamicVars["CalculatedHits"]).Calculate(cardPlay.Target))
					.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
					.Execute(choiceContext);
			}
		}

		// Token: 0x06006F03 RID: 28419 RVA: 0x00264707 File Offset: 0x00262907
		protected override void OnUpgrade()
		{
			base.DynamicVars.OstyDamage.UpgradeValueBy(2m);
		}

		// Token: 0x040025C5 RID: 9669
		private const string _calculatedHitsKey = "CalculatedHits";
	}
}
