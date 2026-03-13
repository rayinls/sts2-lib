using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Saves.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009BC RID: 2492
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MadScience : CardModel
	{
		// Token: 0x06006D05 RID: 27909 RVA: 0x0026047C File Offset: 0x0025E67C
		public MadScience()
			: base(1, CardType.Attack, CardRarity.Event, TargetType.AnyEnemy, false)
		{
		}

		// Token: 0x17001D3E RID: 7486
		// (get) Token: 0x06006D06 RID: 27910 RVA: 0x00260489 File Offset: 0x0025E689
		public override string PortraitPath
		{
			get
			{
				return this.GetPortraitPath(this.TinkerTimeType);
			}
		}

		// Token: 0x17001D3F RID: 7487
		// (get) Token: 0x06006D07 RID: 27911 RVA: 0x00260497 File Offset: 0x0025E697
		public override string BetaPortraitPath
		{
			get
			{
				return CardModel.MissingPortraitPath;
			}
		}

		// Token: 0x17001D40 RID: 7488
		// (get) Token: 0x06006D08 RID: 27912 RVA: 0x0026049E File Offset: 0x0025E69E
		public string[] AllPortraitPaths
		{
			[PreserveBaseOverrides]
			get
			{
				return new string[]
				{
					this.GetPortraitPath(CardType.Attack),
					this.GetPortraitPath(CardType.Skill),
					this.GetPortraitPath(CardType.Power)
				};
			}
		}

		// Token: 0x06006D09 RID: 27913 RVA: 0x002604C4 File Offset: 0x0025E6C4
		private string GetPortraitPath(CardType cardType)
		{
			return ImageHelper.GetImagePath("atlases/card_atlas.sprites/event/" + this.GetPortraitFilename(cardType) + ".tres");
		}

		// Token: 0x06006D0A RID: 27914 RVA: 0x002604E4 File Offset: 0x0025E6E4
		private string GetPortraitFilename(CardType cardType)
		{
			string text;
			switch (cardType)
			{
			case CardType.Attack:
				text = "mad_science_attack";
				break;
			case CardType.Skill:
				text = "mad_science_skill";
				break;
			case CardType.Power:
				text = "mad_science_power";
				break;
			default:
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(29, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Mad Science is invalid type ");
				defaultInterpolatedStringHandler.AppendFormatted<CardType>(this.TinkerTimeType);
				defaultInterpolatedStringHandler.AppendLiteral(".");
				throw new InvalidOperationException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			}
			return text;
		}

		// Token: 0x17001D41 RID: 7489
		// (get) Token: 0x06006D0B RID: 27915 RVA: 0x0026055C File Offset: 0x0025E75C
		public override CardType Type
		{
			get
			{
				return this.TinkerTimeType;
			}
		}

		// Token: 0x17001D42 RID: 7490
		// (get) Token: 0x06006D0C RID: 27916 RVA: 0x00260564 File Offset: 0x0025E764
		public override TargetType TargetType
		{
			get
			{
				if (this.TinkerTimeType != CardType.Attack)
				{
					return TargetType.Self;
				}
				return TargetType.AnyEnemy;
			}
		}

		// Token: 0x17001D43 RID: 7491
		// (get) Token: 0x06006D0D RID: 27917 RVA: 0x00260572 File Offset: 0x0025E772
		public override bool GainsBlock
		{
			get
			{
				return this.TinkerTimeType == CardType.Skill;
			}
		}

		// Token: 0x17001D44 RID: 7492
		// (get) Token: 0x06006D0E RID: 27918 RVA: 0x00260580 File Offset: 0x0025E780
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
					new DynamicVar("CuriousReduction", 1m)
				});
			}
		}

		// Token: 0x17001D45 RID: 7493
		// (get) Token: 0x06006D0F RID: 27919 RVA: 0x0026065A File Offset: 0x0025E85A
		// (set) Token: 0x06006D10 RID: 27920 RVA: 0x00260662 File Offset: 0x0025E862
		[SavedProperty(SerializationCondition.AlwaysSave, -1)]
		public CardType TinkerTimeType
		{
			get
			{
				return this._tinkerTimeType;
			}
			set
			{
				base.AssertMutable();
				this._tinkerTimeType = value;
			}
		}

		// Token: 0x17001D46 RID: 7494
		// (get) Token: 0x06006D11 RID: 27921 RVA: 0x00260671 File Offset: 0x0025E871
		// (set) Token: 0x06006D12 RID: 27922 RVA: 0x00260679 File Offset: 0x0025E879
		[SavedProperty(SerializationCondition.SaveIfNotTypeDefault)]
		public TinkerTime.RiderEffect TinkerTimeRider
		{
			get
			{
				return this._tinkerTimeRider;
			}
			set
			{
				base.AssertMutable();
				this._tinkerTimeRider = value;
			}
		}

		// Token: 0x17001D47 RID: 7495
		// (get) Token: 0x06006D13 RID: 27923 RVA: 0x00260688 File Offset: 0x0025E888
		// (set) Token: 0x06006D14 RID: 27924 RVA: 0x00260690 File Offset: 0x0025E890
		[Nullable(2)]
		private CardModel MockedChaosCard
		{
			[NullableContext(2)]
			get
			{
				return this._mockedChaosCard;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				this._mockedChaosCard = value;
			}
		}

		// Token: 0x17001D48 RID: 7496
		// (get) Token: 0x06006D15 RID: 27925 RVA: 0x0026069F File Offset: 0x0025E89F
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				if (this.TinkerTimeRider != TinkerTime.RiderEffect.None)
				{
					return TinkerTime.GetRiderHoverTips(this.TinkerTimeRider);
				}
				return Array.Empty<IHoverTip>();
			}
		}

		// Token: 0x06006D16 RID: 27926 RVA: 0x002606BC File Offset: 0x0025E8BC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			if (this.TargetType == TargetType.AnyEnemy && cardPlay.Target == null)
			{
				ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			}
			switch (this.TinkerTimeType)
			{
			case CardType.Attack:
				await this.ExecuteAttack(choiceContext, cardPlay.Target);
				break;
			case CardType.Skill:
				await this.ExecuteSkill(cardPlay);
				break;
			case CardType.Power:
				await this.ExecutePower();
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			TinkerTime.RiderEffect tinkerTimeRider = this.TinkerTimeRider;
			if (tinkerTimeRider == TinkerTime.RiderEffect.Sapping || tinkerTimeRider - TinkerTime.RiderEffect.Choking <= 3)
			{
				await this.ExecuteRider(this.TinkerTimeRider, cardPlay.Target, choiceContext);
			}
		}

		// Token: 0x06006D17 RID: 27927 RVA: 0x00260710 File Offset: 0x0025E910
		private async Task ExecuteAttack(PlayerChoiceContext choiceContext, Creature target)
		{
			int hits = ((this.TinkerTimeRider == TinkerTime.RiderEffect.Violence) ? base.DynamicVars["ViolenceHits"].IntValue : 1);
			for (int i = 0; i < hits; i++)
			{
				await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(target)
					.WithHitFx("vfx/vfx_attack_slash", null, null)
					.Execute(choiceContext);
			}
		}

		// Token: 0x06006D18 RID: 27928 RVA: 0x00260764 File Offset: 0x0025E964
		private async Task ExecuteSkill(CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
		}

		// Token: 0x06006D19 RID: 27929 RVA: 0x002607B0 File Offset: 0x0025E9B0
		private async Task ExecutePower()
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			switch (this.TinkerTimeRider)
			{
			case TinkerTime.RiderEffect.Expertise:
				await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, base.DynamicVars["ExpertiseStrength"].BaseValue, base.Owner.Creature, this, false);
				await PowerCmd.Apply<DexterityPower>(base.Owner.Creature, base.DynamicVars["ExpertiseDexterity"].BaseValue, base.Owner.Creature, this, false);
				break;
			case TinkerTime.RiderEffect.Curious:
				await PowerCmd.Apply<CuriousPower>(base.Owner.Creature, base.DynamicVars["CuriousReduction"].BaseValue, base.Owner.Creature, this, false);
				break;
			case TinkerTime.RiderEffect.Improvement:
				await PowerCmd.Apply<ImprovementPower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
				break;
			}
		}

		// Token: 0x06006D1A RID: 27930 RVA: 0x002607F3 File Offset: 0x0025E9F3
		protected override void OnUpgrade()
		{
			base.AddKeyword(CardKeyword.Innate);
		}

		// Token: 0x06006D1B RID: 27931 RVA: 0x002607FC File Offset: 0x0025E9FC
		protected override void AddExtraArgsToDescription(LocString description)
		{
			description.Add("CardType", this.TinkerTimeType.ToString());
			description.Add("HasRider", this.TinkerTimeRider > TinkerTime.RiderEffect.None);
			foreach (TinkerTime.RiderEffect riderEffect in Enum.GetValues<TinkerTime.RiderEffect>())
			{
				description.Add(riderEffect.ToString(), this.TinkerTimeRider == riderEffect);
			}
		}

		// Token: 0x06006D1C RID: 27932 RVA: 0x00260870 File Offset: 0x0025EA70
		private async Task ExecuteRider(TinkerTime.RiderEffect rider, [Nullable(2)] Creature target, PlayerChoiceContext choiceContext)
		{
			switch (rider)
			{
			case TinkerTime.RiderEffect.Sapping:
				await PowerCmd.Apply<WeakPower>(target, base.DynamicVars["SappingWeak"].BaseValue, base.Owner.Creature, this, false);
				await PowerCmd.Apply<VulnerablePower>(target, base.DynamicVars["SappingVulnerable"].BaseValue, base.Owner.Creature, this, false);
				return;
			case TinkerTime.RiderEffect.Choking:
				await PowerCmd.Apply<StranglePower>(target, base.DynamicVars["ChokingDamage"].BaseValue, base.Owner.Creature, this, false);
				return;
			case TinkerTime.RiderEffect.Energized:
				await PlayerCmd.GainEnergy(base.DynamicVars["EnergizedEnergy"].IntValue, base.Owner);
				return;
			case TinkerTime.RiderEffect.Wisdom:
				await CardPileCmd.Draw(choiceContext, base.DynamicVars["WisdomCards"].IntValue, base.Owner, false);
				return;
			case TinkerTime.RiderEffect.Chaos:
			{
				CardModel cardModel;
				if (this.MockedChaosCard != null)
				{
					cardModel = this.MockedChaosCard;
				}
				else
				{
					cardModel = CardFactory.GetDistinctForCombat(base.Owner, base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint), 1, base.Owner.RunState.Rng.CombatCardGeneration).First<CardModel>();
				}
				cardModel.EnergyCost.SetThisTurnOrUntilPlayed(0, false);
				await CardPileCmd.Add(cardModel, PileType.Hand, CardPilePosition.Bottom, null, false);
				return;
			}
			}
			throw new ArgumentOutOfRangeException("rider", rider, null);
		}

		// Token: 0x06006D1D RID: 27933 RVA: 0x002608CB File Offset: 0x0025EACB
		public void MockChaosCard(CardModel card)
		{
			base.AssertMutable();
			card.AssertMutable();
			this.MockedChaosCard = card;
		}

		// Token: 0x04002598 RID: 9624
		public const int attackDamage = 12;

		// Token: 0x04002599 RID: 9625
		public const int skillBlock = 8;

		// Token: 0x0400259A RID: 9626
		public const string sappingWeakKey = "SappingWeak";

		// Token: 0x0400259B RID: 9627
		public const int sappingWeakValue = 2;

		// Token: 0x0400259C RID: 9628
		public const string sappingVulnerableKey = "SappingVulnerable";

		// Token: 0x0400259D RID: 9629
		public const int sappingVulnerableValue = 2;

		// Token: 0x0400259E RID: 9630
		public const string violenceHitsKey = "ViolenceHits";

		// Token: 0x0400259F RID: 9631
		public const int violenceHitsValue = 3;

		// Token: 0x040025A0 RID: 9632
		public const string chokingDamageKey = "ChokingDamage";

		// Token: 0x040025A1 RID: 9633
		public const int chokingDamageValue = 6;

		// Token: 0x040025A2 RID: 9634
		public const string energizedEnergyKey = "EnergizedEnergy";

		// Token: 0x040025A3 RID: 9635
		public const int energizedEnergyValue = 2;

		// Token: 0x040025A4 RID: 9636
		public const string wisdomCardsKey = "WisdomCards";

		// Token: 0x040025A5 RID: 9637
		public const int wisdomCardsValue = 3;

		// Token: 0x040025A6 RID: 9638
		public const string expertiseStrengthKey = "ExpertiseStrength";

		// Token: 0x040025A7 RID: 9639
		public const int expertiseStrengthValue = 2;

		// Token: 0x040025A8 RID: 9640
		public const string expertiseDexterityKey = "ExpertiseDexterity";

		// Token: 0x040025A9 RID: 9641
		public const int expertiseDexterityValue = 2;

		// Token: 0x040025AA RID: 9642
		public const string curiousReductionKey = "CuriousReduction";

		// Token: 0x040025AB RID: 9643
		public const int curiousReductionValue = 1;

		// Token: 0x040025AC RID: 9644
		private CardType _tinkerTimeType;

		// Token: 0x040025AD RID: 9645
		private TinkerTime.RiderEffect _tinkerTimeRider;

		// Token: 0x040025AE RID: 9646
		[Nullable(2)]
		private CardModel _mockedChaosCard;
	}
}
