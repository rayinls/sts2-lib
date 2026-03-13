using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000527 RID: 1319
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LavaLamp : RelicModel
	{
		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06004CAB RID: 19627 RVA: 0x00216083 File Offset: 0x00214283
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06004CAC RID: 19628 RVA: 0x00216086 File Offset: 0x00214286
		// (set) Token: 0x06004CAD RID: 19629 RVA: 0x0021608E File Offset: 0x0021428E
		[SavedProperty]
		public bool TookDamageThisCombat
		{
			get
			{
				return this._tookDamageThisCombat;
			}
			set
			{
				base.AssertMutable();
				this._tookDamageThisCombat = value;
			}
		}

		// Token: 0x06004CAE RID: 19630 RVA: 0x0021609D File Offset: 0x0021429D
		public override Task AfterRoomEntered(AbstractRoom room)
		{
			this.TookDamageThisCombat = false;
			return Task.CompletedTask;
		}

		// Token: 0x06004CAF RID: 19631 RVA: 0x002160AC File Offset: 0x002142AC
		public override Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			if (!(base.Owner.RunState.CurrentRoom is CombatRoom))
			{
				return Task.CompletedTask;
			}
			if (target != base.Owner.Creature)
			{
				return Task.CompletedTask;
			}
			if (result.UnblockedDamage <= 0)
			{
				return Task.CompletedTask;
			}
			if (props.HasFlag(ValueProp.Unblockable))
			{
				return Task.CompletedTask;
			}
			this.TookDamageThisCombat = true;
			return Task.CompletedTask;
		}

		// Token: 0x06004CB0 RID: 19632 RVA: 0x00216120 File Offset: 0x00214320
		public override bool TryModifyCardRewardOptionsLate(Player player, List<CardCreationResult> cardRewards, CardCreationOptions options)
		{
			if (!(base.Owner.RunState.CurrentRoom is CombatRoom))
			{
				return false;
			}
			if (player != base.Owner)
			{
				return false;
			}
			if (this.TookDamageThisCombat)
			{
				return false;
			}
			foreach (CardCreationResult cardCreationResult in cardRewards)
			{
				CardModel card = cardCreationResult.Card;
				if (card.IsUpgradable)
				{
					CardModel cardModel = base.Owner.RunState.CloneCard(card);
					CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
					cardCreationResult.ModifyCard(cardModel, this);
				}
			}
			return true;
		}

		// Token: 0x040021D3 RID: 8659
		private bool _tookDamageThisCombat;
	}
}
