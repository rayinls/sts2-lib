using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200067E RID: 1662
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ReaperFormPower : PowerModel
	{
		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x060054BA RID: 21690 RVA: 0x0022515B File Offset: 0x0022335B
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x060054BB RID: 21691 RVA: 0x0022515E File Offset: 0x0022335E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x060054BC RID: 21692 RVA: 0x00225161 File Offset: 0x00223361
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DoomPower>());
			}
		}

		// Token: 0x060054BD RID: 21693 RVA: 0x00225170 File Offset: 0x00223370
		public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, [Nullable(2)] Creature dealer, DamageResult result, ValueProp props, Creature target, [Nullable(2)] CardModel cardSource)
		{
			if (dealer != null)
			{
				if (dealer != base.Owner)
				{
					Player petOwner = dealer.PetOwner;
					if (((petOwner != null) ? petOwner.Creature : null) != base.Owner)
					{
						return;
					}
				}
				if (props.IsPoweredAttack())
				{
					if (result.TotalDamage > 0)
					{
						await PowerCmd.Apply<DoomPower>(target, result.TotalDamage * base.Amount, base.Owner, null, false);
					}
				}
			}
		}
	}
}
