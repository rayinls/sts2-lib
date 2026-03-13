using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000649 RID: 1609
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class IntangiblePower : PowerModel
	{
		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x06005382 RID: 21378 RVA: 0x00222F0F File Offset: 0x0022110F
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x06005383 RID: 21379 RVA: 0x00222F12 File Offset: 0x00221112
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005384 RID: 21380 RVA: 0x00222F15 File Offset: 0x00221115
		[NullableContext(2)]
		public override decimal ModifyHpLostAfterOsty([Nullable(1)] Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (!CombatManager.Instance.IsInProgress)
			{
				return amount;
			}
			if (target != base.Owner)
			{
				return amount;
			}
			return Math.Min(this.GetDamageCap(dealer), amount);
		}

		// Token: 0x06005385 RID: 21381 RVA: 0x00222F43 File Offset: 0x00221143
		public override Task AfterModifyingHpLostAfterOsty()
		{
			base.Flash();
			return Task.CompletedTask;
		}

		// Token: 0x06005386 RID: 21382 RVA: 0x00222F50 File Offset: 0x00221150
		[NullableContext(2)]
		public override decimal ModifyDamageCap(Creature target, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (target != base.Owner)
			{
				return decimal.MaxValue;
			}
			return this.GetDamageCap(dealer);
		}

		// Token: 0x06005387 RID: 21383 RVA: 0x00222F72 File Offset: 0x00221172
		public override Task AfterModifyingDamageAmount([Nullable(2)] CardModel cardSource)
		{
			base.Flash();
			return Task.CompletedTask;
		}

		// Token: 0x06005388 RID: 21384 RVA: 0x00222F80 File Offset: 0x00221180
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == CombatSide.Enemy)
			{
				await PowerCmd.TickDownDuration(this);
			}
		}

		// Token: 0x06005389 RID: 21385 RVA: 0x00222FCC File Offset: 0x002211CC
		[NullableContext(2)]
		private int GetDamageCap(Creature dealer)
		{
			Player player = ((dealer != null) ? dealer.Player : null) ?? ((dealer != null) ? dealer.PetOwner : null);
			if (player != null)
			{
				if (player.Relics.Any((RelicModel r) => r is TheBoot))
				{
					return 5;
				}
			}
			return 1;
		}
	}
}
