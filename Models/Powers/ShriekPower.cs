using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000693 RID: 1683
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ShriekPower : PowerModel
	{
		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x0600553A RID: 21818 RVA: 0x00226197 File Offset: 0x00224397
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x0600553B RID: 21819 RVA: 0x0022619A File Offset: 0x0022439A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001293 RID: 4755
		// (get) Token: 0x0600553C RID: 21820 RVA: 0x0022619D File Offset: 0x0022439D
		public override bool AllowNegative
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x0600553D RID: 21821 RVA: 0x002261A0 File Offset: 0x002243A0
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Stun, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x0600553E RID: 21822 RVA: 0x002261B2 File Offset: 0x002243B2
		public override bool ShouldScaleInMultiplayer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600553F RID: 21823 RVA: 0x002261B8 File Offset: 0x002243B8
		public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			if (target == base.Owner)
			{
				if (result.UnblockedDamage > 0)
				{
					if (target.CurrentHp <= base.Amount)
					{
						base.Flash();
						await CreatureCmd.Stun(base.Owner, ((TerrorEel)base.Owner.Monster).TerrorState.StateId);
						await PowerCmd.Remove(this);
					}
				}
			}
		}
	}
}
