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
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A87 RID: 2695
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Synchronize : CardModel
	{
		// Token: 0x06007158 RID: 29016 RVA: 0x00269091 File Offset: 0x00267291
		public Synchronize()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001F05 RID: 7941
		// (get) Token: 0x06007159 RID: 29017 RVA: 0x0026909E File Offset: 0x0026729E
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<FocusPower>());
			}
		}

		// Token: 0x17001F06 RID: 7942
		// (get) Token: 0x0600715A RID: 29018 RVA: 0x002690AC File Offset: 0x002672AC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(0m);
				array[1] = new CalculationExtraVar(2m);
				array[2] = new CalculatedVar("CalculatedFocus").WithMultiplier((CardModel card, [Nullable(2)] Creature _) => (from orb in card.Owner.PlayerCombatState.OrbQueue.Orbs
					group orb by orb.Id).Count<IGrouping<ModelId, OrbModel>>());
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x17001F07 RID: 7943
		// (get) Token: 0x0600715B RID: 29019 RVA: 0x00269110 File Offset: 0x00267310
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x0600715C RID: 29020 RVA: 0x00269118 File Offset: 0x00267318
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<SynchronizePower>(base.Owner.Creature, ((CalculatedVar)base.DynamicVars["CalculatedFocus"]).Calculate(cardPlay.Target), base.Owner.Creature, this, false);
		}

		// Token: 0x0600715D RID: 29021 RVA: 0x00269163 File Offset: 0x00267363
		protected override void OnUpgrade()
		{
			base.RemoveKeyword(CardKeyword.Exhaust);
		}

		// Token: 0x040025D8 RID: 9688
		private const string _calculatedFocusKey = "CalculatedFocus";
	}
}
