using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Merchant;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Encounters;
using MegaCrit.Sts2.Core.Models.Potions;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes.Events.Custom;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007D3 RID: 2003
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FakeMerchant : EventModel
	{
		// Token: 0x17001815 RID: 6165
		// (get) Token: 0x0600618C RID: 24972 RVA: 0x00246868 File Offset: 0x00244A68
		public static MerchantDialogueSet Dialogue
		{
			get
			{
				if (FakeMerchant._dialogue != null)
				{
					return FakeMerchant._dialogue;
				}
				LocTable table = LocManager.Instance.GetTable("events");
				string text = StringHelper.Slugify("FakeMerchant") + ".talk.";
				IReadOnlyList<LocString> locStringsWithPrefix = table.GetLocStringsWithPrefix(text);
				FakeMerchant._dialogue = MerchantDialogueSet.CreateFromLocStrings(locStringsWithPrefix);
				return FakeMerchant._dialogue;
			}
		}

		// Token: 0x17001816 RID: 6166
		// (get) Token: 0x0600618D RID: 24973 RVA: 0x002468BF File Offset: 0x00244ABF
		public override EventLayoutType LayoutType
		{
			get
			{
				return EventLayoutType.Custom;
			}
		}

		// Token: 0x17001817 RID: 6167
		// (get) Token: 0x0600618E RID: 24974 RVA: 0x002468C2 File Offset: 0x00244AC2
		public override bool IsShared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001818 RID: 6168
		// (get) Token: 0x0600618F RID: 24975 RVA: 0x002468C5 File Offset: 0x00244AC5
		// (set) Token: 0x06006190 RID: 24976 RVA: 0x002468CD File Offset: 0x00244ACD
		public MerchantInventory Inventory
		{
			get
			{
				return this._inventory;
			}
			private set
			{
				base.AssertMutable();
				this._inventory = value;
			}
		}

		// Token: 0x17001819 RID: 6169
		// (get) Token: 0x06006191 RID: 24977 RVA: 0x002468DC File Offset: 0x00244ADC
		// (set) Token: 0x06006192 RID: 24978 RVA: 0x002468E4 File Offset: 0x00244AE4
		public bool StartedFight
		{
			get
			{
				return this._startedFight;
			}
			private set
			{
				base.AssertMutable();
				this._startedFight = value;
			}
		}

		// Token: 0x06006193 RID: 24979 RVA: 0x002468F3 File Offset: 0x00244AF3
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return Array.Empty<EventOption>();
		}

		// Token: 0x1700181A RID: 6170
		// (get) Token: 0x06006194 RID: 24980 RVA: 0x002468FA File Offset: 0x00244AFA
		public override IEnumerable<LocString> GameInfoOptions
		{
			get
			{
				return Array.Empty<LocString>();
			}
		}

		// Token: 0x06006195 RID: 24981 RVA: 0x00246904 File Offset: 0x00244B04
		public override bool IsAllowed(RunState runState)
		{
			if (runState.CurrentActIndex < 1)
			{
				return false;
			}
			if (runState.Players.Count > 1)
			{
				return false;
			}
			return runState.Players.All(delegate(Player player)
			{
				if (player.Gold < 100)
				{
					return player.Potions.Any((PotionModel potion) => potion is FoulPotion);
				}
				return true;
			});
		}

		// Token: 0x06006196 RID: 24982 RVA: 0x00246958 File Offset: 0x00244B58
		protected override Task BeforeEventStarted()
		{
			this.Inventory = new MerchantInventory(base.Owner);
			List<RelicModel> list = FakeMerchant._inventoryRelics.ToList<RelicModel>().UnstableShuffle(base.Rng).Take(6)
				.ToList<RelicModel>();
			foreach (RelicModel relicModel in list)
			{
				MerchantRelicEntry merchantRelicEntry = new MerchantRelicEntry(relicModel.ToMutable(), base.Owner);
				this.Inventory.AddRelicEntry(merchantRelicEntry);
			}
			return Task.CompletedTask;
		}

		// Token: 0x06006197 RID: 24983 RVA: 0x002469F4 File Offset: 0x00244BF4
		public async Task FoulPotionThrown(FoulPotion potion)
		{
			if (LocalContext.IsMine(this))
			{
				NFakeMerchant nfakeMerchant = base.Node as NFakeMerchant;
				if (nfakeMerchant != null)
				{
					await nfakeMerchant.FoulPotionThrown(potion);
				}
			}
			this.StartedFight = true;
			List<RelicReward> list = new List<RelicReward>(1)
			{
				new RelicReward(ModelDb.Relic<FakeMerchantsRug>().ToMutable(), base.Owner)
			};
			foreach (MerchantRelicEntry merchantRelicEntry in this.Inventory.RelicEntries)
			{
				if (merchantRelicEntry.IsStocked)
				{
					list.Add(new RelicReward(merchantRelicEntry.Model, base.Owner));
				}
			}
			base.EnterCombatWithoutExitingEvent<FakeMerchantEventEncounter>(list, false);
		}

		// Token: 0x04002496 RID: 9366
		public const int relicCost = 50;

		// Token: 0x04002497 RID: 9367
		private static readonly RelicModel[] _inventoryRelics = new RelicModel[]
		{
			ModelDb.Relic<FakeAnchor>(),
			ModelDb.Relic<FakeBloodVial>(),
			ModelDb.Relic<FakeHappyFlower>(),
			ModelDb.Relic<FakeLeesWaffle>(),
			ModelDb.Relic<FakeMango>(),
			ModelDb.Relic<FakeOrichalcum>(),
			ModelDb.Relic<FakeSneckoEye>(),
			ModelDb.Relic<FakeStrikeDummy>(),
			ModelDb.Relic<FakeVenerableTeaSet>()
		};

		// Token: 0x04002498 RID: 9368
		[Nullable(2)]
		private static MerchantDialogueSet _dialogue;

		// Token: 0x04002499 RID: 9369
		[Nullable(2)]
		private MerchantInventory _inventory;

		// Token: 0x0400249A RID: 9370
		private bool _startedFight;
	}
}
