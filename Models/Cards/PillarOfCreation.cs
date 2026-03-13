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
	// Token: 0x020009FC RID: 2556
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PillarOfCreation : CardModel
	{
		// Token: 0x06006E6C RID: 28268 RVA: 0x002632CF File Offset: 0x002614CF
		public PillarOfCreation()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001DD5 RID: 7637
		// (get) Token: 0x06006E6D RID: 28269 RVA: 0x002632DC File Offset: 0x002614DC
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x17001DD6 RID: 7638
		// (get) Token: 0x06006E6E RID: 28270 RVA: 0x002632EE File Offset: 0x002614EE
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(3m, ValueProp.Unpowered));
			}
		}

		// Token: 0x06006E6F RID: 28271 RVA: 0x00263304 File Offset: 0x00261504
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<PillarOfCreationPower>(base.Owner.Creature, base.DynamicVars.Block.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006E70 RID: 28272 RVA: 0x00263347 File Offset: 0x00261547
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(1m);
		}
	}
}
