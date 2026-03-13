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
	// Token: 0x020009F2 RID: 2546
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Parry : CardModel
	{
		// Token: 0x06006E38 RID: 28216 RVA: 0x00262C1C File Offset: 0x00260E1C
		public Parry()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001DC0 RID: 7616
		// (get) Token: 0x06006E39 RID: 28217 RVA: 0x00262C29 File Offset: 0x00260E29
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromCard<SovereignBlade>(false),
					HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>())
				});
			}
		}

		// Token: 0x17001DC1 RID: 7617
		// (get) Token: 0x06006E3A RID: 28218 RVA: 0x00262C4D File Offset: 0x00260E4D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<ParryPower>(6m));
			}
		}

		// Token: 0x06006E3B RID: 28219 RVA: 0x00262C60 File Offset: 0x00260E60
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<ParryPower>(base.Owner.Creature, base.DynamicVars["ParryPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006E3C RID: 28220 RVA: 0x00262CA3 File Offset: 0x00260EA3
		protected override void OnUpgrade()
		{
			base.DynamicVars["ParryPower"].UpgradeValueBy(3m);
		}
	}
}
