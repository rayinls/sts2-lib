using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004C5 RID: 1221
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BowlerHat : RelicModel
	{
		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06004A43 RID: 19011 RVA: 0x00211A33 File Offset: 0x0020FC33
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x00211A36 File Offset: 0x0020FC36
		public override bool IsAllowed(IRunState runState)
		{
			return RelicModel.IsBeforeAct3TreasureChest(runState);
		}

		// Token: 0x06004A45 RID: 19013 RVA: 0x00211A3E File Offset: 0x0020FC3E
		public override bool ShouldGainGold(decimal amount, Player player)
		{
			if (this._isApplyingBonus)
			{
				return true;
			}
			if (player != base.Owner)
			{
				return true;
			}
			this._pendingBonusGold = Math.Floor(amount * 0.2m);
			return true;
		}

		// Token: 0x06004A46 RID: 19014 RVA: 0x00211A74 File Offset: 0x0020FC74
		public override async Task AfterGoldGained(Player player)
		{
			if (player == base.Owner)
			{
				if (!this._isApplyingBonus)
				{
					if (!(this._pendingBonusGold <= 0m))
					{
						decimal pendingBonusGold = this._pendingBonusGold;
						this._pendingBonusGold = 0m;
						this._isApplyingBonus = true;
						base.Flash();
						await PlayerCmd.GainGold(pendingBonusGold, base.Owner, false);
						this._isApplyingBonus = false;
					}
				}
			}
		}

		// Token: 0x04002190 RID: 8592
		private const decimal _bonusMultiplier = 0.2m;

		// Token: 0x04002191 RID: 8593
		private decimal _pendingBonusGold;

		// Token: 0x04002192 RID: 8594
		private bool _isApplyingBonus;
	}
}
