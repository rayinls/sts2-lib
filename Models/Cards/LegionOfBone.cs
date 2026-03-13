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

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009B3 RID: 2483
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LegionOfBone : CardModel
	{
		// Token: 0x06006CD5 RID: 27861 RVA: 0x0025FECB File Offset: 0x0025E0CB
		public LegionOfBone()
			: base(2, CardType.Skill, CardRarity.Uncommon, TargetType.AllAllies, true)
		{
		}

		// Token: 0x17001D29 RID: 7465
		// (get) Token: 0x06006CD6 RID: 27862 RVA: 0x0025FED8 File Offset: 0x0025E0D8
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001D2A RID: 7466
		// (get) Token: 0x06006CD7 RID: 27863 RVA: 0x0025FEE0 File Offset: 0x0025E0E0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new SummonVar(6m));
			}
		}

		// Token: 0x17001D2B RID: 7467
		// (get) Token: 0x06006CD8 RID: 27864 RVA: 0x0025FEF2 File Offset: 0x0025E0F2
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.SummonDynamic, new DynamicVar[] { base.DynamicVars.Summon }));
			}
		}

		// Token: 0x17001D2C RID: 7468
		// (get) Token: 0x06006CD9 RID: 27865 RVA: 0x0025FF14 File Offset: 0x0025E114
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.MultiplayerOnly;
			}
		}

		// Token: 0x06006CDA RID: 27866 RVA: 0x0025FF18 File Offset: 0x0025E118
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			IEnumerable<Creature> enumerable = base.CombatState.PlayerCreatures.Where((Creature c) => c != null && c.IsAlive).ToList<Creature>();
			foreach (Creature creature in enumerable)
			{
				await OstyCmd.Summon(choiceContext, creature.Player, base.DynamicVars.Summon.BaseValue, this);
			}
			IEnumerator<Creature> enumerator = null;
		}

		// Token: 0x06006CDB RID: 27867 RVA: 0x0025FF63 File Offset: 0x0025E163
		protected override void OnUpgrade()
		{
			base.DynamicVars.Summon.UpgradeValueBy(2m);
		}
	}
}
