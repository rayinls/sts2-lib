using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004C2 RID: 1218
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BookRepairKnife : RelicModel
	{
		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06004A31 RID: 18993 RVA: 0x002117DB File Offset: 0x0020F9DB
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06004A32 RID: 18994 RVA: 0x002117DE File Offset: 0x0020F9DE
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DoomPower>());
			}
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06004A33 RID: 18995 RVA: 0x002117EA File Offset: 0x0020F9EA
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new HealVar(3m));
			}
		}

		// Token: 0x06004A34 RID: 18996 RVA: 0x002117FC File Offset: 0x0020F9FC
		public override Task AfterDiedToDoom(PlayerChoiceContext choiceContext, IReadOnlyList<Creature> creatures)
		{
			int num = creatures.Count(delegate(Creature c)
			{
				if (c != base.Owner.Creature)
				{
					return c.Powers.All((PowerModel p) => p.ShouldOwnerDeathTriggerFatal());
				}
				return false;
			});
			if (num == 0)
			{
				return Task.CompletedTask;
			}
			base.Flash();
			return CreatureCmd.Heal(base.Owner.Creature, base.DynamicVars.Heal.BaseValue * num, true);
		}
	}
}
