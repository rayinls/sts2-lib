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
	// Token: 0x020008C2 RID: 2242
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BrightestFlame : CardModel
	{
		// Token: 0x060067E5 RID: 26597 RVA: 0x00256523 File Offset: 0x00254723
		public BrightestFlame()
			: base(0, CardType.Skill, CardRarity.Ancient, TargetType.Self, true)
		{
		}

		// Token: 0x17001B10 RID: 6928
		// (get) Token: 0x060067E6 RID: 26598 RVA: 0x00256530 File Offset: 0x00254730
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new MaxHpVar(1m),
					new EnergyVar(2),
					new CardsVar(2)
				});
			}
		}

		// Token: 0x17001B11 RID: 6929
		// (get) Token: 0x060067E7 RID: 26599 RVA: 0x0025655C File Offset: 0x0025475C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x060067E8 RID: 26600 RVA: 0x0025656C File Offset: 0x0025476C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
			await CreatureCmd.LoseMaxHp(choiceContext, base.Owner.Creature, base.DynamicVars.MaxHp.BaseValue, true);
		}

		// Token: 0x060067E9 RID: 26601 RVA: 0x002565B7 File Offset: 0x002547B7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Energy.UpgradeValueBy(1m);
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
