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
	// Token: 0x02000903 RID: 2307
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DarkShackles : CardModel
	{
		// Token: 0x06006924 RID: 26916 RVA: 0x00258C8F File Offset: 0x00256E8F
		public DarkShackles()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001B8F RID: 7055
		// (get) Token: 0x06006925 RID: 26917 RVA: 0x00258C9C File Offset: 0x00256E9C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("StrengthLoss", 9m));
			}
		}

		// Token: 0x17001B90 RID: 7056
		// (get) Token: 0x06006926 RID: 26918 RVA: 0x00258CB4 File Offset: 0x00256EB4
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001B91 RID: 7057
		// (get) Token: 0x06006927 RID: 26919 RVA: 0x00258CBC File Offset: 0x00256EBC
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x06006928 RID: 26920 RVA: 0x00258CC8 File Offset: 0x00256EC8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<DarkShacklesPower>(cardPlay.Target, base.DynamicVars["StrengthLoss"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006929 RID: 26921 RVA: 0x00258D13 File Offset: 0x00256F13
		protected override void OnUpgrade()
		{
			base.DynamicVars["StrengthLoss"].UpgradeValueBy(6m);
		}

		// Token: 0x0400256D RID: 9581
		private const string _strengthLossKey = "StrengthLoss";
	}
}
