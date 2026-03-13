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
	// Token: 0x020008DC RID: 2268
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ChargeBattery : CardModel
	{
		// Token: 0x0600685C RID: 26716 RVA: 0x00257367 File Offset: 0x00255567
		public ChargeBattery()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001B3D RID: 6973
		// (get) Token: 0x0600685D RID: 26717 RVA: 0x00257374 File Offset: 0x00255574
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B3E RID: 6974
		// (get) Token: 0x0600685E RID: 26718 RVA: 0x00257377 File Offset: 0x00255577
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(7m, ValueProp.Move),
					new EnergyVar(1)
				});
			}
		}

		// Token: 0x17001B3F RID: 6975
		// (get) Token: 0x0600685F RID: 26719 RVA: 0x0025739C File Offset: 0x0025559C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x06006860 RID: 26720 RVA: 0x002573AC File Offset: 0x002555AC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await PowerCmd.Apply<EnergyNextTurnPower>(base.Owner.Creature, base.DynamicVars.Energy.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006861 RID: 26721 RVA: 0x002573F7 File Offset: 0x002555F7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}
