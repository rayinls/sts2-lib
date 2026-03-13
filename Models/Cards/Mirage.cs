using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009D0 RID: 2512
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Mirage : CardModel
	{
		// Token: 0x06006D89 RID: 28041 RVA: 0x00261683 File Offset: 0x0025F883
		public Mirage()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001D77 RID: 7543
		// (get) Token: 0x06006D8A RID: 28042 RVA: 0x00261690 File Offset: 0x0025F890
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001D78 RID: 7544
		// (get) Token: 0x06006D8B RID: 28043 RVA: 0x00261694 File Offset: 0x0025F894
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(0m);
				array[1] = new CalculationExtraVar(1m);
				array[2] = new CalculatedBlockVar(ValueProp.Move).WithMultiplier(delegate(CardModel card, [Nullable(2)] Creature _)
				{
					CombatState combatState = card.CombatState;
					int num;
					if (combatState == null)
					{
						num = 0;
					}
					else
					{
						num = combatState.Enemies.Where((Creature c) => c.IsAlive).Sum((Creature c) => c.GetPowerAmount<PoisonPower>());
					}
					return num;
				});
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x17001D79 RID: 7545
		// (get) Token: 0x06006D8C RID: 28044 RVA: 0x002616F3 File Offset: 0x0025F8F3
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<PoisonPower>());
			}
		}

		// Token: 0x17001D7A RID: 7546
		// (get) Token: 0x06006D8D RID: 28045 RVA: 0x002616FF File Offset: 0x0025F8FF
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x06006D8E RID: 28046 RVA: 0x00261708 File Offset: 0x0025F908
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.CalculatedBlock.Calculate(cardPlay.Target), base.DynamicVars.CalculatedBlock.Props, cardPlay, false);
		}

		// Token: 0x06006D8F RID: 28047 RVA: 0x00261753 File Offset: 0x0025F953
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
