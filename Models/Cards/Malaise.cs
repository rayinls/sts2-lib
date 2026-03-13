using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009BE RID: 2494
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Malaise : CardModel
	{
		// Token: 0x06006D24 RID: 27940 RVA: 0x00260A08 File Offset: 0x0025EC08
		public Malaise()
			: base(0, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D4A RID: 7498
		// (get) Token: 0x06006D25 RID: 27941 RVA: 0x00260A15 File Offset: 0x0025EC15
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyEnemy;
			}
		}

		// Token: 0x17001D4B RID: 7499
		// (get) Token: 0x06006D26 RID: 27942 RVA: 0x00260A18 File Offset: 0x0025EC18
		protected override bool HasEnergyCostX
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001D4C RID: 7500
		// (get) Token: 0x06006D27 RID: 27943 RVA: 0x00260A1B File Offset: 0x0025EC1B
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<StrengthPower>(),
					HoverTipFactory.FromPower<WeakPower>()
				});
			}
		}

		// Token: 0x17001D4D RID: 7501
		// (get) Token: 0x06006D28 RID: 27944 RVA: 0x00260A38 File Offset: 0x0025EC38
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x06006D29 RID: 27945 RVA: 0x00260A40 File Offset: 0x0025EC40
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			int powerAmount = base.ResolveEnergyXValue();
			if (base.IsUpgraded)
			{
				int num = powerAmount;
				powerAmount = num + 1;
			}
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<StrengthPower>(cardPlay.Target, -powerAmount, base.Owner.Creature, this, false);
			await PowerCmd.Apply<WeakPower>(cardPlay.Target, powerAmount, base.Owner.Creature, this, false);
		}
	}
}
