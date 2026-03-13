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
	// Token: 0x020006D1 RID: 1745
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class VulnerablePower : PowerModel
	{
		// Token: 0x17001356 RID: 4950
		// (get) Token: 0x060056CB RID: 22219 RVA: 0x002291B2 File Offset: 0x002273B2
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x17001357 RID: 4951
		// (get) Token: 0x060056CC RID: 22220 RVA: 0x002291B5 File Offset: 0x002273B5
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001358 RID: 4952
		// (get) Token: 0x060056CD RID: 22221 RVA: 0x002291B8 File Offset: 0x002273B8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("DamageIncrease", 1.5m));
			}
		}

		// Token: 0x060056CE RID: 22222 RVA: 0x002291D4 File Offset: 0x002273D4
		[NullableContext(2)]
		public override decimal ModifyDamageMultiplicative(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (target != base.Owner)
			{
				return 1m;
			}
			if (!props.IsPoweredAttack())
			{
				return 1m;
			}
			decimal num = base.DynamicVars["DamageIncrease"].BaseValue;
			if (dealer != null)
			{
				Player player = dealer.Player;
				PaperPhrog paperPhrog = ((player != null) ? player.GetRelic<PaperPhrog>() : null);
				if (paperPhrog != null)
				{
					num = paperPhrog.ModifyVulnerableMultiplier(target, num, props, dealer, cardSource);
				}
				CrueltyPower power = dealer.GetPower<CrueltyPower>();
				if (power != null)
				{
					num = power.ModifyVulnerableMultiplier(target, num, props, dealer, cardSource);
				}
			}
			DebilitatePower power2 = target.GetPower<DebilitatePower>();
			if (power2 != null)
			{
				num = power2.ModifyVulnerableMultiplier(target, num, props, dealer, cardSource);
			}
			return num;
		}

		// Token: 0x060056CF RID: 22223 RVA: 0x00229270 File Offset: 0x00227470
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == CombatSide.Enemy)
			{
				await PowerCmd.TickDownDuration(this);
			}
		}

		// Token: 0x0400227B RID: 8827
		private const string _damageIncrease = "DamageIncrease";
	}
}
