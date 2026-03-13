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
	// Token: 0x02000622 RID: 1570
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FlankingPower : PowerModel
	{
		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x060052AA RID: 21162 RVA: 0x0022173B File Offset: 0x0021F93B
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x060052AB RID: 21163 RVA: 0x0022173E File Offset: 0x0021F93E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x060052AC RID: 21164 RVA: 0x00221741 File Offset: 0x0021F941
		public override bool IsInstanced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x060052AD RID: 21165 RVA: 0x00221744 File Offset: 0x0021F944
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new StringVar("Applier", ""));
			}
		}

		// Token: 0x060052AE RID: 21166 RVA: 0x0022175C File Offset: 0x0021F95C
		[NullableContext(2)]
		[return: Nullable(1)]
		public override Task AfterApplied(Creature applier, CardModel cardSource)
		{
			((StringVar)base.DynamicVars["Applier"]).StringValue = PlatformUtil.GetPlayerName(RunManager.Instance.NetService.Platform, base.Applier.Player.NetId);
			return Task.CompletedTask;
		}

		// Token: 0x060052AF RID: 21167 RVA: 0x002217AC File Offset: 0x0021F9AC
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

		// Token: 0x060052B0 RID: 21168 RVA: 0x002217E8 File Offset: 0x0021F9E8
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				await PowerCmd.Remove(this);
			}
		}

		// Token: 0x04002250 RID: 8784
		private const string _applierTagKey = "Applier";
	}
}
