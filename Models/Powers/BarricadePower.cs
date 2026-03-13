using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005DE RID: 1502
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BarricadePower : PowerModel
	{
		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x0600512C RID: 20780 RVA: 0x0021EE5B File Offset: 0x0021D05B
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x0600512D RID: 20781 RVA: 0x0021EE5E File Offset: 0x0021D05E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x0600512E RID: 20782 RVA: 0x0021EE61 File Offset: 0x0021D061
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new StringVar("ApplierName", ""));
			}
		}

		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x0600512F RID: 20783 RVA: 0x0021EE77 File Offset: 0x0021D077
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06005130 RID: 20784 RVA: 0x0021EE8C File Offset: 0x0021D08C
		[NullableContext(2)]
		[return: Nullable(1)]
		public override Task AfterApplied(Creature applier, CardModel cardSource)
		{
			Creature applier2 = base.Applier;
			if (applier2 != null && applier2.IsMonster)
			{
				((StringVar)base.DynamicVars["ApplierName"]).StringValue = base.Applier.Monster.Title.GetFormattedText();
			}
			return Task.CompletedTask;
		}

		// Token: 0x06005131 RID: 20785 RVA: 0x0021EEDF File Offset: 0x0021D0DF
		public override bool ShouldClearBlock(Creature creature)
		{
			return base.Owner != creature;
		}

		// Token: 0x0400224A RID: 8778
		private const string _applierNameKey = "ApplierName";
	}
}
