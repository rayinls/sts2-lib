using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;

namespace MegaCrit.Sts2.Core.Models.Modifiers
{
	// Token: 0x020007B8 RID: 1976
	[NullableContext(1)]
	[Nullable(0)]
	public class NightTerrors : ModifierModel
	{
		// Token: 0x060060E6 RID: 24806 RVA: 0x00243C5B File Offset: 0x00241E5B
		public override decimal ModifyRestSiteHealAmount(Creature creature, decimal amount)
		{
			return creature.MaxHp;
		}

		// Token: 0x060060E7 RID: 24807 RVA: 0x00243C68 File Offset: 0x00241E68
		public override async Task AfterRestSiteHeal(Player player, bool isMimicked)
		{
			if (!isMimicked)
			{
				await CreatureCmd.LoseMaxHp(new ThrowingPlayerChoiceContext(), player.Creature, 5m, false);
			}
		}

		// Token: 0x060060E8 RID: 24808 RVA: 0x00243CB4 File Offset: 0x00241EB4
		public override IReadOnlyList<LocString> ModifyExtraRestSiteHealText(Player player, IReadOnlyList<LocString> currentExtraText)
		{
			LocString additionalRestSiteHealText = base.AdditionalRestSiteHealText;
			additionalRestSiteHealText.Add("Heal", player.Creature.MaxHp);
			additionalRestSiteHealText.Add("MaxHpLoss", 5m);
			int num = 0;
			LocString[] array = new LocString[1 + currentExtraText.Count];
			foreach (LocString locString in currentExtraText)
			{
				array[num] = locString;
				num++;
			}
			array[num] = additionalRestSiteHealText;
			return new <>z__ReadOnlyArray<LocString>(array);
		}

		// Token: 0x04002469 RID: 9321
		private const int _maxHpLoss = 5;
	}
}
