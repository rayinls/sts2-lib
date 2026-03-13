using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004CD RID: 1229
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CallingBell : RelicModel
	{
		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x06004A7F RID: 19071 RVA: 0x00212130 File Offset: 0x00210330
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x06004A80 RID: 19072 RVA: 0x00212133 File Offset: 0x00210333
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x06004A81 RID: 19073 RVA: 0x00212136 File Offset: 0x00210336
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Relics", 3m));
			}
		}

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x06004A82 RID: 19074 RVA: 0x0021214D File Offset: 0x0021034D
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromCardWithCardHoverTips<CurseOfTheBell>(false);
			}
		}

		// Token: 0x06004A83 RID: 19075 RVA: 0x00212158 File Offset: 0x00210358
		public override async Task AfterObtained()
		{
			await CardPileCmd.AddCurseToDeck<CurseOfTheBell>(base.Owner);
			await Cmd.Wait(0.75f, false);
			await RewardsCmd.OfferCustom(base.Owner, this.GenerateRewards());
		}

		// Token: 0x06004A84 RID: 19076 RVA: 0x0021219C File Offset: 0x0021039C
		private unsafe List<Reward> GenerateRewards()
		{
			int num;
			Span<Reward> span;
			int num2;
			if (TestMode.IsOn)
			{
				num = 3;
				List<Reward> list = new List<Reward>(num);
				CollectionsMarshal.SetCount<Reward>(list, num);
				span = CollectionsMarshal.AsSpan<Reward>(list);
				num2 = 0;
				*span[num2] = new RelicReward(ModelDb.Relic<Anchor>().ToMutable(), base.Owner);
				num2++;
				*span[num2] = new RelicReward(ModelDb.Relic<GremlinHorn>().ToMutable(), base.Owner);
				num2++;
				*span[num2] = new RelicReward(ModelDb.Relic<MummifiedHand>().ToMutable(), base.Owner);
				return list;
			}
			num2 = 3;
			List<Reward> list2 = new List<Reward>(num2);
			CollectionsMarshal.SetCount<Reward>(list2, num2);
			span = CollectionsMarshal.AsSpan<Reward>(list2);
			num = 0;
			*span[num] = new RelicReward(RelicRarity.Common, base.Owner);
			num++;
			*span[num] = new RelicReward(RelicRarity.Uncommon, base.Owner);
			num++;
			*span[num] = new RelicReward(RelicRarity.Rare, base.Owner);
			return list2;
		}

		// Token: 0x0400219A RID: 8602
		private const string _relicsKey = "Relics";
	}
}
