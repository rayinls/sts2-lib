using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.RedIronclad.cards
{
	public sealed class RedBarricadePower : PowerModel
	{
		// (get) Token: 0x0600512C RID: 20780 RVA: 0x0021EE5B File Offset: 0x0021D05B
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// (get) Token: 0x0600512D RID: 20781 RVA: 0x0021EE5E File Offset: 0x0021D05E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// (get) Token: 0x0600512E RID: 20782 RVA: 0x0021EE61 File Offset: 0x0021D061
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new DynamicVar[] { new StringVar("ApplierName", "") };
			}
		}

		// (get) Token: 0x0600512F RID: 20783 RVA: 0x0021EE77 File Offset: 0x0021D077
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new IHoverTip[] { HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()) };
			}
		}

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

		public override bool ShouldClearBlock(Creature creature)
		{
			return base.Owner != creature;
		}

		private const string _applierNameKey = "ApplierName";
	}
}
