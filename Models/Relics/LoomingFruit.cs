using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Saves;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200052E RID: 1326
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LoomingFruit : RelicModel
	{
		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06004CDC RID: 19676 RVA: 0x0021664F File Offset: 0x0021484F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06004CDD RID: 19677 RVA: 0x00216652 File Offset: 0x00214852
		protected override string IconBaseName
		{
			get
			{
				if (!LoomingFruit.HasCornucopia())
				{
					return base.IconBaseName + "_2";
				}
				return base.IconBaseName;
			}
		}

		// Token: 0x06004CDE RID: 19678 RVA: 0x00216674 File Offset: 0x00214874
		private static bool HasCornucopia()
		{
			string uniqueId = SaveManager.Instance.Progress.UniqueId;
			if (string.IsNullOrEmpty(uniqueId))
			{
				return false;
			}
			string text = uniqueId;
			return text[text.Length - 1] % '\u0002' == '\0';
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06004CDF RID: 19679 RVA: 0x002166AE File Offset: 0x002148AE
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06004CE0 RID: 19680 RVA: 0x002166B1 File Offset: 0x002148B1
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new MaxHpVar(31m));
			}
		}

		// Token: 0x06004CE1 RID: 19681 RVA: 0x002166C4 File Offset: 0x002148C4
		public override async Task AfterObtained()
		{
			await CreatureCmd.GainMaxHp(base.Owner.Creature, base.DynamicVars.MaxHp.BaseValue);
		}
	}
}
