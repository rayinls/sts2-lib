using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000925 RID: 2341
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Dominate : CardModel
	{
		// Token: 0x060069DE RID: 27102 RVA: 0x0025A0A7 File Offset: 0x002582A7
		public Dominate()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001BE9 RID: 7145
		// (get) Token: 0x060069DF RID: 27103 RVA: 0x0025A0B4 File Offset: 0x002582B4
		protected override bool ShouldGlowGoldInternal
		{
			get
			{
				CombatState combatState = base.CombatState;
				if (combatState == null)
				{
					return false;
				}
				return combatState.HittableEnemies.Any((Creature e) => e.HasPower<VulnerablePower>());
			}
		}

		// Token: 0x17001BEA RID: 7146
		// (get) Token: 0x060069E0 RID: 27104 RVA: 0x0025A0EB File Offset: 0x002582EB
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001BEB RID: 7147
		// (get) Token: 0x060069E1 RID: 27105 RVA: 0x0025A0F3 File Offset: 0x002582F3
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("StrengthPerVulnerable", 1m));
			}
		}

		// Token: 0x17001BEC RID: 7148
		// (get) Token: 0x060069E2 RID: 27106 RVA: 0x0025A109 File Offset: 0x00258309
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<StrengthPower>(),
					HoverTipFactory.FromPower<VulnerablePower>()
				});
			}
		}

		// Token: 0x060069E3 RID: 27107 RVA: 0x0025A128 File Offset: 0x00258328
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			VulnerablePower power = cardPlay.Target.GetPower<VulnerablePower>();
			int strengthToApply = ((power != null) ? power.Amount : 0);
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, strengthToApply, base.Owner.Creature, this, false);
		}

		// Token: 0x060069E4 RID: 27108 RVA: 0x0025A173 File Offset: 0x00258373
		protected override void OnUpgrade()
		{
			base.RemoveKeyword(CardKeyword.Exhaust);
		}

		// Token: 0x0400256F RID: 9583
		private const string _strengthPerVulnerableKey = "StrengthPerVulnerable";
	}
}
