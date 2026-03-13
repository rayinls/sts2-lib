using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006B0 RID: 1712
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SuckPower : PowerModel
	{
		// Token: 0x170012E0 RID: 4832
		// (get) Token: 0x060055E4 RID: 21988 RVA: 0x002273A7 File Offset: 0x002255A7
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012E1 RID: 4833
		// (get) Token: 0x060055E5 RID: 21989 RVA: 0x002273AA File Offset: 0x002255AA
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x060055E6 RID: 21990 RVA: 0x002273AD File Offset: 0x002255AD
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x060055E7 RID: 21991 RVA: 0x002273BC File Offset: 0x002255BC
		public override async Task AfterAttack(AttackCommand command)
		{
			if (command.Attacker == base.Owner)
			{
				if (command.TargetSide != base.Owner.Side)
				{
					if (command.DamageProps.IsPoweredAttack())
					{
						List<DamageResult> list = command.Results.ToList<DamageResult>();
						List<DamageResult> list2 = list.Where((DamageResult r) => r.Receiver.IsPet).ToList<DamageResult>();
						using (List<DamageResult>.Enumerator enumerator = list2.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								DamageResult petHit = enumerator.Current;
								list.RemoveAll(delegate(DamageResult r)
								{
									Creature receiver = r.Receiver;
									Player petOwner = petHit.Receiver.PetOwner;
									return receiver == ((petOwner != null) ? petOwner.Creature : null);
								});
							}
						}
						int num = list.Count((DamageResult r) => r.UnblockedDamage > 0);
						if (num > 0)
						{
							base.Flash();
							await PowerCmd.Apply<StrengthPower>(base.Owner, base.Amount * num, base.Owner, null, false);
						}
					}
				}
			}
		}
	}
}
