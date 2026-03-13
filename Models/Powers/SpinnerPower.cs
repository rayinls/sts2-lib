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
	// Token: 0x020006A5 RID: 1701
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SpinnerPower : PowerModel
	{
		// Token: 0x170012C6 RID: 4806
		// (get) Token: 0x060055A9 RID: 21929 RVA: 0x00226DE7 File Offset: 0x00224FE7
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012C7 RID: 4807
		// (get) Token: 0x060055AA RID: 21930 RVA: 0x00226DEA File Offset: 0x00224FEA
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170012C8 RID: 4808
		// (get) Token: 0x060055AB RID: 21931 RVA: 0x00226DED File Offset: 0x00224FED
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<GlassOrb>()
				});
			}
		}

		// Token: 0x060055AC RID: 21932 RVA: 0x00226E10 File Offset: 0x00225010
		public override async Task AfterEnergyReset(Player player)
		{
			if (player == base.Owner.Player)
			{
				for (int i = 0; i < base.Amount; i++)
				{
					await OrbCmd.Channel<GlassOrb>(new ThrowingPlayerChoiceContext(), base.Owner.Player);
				}
			}
		}
	}
}
