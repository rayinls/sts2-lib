using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005FA RID: 1530
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CrabRagePower : PowerModel
	{
		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x060051CB RID: 20939 RVA: 0x0021FFAB File Offset: 0x0021E1AB
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x060051CC RID: 20940 RVA: 0x0021FFAE File Offset: 0x0021E1AE
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x060051CD RID: 20941 RVA: 0x0021FFB1 File Offset: 0x0021E1B1
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<StrengthPower>(),
					HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>())
				});
			}
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x060051CE RID: 20942 RVA: 0x0021FFD4 File Offset: 0x0021E1D4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<StrengthPower>(5m),
					new BlockVar(99m, ValueProp.Unpowered)
				});
			}
		}

		// Token: 0x060051CF RID: 20943 RVA: 0x00220000 File Offset: 0x0021E200
		public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (creature != base.Owner)
			{
				if (creature.Side == base.Owner.Side)
				{
					base.Flash();
					await PowerCmd.Apply<StrengthPower>(base.Owner, base.DynamicVars.Strength.IntValue, base.Owner, null, false);
					await CreatureCmd.GainBlock(base.Owner, base.DynamicVars.Block, null, false);
					await PowerCmd.Remove(this);
				}
			}
		}
	}
}
