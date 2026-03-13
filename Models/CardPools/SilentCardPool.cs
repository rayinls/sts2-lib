using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Timeline.Epochs;
using MegaCrit.Sts2.Core.Unlocks;

namespace MegaCrit.Sts2.Core.Models.CardPools
{
	// Token: 0x02000AD6 RID: 2774
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SilentCardPool : CardPoolModel
	{
		// Token: 0x17001FE4 RID: 8164
		// (get) Token: 0x06007347 RID: 29511 RVA: 0x0026D3BB File Offset: 0x0026B5BB
		public override string Title
		{
			get
			{
				return "silent";
			}
		}

		// Token: 0x17001FE5 RID: 8165
		// (get) Token: 0x06007348 RID: 29512 RVA: 0x0026D3C2 File Offset: 0x0026B5C2
		public override string EnergyColorName
		{
			get
			{
				return "silent";
			}
		}

		// Token: 0x17001FE6 RID: 8166
		// (get) Token: 0x06007349 RID: 29513 RVA: 0x0026D3C9 File Offset: 0x0026B5C9
		public override string CardFrameMaterialPath
		{
			get
			{
				return "card_frame_green";
			}
		}

		// Token: 0x17001FE7 RID: 8167
		// (get) Token: 0x0600734A RID: 29514 RVA: 0x0026D3D0 File Offset: 0x0026B5D0
		public override Color DeckEntryCardColor
		{
			get
			{
				return new Color("5EBD00");
			}
		}

		// Token: 0x17001FE8 RID: 8168
		// (get) Token: 0x0600734B RID: 29515 RVA: 0x0026D3DC File Offset: 0x0026B5DC
		public override Color EnergyOutlineColor
		{
			get
			{
				return new Color("1A6625");
			}
		}

		// Token: 0x17001FE9 RID: 8169
		// (get) Token: 0x0600734C RID: 29516 RVA: 0x0026D3E8 File Offset: 0x0026B5E8
		public override bool IsColorless
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600734D RID: 29517 RVA: 0x0026D3EC File Offset: 0x0026B5EC
		protected override CardModel[] GenerateAllCards()
		{
			return new CardModel[]
			{
				ModelDb.Card<Abrasive>(),
				ModelDb.Card<Accelerant>(),
				ModelDb.Card<Accuracy>(),
				ModelDb.Card<Acrobatics>(),
				ModelDb.Card<Adrenaline>(),
				ModelDb.Card<Afterimage>(),
				ModelDb.Card<Anticipate>(),
				ModelDb.Card<Assassinate>(),
				ModelDb.Card<Backflip>(),
				ModelDb.Card<Backstab>(),
				ModelDb.Card<BladeOfInk>(),
				ModelDb.Card<BladeDance>(),
				ModelDb.Card<Blur>(),
				ModelDb.Card<BouncingFlask>(),
				ModelDb.Card<BubbleBubble>(),
				ModelDb.Card<BulletTime>(),
				ModelDb.Card<Burst>(),
				ModelDb.Card<CalculatedGamble>(),
				ModelDb.Card<CloakAndDagger>(),
				ModelDb.Card<CorrosiveWave>(),
				ModelDb.Card<DaggerSpray>(),
				ModelDb.Card<DaggerThrow>(),
				ModelDb.Card<Dash>(),
				ModelDb.Card<DeadlyPoison>(),
				ModelDb.Card<DefendSilent>(),
				ModelDb.Card<Deflect>(),
				ModelDb.Card<DodgeAndRoll>(),
				ModelDb.Card<EchoingSlash>(),
				ModelDb.Card<Envenom>(),
				ModelDb.Card<EscapePlan>(),
				ModelDb.Card<Expertise>(),
				ModelDb.Card<Expose>(),
				ModelDb.Card<FanOfKnives>(),
				ModelDb.Card<Finisher>(),
				ModelDb.Card<Flanking>(),
				ModelDb.Card<Flechettes>(),
				ModelDb.Card<FlickFlack>(),
				ModelDb.Card<FollowThrough>(),
				ModelDb.Card<Footwork>(),
				ModelDb.Card<GrandFinale>(),
				ModelDb.Card<HandTrick>(),
				ModelDb.Card<Haze>(),
				ModelDb.Card<HiddenDaggers>(),
				ModelDb.Card<InfiniteBlades>(),
				ModelDb.Card<KnifeTrap>(),
				ModelDb.Card<LeadingStrike>(),
				ModelDb.Card<LegSweep>(),
				ModelDb.Card<Malaise>(),
				ModelDb.Card<MasterPlanner>(),
				ModelDb.Card<MementoMori>(),
				ModelDb.Card<Mirage>(),
				ModelDb.Card<Murder>(),
				ModelDb.Card<Neutralize>(),
				ModelDb.Card<Nightmare>(),
				ModelDb.Card<NoxiousFumes>(),
				ModelDb.Card<Outbreak>(),
				ModelDb.Card<PhantomBlades>(),
				ModelDb.Card<PiercingWail>(),
				ModelDb.Card<Pinpoint>(),
				ModelDb.Card<PoisonedStab>(),
				ModelDb.Card<Pounce>(),
				ModelDb.Card<PreciseCut>(),
				ModelDb.Card<Predator>(),
				ModelDb.Card<Prepared>(),
				ModelDb.Card<Reflex>(),
				ModelDb.Card<Ricochet>(),
				ModelDb.Card<SerpentForm>(),
				ModelDb.Card<ShadowStep>(),
				ModelDb.Card<Shadowmeld>(),
				ModelDb.Card<Skewer>(),
				ModelDb.Card<Slice>(),
				ModelDb.Card<Snakebite>(),
				ModelDb.Card<Sneaky>(),
				ModelDb.Card<Speedster>(),
				ModelDb.Card<StormOfSteel>(),
				ModelDb.Card<Strangle>(),
				ModelDb.Card<StrikeSilent>(),
				ModelDb.Card<SuckerPunch>(),
				ModelDb.Card<Suppress>(),
				ModelDb.Card<Survivor>(),
				ModelDb.Card<Tactician>(),
				ModelDb.Card<TheHunt>(),
				ModelDb.Card<ToolsOfTheTrade>(),
				ModelDb.Card<Tracking>(),
				ModelDb.Card<Untouchable>(),
				ModelDb.Card<UpMySleeve>(),
				ModelDb.Card<WellLaidPlans>(),
				ModelDb.Card<WraithForm>()
			};
		}

		// Token: 0x0600734E RID: 29518 RVA: 0x0026D710 File Offset: 0x0026B910
		protected override IEnumerable<CardModel> FilterThroughEpochs(UnlockState unlockState, IEnumerable<CardModel> cards)
		{
			List<CardModel> list = cards.ToList<CardModel>();
			if (!unlockState.IsEpochRevealed<Silent2Epoch>())
			{
				list.RemoveAll((CardModel c) => Silent2Epoch.Cards.Any((CardModel card) => card.Id == c.Id));
			}
			if (!unlockState.IsEpochRevealed<Silent5Epoch>())
			{
				list.RemoveAll((CardModel c) => Silent5Epoch.Cards.Any((CardModel card) => card.Id == c.Id));
			}
			if (!unlockState.IsEpochRevealed<Silent7Epoch>())
			{
				list.RemoveAll((CardModel c) => Silent7Epoch.Cards.Any((CardModel card) => card.Id == c.Id));
			}
			return list;
		}
	}
}
