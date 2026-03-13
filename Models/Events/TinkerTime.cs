using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007F9 RID: 2041
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TinkerTime : EventModel
	{
		// Token: 0x1700188B RID: 6283
		// (get) Token: 0x060062F5 RID: 25333 RVA: 0x0024D6BF File Offset: 0x0024B8BF
		// (set) Token: 0x060062F6 RID: 25334 RVA: 0x0024D6C7 File Offset: 0x0024B8C7
		private CardType ChosenCardType
		{
			get
			{
				return this._chosenCardType;
			}
			set
			{
				base.AssertMutable();
				this._chosenCardType = value;
			}
		}

		// Token: 0x060062F7 RID: 25335 RVA: 0x0024D6D6 File Offset: 0x0024B8D6
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlySingleElementList<EventOption>(new EventOption(this, new Func<Task>(this.ChooseCardType), "TINKER_TIME.pages.INITIAL.options.CHOOSE_CARD_TYPE", Array.Empty<IHoverTip>()));
		}

		// Token: 0x1700188C RID: 6284
		// (get) Token: 0x060062F8 RID: 25336 RVA: 0x0024D6FC File Offset: 0x0024B8FC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(12m, ValueProp.Move),
					new BlockVar(8m, ValueProp.Move),
					new PowerVar<WeakPower>("SappingWeak", 2m),
					new PowerVar<VulnerablePower>("SappingVulnerable", 2m),
					new DynamicVar("ViolenceHits", 3m),
					new PowerVar<StranglePower>("ChokingDamage", 6m),
					new EnergyVar("EnergizedEnergy", 2),
					new CardsVar("WisdomCards", 3),
					new PowerVar<StrengthPower>("ExpertiseStrength", 2m),
					new PowerVar<DexterityPower>("ExpertiseDexterity", 2m),
					new DynamicVar("CuriousReduction", 1m),
					new EnergyVar("energyPrefix", 1)
				});
			}
		}

		// Token: 0x060062F9 RID: 25337 RVA: 0x0024D7E8 File Offset: 0x0024B9E8
		private Task ChooseCardType()
		{
			IEnumerable<EventOption> enumerable = new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.Attack), "TINKER_TIME.pages.CHOOSE_CARD_TYPE.options.ATTACK", new IHoverTip[] { this.GetCardTypeHoverTip(CardType.Attack) }),
				new EventOption(this, new Func<Task>(this.Skill), "TINKER_TIME.pages.CHOOSE_CARD_TYPE.options.SKILL", new IHoverTip[] { this.GetCardTypeHoverTip(CardType.Skill) }),
				new EventOption(this, new Func<Task>(this.Power), "TINKER_TIME.pages.CHOOSE_CARD_TYPE.options.POWER", new IHoverTip[] { this.GetCardTypeHoverTip(CardType.Power) })
			});
			this.SetEventState(base.L10NLookup("TINKER_TIME.pages.CHOOSE_CARD_TYPE.description"), enumerable.TakeRandom(2, base.Rng));
			return Task.CompletedTask;
		}

		// Token: 0x060062FA RID: 25338 RVA: 0x0024D8A4 File Offset: 0x0024BAA4
		private CardHoverTip GetCardTypeHoverTip(CardType cardType)
		{
			MadScience madScience = base.Owner.RunState.CreateCard<MadScience>(base.Owner);
			madScience.TinkerTimeType = cardType;
			madScience.TinkerTimeRider = TinkerTime.RiderEffect.None;
			return new CardHoverTip(madScience);
		}

		// Token: 0x060062FB RID: 25339 RVA: 0x0024D8DC File Offset: 0x0024BADC
		private Task Attack()
		{
			this.ChosenCardType = CardType.Attack;
			return this.ChooseRiderEffect();
		}

		// Token: 0x060062FC RID: 25340 RVA: 0x0024D8EB File Offset: 0x0024BAEB
		private Task Skill()
		{
			this.ChosenCardType = CardType.Skill;
			return this.ChooseRiderEffect();
		}

		// Token: 0x060062FD RID: 25341 RVA: 0x0024D8FA File Offset: 0x0024BAFA
		private Task Power()
		{
			this.ChosenCardType = CardType.Power;
			return this.ChooseRiderEffect();
		}

		// Token: 0x060062FE RID: 25342 RVA: 0x0024D90C File Offset: 0x0024BB0C
		private Task ChooseRiderEffect()
		{
			IEnumerable<TinkerTime.RiderEffect> enumerable;
			switch (this.ChosenCardType)
			{
			case CardType.Attack:
				enumerable = new <>z__ReadOnlyArray<TinkerTime.RiderEffect>(new TinkerTime.RiderEffect[]
				{
					TinkerTime.RiderEffect.Sapping,
					TinkerTime.RiderEffect.Violence,
					TinkerTime.RiderEffect.Choking
				});
				break;
			case CardType.Skill:
				enumerable = new <>z__ReadOnlyArray<TinkerTime.RiderEffect>(new TinkerTime.RiderEffect[]
				{
					TinkerTime.RiderEffect.Energized,
					TinkerTime.RiderEffect.Wisdom,
					TinkerTime.RiderEffect.Chaos
				});
				break;
			case CardType.Power:
				enumerable = new <>z__ReadOnlyArray<TinkerTime.RiderEffect>(new TinkerTime.RiderEffect[]
				{
					TinkerTime.RiderEffect.Expertise,
					TinkerTime.RiderEffect.Curious,
					TinkerTime.RiderEffect.Improvement
				});
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			IEnumerable<TinkerTime.RiderEffect> enumerable2 = enumerable;
			List<TinkerTime.RiderEffect> riders = enumerable2.TakeRandom(2, base.Rng).ToList<TinkerTime.RiderEffect>();
			this.SetEventState(base.L10NLookup("TINKER_TIME.pages.CHOOSE_RIDER.description"), new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, () => this.RiderChosen(riders[0]), TinkerTime.GetRiderLocKey(riders[0]), new IHoverTip[] { this.GetRiderHoverTip(riders[0]) }),
				new EventOption(this, () => this.RiderChosen(riders[1]), TinkerTime.GetRiderLocKey(riders[1]), new IHoverTip[] { this.GetRiderHoverTip(riders[1]) })
			}));
			return Task.CompletedTask;
		}

		// Token: 0x060062FF RID: 25343 RVA: 0x0024DA54 File Offset: 0x0024BC54
		private CardHoverTip GetRiderHoverTip(TinkerTime.RiderEffect rider)
		{
			MadScience madScience = base.Owner.RunState.CreateCard<MadScience>(base.Owner);
			madScience.TinkerTimeType = this.ChosenCardType;
			madScience.TinkerTimeRider = rider;
			return new CardHoverTip(madScience);
		}

		// Token: 0x06006300 RID: 25344 RVA: 0x0024DA94 File Offset: 0x0024BC94
		private async Task RiderChosen(TinkerTime.RiderEffect rider)
		{
			MadScience madScience = base.Owner.RunState.CreateCard<MadScience>(base.Owner);
			madScience.TinkerTimeType = this.ChosenCardType;
			madScience.TinkerTimeRider = rider;
			CardPileAddResult cardPileAddResult = await CardPileCmd.Add(madScience, PileType.Deck, CardPilePosition.Bottom, null, false);
			CardPileAddResult cardPileAddResult2 = cardPileAddResult;
			CardCmd.PreviewCardPileAdd(cardPileAddResult2, 3f, CardPreviewStyle.HorizontalLayout);
			base.SetEventFinished(base.L10NLookup("TINKER_TIME.pages.DONE.description"));
		}

		// Token: 0x06006301 RID: 25345 RVA: 0x0024DAE0 File Offset: 0x0024BCE0
		private static string GetRiderLocKey(TinkerTime.RiderEffect rider)
		{
			string text;
			switch (rider)
			{
			case TinkerTime.RiderEffect.None:
				throw new ArgumentOutOfRangeException("rider", rider, "None is not a valid rider");
			case TinkerTime.RiderEffect.Sapping:
				text = "TINKER_TIME.pages.CHOOSE_RIDER.options.SAPPING";
				break;
			case TinkerTime.RiderEffect.Violence:
				text = "TINKER_TIME.pages.CHOOSE_RIDER.options.VIOLENCE";
				break;
			case TinkerTime.RiderEffect.Choking:
				text = "TINKER_TIME.pages.CHOOSE_RIDER.options.CHOKING";
				break;
			case TinkerTime.RiderEffect.Energized:
				text = "TINKER_TIME.pages.CHOOSE_RIDER.options.ENERGIZED";
				break;
			case TinkerTime.RiderEffect.Wisdom:
				text = "TINKER_TIME.pages.CHOOSE_RIDER.options.WISDOM";
				break;
			case TinkerTime.RiderEffect.Chaos:
				text = "TINKER_TIME.pages.CHOOSE_RIDER.options.CHAOS";
				break;
			case TinkerTime.RiderEffect.Expertise:
				text = "TINKER_TIME.pages.CHOOSE_RIDER.options.EXPERTISE";
				break;
			case TinkerTime.RiderEffect.Curious:
				text = "TINKER_TIME.pages.CHOOSE_RIDER.options.CURIOUS";
				break;
			case TinkerTime.RiderEffect.Improvement:
				text = "TINKER_TIME.pages.CHOOSE_RIDER.options.IMPROVEMENT";
				break;
			default:
				throw new ArgumentOutOfRangeException("rider", rider, null);
			}
			return text;
		}

		// Token: 0x06006302 RID: 25346 RVA: 0x0024DB90 File Offset: 0x0024BD90
		public static IHoverTip[] GetRiderHoverTips(TinkerTime.RiderEffect rider)
		{
			IHoverTip[] array;
			switch (rider)
			{
			case TinkerTime.RiderEffect.None:
				array = Array.Empty<IHoverTip>();
				break;
			case TinkerTime.RiderEffect.Sapping:
				array = new IHoverTip[]
				{
					HoverTipFactory.FromPower<WeakPower>(),
					HoverTipFactory.FromPower<VulnerablePower>()
				};
				break;
			case TinkerTime.RiderEffect.Violence:
				array = Array.Empty<IHoverTip>();
				break;
			case TinkerTime.RiderEffect.Choking:
				array = new IHoverTip[] { HoverTipFactory.FromPower<StranglePower>() };
				break;
			case TinkerTime.RiderEffect.Energized:
				array = Array.Empty<IHoverTip>();
				break;
			case TinkerTime.RiderEffect.Wisdom:
				array = Array.Empty<IHoverTip>();
				break;
			case TinkerTime.RiderEffect.Chaos:
				array = Array.Empty<IHoverTip>();
				break;
			case TinkerTime.RiderEffect.Expertise:
				array = new IHoverTip[]
				{
					HoverTipFactory.FromPower<StrengthPower>(),
					HoverTipFactory.FromPower<DexterityPower>()
				};
				break;
			case TinkerTime.RiderEffect.Curious:
				array = Array.Empty<IHoverTip>();
				break;
			case TinkerTime.RiderEffect.Improvement:
				array = Array.Empty<IHoverTip>();
				break;
			default:
				throw new ArgumentOutOfRangeException("rider", rider, null);
			}
			return array;
		}

		// Token: 0x040024F3 RID: 9459
		private CardType _chosenCardType;

		// Token: 0x02001D9A RID: 7578
		[NullableContext(0)]
		public enum RiderEffect
		{
			// Token: 0x040076F5 RID: 30453
			None,
			// Token: 0x040076F6 RID: 30454
			Sapping,
			// Token: 0x040076F7 RID: 30455
			Violence,
			// Token: 0x040076F8 RID: 30456
			Choking,
			// Token: 0x040076F9 RID: 30457
			Energized,
			// Token: 0x040076FA RID: 30458
			Wisdom,
			// Token: 0x040076FB RID: 30459
			Chaos,
			// Token: 0x040076FC RID: 30460
			Expertise,
			// Token: 0x040076FD RID: 30461
			Curious,
			// Token: 0x040076FE RID: 30462
			Improvement
		}
	}
}
