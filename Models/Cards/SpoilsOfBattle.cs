using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A67 RID: 2663
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SpoilsOfBattle : CardModel
	{
		// Token: 0x060070B9 RID: 28857 RVA: 0x00267BF7 File Offset: 0x00265DF7
		public SpoilsOfBattle()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001EC8 RID: 7880
		// (get) Token: 0x060070BA RID: 28858 RVA: 0x00267C04 File Offset: 0x00265E04
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new ForgeVar(10));
			}
		}

		// Token: 0x17001EC9 RID: 7881
		// (get) Token: 0x060070BB RID: 28859 RVA: 0x00267C12 File Offset: 0x00265E12
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromForge();
			}
		}

		// Token: 0x060070BC RID: 28860 RVA: 0x00267C1C File Offset: 0x00265E1C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await ForgeCmd.Forge(base.DynamicVars.Forge.IntValue, base.Owner, this);
		}

		// Token: 0x060070BD RID: 28861 RVA: 0x00267C5F File Offset: 0x00265E5F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Forge.UpgradeValueBy(5m);
		}
	}
}
