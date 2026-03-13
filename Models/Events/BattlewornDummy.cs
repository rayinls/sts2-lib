using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007C4 RID: 1988
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BattlewornDummy : EventModel
	{
		// Token: 0x170017FD RID: 6141
		// (get) Token: 0x06006110 RID: 24848 RVA: 0x002444B7 File Offset: 0x002426B7
		public override bool IsShared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170017FE RID: 6142
		// (get) Token: 0x06006111 RID: 24849 RVA: 0x002444BC File Offset: 0x002426BC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("Setting1Hp", ModelDb.Monster<BattleFriendV1>().MinInitialHp),
					new DynamicVar("Setting2Hp", ModelDb.Monster<BattleFriendV2>().MinInitialHp),
					new DynamicVar("Setting3Hp", ModelDb.Monster<BattleFriendV3>().MinInitialHp)
				});
			}
		}

		// Token: 0x06006112 RID: 24850 RVA: 0x00244528 File Offset: 0x00242728
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			Player owner = base.Owner;
			int num = ((owner != null) ? owner.RunState.Players.Count : 1);
			Player owner2 = base.Owner;
			int num2 = ((owner2 != null) ? owner2.RunState.CurrentActIndex : 0);
			base.DynamicVars["Setting1Hp"].BaseValue = Creature.ScaleHpForMultiplayer(ModelDb.Monster<BattleFriendV1>().MinInitialHp, ModelDb.Encounter<BattlewornDummyEventEncounter>(), num, num2);
			base.DynamicVars["Setting2Hp"].BaseValue = Creature.ScaleHpForMultiplayer(ModelDb.Monster<BattleFriendV2>().MinInitialHp, ModelDb.Encounter<BattlewornDummyEventEncounter>(), num, num2);
			base.DynamicVars["Setting3Hp"].BaseValue = Creature.ScaleHpForMultiplayer(ModelDb.Monster<BattleFriendV3>().MinInitialHp, ModelDb.Encounter<BattlewornDummyEventEncounter>(), num, num2);
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.Setting1), "BATTLEWORN_DUMMY.pages.INITIAL.options.SETTING_1", Array.Empty<IHoverTip>()),
				new EventOption(this, new Func<Task>(this.Setting2), "BATTLEWORN_DUMMY.pages.INITIAL.options.SETTING_2", Array.Empty<IHoverTip>()),
				new EventOption(this, new Func<Task>(this.Setting3), "BATTLEWORN_DUMMY.pages.INITIAL.options.SETTING_3", Array.Empty<IHoverTip>())
			});
		}

		// Token: 0x06006113 RID: 24851 RVA: 0x00244662 File Offset: 0x00242862
		private Task Setting1()
		{
			this.StartCombat(BattlewornDummyEventEncounter.DummySetting.Setting1);
			return Task.CompletedTask;
		}

		// Token: 0x06006114 RID: 24852 RVA: 0x00244670 File Offset: 0x00242870
		private Task Setting2()
		{
			this.StartCombat(BattlewornDummyEventEncounter.DummySetting.Setting2);
			return Task.CompletedTask;
		}

		// Token: 0x06006115 RID: 24853 RVA: 0x0024467E File Offset: 0x0024287E
		private Task Setting3()
		{
			this.StartCombat(BattlewornDummyEventEncounter.DummySetting.Setting3);
			return Task.CompletedTask;
		}

		// Token: 0x06006116 RID: 24854 RVA: 0x0024468C File Offset: 0x0024288C
		public override async Task Resume(AbstractRoom room)
		{
			CombatRoom combatRoom = (CombatRoom)room;
			BattlewornDummyEventEncounter battlewornDummyEventEncounter = (BattlewornDummyEventEncounter)combatRoom.Encounter;
			if (battlewornDummyEventEncounter.RanOutOfTime)
			{
				base.SetEventFinished(base.L10NLookup("BATTLEWORN_DUMMY.pages.DEFEAT.description"));
			}
			else
			{
				base.SetEventFinished(base.L10NLookup("BATTLEWORN_DUMMY.pages.VICTORY.description"));
				switch (battlewornDummyEventEncounter.Setting)
				{
				case BattlewornDummyEventEncounter.DummySetting.Setting1:
				{
					IEnumerable<PotionModel> enumerable = base.Owner.Character.PotionPool.GetUnlockedPotions(base.Owner.UnlockState).Concat(ModelDb.PotionPool<SharedPotionPool>().GetUnlockedPotions(base.Owner.UnlockState));
					PotionModel potionModel = base.Owner.PlayerRng.Rewards.NextItem<PotionModel>(enumerable);
					if (potionModel != null)
					{
						await RewardsCmd.OfferCustom(base.Owner, new List<Reward>(1)
						{
							new PotionReward(potionModel.ToMutable(), base.Owner)
						});
						return;
					}
					return;
				}
				case BattlewornDummyEventEncounter.DummySetting.Setting2:
				{
					IEnumerable<CardModel> enumerable2 = PileType.Deck.GetPile(base.Owner).Cards.Where((CardModel c) => c != null && c.IsUpgradable).ToList<CardModel>().StableShuffle(base.Owner.RunState.Rng.Niche)
						.Take(2);
					using (IEnumerator<CardModel> enumerator = enumerable2.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							CardModel cardModel = enumerator.Current;
							CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
						}
						return;
					}
					break;
				}
				case BattlewornDummyEventEncounter.DummySetting.Setting3:
					break;
				default:
					throw new InvalidOperationException("Setting must be set!");
				}
				RelicModel relicModel = RelicFactory.PullNextRelicFromFront(base.Owner).ToMutable();
				await RelicCmd.Obtain(relicModel, base.Owner, -1);
			}
		}

		// Token: 0x06006117 RID: 24855 RVA: 0x002446D8 File Offset: 0x002428D8
		private void StartCombat(BattlewornDummyEventEncounter.DummySetting setting)
		{
			BattlewornDummyEventEncounter battlewornDummyEventEncounter = (BattlewornDummyEventEncounter)ModelDb.Encounter<BattlewornDummyEventEncounter>().ToMutable();
			battlewornDummyEventEncounter.Setting = setting;
			base.EnterCombatWithoutExitingEvent(battlewornDummyEventEncounter, Array.Empty<Reward>(), true);
		}

		// Token: 0x0400246D RID: 9325
		private const string _setting1HpKey = "Setting1Hp";

		// Token: 0x0400246E RID: 9326
		private const string _setting2HpKey = "Setting2Hp";

		// Token: 0x0400246F RID: 9327
		private const string _setting3HpKey = "Setting3Hp";
	}
}
