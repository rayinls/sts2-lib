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
	// Token: 0x020009F8 RID: 2552
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PhantomBlades : CardModel
	{
		// Token: 0x06006E58 RID: 28248 RVA: 0x0026303E File Offset: 0x0026123E
		public PhantomBlades()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001DCD RID: 7629
		// (get) Token: 0x06006E59 RID: 28249 RVA: 0x0026304B File Offset: 0x0026124B
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag>();
			}
		}

		// Token: 0x17001DCE RID: 7630
		// (get) Token: 0x06006E5A RID: 28250 RVA: 0x00263052 File Offset: 0x00261252
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromCard<Shiv>(false),
					HoverTipFactory.FromKeyword(CardKeyword.Retain)
				});
			}
		}

		// Token: 0x17001DCF RID: 7631
		// (get) Token: 0x06006E5B RID: 28251 RVA: 0x00263071 File Offset: 0x00261271
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<PhantomBladesPower>(9m));
			}
		}

		// Token: 0x06006E5C RID: 28252 RVA: 0x00263084 File Offset: 0x00261284
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<PhantomBladesPower>(base.Owner.Creature, base.DynamicVars["PhantomBladesPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006E5D RID: 28253 RVA: 0x002630C7 File Offset: 0x002612C7
		protected override void OnUpgrade()
		{
			base.DynamicVars["PhantomBladesPower"].UpgradeValueBy(3m);
		}
	}
}
