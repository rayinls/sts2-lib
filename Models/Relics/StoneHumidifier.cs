using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200059A RID: 1434
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class StoneHumidifier : RelicModel
	{
		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x06004F9C RID: 20380 RVA: 0x0021B713 File Offset: 0x00219913
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x06004F9D RID: 20381 RVA: 0x0021B716 File Offset: 0x00219916
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new MaxHpVar(5m));
			}
		}

		// Token: 0x06004F9E RID: 20382 RVA: 0x0021B728 File Offset: 0x00219928
		public override async Task AfterRestSiteHeal(Player player, bool isMimicked)
		{
			if (player == base.Owner)
			{
				base.Flash();
				await CreatureCmd.GainMaxHp(base.Owner.Creature, base.DynamicVars.MaxHp.BaseValue);
			}
		}

		// Token: 0x06004F9F RID: 20383 RVA: 0x0021B774 File Offset: 0x00219974
		public override IReadOnlyList<LocString> ModifyExtraRestSiteHealText(Player player, IReadOnlyList<LocString> currentExtraText)
		{
			if (!LocalContext.IsMe(base.Owner))
			{
				return currentExtraText;
			}
			int num = 0;
			LocString[] array = new LocString[1 + currentExtraText.Count];
			foreach (LocString locString in currentExtraText)
			{
				array[num] = locString;
				num++;
			}
			array[num] = base.AdditionalRestSiteHealText;
			return new <>z__ReadOnlyArray<LocString>(array);
		}
	}
}
