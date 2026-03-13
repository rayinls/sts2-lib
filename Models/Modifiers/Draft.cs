using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Modifiers
{
	// Token: 0x020007B2 RID: 1970
	[NullableContext(1)]
	[Nullable(0)]
	public class Draft : ModifierModel
	{
		// Token: 0x170017F6 RID: 6134
		// (get) Token: 0x060060D4 RID: 24788 RVA: 0x0024399F File Offset: 0x00241B9F
		public override bool ClearsPlayerDeck
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060060D5 RID: 24789 RVA: 0x002439A4 File Offset: 0x00241BA4
		public override Func<Task> GenerateNeowOption(EventModel eventModel)
		{
			return () => Draft.OfferRewards(eventModel.Owner);
		}

		// Token: 0x060060D6 RID: 24790 RVA: 0x002439CC File Offset: 0x00241BCC
		private static async Task OfferRewards(Player player)
		{
			CardCreationOptions creationOptions = new CardCreationOptions(new <>z__ReadOnlySingleElementList<CardPoolModel>(player.Character.CardPool), CardCreationSource.Other, CardRarityOddsType.RegularEncounter, null).WithFlags(CardCreationFlags.NoUpgradeRoll);
			for (int i = 0; i < 10; i++)
			{
				CardReward reward = new CardReward(creationOptions, 3, player)
				{
					CanSkip = false
				};
				await reward.Populate();
				if (LocalContext.IsMe(player))
				{
					await reward.OnSelectWrapper();
				}
				reward = null;
			}
			foreach (Player player2 in player.RunState.Players)
			{
				player2.RelicGrabBag.Remove<PandorasBox>();
			}
			player.RunState.SharedRelicGrabBag.Remove<PandorasBox>();
		}
	}
}
