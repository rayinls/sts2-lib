using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004C4 RID: 1220
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BoundPhylactery : RelicModel
	{
		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06004A3B RID: 19003 RVA: 0x0021191D File Offset: 0x0020FB1D
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Starter;
			}
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06004A3C RID: 19004 RVA: 0x00211920 File Offset: 0x0020FB20
		public override bool SpawnsPets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06004A3D RID: 19005 RVA: 0x00211923 File Offset: 0x0020FB23
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new SummonVar(1m));
			}
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06004A3E RID: 19006 RVA: 0x00211934 File Offset: 0x0020FB34
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.SummonDynamic, new DynamicVar[] { base.DynamicVars.Summon }));
			}
		}

		// Token: 0x06004A3F RID: 19007 RVA: 0x00211958 File Offset: 0x0020FB58
		public override async Task BeforeCombatStart()
		{
			await this.SummonPet();
		}

		// Token: 0x06004A40 RID: 19008 RVA: 0x0021199C File Offset: 0x0020FB9C
		public override async Task AfterEnergyResetLate(Player player)
		{
			if (player == base.Owner)
			{
				if (player.Creature.CombatState.RoundNumber != 1)
				{
					await this.SummonPet();
				}
			}
		}

		// Token: 0x06004A41 RID: 19009 RVA: 0x002119E8 File Offset: 0x0020FBE8
		private async Task SummonPet()
		{
			await OstyCmd.Summon(new ThrowingPlayerChoiceContext(), base.Owner, base.DynamicVars.Summon.BaseValue, this);
		}
	}
}
