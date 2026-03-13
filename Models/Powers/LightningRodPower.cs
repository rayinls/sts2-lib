using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Orbs;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000651 RID: 1617
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LightningRodPower : PowerModel
	{
		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x060053B5 RID: 21429 RVA: 0x00223601 File Offset: 0x00221801
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x060053B6 RID: 21430 RVA: 0x00223604 File Offset: 0x00221804
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x060053B7 RID: 21431 RVA: 0x00223607 File Offset: 0x00221807
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<LightningOrb>()
				});
			}
		}

		// Token: 0x060053B8 RID: 21432 RVA: 0x0022362C File Offset: 0x0022182C
		public override async Task AfterEnergyReset(Player player)
		{
			if (player == base.Owner.Player)
			{
				await OrbCmd.Channel<LightningOrb>(new ThrowingPlayerChoiceContext(), base.Owner.Player);
				await PowerCmd.Decrement(this);
			}
		}
	}
}
