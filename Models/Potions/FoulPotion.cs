using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Events.Custom;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.TestSupport;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x020006FA RID: 1786
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FoulPotion : PotionModel
	{
		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x060057BA RID: 22458 RVA: 0x0022A3EB File Offset: 0x002285EB
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Event;
			}
		}

		// Token: 0x170013E7 RID: 5095
		// (get) Token: 0x060057BB RID: 22459 RVA: 0x0022A3EE File Offset: 0x002285EE
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.AnyTime;
			}
		}

		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x060057BC RID: 22460 RVA: 0x0022A3F1 File Offset: 0x002285F1
		public override TargetType TargetType
		{
			get
			{
				if (!CombatManager.Instance.IsInProgress)
				{
					return TargetType.TargetedNoCreature;
				}
				return TargetType.AllEnemies;
			}
		}

		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x060057BD RID: 22461 RVA: 0x0022A402 File Offset: 0x00228602
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(12m, ValueProp.Unpowered),
					new GoldVar(100)
				});
			}
		}

		// Token: 0x170013EA RID: 5098
		// (get) Token: 0x060057BE RID: 22462 RVA: 0x0022A42C File Offset: 0x0022862C
		public override bool PassesCustomUsabilityCheck
		{
			get
			{
				if (CombatManager.Instance.IsInProgress)
				{
					return true;
				}
				if (base.Owner.RunState.CurrentRoom is MerchantRoom)
				{
					return true;
				}
				EventRoom eventRoom = base.Owner.RunState.CurrentRoom as EventRoom;
				return eventRoom != null && eventRoom.CanonicalEvent is FakeMerchant;
			}
		}

		// Token: 0x060057BF RID: 22463 RVA: 0x0022A48C File Offset: 0x0022868C
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			if (CombatManager.Instance.IsInProgress)
			{
				Creature creature = base.Owner.Creature;
				DamageVar damage = base.DynamicVars.Damage;
				await CreatureCmd.Damage(choiceContext, base.Owner.Creature.CombatState.Creatures, damage.BaseValue, damage.Props, creature, null);
			}
			else if (base.Owner.RunState.CurrentRoom is MerchantRoom)
			{
				NRun instance = NRun.Instance;
				NMerchantRoom nmerchantRoom = ((instance != null) ? instance.MerchantRoom : null);
				if (nmerchantRoom != null)
				{
					this.ShowPotionVfx(nmerchantRoom.MerchantButton);
					nmerchantRoom.FoulPotionThrown(this);
				}
				await PlayerCmd.GainGold(base.DynamicVars.Gold.BaseValue, base.Owner, false);
			}
			else
			{
				EventRoom eventRoom = base.Owner.RunState.CurrentRoom as EventRoom;
				if (eventRoom != null && eventRoom.CanonicalEvent is FakeMerchant)
				{
					EventModel localMutableEvent = eventRoom.LocalMutableEvent;
					if (localMutableEvent != null)
					{
						NFakeMerchant nfakeMerchant = localMutableEvent.Node as NFakeMerchant;
						if (nfakeMerchant != null)
						{
							this.ShowPotionVfx(nfakeMerchant.MerchantButton);
							List<Task> list = new List<Task>();
							foreach (Player player in base.Owner.RunState.Players)
							{
								list.Add(((FakeMerchant)RunManager.Instance.EventSynchronizer.GetEventForPlayer(player)).FoulPotionThrown(this));
							}
							await Task.WhenAll(list);
						}
					}
				}
			}
		}

		// Token: 0x060057C0 RID: 22464 RVA: 0x0022A4D8 File Offset: 0x002286D8
		[NullableContext(2)]
		private void ShowPotionVfx(NMerchantButton merchantButton)
		{
			if (TestMode.IsOn)
			{
				return;
			}
			if (merchantButton == null)
			{
				return;
			}
			string scenePath = SceneHelper.GetScenePath("vfx/vfx_slime_impact");
			Node2D node2D = PreloadManager.Cache.GetScene(scenePath).Instantiate<Node2D>(PackedScene.GenEditState.Disabled);
			merchantButton.GetParent().AddChildSafely(node2D);
			node2D.GlobalPosition = merchantButton.GlobalPosition;
		}
	}
}
