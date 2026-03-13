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
	// Token: 0x020009AE RID: 2478
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class KnowThyPlace : CardModel
	{
		// Token: 0x06006CBC RID: 27836 RVA: 0x0025FC0F File Offset: 0x0025DE0F
		public KnowThyPlace()
			: base(0, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D1E RID: 7454
		// (get) Token: 0x06006CBD RID: 27837 RVA: 0x0025FC1C File Offset: 0x0025DE1C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<WeakPower>(1m),
					new PowerVar<VulnerablePower>(1m)
				});
			}
		}

		// Token: 0x17001D1F RID: 7455
		// (get) Token: 0x06006CBE RID: 27838 RVA: 0x0025FC43 File Offset: 0x0025DE43
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001D20 RID: 7456
		// (get) Token: 0x06006CBF RID: 27839 RVA: 0x0025FC4B File Offset: 0x0025DE4B
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<WeakPower>(),
					HoverTipFactory.FromPower<VulnerablePower>()
				});
			}
		}

		// Token: 0x06006CC0 RID: 27840 RVA: 0x0025FC68 File Offset: 0x0025DE68
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<WeakPower>(cardPlay.Target, base.DynamicVars.Weak.BaseValue, base.Owner.Creature, this, false);
			await PowerCmd.Apply<VulnerablePower>(cardPlay.Target, base.DynamicVars.Vulnerable.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006CC1 RID: 27841 RVA: 0x0025FCB3 File Offset: 0x0025DEB3
		protected override void OnUpgrade()
		{
			base.RemoveKeyword(CardKeyword.Exhaust);
		}
	}
}
