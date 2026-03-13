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
	// Token: 0x02000ACE RID: 2766
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DefectCardPool : CardPoolModel
	{
		// Token: 0x17001FB6 RID: 8118
		// (get) Token: 0x06007302 RID: 29442 RVA: 0x0026C101 File Offset: 0x0026A301
		public override string Title
		{
			get
			{
				return "defect";
			}
		}

		// Token: 0x17001FB7 RID: 8119
		// (get) Token: 0x06007303 RID: 29443 RVA: 0x0026C108 File Offset: 0x0026A308
		public override string EnergyColorName
		{
			get
			{
				return "defect";
			}
		}

		// Token: 0x17001FB8 RID: 8120
		// (get) Token: 0x06007304 RID: 29444 RVA: 0x0026C10F File Offset: 0x0026A30F
		public override string CardFrameMaterialPath
		{
			get
			{
				return "card_frame_blue";
			}
		}

		// Token: 0x17001FB9 RID: 8121
		// (get) Token: 0x06007305 RID: 29445 RVA: 0x0026C116 File Offset: 0x0026A316
		public override Color DeckEntryCardColor
		{
			get
			{
				return new Color("3EB3ED");
			}
		}

		// Token: 0x17001FBA RID: 8122
		// (get) Token: 0x06007306 RID: 29446 RVA: 0x0026C122 File Offset: 0x0026A322
		public override Color EnergyOutlineColor
		{
			get
			{
				return new Color("1D5673");
			}
		}

		// Token: 0x17001FBB RID: 8123
		// (get) Token: 0x06007307 RID: 29447 RVA: 0x0026C12E File Offset: 0x0026A32E
		public override bool IsColorless
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007308 RID: 29448 RVA: 0x0026C134 File Offset: 0x0026A334
		protected override CardModel[] GenerateAllCards()
		{
			return new CardModel[]
			{
				ModelDb.Card<AdaptiveStrike>(),
				ModelDb.Card<AllForOne>(),
				ModelDb.Card<BallLightning>(),
				ModelDb.Card<Barrage>(),
				ModelDb.Card<BeamCell>(),
				ModelDb.Card<BiasedCognition>(),
				ModelDb.Card<BoostAway>(),
				ModelDb.Card<BootSequence>(),
				ModelDb.Card<Buffer>(),
				ModelDb.Card<BulkUp>(),
				ModelDb.Card<Capacitor>(),
				ModelDb.Card<Chaos>(),
				ModelDb.Card<ChargeBattery>(),
				ModelDb.Card<Chill>(),
				ModelDb.Card<Claw>(),
				ModelDb.Card<ColdSnap>(),
				ModelDb.Card<Compact>(),
				ModelDb.Card<CompileDriver>(),
				ModelDb.Card<ConsumingShadow>(),
				ModelDb.Card<Coolant>(),
				ModelDb.Card<Coolheaded>(),
				ModelDb.Card<CreativeAi>(),
				ModelDb.Card<Darkness>(),
				ModelDb.Card<DefendDefect>(),
				ModelDb.Card<Defragment>(),
				ModelDb.Card<DoubleEnergy>(),
				ModelDb.Card<Dualcast>(),
				ModelDb.Card<EchoForm>(),
				ModelDb.Card<EnergySurge>(),
				ModelDb.Card<Feral>(),
				ModelDb.Card<FightThrough>(),
				ModelDb.Card<FlakCannon>(),
				ModelDb.Card<FocusedStrike>(),
				ModelDb.Card<Ftl>(),
				ModelDb.Card<Fusion>(),
				ModelDb.Card<GeneticAlgorithm>(),
				ModelDb.Card<Glacier>(),
				ModelDb.Card<Glasswork>(),
				ModelDb.Card<GoForTheEyes>(),
				ModelDb.Card<GunkUp>(),
				ModelDb.Card<Hailstorm>(),
				ModelDb.Card<HelixDrill>(),
				ModelDb.Card<Hologram>(),
				ModelDb.Card<Hotfix>(),
				ModelDb.Card<Hyperbeam>(),
				ModelDb.Card<IceLance>(),
				ModelDb.Card<Ignition>(),
				ModelDb.Card<Iteration>(),
				ModelDb.Card<Leap>(),
				ModelDb.Card<LightningRod>(),
				ModelDb.Card<Loop>(),
				ModelDb.Card<MachineLearning>(),
				ModelDb.Card<MeteorStrike>(),
				ModelDb.Card<Modded>(),
				ModelDb.Card<MomentumStrike>(),
				ModelDb.Card<MultiCast>(),
				ModelDb.Card<Null>(),
				ModelDb.Card<Overclock>(),
				ModelDb.Card<Quadcast>(),
				ModelDb.Card<Rainbow>(),
				ModelDb.Card<Reboot>(),
				ModelDb.Card<Refract>(),
				ModelDb.Card<RocketPunch>(),
				ModelDb.Card<Scavenge>(),
				ModelDb.Card<Scrape>(),
				ModelDb.Card<ShadowShield>(),
				ModelDb.Card<Shatter>(),
				ModelDb.Card<SignalBoost>(),
				ModelDb.Card<Skim>(),
				ModelDb.Card<Smokestack>(),
				ModelDb.Card<Spinner>(),
				ModelDb.Card<Storm>(),
				ModelDb.Card<StrikeDefect>(),
				ModelDb.Card<Subroutine>(),
				ModelDb.Card<Sunder>(),
				ModelDb.Card<Supercritical>(),
				ModelDb.Card<SweepingBeam>(),
				ModelDb.Card<Synchronize>(),
				ModelDb.Card<Synthesis>(),
				ModelDb.Card<Tempest>(),
				ModelDb.Card<TeslaCoil>(),
				ModelDb.Card<Thunder>(),
				ModelDb.Card<TrashToTreasure>(),
				ModelDb.Card<Turbo>(),
				ModelDb.Card<Uproar>(),
				ModelDb.Card<Voltaic>(),
				ModelDb.Card<WhiteNoise>(),
				ModelDb.Card<Zap>()
			};
		}

		// Token: 0x06007309 RID: 29449 RVA: 0x0026C458 File Offset: 0x0026A658
		protected override IEnumerable<CardModel> FilterThroughEpochs(UnlockState unlockState, IEnumerable<CardModel> cards)
		{
			List<CardModel> list = cards.ToList<CardModel>();
			if (!unlockState.IsEpochRevealed<Defect2Epoch>())
			{
				list.RemoveAll((CardModel c) => Defect2Epoch.Cards.Any((CardModel card) => card.Id == c.Id));
			}
			if (!unlockState.IsEpochRevealed<Defect5Epoch>())
			{
				list.RemoveAll((CardModel c) => Defect5Epoch.Cards.Any((CardModel card) => card.Id == c.Id));
			}
			if (!unlockState.IsEpochRevealed<Defect7Epoch>())
			{
				list.RemoveAll((CardModel c) => Defect7Epoch.Cards.Any((CardModel card) => card.Id == c.Id));
			}
			return list;
		}
	}
}
