using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004F0 RID: 1264
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EmotionChip : RelicModel
	{
		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x06004B33 RID: 19251 RVA: 0x002135EA File Offset: 0x002117EA
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x06004B34 RID: 19252 RVA: 0x002135ED File Offset: 0x002117ED
		private bool LostHpInPreviousTurn
		{
			get
			{
				return CombatManager.Instance.History.Entries.OfType<DamageReceivedEntry>().Any((DamageReceivedEntry e) => e.Receiver == base.Owner.Creature && !e.Result.WasFullyBlocked && e.RoundNumber + 1 == base.Owner.Creature.CombatState.RoundNumber);
			}
		}

		// Token: 0x06004B35 RID: 19253 RVA: 0x00213614 File Offset: 0x00211814
		public override Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			if (!CombatManager.Instance.IsInProgress)
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
			base.Status = RelicStatus.Active;
			base.Flash();
			return Task.CompletedTask;
		}

		// Token: 0x06004B36 RID: 19254 RVA: 0x00213668 File Offset: 0x00211868
		public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Owner)
			{
				base.Status = RelicStatus.Normal;
				if (this.LostHpInPreviousTurn)
				{
					base.Flash();
					foreach (OrbModel orbModel in base.Owner.PlayerCombatState.OrbQueue.Orbs)
					{
						await OrbCmd.Passive(choiceContext, orbModel, null);
						await Cmd.Wait(0.25f, false);
					}
					IEnumerator<OrbModel> enumerator = null;
				}
			}
		}

		// Token: 0x06004B37 RID: 19255 RVA: 0x002136BB File Offset: 0x002118BB
		public override Task AfterCombatEnd(CombatRoom room)
		{
			base.Status = RelicStatus.Normal;
			return Task.CompletedTask;
		}
	}
}
