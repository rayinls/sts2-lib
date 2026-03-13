using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200066E RID: 1646
	public class ParryPower : PowerModel
	{
		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x0600545A RID: 21594 RVA: 0x00224595 File Offset: 0x00222795
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x0600545B RID: 21595 RVA: 0x00224598 File Offset: 0x00222798
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x0600545C RID: 21596 RVA: 0x0022459C File Offset: 0x0022279C
		[NullableContext(1)]
		public async Task AfterSovereignBladePlayed([Nullable(2)] Creature dealer, IEnumerable<DamageResult> damageResults)
		{
			if (dealer != null)
			{
				if (dealer == base.Owner)
				{
					base.Flash();
					await CreatureCmd.GainBlock(dealer, base.Amount, ValueProp.Unpowered, null, false);
				}
			}
		}
	}
}
