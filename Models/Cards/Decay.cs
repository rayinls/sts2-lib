using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200090D RID: 2317
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Decay : CardModel
	{
		// Token: 0x06006959 RID: 26969 RVA: 0x002592B7 File Offset: 0x002574B7
		public Decay()
			: base(-1, CardType.Curse, CardRarity.Curse, TargetType.None, true)
		{
		}

		// Token: 0x17001BA9 RID: 7081
		// (get) Token: 0x0600695A RID: 26970 RVA: 0x002592C5 File Offset: 0x002574C5
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001BAA RID: 7082
		// (get) Token: 0x0600695B RID: 26971 RVA: 0x002592C8 File Offset: 0x002574C8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(2m, ValueProp.Unpowered | ValueProp.Move));
			}
		}

		// Token: 0x17001BAB RID: 7083
		// (get) Token: 0x0600695C RID: 26972 RVA: 0x002592DC File Offset: 0x002574DC
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Unplayable);
			}
		}

		// Token: 0x17001BAC RID: 7084
		// (get) Token: 0x0600695D RID: 26973 RVA: 0x002592E4 File Offset: 0x002574E4
		public override bool HasTurnEndInHandEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600695E RID: 26974 RVA: 0x002592E8 File Offset: 0x002574E8
		public override async Task OnTurnEndInHand(PlayerChoiceContext choiceContext)
		{
			await CreatureCmd.Damage(choiceContext, base.Owner.Creature, base.DynamicVars.Damage, this);
		}
	}
}
