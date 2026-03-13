using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000948 RID: 2376
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FeedingFrenzy : CardModel
	{
		// Token: 0x06006A9F RID: 27295 RVA: 0x0025B714 File Offset: 0x00259914
		public FeedingFrenzy()
			: base(0, CardType.Skill, CardRarity.Event, TargetType.Self, true)
		{
		}

		// Token: 0x17001C42 RID: 7234
		// (get) Token: 0x06006AA0 RID: 27296 RVA: 0x0025B721 File Offset: 0x00259921
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<StrengthPower>(5m));
			}
		}

		// Token: 0x17001C43 RID: 7235
		// (get) Token: 0x06006AA1 RID: 27297 RVA: 0x0025B733 File Offset: 0x00259933
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x06006AA2 RID: 27298 RVA: 0x0025B740 File Offset: 0x00259940
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			decimal baseValue = base.DynamicVars.Strength.BaseValue;
			await PowerCmd.Apply<FeedingFrenzyPower>(base.Owner.Creature, baseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006AA3 RID: 27299 RVA: 0x0025B783 File Offset: 0x00259983
		protected override void OnUpgrade()
		{
			base.DynamicVars.Strength.UpgradeValueBy(2m);
		}
	}
}
