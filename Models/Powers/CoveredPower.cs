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
	// Token: 0x020005F9 RID: 1529
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CoveredPower : PowerModel
	{
		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x060051C2 RID: 20930 RVA: 0x0021FE7B File Offset: 0x0021E07B
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x060051C3 RID: 20931 RVA: 0x0021FE7E File Offset: 0x0021E07E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x060051C4 RID: 20932 RVA: 0x0021FE81 File Offset: 0x0021E081
		public override bool IsInstanced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x060051C5 RID: 20933 RVA: 0x0021FE84 File Offset: 0x0021E084
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new StringVar("Applier", ""));
			}
		}

		// Token: 0x060051C6 RID: 20934 RVA: 0x0021FE9C File Offset: 0x0021E09C
		[NullableContext(2)]
		[return: Nullable(1)]
		public override async Task AfterApplied(Creature applier, CardModel cardSource)
		{
			((StringVar)base.DynamicVars["Applier"]).StringValue = PlatformUtil.GetPlayerName(RunManager.Instance.NetService.Platform, base.Applier.Player.NetId);
			InterceptPower interceptPower = base.Applier.GetPower<InterceptPower>();
			if (interceptPower == null)
			{
				InterceptPower interceptPower2 = await PowerCmd.Apply<InterceptPower>(base.Applier, 1m, base.Owner, null, false);
				interceptPower = interceptPower2;
			}
			interceptPower.AddCoveredCreature(base.Owner);
		}

		// Token: 0x060051C7 RID: 20935 RVA: 0x0021FEE0 File Offset: 0x0021E0E0
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

		// Token: 0x060051C8 RID: 20936 RVA: 0x0021FF33 File Offset: 0x0021E133
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
			return 0m;
		}

		// Token: 0x060051C9 RID: 20937 RVA: 0x0021FF58 File Offset: 0x0021E158
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == CombatSide.Enemy)
			{
				await PowerCmd.Remove(this);
			}
		}

		// Token: 0x0400224E RID: 8782
		private const string _applierTag = "Applier";
	}
}
