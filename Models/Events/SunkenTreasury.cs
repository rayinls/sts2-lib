using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007EE RID: 2030
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SunkenTreasury : EventModel
	{
		// Token: 0x0600627E RID: 25214 RVA: 0x0024B09C File Offset: 0x0024929C
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.FirstChest), "SUNKEN_TREASURY.pages.INITIAL.options.FIRST_CHEST", Array.Empty<IHoverTip>()),
				new EventOption(this, new Func<Task>(this.SecondChest), "SUNKEN_TREASURY.pages.INITIAL.options.SECOND_CHEST", new IHoverTip[] { HoverTipFactory.FromCard<Greed>(false) })
			});
		}

		// Token: 0x17001865 RID: 6245
		// (get) Token: 0x0600627F RID: 25215 RVA: 0x0024B0FC File Offset: 0x002492FC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("SmallChestGold", 60m),
					new DynamicVar("LargeChestGold", 333m)
				});
			}
		}

		// Token: 0x06006280 RID: 25216 RVA: 0x0024B134 File Offset: 0x00249334
		public override void CalculateVars()
		{
			base.DynamicVars["SmallChestGold"].BaseValue += base.Rng.NextInt(16) - 8;
			base.DynamicVars["LargeChestGold"].BaseValue += base.Rng.NextInt(61) - 30;
		}

		// Token: 0x06006281 RID: 25217 RVA: 0x0024B1AC File Offset: 0x002493AC
		private async Task FirstChest()
		{
			await PlayerCmd.GainGold(base.DynamicVars["SmallChestGold"].BaseValue, base.Owner, false);
			base.SetEventFinished(base.L10NLookup("SUNKEN_TREASURY.pages.FIRST_CHEST.description"));
		}

		// Token: 0x06006282 RID: 25218 RVA: 0x0024B1F0 File Offset: 0x002493F0
		private async Task SecondChest()
		{
			await PlayerCmd.GainGold(base.DynamicVars["LargeChestGold"].BaseValue, base.Owner, false);
			await CardPileCmd.AddCurseToDeck<Greed>(base.Owner);
			LocString locString = base.L10NLookup("SUNKEN_TREASURY.pages.SECOND_CHEST.description");
			locString.Add("Monologue", new LocString("characters", base.Owner.Character.Id.Entry + ".goldMonologue"));
			base.SetEventFinished(locString);
		}

		// Token: 0x040024D6 RID: 9430
		private const string _smallChestGoldKey = "SmallChestGold";

		// Token: 0x040024D7 RID: 9431
		private const string _largeChestGoldKey = "LargeChestGold";
	}
}
