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
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009FA RID: 2554
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PiercingWail : CardModel
	{
		// Token: 0x06006E62 RID: 28258 RVA: 0x002631AC File Offset: 0x002613AC
		public PiercingWail()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001DD1 RID: 7633
		// (get) Token: 0x06006E63 RID: 28259 RVA: 0x002631B9 File Offset: 0x002613B9
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("StrengthLoss", 6m));
			}
		}

		// Token: 0x17001DD2 RID: 7634
		// (get) Token: 0x06006E64 RID: 28260 RVA: 0x002631D0 File Offset: 0x002613D0
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001DD3 RID: 7635
		// (get) Token: 0x06006E65 RID: 28261 RVA: 0x002631D8 File Offset: 0x002613D8
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x06006E66 RID: 28262 RVA: 0x002631E4 File Offset: 0x002613E4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			foreach (Creature creature in base.CombatState.HittableEnemies)
			{
				await PowerCmd.Apply<PiercingWailPower>(creature, base.DynamicVars["StrengthLoss"].BaseValue, base.Owner.Creature, this, false);
			}
			IEnumerator<Creature> enumerator = null;
		}

		// Token: 0x06006E67 RID: 28263 RVA: 0x00263227 File Offset: 0x00261427
		protected override void OnUpgrade()
		{
			base.DynamicVars["StrengthLoss"].UpgradeValueBy(2m);
		}

		// Token: 0x040025BE RID: 9662
		private const string _strengthLossKey = "StrengthLoss";
	}
}
