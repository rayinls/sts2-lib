using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006D3 RID: 1747
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WeakPower : PowerModel
	{
		// Token: 0x1700135C RID: 4956
		// (get) Token: 0x060056D6 RID: 22230 RVA: 0x00229301 File Offset: 0x00227501
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x1700135D RID: 4957
		// (get) Token: 0x060056D7 RID: 22231 RVA: 0x00229304 File Offset: 0x00227504
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700135E RID: 4958
		// (get) Token: 0x060056D8 RID: 22232 RVA: 0x00229307 File Offset: 0x00227507
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("DamageDecrease", 0.75m));
			}
		}

		// Token: 0x060056D9 RID: 22233 RVA: 0x00229324 File Offset: 0x00227524
		[NullableContext(2)]
		public override decimal ModifyDamageMultiplicative(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (dealer != base.Owner)
			{
				return 1m;
			}
			if (!props.IsPoweredAttack())
			{
				return 1m;
			}
			decimal num = base.DynamicVars["DamageDecrease"].BaseValue;
			PaperKrane paperKrane;
			if (target == null)
			{
				paperKrane = null;
			}
			else
			{
				Player player = target.Player;
				paperKrane = ((player != null) ? player.GetRelic<PaperKrane>() : null);
			}
			PaperKrane paperKrane2 = paperKrane;
			if (paperKrane2 != null)
			{
				num = paperKrane2.ModifyWeakMultiplier(target, num, props, dealer, cardSource);
			}
			DebilitatePower power = dealer.GetPower<DebilitatePower>();
			if (power != null)
			{
				num = power.ModifyWeakMultiplier(dealer, num, props, dealer, cardSource);
			}
			return num;
		}

		// Token: 0x060056DA RID: 22234 RVA: 0x002293AC File Offset: 0x002275AC
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == CombatSide.Enemy)
			{
				await PowerCmd.TickDownDuration(this);
			}
		}

		// Token: 0x0400227C RID: 8828
		private const string _damageDecrease = "DamageDecrease";
	}
}
