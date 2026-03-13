using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005B3 RID: 1459
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TwistedFunnel : RelicModel
	{
		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x0600503D RID: 20541 RVA: 0x0021C9BF File Offset: 0x0021ABBF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x0600503E RID: 20542 RVA: 0x0021C9C2 File Offset: 0x0021ABC2
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<PoisonPower>(4m));
			}
		}

		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x0600503F RID: 20543 RVA: 0x0021C9D4 File Offset: 0x0021ABD4
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<PoisonPower>());
			}
		}

		// Token: 0x06005040 RID: 20544 RVA: 0x0021C9E0 File Offset: 0x0021ABE0
		public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				if (combatState.RoundNumber <= 1)
				{
					base.Flash();
					foreach (Creature creature in base.Owner.Creature.CombatState.HittableEnemies)
					{
						NCombatRoom instance = NCombatRoom.Instance;
						if (instance != null)
						{
							instance.CombatVfxContainer.AddChildSafely(NSmokePuffVfx.Create(creature, NSmokePuffVfx.SmokePuffColor.Green));
						}
					}
					await Cmd.CustomScaledWait(0.2f, 0.4f, false, default(CancellationToken));
					foreach (Creature creature2 in base.Owner.Creature.CombatState.HittableEnemies)
					{
						await PowerCmd.Apply<PoisonPower>(creature2, base.DynamicVars["PoisonPower"].IntValue, base.Owner.Creature, null, false);
					}
					IEnumerator<Creature> enumerator2 = null;
				}
			}
		}
	}
}
