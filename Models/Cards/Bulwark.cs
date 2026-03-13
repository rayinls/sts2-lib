using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008C8 RID: 2248
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Bulwark : CardModel
	{
		// Token: 0x06006802 RID: 26626 RVA: 0x0025694E File Offset: 0x00254B4E
		public Bulwark()
			: base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001B1B RID: 6939
		// (get) Token: 0x06006803 RID: 26627 RVA: 0x0025695B File Offset: 0x00254B5B
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B1C RID: 6940
		// (get) Token: 0x06006804 RID: 26628 RVA: 0x0025695E File Offset: 0x00254B5E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(13m, ValueProp.Move),
					new ForgeVar(10)
				});
			}
		}

		// Token: 0x17001B1D RID: 6941
		// (get) Token: 0x06006805 RID: 26629 RVA: 0x00256985 File Offset: 0x00254B85
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromForge();
			}
		}

		// Token: 0x06006806 RID: 26630 RVA: 0x0025698C File Offset: 0x00254B8C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await ForgeCmd.Forge(base.DynamicVars.Forge.IntValue, base.Owner, this);
		}

		// Token: 0x06006807 RID: 26631 RVA: 0x002569D7 File Offset: 0x00254BD7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
			base.DynamicVars.Forge.UpgradeValueBy(3m);
		}
	}
}
