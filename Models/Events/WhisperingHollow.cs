using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x02000802 RID: 2050
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WhisperingHollow : EventModel
	{
		// Token: 0x0600634D RID: 25421 RVA: 0x0024F3EF File Offset: 0x0024D5EF
		public override bool IsAllowed(RunState runState)
		{
			return runState.Players.All((Player p) => p.Gold >= base.DynamicVars.Gold.BaseValue);
		}

		// Token: 0x1700189E RID: 6302
		// (get) Token: 0x0600634E RID: 25422 RVA: 0x0024F408 File Offset: 0x0024D608
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new GoldVar(50),
					new HpLossVar(9m)
				});
			}
		}

		// Token: 0x0600634F RID: 25423 RVA: 0x0024F430 File Offset: 0x0024D630
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.Gold), "WHISPERING_HOLLOW.pages.INITIAL.options.GOLD", Array.Empty<IHoverTip>()),
				new EventOption(this, new Func<Task>(this.Hug), "WHISPERING_HOLLOW.pages.INITIAL.options.HUG", new IHoverTip[] { HoverTipFactory.Static(StaticHoverTip.Transform, Array.Empty<DynamicVar>()) }).ThatDoesDamage(base.DynamicVars.HpLoss.IntValue)
			});
		}

		// Token: 0x06006350 RID: 25424 RVA: 0x0024F4B0 File Offset: 0x0024D6B0
		private async Task Gold()
		{
			await PlayerCmd.LoseGold(base.DynamicVars.Gold.IntValue, base.Owner, GoldLossType.Spent);
			await RewardsCmd.OfferCustom(base.Owner, new List<Reward>(2)
			{
				new PotionReward(base.Owner),
				new PotionReward(base.Owner)
			});
			base.SetEventFinished(base.L10NLookup("WHISPERING_HOLLOW.pages.GOLD.description"));
		}

		// Token: 0x06006351 RID: 25425 RVA: 0x0024F4F4 File Offset: 0x0024D6F4
		private async Task Hug()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.TransformSelectionPrompt, 1);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForTransformation(base.Owner, cardSelectorPrefs, null);
			List<CardModel> list = enumerable.ToList<CardModel>();
			foreach (CardModel cardModel in list)
			{
				await CardCmd.TransformToRandom(cardModel, base.Rng, CardPreviewStyle.EventLayout);
			}
			List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
			await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner.Creature, base.DynamicVars.HpLoss.BaseValue, ValueProp.Unblockable | ValueProp.Unpowered, null, null);
			base.SetEventFinished(base.L10NLookup("WHISPERING_HOLLOW.pages.HUG.description"));
		}
	}
}
