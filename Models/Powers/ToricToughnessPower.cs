using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006C7 RID: 1735
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ToricToughnessPower : PowerModel
	{
		// Token: 0x17001338 RID: 4920
		// (get) Token: 0x06005689 RID: 22153 RVA: 0x002289CB File Offset: 0x00226BCB
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x0600568A RID: 22154 RVA: 0x002289CE File Offset: 0x00226BCE
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x0600568B RID: 22155 RVA: 0x002289D1 File Offset: 0x00226BD1
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x0600568C RID: 22156 RVA: 0x002289E3 File Offset: 0x00226BE3
		public override bool IsInstanced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700133C RID: 4924
		// (get) Token: 0x0600568D RID: 22157 RVA: 0x002289E6 File Offset: 0x00226BE6
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(0m, ValueProp.Unpowered));
			}
		}

		// Token: 0x0600568E RID: 22158 RVA: 0x002289F8 File Offset: 0x00226BF8
		public void SetBlock(decimal block)
		{
			base.AssertMutable();
			base.DynamicVars.Block.BaseValue = block;
		}

		// Token: 0x0600568F RID: 22159 RVA: 0x00228A14 File Offset: 0x00226C14
		public override async Task AfterBlockCleared(Creature creature)
		{
			if (creature == base.Owner)
			{
				base.Flash();
				await CreatureCmd.GainBlock(base.Owner, base.DynamicVars.Block, null, false);
				await PowerCmd.Decrement(this);
			}
		}
	}
}
