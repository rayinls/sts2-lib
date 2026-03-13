using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000636 RID: 1590
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HammerTimePower : PowerModel
	{
		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x06005318 RID: 21272 RVA: 0x002223CF File Offset: 0x002205CF
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x06005319 RID: 21273 RVA: 0x002223D2 File Offset: 0x002205D2
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x0600531A RID: 21274 RVA: 0x002223D5 File Offset: 0x002205D5
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromForge();
			}
		}

		// Token: 0x0600531B RID: 21275 RVA: 0x002223DC File Offset: 0x002205DC
		public override async Task AfterForge(decimal amount, Player forger, [Nullable(2)] AbstractModel source)
		{
			if (!(source is HammerTimePower))
			{
				if (forger == base.Owner.Player)
				{
					IEnumerable<Player> enumerable = base.CombatState.Players.Where((Player p) => p.Creature.IsAlive && p != forger);
					foreach (Player player in enumerable)
					{
						await ForgeCmd.Forge(amount, player, this);
					}
					IEnumerator<Player> enumerator = null;
				}
			}
		}
	}
}
