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
	// Token: 0x02000961 RID: 2401
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Friendship : CardModel
	{
		// Token: 0x06006B2C RID: 27436 RVA: 0x0025C92F File Offset: 0x0025AB2F
		public Friendship()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001C7D RID: 7293
		// (get) Token: 0x06006B2D RID: 27437 RVA: 0x0025C93C File Offset: 0x0025AB3C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<StrengthPower>(2m),
					new EnergyVar(1)
				});
			}
		}

		// Token: 0x17001C7E RID: 7294
		// (get) Token: 0x06006B2E RID: 27438 RVA: 0x0025C960 File Offset: 0x0025AB60
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x06006B2F RID: 27439 RVA: 0x0025C970 File Offset: 0x0025AB70
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, -base.DynamicVars["StrengthPower"].BaseValue, base.Owner.Creature, this, false);
			await PowerCmd.Apply<FriendshipPower>(base.Owner.Creature, base.DynamicVars.Energy.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006B30 RID: 27440 RVA: 0x0025C9B3 File Offset: 0x0025ABB3
		protected override void OnUpgrade()
		{
			base.DynamicVars["StrengthPower"].UpgradeValueBy(-1m);
		}
	}
}
