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
	// Token: 0x02000927 RID: 2343
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Doubt : CardModel
	{
		// Token: 0x060069EA RID: 27114 RVA: 0x0025A1F1 File Offset: 0x002583F1
		public Doubt()
			: base(-1, CardType.Curse, CardRarity.Curse, TargetType.None, true)
		{
		}

		// Token: 0x17001BEF RID: 7151
		// (get) Token: 0x060069EB RID: 27115 RVA: 0x0025A1FF File Offset: 0x002583FF
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001BF0 RID: 7152
		// (get) Token: 0x060069EC RID: 27116 RVA: 0x0025A202 File Offset: 0x00258402
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Unplayable);
			}
		}

		// Token: 0x17001BF1 RID: 7153
		// (get) Token: 0x060069ED RID: 27117 RVA: 0x0025A20A File Offset: 0x0025840A
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<WeakPower>());
			}
		}

		// Token: 0x17001BF2 RID: 7154
		// (get) Token: 0x060069EE RID: 27118 RVA: 0x0025A216 File Offset: 0x00258416
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<WeakPower>(1m));
			}
		}

		// Token: 0x17001BF3 RID: 7155
		// (get) Token: 0x060069EF RID: 27119 RVA: 0x0025A227 File Offset: 0x00258427
		public override bool HasTurnEndInHandEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060069F0 RID: 27120 RVA: 0x0025A22C File Offset: 0x0025842C
		public override async Task OnTurnEndInHand(PlayerChoiceContext choiceContext)
		{
			bool alreadyHasWeak = base.Owner.Creature.HasPower<WeakPower>();
			WeakPower weakPower = await PowerCmd.Apply<WeakPower>(base.Owner.Creature, base.DynamicVars.Weak.BaseValue, null, this, false);
			PowerModel powerModel = weakPower;
			if (powerModel != null && !alreadyHasWeak)
			{
				powerModel.SkipNextDurationTick = true;
			}
		}
	}
}
