using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006C0 RID: 1728
	public sealed class TheGambitPower : PowerModel
	{
		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x0600566A RID: 22122 RVA: 0x00228703 File Offset: 0x00226903
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x17001328 RID: 4904
		// (get) Token: 0x0600566B RID: 22123 RVA: 0x00228706 File Offset: 0x00226906
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x0600566C RID: 22124 RVA: 0x0022870C File Offset: 0x0022690C
		[NullableContext(1)]
		public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, [Nullable(2)] Creature _, [Nullable(2)] CardModel __)
		{
			if (target == base.Owner)
			{
				if (props.IsPoweredAttack())
				{
					if (result.UnblockedDamage > 0)
					{
						await PowerCmd.Remove(this);
						base.Flash();
						await CreatureCmd.Kill(base.Owner, false);
					}
				}
			}
		}
	}
}
