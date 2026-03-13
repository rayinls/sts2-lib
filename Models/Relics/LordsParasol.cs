using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Merchant;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Screens.Map;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200052F RID: 1327
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LordsParasol : RelicModel
	{
		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06004CE3 RID: 19683 RVA: 0x0021670F File Offset: 0x0021490F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x06004CE4 RID: 19684 RVA: 0x00216714 File Offset: 0x00214914
		public override Task AfterRoomEntered(AbstractRoom room)
		{
			MerchantRoom merchantRoom = room as MerchantRoom;
			if (merchantRoom == null)
			{
				return Task.CompletedTask;
			}
			TaskHelper.RunSafely(this.PurchaseEverything(merchantRoom.Inventory));
			return Task.CompletedTask;
		}

		// Token: 0x06004CE5 RID: 19685 RVA: 0x00216748 File Offset: 0x00214948
		private async Task PurchaseEverything(MerchantInventory inventory)
		{
			if (inventory.Player == base.Owner)
			{
				bool uiBlocked = false;
				try
				{
					if (TestMode.IsOff)
					{
						NRun.Instance.GlobalUi.TopBar.Map.Disable();
						NRun.Instance.GlobalUi.TopBar.Deck.Disable();
						NMapScreen.Instance.SetTravelEnabled(false);
						uiBlocked = true;
						NMerchantRoom.Instance.BlockInput();
						await Cmd.Wait(0.75f, false);
						NMerchantRoom.Instance.Inventory.Open();
						NHotkeyManager.Instance.AddBlockingScreen(NMerchantRoom.Instance.Inventory);
						await Cmd.Wait(1f, false);
					}
					foreach (MerchantCardEntry merchantCardEntry in inventory.CharacterCardEntries)
					{
						await merchantCardEntry.OnTryPurchaseWrapper(inventory, true);
						await Cmd.Wait(0.25f, false);
					}
					IEnumerator<MerchantCardEntry> enumerator = null;
					foreach (MerchantCardEntry merchantCardEntry2 in inventory.ColorlessCardEntries)
					{
						await merchantCardEntry2.OnTryPurchaseWrapper(inventory, true);
						await Cmd.Wait(0.25f, false);
					}
					enumerator = null;
					foreach (MerchantRelicEntry merchantRelicEntry in inventory.RelicEntries)
					{
						NRun.Instance.GlobalUi.TopBar.Map.Enable();
						NRun.Instance.GlobalUi.TopBar.Deck.Enable();
						await merchantRelicEntry.OnTryPurchaseWrapper(inventory, true);
						NRun.Instance.GlobalUi.TopBar.Deck.Disable();
						NRun.Instance.GlobalUi.TopBar.Map.Disable();
						await Cmd.Wait(0.25f, false);
					}
					IEnumerator<MerchantRelicEntry> enumerator2 = null;
					foreach (MerchantPotionEntry merchantPotionEntry in inventory.PotionEntries)
					{
						await merchantPotionEntry.OnTryPurchaseWrapper(inventory, true);
						await Cmd.Wait(0.25f, false);
					}
					IEnumerator<MerchantPotionEntry> enumerator3 = null;
				}
				finally
				{
					if (uiBlocked)
					{
						NHotkeyManager.Instance.RemoveBlockingScreen(NMerchantRoom.Instance.Inventory);
						NMerchantRoom.Instance.UnblockInput();
						NRun.Instance.GlobalUi.TopBar.Map.Enable();
						NRun.Instance.GlobalUi.TopBar.Deck.Enable();
						NMapScreen.Instance.SetTravelEnabled(true);
					}
				}
				if (inventory.CardRemovalEntry != null)
				{
					NMapScreen.Instance.SetTravelEnabled(false);
					await inventory.CardRemovalEntry.OnTryPurchaseWrapper(inventory, true, false);
					NMapScreen.Instance.SetTravelEnabled(true);
				}
			}
		}
	}
}
