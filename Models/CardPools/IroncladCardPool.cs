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
	// Token: 0x02000AD1 RID: 2769
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class IroncladCardPool : CardPoolModel
	{
		// Token: 0x17001FC6 RID: 8134
		// (get) Token: 0x06007319 RID: 29465 RVA: 0x0026C662 File Offset: 0x0026A862
		public override string Title
		{
			get
			{
				return "ironclad";
			}
		}

		// Token: 0x17001FC7 RID: 8135
		// (get) Token: 0x0600731A RID: 29466 RVA: 0x0026C669 File Offset: 0x0026A869
		public override string EnergyColorName
		{
			get
			{
				return "ironclad";
			}
		}

		// Token: 0x17001FC8 RID: 8136
		// (get) Token: 0x0600731B RID: 29467 RVA: 0x0026C670 File Offset: 0x0026A870
		public override string CardFrameMaterialPath
		{
			get
			{
				return "card_frame_red";
			}
		}

		// Token: 0x17001FC9 RID: 8137
		// (get) Token: 0x0600731C RID: 29468 RVA: 0x0026C677 File Offset: 0x0026A877
		public override Color DeckEntryCardColor
		{
			get
			{
				return new Color("D62000");
			}
		}

		// Token: 0x17001FCA RID: 8138
		// (get) Token: 0x0600731D RID: 29469 RVA: 0x0026C683 File Offset: 0x0026A883
		public override Color EnergyOutlineColor
		{
			get
			{
				return new Color("802020");
			}
		}

		// Token: 0x17001FCB RID: 8139
		// (get) Token: 0x0600731E RID: 29470 RVA: 0x0026C68F File Offset: 0x0026A88F
		public override bool IsColorless
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600731F RID: 29471 RVA: 0x0026C694 File Offset: 0x0026A894
		protected override CardModel[] GenerateAllCards()
		{
			return new CardModel[]
			{
				ModelDb.Card<Aggression>(),
				ModelDb.Card<Anger>(),
				ModelDb.Card<Armaments>(),
				ModelDb.Card<AshenStrike>(),
				ModelDb.Card<Barricade>(),
				ModelDb.Card<Bash>(),
				ModelDb.Card<BattleTrance>(),
				ModelDb.Card<BloodWall>(),
				ModelDb.Card<Bloodletting>(),
				ModelDb.Card<Bludgeon>(),
				ModelDb.Card<BodySlam>(),
				ModelDb.Card<Brand>(),
				ModelDb.Card<Break>(),
				ModelDb.Card<Breakthrough>(),
				ModelDb.Card<Bully>(),
				ModelDb.Card<BurningPact>(),
				ModelDb.Card<Cascade>(),
				ModelDb.Card<Cinder>(),
				ModelDb.Card<Colossus>(),
				ModelDb.Card<Conflagration>(),
				ModelDb.Card<Corruption>(),
				ModelDb.Card<CrimsonMantle>(),
				ModelDb.Card<Cruelty>(),
				ModelDb.Card<DarkEmbrace>(),
				ModelDb.Card<DefendIronclad>(),
				ModelDb.Card<DemonForm>(),
				ModelDb.Card<DemonicShield>(),
				ModelDb.Card<Dismantle>(),
				ModelDb.Card<Dominate>(),
				ModelDb.Card<DrumOfBattle>(),
				ModelDb.Card<EvilEye>(),
				ModelDb.Card<ExpectAFight>(),
				ModelDb.Card<Feed>(),
				ModelDb.Card<FeelNoPain>(),
				ModelDb.Card<FiendFire>(),
				ModelDb.Card<FightMe>(),
				ModelDb.Card<FlameBarrier>(),
				ModelDb.Card<ForgottenRitual>(),
				ModelDb.Card<Grapple>(),
				ModelDb.Card<Havoc>(),
				ModelDb.Card<Headbutt>(),
				ModelDb.Card<Hellraiser>(),
				ModelDb.Card<Hemokinesis>(),
				ModelDb.Card<HowlFromBeyond>(),
				ModelDb.Card<Impervious>(),
				ModelDb.Card<InfernalBlade>(),
				ModelDb.Card<Inferno>(),
				ModelDb.Card<Inflame>(),
				ModelDb.Card<IronWave>(),
				ModelDb.Card<Juggernaut>(),
				ModelDb.Card<Juggling>(),
				ModelDb.Card<Mangle>(),
				ModelDb.Card<MoltenFist>(),
				ModelDb.Card<Offering>(),
				ModelDb.Card<OneTwoPunch>(),
				ModelDb.Card<PactsEnd>(),
				ModelDb.Card<PerfectedStrike>(),
				ModelDb.Card<Pillage>(),
				ModelDb.Card<PommelStrike>(),
				ModelDb.Card<PrimalForce>(),
				ModelDb.Card<Pyre>(),
				ModelDb.Card<Rage>(),
				ModelDb.Card<Rampage>(),
				ModelDb.Card<Rupture>(),
				ModelDb.Card<SecondWind>(),
				ModelDb.Card<SetupStrike>(),
				ModelDb.Card<ShrugItOff>(),
				ModelDb.Card<Spite>(),
				ModelDb.Card<Stampede>(),
				ModelDb.Card<Stoke>(),
				ModelDb.Card<Stomp>(),
				ModelDb.Card<StoneArmor>(),
				ModelDb.Card<StrikeIronclad>(),
				ModelDb.Card<SwordBoomerang>(),
				ModelDb.Card<Tank>(),
				ModelDb.Card<Taunt>(),
				ModelDb.Card<TearAsunder>(),
				ModelDb.Card<Thrash>(),
				ModelDb.Card<Thunderclap>(),
				ModelDb.Card<Tremble>(),
				ModelDb.Card<TrueGrit>(),
				ModelDb.Card<TwinStrike>(),
				ModelDb.Card<Unmovable>(),
				ModelDb.Card<Unrelenting>(),
				ModelDb.Card<Uppercut>(),
				ModelDb.Card<Vicious>(),
				ModelDb.Card<Whirlwind>()
			};
		}

		// Token: 0x06007320 RID: 29472 RVA: 0x0026C9B0 File Offset: 0x0026ABB0
		protected override IEnumerable<CardModel> FilterThroughEpochs(UnlockState unlockState, IEnumerable<CardModel> cards)
		{
			List<CardModel> list = cards.ToList<CardModel>();
			if (!unlockState.IsEpochRevealed<Ironclad2Epoch>())
			{
				list.RemoveAll((CardModel c) => Ironclad2Epoch.Cards.Any((CardModel card) => card.Id == c.Id));
			}
			if (!unlockState.IsEpochRevealed<Ironclad5Epoch>())
			{
				list.RemoveAll((CardModel c) => Ironclad5Epoch.Cards.Any((CardModel card) => card.Id == c.Id));
			}
			if (!unlockState.IsEpochRevealed<Ironclad7Epoch>())
			{
				list.RemoveAll((CardModel c) => Ironclad7Epoch.Cards.Any((CardModel card) => card.Id == c.Id));
			}
			return list;
		}
	}
}
