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
	// Token: 0x0200093A RID: 2362
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Equilibrium : CardModel
	{
		// Token: 0x06006A4E RID: 27214 RVA: 0x0025AC8B File Offset: 0x00258E8B
		public Equilibrium()
			: base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001C1D RID: 7197
		// (get) Token: 0x06006A4F RID: 27215 RVA: 0x0025AC98 File Offset: 0x00258E98
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C1E RID: 7198
		// (get) Token: 0x06006A50 RID: 27216 RVA: 0x0025AC9B File Offset: 0x00258E9B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(13m, ValueProp.Move),
					new DynamicVar("Equilibrium", 1m)
				});
			}
		}

		// Token: 0x17001C1F RID: 7199
		// (get) Token: 0x06006A51 RID: 27217 RVA: 0x0025ACCA File Offset: 0x00258ECA
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Retain));
			}
		}

		// Token: 0x06006A52 RID: 27218 RVA: 0x0025ACD8 File Offset: 0x00258ED8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await PowerCmd.Apply<RetainHandPower>(base.Owner.Creature, base.DynamicVars["Equilibrium"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006A53 RID: 27219 RVA: 0x0025AD23 File Offset: 0x00258F23
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}

		// Token: 0x04002576 RID: 9590
		private const string _equilibriumKey = "Equilibrium";
	}
}
