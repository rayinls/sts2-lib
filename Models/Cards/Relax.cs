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
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A25 RID: 2597
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Relax : CardModel
	{
		// Token: 0x06006F3F RID: 28479 RVA: 0x00264DB7 File Offset: 0x00262FB7
		public Relax()
			: base(3, CardType.Skill, CardRarity.Ancient, TargetType.Self, true)
		{
		}

		// Token: 0x17001E29 RID: 7721
		// (get) Token: 0x06006F40 RID: 28480 RVA: 0x00264DC4 File Offset: 0x00262FC4
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001E2A RID: 7722
		// (get) Token: 0x06006F41 RID: 28481 RVA: 0x00264DC7 File Offset: 0x00262FC7
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(15m, ValueProp.Move),
					new CardsVar(2),
					new EnergyVar(2)
				});
			}
		}

		// Token: 0x17001E2B RID: 7723
		// (get) Token: 0x06006F42 RID: 28482 RVA: 0x00264DF6 File Offset: 0x00262FF6
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x17001E2C RID: 7724
		// (get) Token: 0x06006F43 RID: 28483 RVA: 0x00264E03 File Offset: 0x00263003
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x06006F44 RID: 28484 RVA: 0x00264E0C File Offset: 0x0026300C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await PowerCmd.Apply<DrawCardsNextTurnPower>(base.Owner.Creature, base.DynamicVars.Cards.BaseValue, base.Owner.Creature, this, false);
			await PowerCmd.Apply<EnergyNextTurnPower>(base.Owner.Creature, base.DynamicVars.Energy.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006F45 RID: 28485 RVA: 0x00264E58 File Offset: 0x00263058
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(2m);
			base.DynamicVars.Cards.UpgradeValueBy(1m);
			base.DynamicVars.Energy.UpgradeValueBy(1m);
		}
	}
}
