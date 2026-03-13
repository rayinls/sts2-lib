using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008AF RID: 2223
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BladeDance : CardModel
	{
		// Token: 0x0600677E RID: 26494 RVA: 0x002557F3 File Offset: 0x002539F3
		public BladeDance()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001AE5 RID: 6885
		// (get) Token: 0x0600677F RID: 26495 RVA: 0x00255800 File Offset: 0x00253A00
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001AE6 RID: 6886
		// (get) Token: 0x06006780 RID: 26496 RVA: 0x00255808 File Offset: 0x00253A08
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x17001AE7 RID: 6887
		// (get) Token: 0x06006781 RID: 26497 RVA: 0x00255815 File Offset: 0x00253A15
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Shiv>(false));
			}
		}

		// Token: 0x06006782 RID: 26498 RVA: 0x00255824 File Offset: 0x00253A24
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			for (int i = 0; i < base.DynamicVars.Cards.IntValue; i++)
			{
				await Shiv.CreateInHand(base.Owner, base.CombatState);
				await Cmd.Wait(0.1f, false);
			}
		}

		// Token: 0x06006783 RID: 26499 RVA: 0x00255867 File Offset: 0x00253A67
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
