using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009B1 RID: 2481
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LeadingStrike : CardModel
	{
		// Token: 0x06006CCA RID: 27850 RVA: 0x0025FD83 File Offset: 0x0025DF83
		public LeadingStrike()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D24 RID: 7460
		// (get) Token: 0x06006CCB RID: 27851 RVA: 0x0025FD90 File Offset: 0x0025DF90
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Strike };
			}
		}

		// Token: 0x17001D25 RID: 7461
		// (get) Token: 0x06006CCC RID: 27852 RVA: 0x0025FD9F File Offset: 0x0025DF9F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar("Shivs", 1),
					new DamageVar(7m, ValueProp.Move)
				});
			}
		}

		// Token: 0x17001D26 RID: 7462
		// (get) Token: 0x06006CCD RID: 27853 RVA: 0x0025FDC9 File Offset: 0x0025DFC9
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Shiv>(false));
			}
		}

		// Token: 0x06006CCE RID: 27854 RVA: 0x0025FDD8 File Offset: 0x0025DFD8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			for (int i = 0; i < base.DynamicVars["Shivs"].IntValue; i++)
			{
				await Shiv.CreateInHand(base.Owner, base.CombatState);
				await Cmd.Wait(0.25f, false);
			}
		}

		// Token: 0x06006CCF RID: 27855 RVA: 0x0025FE2B File Offset: 0x0025E02B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}

		// Token: 0x04002595 RID: 9621
		private const string _shivsKey = "Shivs";
	}
}
