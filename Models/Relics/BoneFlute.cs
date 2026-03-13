using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004BE RID: 1214
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BoneFlute : RelicModel
	{
		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06004A13 RID: 18963 RVA: 0x002113EF File Offset: 0x0020F5EF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06004A14 RID: 18964 RVA: 0x002113F2 File Offset: 0x0020F5F2
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(2m, ValueProp.Unpowered));
			}
		}

		// Token: 0x06004A15 RID: 18965 RVA: 0x00211408 File Offset: 0x0020F608
		public override Task AfterAttack(AttackCommand command)
		{
			Creature attacker = command.Attacker;
			if (!(((attacker != null) ? attacker.Monster : null) is Osty))
			{
				return Task.CompletedTask;
			}
			Player petOwner = command.Attacker.PetOwner;
			if (((petOwner != null) ? petOwner.Creature : null) != base.Owner.Creature)
			{
				return Task.CompletedTask;
			}
			base.Flash();
			return CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, null, false);
		}
	}
}
