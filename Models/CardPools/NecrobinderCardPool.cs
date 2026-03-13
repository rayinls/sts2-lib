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
	// Token: 0x02000AD3 RID: 2771
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NecrobinderCardPool : CardPoolModel
	{
		// Token: 0x17001FD2 RID: 8146
		// (get) Token: 0x0600732D RID: 29485 RVA: 0x0026CB69 File Offset: 0x0026AD69
		public override string Title
		{
			get
			{
				return "necrobinder";
			}
		}

		// Token: 0x17001FD3 RID: 8147
		// (get) Token: 0x0600732E RID: 29486 RVA: 0x0026CB70 File Offset: 0x0026AD70
		public override string EnergyColorName
		{
			get
			{
				return "necrobinder";
			}
		}

		// Token: 0x17001FD4 RID: 8148
		// (get) Token: 0x0600732F RID: 29487 RVA: 0x0026CB77 File Offset: 0x0026AD77
		public override string CardFrameMaterialPath
		{
			get
			{
				return "card_frame_pink";
			}
		}

		// Token: 0x17001FD5 RID: 8149
		// (get) Token: 0x06007330 RID: 29488 RVA: 0x0026CB7E File Offset: 0x0026AD7E
		public override Color DeckEntryCardColor
		{
			get
			{
				return new Color("CD4EED");
			}
		}

		// Token: 0x17001FD6 RID: 8150
		// (get) Token: 0x06007331 RID: 29489 RVA: 0x0026CB8A File Offset: 0x0026AD8A
		public override Color EnergyOutlineColor
		{
			get
			{
				return new Color("803367");
			}
		}

		// Token: 0x17001FD7 RID: 8151
		// (get) Token: 0x06007332 RID: 29490 RVA: 0x0026CB96 File Offset: 0x0026AD96
		public override bool IsColorless
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007333 RID: 29491 RVA: 0x0026CB9C File Offset: 0x0026AD9C
		protected override CardModel[] GenerateAllCards()
		{
			return new CardModel[]
			{
				ModelDb.Card<Afterlife>(),
				ModelDb.Card<BansheesCry>(),
				ModelDb.Card<BlightStrike>(),
				ModelDb.Card<Bodyguard>(),
				ModelDb.Card<BoneShards>(),
				ModelDb.Card<BorrowedTime>(),
				ModelDb.Card<Bury>(),
				ModelDb.Card<Calcify>(),
				ModelDb.Card<CallOfTheVoid>(),
				ModelDb.Card<CaptureSpirit>(),
				ModelDb.Card<Cleanse>(),
				ModelDb.Card<Countdown>(),
				ModelDb.Card<DanseMacabre>(),
				ModelDb.Card<DeathMarch>(),
				ModelDb.Card<Deathbringer>(),
				ModelDb.Card<DeathsDoor>(),
				ModelDb.Card<Debilitate>(),
				ModelDb.Card<DefendNecrobinder>(),
				ModelDb.Card<Defile>(),
				ModelDb.Card<Defy>(),
				ModelDb.Card<Delay>(),
				ModelDb.Card<Demesne>(),
				ModelDb.Card<DevourLife>(),
				ModelDb.Card<Dirge>(),
				ModelDb.Card<DrainPower>(),
				ModelDb.Card<Dredge>(),
				ModelDb.Card<Eidolon>(),
				ModelDb.Card<EndOfDays>(),
				ModelDb.Card<EnfeeblingTouch>(),
				ModelDb.Card<Eradicate>(),
				ModelDb.Card<Fear>(),
				ModelDb.Card<Fetch>(),
				ModelDb.Card<Flatten>(),
				ModelDb.Card<ForbiddenGrimoire>(),
				ModelDb.Card<Friendship>(),
				ModelDb.Card<GlimpseBeyond>(),
				ModelDb.Card<GraveWarden>(),
				ModelDb.Card<Graveblast>(),
				ModelDb.Card<Hang>(),
				ModelDb.Card<Haunt>(),
				ModelDb.Card<HighFive>(),
				ModelDb.Card<Invoke>(),
				ModelDb.Card<LegionOfBone>(),
				ModelDb.Card<Lethality>(),
				ModelDb.Card<Melancholy>(),
				ModelDb.Card<Misery>(),
				ModelDb.Card<NecroMastery>(),
				ModelDb.Card<NegativePulse>(),
				ModelDb.Card<Neurosurge>(),
				ModelDb.Card<NoEscape>(),
				ModelDb.Card<Oblivion>(),
				ModelDb.Card<Pagestorm>(),
				ModelDb.Card<Parse>(),
				ModelDb.Card<Poke>(),
				ModelDb.Card<Protector>(),
				ModelDb.Card<PullAggro>(),
				ModelDb.Card<PullFromBelow>(),
				ModelDb.Card<Putrefy>(),
				ModelDb.Card<Rattle>(),
				ModelDb.Card<Reanimate>(),
				ModelDb.Card<Reap>(),
				ModelDb.Card<ReaperForm>(),
				ModelDb.Card<Reave>(),
				ModelDb.Card<RightHandHand>(),
				ModelDb.Card<Sacrifice>(),
				ModelDb.Card<Scourge>(),
				ModelDb.Card<SculptingStrike>(),
				ModelDb.Card<Seance>(),
				ModelDb.Card<SentryMode>(),
				ModelDb.Card<Severance>(),
				ModelDb.Card<SharedFate>(),
				ModelDb.Card<Shroud>(),
				ModelDb.Card<SicEm>(),
				ModelDb.Card<SleightOfFlesh>(),
				ModelDb.Card<Snap>(),
				ModelDb.Card<SoulStorm>(),
				ModelDb.Card<Sow>(),
				ModelDb.Card<SpiritOfAsh>(),
				ModelDb.Card<Spur>(),
				ModelDb.Card<Squeeze>(),
				ModelDb.Card<StrikeNecrobinder>(),
				ModelDb.Card<TheScythe>(),
				ModelDb.Card<TimesUp>(),
				ModelDb.Card<Transfigure>(),
				ModelDb.Card<Undeath>(),
				ModelDb.Card<Unleash>(),
				ModelDb.Card<Veilpiercer>(),
				ModelDb.Card<Wisp>()
			};
		}

		// Token: 0x06007334 RID: 29492 RVA: 0x0026CEC0 File Offset: 0x0026B0C0
		protected override IEnumerable<CardModel> FilterThroughEpochs(UnlockState unlockState, IEnumerable<CardModel> cards)
		{
			List<CardModel> list = cards.ToList<CardModel>();
			if (!unlockState.IsEpochRevealed<Necrobinder2Epoch>())
			{
				list.RemoveAll((CardModel c) => Necrobinder2Epoch.Cards.Any((CardModel card) => card.Id == c.Id));
			}
			if (!unlockState.IsEpochRevealed<Necrobinder5Epoch>())
			{
				list.RemoveAll((CardModel c) => Necrobinder5Epoch.Cards.Any((CardModel card) => card.Id == c.Id));
			}
			if (!unlockState.IsEpochRevealed<Necrobinder7Epoch>())
			{
				list.RemoveAll((CardModel c) => Necrobinder7Epoch.Cards.Any((CardModel card) => card.Id == c.Id));
			}
			return list;
		}
	}
}
