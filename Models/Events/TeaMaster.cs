using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007F2 RID: 2034
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TeaMaster : EventModel
	{
		// Token: 0x0600629F RID: 25247 RVA: 0x0024BB4B File Offset: 0x00249D4B
		public override bool IsAllowed(RunState runState)
		{
			if (runState.CurrentActIndex < 2)
			{
				return runState.Players.All((Player p) => p.Gold >= 150);
			}
			return false;
		}

		// Token: 0x1700186E RID: 6254
		// (get) Token: 0x060062A0 RID: 25248 RVA: 0x0024BB84 File Offset: 0x00249D84
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("BoneTeaCost", 50m),
					new DynamicVar("EmberTeaCost", 150m),
					new StringVar("BoneTeaDescription", ModelDb.Relic<BoneTea>().DynamicDescription.GetFormattedText()),
					new StringVar("EmberTeaDescription", ModelDb.Relic<EmberTea>().DynamicDescription.GetFormattedText()),
					new StringVar("TeaOfDiscourtesyDescription", ModelDb.Relic<TeaOfDiscourtesy>().DynamicDescription.GetFormattedText())
				});
			}
		}

		// Token: 0x060062A1 RID: 25249 RVA: 0x0024BC1C File Offset: 0x00249E1C
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			List<EventOption> list = new List<EventOption>();
			if (base.Owner.Gold >= base.DynamicVars["BoneTeaCost"].BaseValue)
			{
				list.Add(new EventOption(this, new Func<Task>(this.BoneTea), "TEA_MASTER.pages.INITIAL.options.BONE_TEA", HoverTipFactory.FromRelicExcludingItself<BoneTea>()));
			}
			else
			{
				list.Add(new EventOption(this, null, "TEA_MASTER.pages.INITIAL.options.BONE_TEA_LOCKED", Array.Empty<IHoverTip>()));
			}
			if (base.Owner.Gold >= base.DynamicVars["EmberTeaCost"].BaseValue)
			{
				list.Add(new EventOption(this, new Func<Task>(this.EmberTea), "TEA_MASTER.pages.INITIAL.options.EMBER_TEA", HoverTipFactory.FromRelicExcludingItself<EmberTea>()));
			}
			else
			{
				list.Add(new EventOption(this, null, "TEA_MASTER.pages.INITIAL.options.EMBER_TEA_LOCKED", Array.Empty<IHoverTip>()));
			}
			list.Add(new EventOption(this, new Func<Task>(this.TeaOfDiscourtesy), "TEA_MASTER.pages.INITIAL.options.TEA_OF_DISCOURTESY", HoverTipFactory.FromRelicExcludingItself<TeaOfDiscourtesy>()));
			return list;
		}

		// Token: 0x060062A2 RID: 25250 RVA: 0x0024BD20 File Offset: 0x00249F20
		private async Task BoneTea()
		{
			await PlayerCmd.LoseGold(base.DynamicVars["BoneTeaCost"].BaseValue, base.Owner, GoldLossType.Spent);
			await RelicCmd.Obtain<BoneTea>(base.Owner);
			base.SetEventFinished(base.L10NLookup("TEA_MASTER.pages.DONE.description"));
		}

		// Token: 0x060062A3 RID: 25251 RVA: 0x0024BD64 File Offset: 0x00249F64
		private async Task EmberTea()
		{
			await PlayerCmd.LoseGold(base.DynamicVars["EmberTeaCost"].BaseValue, base.Owner, GoldLossType.Spent);
			await RelicCmd.Obtain<EmberTea>(base.Owner);
			base.SetEventFinished(base.L10NLookup("TEA_MASTER.pages.DONE.description"));
		}

		// Token: 0x060062A4 RID: 25252 RVA: 0x0024BDA8 File Offset: 0x00249FA8
		private async Task TeaOfDiscourtesy()
		{
			await RelicCmd.Obtain<TeaOfDiscourtesy>(base.Owner);
			base.SetEventFinished(base.L10NLookup("TEA_MASTER.pages.TEA_OF_DISCOURTESY.description"));
		}

		// Token: 0x040024E0 RID: 9440
		private const string _boneTeaCostKey = "BoneTeaCost";

		// Token: 0x040024E1 RID: 9441
		private const string _emberTeaCostKey = "EmberTeaCost";

		// Token: 0x040024E2 RID: 9442
		private const int _boneTeaCost = 50;

		// Token: 0x040024E3 RID: 9443
		private const int _emberTeaCost = 150;
	}
}
