using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Orbs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000635 RID: 1589
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HailstormPower : PowerModel
	{
		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x06005312 RID: 21266 RVA: 0x0022234C File Offset: 0x0022054C
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x06005313 RID: 21267 RVA: 0x0022234F File Offset: 0x0022054F
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x06005314 RID: 21268 RVA: 0x00222352 File Offset: 0x00220552
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromOrb<FrostOrb>());
			}
		}

		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x06005315 RID: 21269 RVA: 0x0022235E File Offset: 0x0022055E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("FrostOrbs", 1m));
			}
		}

		// Token: 0x06005316 RID: 21270 RVA: 0x00222374 File Offset: 0x00220574
		public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				int num = base.Owner.Player.PlayerCombatState.OrbQueue.Orbs.Count((OrbModel o) => o is FrostOrb);
				if (num >= base.DynamicVars["FrostOrbs"].IntValue)
				{
					base.Flash();
					await CreatureCmd.Damage(choiceContext, base.CombatState.HittableEnemies, base.Amount, ValueProp.Unpowered, base.Owner);
				}
			}
		}

		// Token: 0x04002253 RID: 8787
		public const string frostOrbKey = "FrostOrbs";

		// Token: 0x04002254 RID: 8788
		public const int frostOrbCount = 1;
	}
}
