using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Platform;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200064E RID: 1614
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class KnockdownPower : PowerModel
	{
		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x060053A4 RID: 21412 RVA: 0x002233A8 File Offset: 0x002215A8
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x060053A5 RID: 21413 RVA: 0x002233AB File Offset: 0x002215AB
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x060053A6 RID: 21414 RVA: 0x002233AE File Offset: 0x002215AE
		public override bool IsInstanced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x060053A7 RID: 21415 RVA: 0x002233B1 File Offset: 0x002215B1
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new StringVar("Applier", ""));
			}
		}

		// Token: 0x060053A8 RID: 21416 RVA: 0x002233C8 File Offset: 0x002215C8
		[NullableContext(2)]
		[return: Nullable(1)]
		public override Task AfterApplied(Creature applier, CardModel cardSource)
		{
			((StringVar)base.DynamicVars["Applier"]).StringValue = PlatformUtil.GetPlayerName(RunManager.Instance.NetService.Platform, base.Applier.Player.NetId);
			return Task.CompletedTask;
		}

		// Token: 0x060053A9 RID: 21417 RVA: 0x00223418 File Offset: 0x00221618
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
			if (dealer == base.Applier)
			{
				return 1m;
			}
			return base.Amount;
		}

		// Token: 0x060053AA RID: 21418 RVA: 0x00223454 File Offset: 0x00221654
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				await PowerCmd.Remove(this);
			}
		}

		// Token: 0x0400225B RID: 8795
		private const string _applierTag = "Applier";
	}
}
