using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000AB9 RID: 2745
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WasteAway : CardModel, KnowledgeDemon.IChoosable
	{
		// Token: 0x0600725E RID: 29278 RVA: 0x0026AF50 File Offset: 0x00269150
		public WasteAway()
			: base(-1, CardType.Status, CardRarity.Status, TargetType.None, true)
		{
		}

		// Token: 0x17001F6E RID: 8046
		// (get) Token: 0x0600725F RID: 29279 RVA: 0x0026AF5D File Offset: 0x0026915D
		public override bool CanBeGeneratedInCombat
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001F6F RID: 8047
		// (get) Token: 0x06007260 RID: 29280 RVA: 0x0026AF60 File Offset: 0x00269160
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001F70 RID: 8048
		// (get) Token: 0x06007261 RID: 29281 RVA: 0x0026AF63 File Offset: 0x00269163
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x17001F71 RID: 8049
		// (get) Token: 0x06007262 RID: 29282 RVA: 0x0026AF70 File Offset: 0x00269170
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<WasteAwayPower>(1m));
			}
		}

		// Token: 0x06007263 RID: 29283 RVA: 0x0026AF84 File Offset: 0x00269184
		public async Task OnChosen()
		{
			await PowerCmd.Apply<WasteAwayPower>(base.Owner.Creature, base.DynamicVars["WasteAwayPower"].IntValue, base.Owner.Creature, this, false);
		}
	}
}
