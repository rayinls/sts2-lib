using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009DE RID: 2526
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NeutronAegis : CardModel
	{
		// Token: 0x06006DD2 RID: 28114 RVA: 0x00261FA7 File Offset: 0x002601A7
		public NeutronAegis()
			: base(1, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001D96 RID: 7574
		// (get) Token: 0x06006DD3 RID: 28115 RVA: 0x00261FB4 File Offset: 0x002601B4
		public override int CanonicalStarCost
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17001D97 RID: 7575
		// (get) Token: 0x06006DD4 RID: 28116 RVA: 0x00261FB7 File Offset: 0x002601B7
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<PlatingPower>());
			}
		}

		// Token: 0x17001D98 RID: 7576
		// (get) Token: 0x06006DD5 RID: 28117 RVA: 0x00261FC3 File Offset: 0x002601C3
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<PlatingPower>(8m));
			}
		}

		// Token: 0x06006DD6 RID: 28118 RVA: 0x00261FD8 File Offset: 0x002601D8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<PlatingPower>(base.Owner.Creature, base.DynamicVars["PlatingPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006DD7 RID: 28119 RVA: 0x0026201B File Offset: 0x0026021B
		protected override void OnUpgrade()
		{
			base.DynamicVars["PlatingPower"].UpgradeValueBy(3m);
		}
	}
}
