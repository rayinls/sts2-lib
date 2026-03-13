using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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
	// Token: 0x02000634 RID: 1588
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GuardedPower : PowerModel
	{
		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x0600530A RID: 21258 RVA: 0x00222257 File Offset: 0x00220457
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x0600530B RID: 21259 RVA: 0x0022225A File Offset: 0x0022045A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x0600530C RID: 21260 RVA: 0x0022225D File Offset: 0x0022045D
		public override bool IsInstanced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x0600530D RID: 21261 RVA: 0x00222260 File Offset: 0x00220460
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new StringVar("Applier", ""));
			}
		}

		// Token: 0x0600530E RID: 21262 RVA: 0x00222278 File Offset: 0x00220478
		[NullableContext(2)]
		[return: Nullable(1)]
		public override Task AfterApplied(Creature applier, CardModel cardSource)
		{
			((StringVar)base.DynamicVars["Applier"]).StringValue = PlatformUtil.GetPlayerName(RunManager.Instance.NetService.Platform, base.Applier.Player.NetId);
			return Task.CompletedTask;
		}

		// Token: 0x0600530F RID: 21263 RVA: 0x002222C8 File Offset: 0x002204C8
		public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (!wasRemovalPrevented)
			{
				if (creature == base.Applier)
				{
					await PowerCmd.Remove(this);
				}
			}
		}

		// Token: 0x06005310 RID: 21264 RVA: 0x0022231B File Offset: 0x0022051B
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
			return 0.5m;
		}

		// Token: 0x04002252 RID: 8786
		private const string _applierTag = "Applier";
	}
}
