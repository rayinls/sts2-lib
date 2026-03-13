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
	// Token: 0x02000934 RID: 2356
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EnfeeblingTouch : CardModel
	{
		// Token: 0x06006A30 RID: 27184 RVA: 0x0025A982 File Offset: 0x00258B82
		public EnfeeblingTouch()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001C0F RID: 7183
		// (get) Token: 0x06006A31 RID: 27185 RVA: 0x0025A98F File Offset: 0x00258B8F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("StrengthLoss", 8m));
			}
		}

		// Token: 0x17001C10 RID: 7184
		// (get) Token: 0x06006A32 RID: 27186 RVA: 0x0025A9A6 File Offset: 0x00258BA6
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Ethereal);
			}
		}

		// Token: 0x17001C11 RID: 7185
		// (get) Token: 0x06006A33 RID: 27187 RVA: 0x0025A9AE File Offset: 0x00258BAE
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x06006A34 RID: 27188 RVA: 0x0025A9BC File Offset: 0x00258BBC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<EnfeeblingTouchPower>(cardPlay.Target, base.DynamicVars["StrengthLoss"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006A35 RID: 27189 RVA: 0x0025AA07 File Offset: 0x00258C07
		protected override void OnUpgrade()
		{
			base.DynamicVars["StrengthLoss"].UpgradeValueBy(3m);
		}

		// Token: 0x04002575 RID: 9589
		private const string _strengthLossKey = "StrengthLoss";
	}
}
