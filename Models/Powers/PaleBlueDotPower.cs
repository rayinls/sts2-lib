using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200066B RID: 1643
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PaleBlueDotPower : PowerModel
	{
		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x06005446 RID: 21574 RVA: 0x0022436F File Offset: 0x0022256F
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x06005447 RID: 21575 RVA: 0x00224372 File Offset: 0x00222572
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x06005448 RID: 21576 RVA: 0x00224375 File Offset: 0x00222575
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("CardPlay", 5m));
			}
		}

		// Token: 0x06005449 RID: 21577 RVA: 0x0022438C File Offset: 0x0022258C
		public override decimal ModifyHandDraw(Player player, decimal count)
		{
			if (player != base.Owner.Player)
			{
				return count;
			}
			int num = CombatManager.Instance.History.CardPlaysFinished.Count((CardPlayFinishedEntry c) => c.RoundNumber == base.CombatState.RoundNumber - 1 && c.CardPlay.Card.Owner == base.Owner.Player);
			if (num < base.DynamicVars["CardPlay"].IntValue)
			{
				return count;
			}
			return count + base.Amount;
		}

		// Token: 0x0600544A RID: 21578 RVA: 0x002243F5 File Offset: 0x002225F5
		public override Task AfterModifyingHandDraw()
		{
			base.Flash();
			return Task.CompletedTask;
		}

		// Token: 0x04002261 RID: 8801
		public const string cardPlayThresholdKey = "CardPlay";

		// Token: 0x04002262 RID: 8802
		public const int cardPlayThresholdValue = 5;
	}
}
