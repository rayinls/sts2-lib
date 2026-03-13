using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200057A RID: 1402
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RegalPillow : RelicModel
	{
		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06004ED7 RID: 20183 RVA: 0x00219E67 File Offset: 0x00218067
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06004ED8 RID: 20184 RVA: 0x00219E6A File Offset: 0x0021806A
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new HealVar(15m));
			}
		}

		// Token: 0x06004ED9 RID: 20185 RVA: 0x00219E7D File Offset: 0x0021807D
		public override decimal ModifyRestSiteHealAmount(Creature creature, decimal amount)
		{
			if (creature.Player != base.Owner && creature.PetOwner != base.Owner)
			{
				return amount;
			}
			return amount + base.DynamicVars.Heal.BaseValue;
		}

		// Token: 0x06004EDA RID: 20186 RVA: 0x00219EB3 File Offset: 0x002180B3
		public override Task AfterRestSiteHeal(Player player, bool isMimicked)
		{
			if (player != base.Owner)
			{
				return Task.CompletedTask;
			}
			base.Flash();
			base.Status = RelicStatus.Normal;
			return Task.CompletedTask;
		}

		// Token: 0x06004EDB RID: 20187 RVA: 0x00219ED8 File Offset: 0x002180D8
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

		// Token: 0x06004EDC RID: 20188 RVA: 0x00219F54 File Offset: 0x00218154
		public override Task AfterRoomEntered(AbstractRoom room)
		{
			base.Status = ((room is RestSiteRoom) ? RelicStatus.Active : RelicStatus.Normal);
			return Task.CompletedTask;
		}
	}
}
