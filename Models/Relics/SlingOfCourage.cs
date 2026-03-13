using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000591 RID: 1425
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SlingOfCourage : RelicModel
	{
		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x06004F63 RID: 20323 RVA: 0x0021B098 File Offset: 0x00219298
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x06004F64 RID: 20324 RVA: 0x0021B09B File Offset: 0x0021929B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<StrengthPower>(2m));
			}
		}

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x06004F65 RID: 20325 RVA: 0x0021B0AD File Offset: 0x002192AD
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x06004F66 RID: 20326 RVA: 0x0021B0BC File Offset: 0x002192BC
		public override async Task AfterRoomEntered(AbstractRoom room)
		{
			if (room.RoomType == RoomType.Elite)
			{
				base.Flash();
				await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, base.DynamicVars.Strength.BaseValue, base.Owner.Creature, null, false);
			}
		}
	}
}
