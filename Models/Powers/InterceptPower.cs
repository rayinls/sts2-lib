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
	// Token: 0x0200064A RID: 1610
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class InterceptPower : PowerModel
	{
		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x0600538B RID: 21387 RVA: 0x00223030 File Offset: 0x00221230
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x0600538C RID: 21388 RVA: 0x00223033 File Offset: 0x00221233
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x0600538D RID: 21389 RVA: 0x00223036 File Offset: 0x00221236
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new StringVar("Covering", ""));
			}
		}

		// Token: 0x0600538E RID: 21390 RVA: 0x0022304C File Offset: 0x0022124C
		protected override object InitInternalData()
		{
			return new InterceptPower.Data();
		}

		// Token: 0x0600538F RID: 21391 RVA: 0x00223054 File Offset: 0x00221254
		public void AddCoveredCreature(Creature c)
		{
			List<Creature> coveredCreatures = base.GetInternalData<InterceptPower.Data>().coveredCreatures;
			if (!base.GetInternalData<InterceptPower.Data>().coveredCreatures.Contains(c))
			{
				coveredCreatures.Add(c);
			}
			StringVar stringVar = (StringVar)base.DynamicVars["Covering"];
			stringVar.StringValue = "";
			for (int i = 0; i < coveredCreatures.Count; i++)
			{
				StringVar stringVar2 = stringVar;
				stringVar2.StringValue += PlatformUtil.GetPlayerName(RunManager.Instance.NetService.Platform, coveredCreatures[i].Player.NetId);
				if (i == coveredCreatures.Count - 2)
				{
					StringVar stringVar3 = stringVar;
					stringVar3.StringValue += ", and ";
				}
				else if (i < coveredCreatures.Count - 2)
				{
					StringVar stringVar4 = stringVar;
					stringVar4.StringValue += ", ";
				}
			}
		}

		// Token: 0x06005390 RID: 21392 RVA: 0x00223136 File Offset: 0x00221336
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
			return base.GetInternalData<InterceptPower.Data>().coveredCreatures.Count + 1;
		}

		// Token: 0x06005391 RID: 21393 RVA: 0x0022316C File Offset: 0x0022136C
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == CombatSide.Enemy)
			{
				await PowerCmd.Remove(this);
			}
		}

		// Token: 0x0400225A RID: 8794
		private const string _coveringKey = "Covering";

		// Token: 0x02001A27 RID: 6695
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x04006680 RID: 26240
			[Nullable(1)]
			public readonly List<Creature> coveredCreatures = new List<Creature>();
		}
	}
}
