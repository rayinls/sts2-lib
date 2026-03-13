using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Potions;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Rewards;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007D1 RID: 2001
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DrowningBeacon : EventModel
	{
		// Token: 0x17001811 RID: 6161
		// (get) Token: 0x06006170 RID: 24944 RVA: 0x00245F50 File Offset: 0x00244150
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new HpLossVar(13m),
					new StringVar("Potion", ModelDb.Potion<GlowwaterPotion>().Title.GetFormattedText()),
					new StringVar("Relic", ModelDb.Relic<FresnelLens>().Title.GetFormattedText())
				});
			}
		}

		// Token: 0x06006171 RID: 24945 RVA: 0x00245FB0 File Offset: 0x002441B0
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.BottleOption), "DROWNING_BEACON.pages.INITIAL.options.BOTTLE", new IHoverTip[] { HoverTipFactory.FromPotion(ModelDb.Potion<GlowwaterPotion>()) }),
				new EventOption(this, new Func<Task>(this.ClimbOption), "DROWNING_BEACON.pages.INITIAL.options.CLIMB", HoverTipFactory.FromRelic<FresnelLens>())
			});
		}

		// Token: 0x06006172 RID: 24946 RVA: 0x00246014 File Offset: 0x00244214
		private async Task BottleOption()
		{
			await RewardsCmd.OfferCustom(base.Owner, new List<Reward>(1)
			{
				new PotionReward(ModelDb.Potion<GlowwaterPotion>().ToMutable(), base.Owner)
			});
			base.SetEventFinished(base.L10NLookup("DROWNING_BEACON.pages.BOTTLE.description"));
		}

		// Token: 0x06006173 RID: 24947 RVA: 0x00246058 File Offset: 0x00244258
		private async Task ClimbOption()
		{
			await CreatureCmd.LoseMaxHp(new ThrowingPlayerChoiceContext(), base.Owner.Creature, base.DynamicVars.HpLoss.BaseValue, false);
			await RelicCmd.Obtain(ModelDb.Relic<FresnelLens>().ToMutable(), base.Owner, -1);
			base.SetEventFinished(base.L10NLookup("DROWNING_BEACON.pages.CLIMB.description"));
		}

		// Token: 0x0400248C RID: 9356
		private const string _potionKey = "Potion";

		// Token: 0x0400248D RID: 9357
		private const string _relicKey = "Relic";
	}
}
