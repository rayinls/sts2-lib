using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004B4 RID: 1204
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BeltBuckle : RelicModel
	{
		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x060049D4 RID: 18900 RVA: 0x00210C73 File Offset: 0x0020EE73
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x060049D5 RID: 18901 RVA: 0x00210C76 File Offset: 0x0020EE76
		// (set) Token: 0x060049D6 RID: 18902 RVA: 0x00210C7E File Offset: 0x0020EE7E
		private bool DexterityApplied
		{
			get
			{
				return this._dexterityApplied;
			}
			set
			{
				base.AssertMutable();
				this._dexterityApplied = value;
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x060049D7 RID: 18903 RVA: 0x00210C8D File Offset: 0x0020EE8D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<DexterityPower>(2m));
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x060049D8 RID: 18904 RVA: 0x00210C9F File Offset: 0x0020EE9F
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DexterityPower>());
			}
		}

		// Token: 0x060049D9 RID: 18905 RVA: 0x00210CAC File Offset: 0x0020EEAC
		public override async Task AfterObtained()
		{
			if (CombatManager.Instance.IsInProgress)
			{
				if (!base.Owner.Potions.Any<PotionModel>())
				{
					await this.ApplyDexterity();
				}
			}
		}

		// Token: 0x060049DA RID: 18906 RVA: 0x00210CF0 File Offset: 0x0020EEF0
		public override async Task BeforeCombatStart()
		{
			this.DexterityApplied = false;
			this.RefreshStatus();
			if (!base.Owner.Potions.Any<PotionModel>())
			{
				await this.ApplyDexterity();
			}
		}

		// Token: 0x060049DB RID: 18907 RVA: 0x00210D33 File Offset: 0x0020EF33
		public override Task AfterCombatEnd(CombatRoom room)
		{
			this.RefreshStatus();
			return Task.CompletedTask;
		}

		// Token: 0x060049DC RID: 18908 RVA: 0x00210D40 File Offset: 0x0020EF40
		public override async Task AfterPotionProcured(PotionModel potion)
		{
			this.RefreshStatus();
			if (CombatManager.Instance.IsInProgress)
			{
				await this.RemoveDexterity();
			}
		}

		// Token: 0x060049DD RID: 18909 RVA: 0x00210D84 File Offset: 0x0020EF84
		public override async Task AfterPotionDiscarded(PotionModel potion)
		{
			this.RefreshStatus();
			if (CombatManager.Instance.IsInProgress)
			{
				if (!base.Owner.Potions.Any<PotionModel>())
				{
					await this.ApplyDexterity();
				}
			}
		}

		// Token: 0x060049DE RID: 18910 RVA: 0x00210DC8 File Offset: 0x0020EFC8
		public override async Task AfterPotionUsed(PotionModel potion, [Nullable(2)] Creature target)
		{
			this.RefreshStatus();
			if (CombatManager.Instance.IsInProgress)
			{
				if (!base.Owner.Potions.Any<PotionModel>())
				{
					await this.ApplyDexterity();
				}
			}
		}

		// Token: 0x060049DF RID: 18911 RVA: 0x00210E0B File Offset: 0x0020F00B
		public override Task AfterCombatVictory(CombatRoom room)
		{
			this.DexterityApplied = false;
			this.RefreshStatus();
			return Task.CompletedTask;
		}

		// Token: 0x060049E0 RID: 18912 RVA: 0x00210E20 File Offset: 0x0020F020
		private async Task ApplyDexterity()
		{
			if (!this.DexterityApplied)
			{
				this.DexterityApplied = true;
				base.Flash();
				await PowerCmd.Apply<DexterityPower>(base.Owner.Creature, base.DynamicVars.Dexterity.BaseValue, null, null, false);
			}
		}

		// Token: 0x060049E1 RID: 18913 RVA: 0x00210E64 File Offset: 0x0020F064
		private async Task RemoveDexterity()
		{
			if (this.DexterityApplied)
			{
				this.DexterityApplied = false;
				base.Flash();
				await PowerCmd.Apply<DexterityPower>(base.Owner.Creature, -base.DynamicVars.Dexterity.BaseValue, null, null, false);
			}
		}

		// Token: 0x060049E2 RID: 18914 RVA: 0x00210EA7 File Offset: 0x0020F0A7
		private void RefreshStatus()
		{
			if (CombatManager.Instance.IsInProgress && !base.Owner.Potions.Any<PotionModel>())
			{
				base.Status = RelicStatus.Active;
				return;
			}
			base.Status = RelicStatus.Normal;
		}

		// Token: 0x0400218A RID: 8586
		private bool _dexterityApplied;
	}
}
