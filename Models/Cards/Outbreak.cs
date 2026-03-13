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
	// Token: 0x020009EA RID: 2538
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Outbreak : CardModel
	{
		// Token: 0x06006E10 RID: 28176 RVA: 0x00262761 File Offset: 0x00260961
		public Outbreak()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001DB0 RID: 7600
		// (get) Token: 0x06006E11 RID: 28177 RVA: 0x0026276E File Offset: 0x0026096E
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<PoisonPower>());
			}
		}

		// Token: 0x17001DB1 RID: 7601
		// (get) Token: 0x06006E12 RID: 28178 RVA: 0x0026277A File Offset: 0x0026097A
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<OutbreakPower>(11m),
					new RepeatVar(3)
				});
			}
		}

		// Token: 0x06006E13 RID: 28179 RVA: 0x002627A0 File Offset: 0x002609A0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<OutbreakPower>(base.Owner.Creature, base.DynamicVars["OutbreakPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006E14 RID: 28180 RVA: 0x002627E3 File Offset: 0x002609E3
		protected override void OnUpgrade()
		{
			base.DynamicVars["OutbreakPower"].UpgradeValueBy(4m);
		}
	}
}
