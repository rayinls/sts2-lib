using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000646 RID: 1606
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class InfernoPower : PowerModel
	{
		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x06005371 RID: 21361 RVA: 0x00222D27 File Offset: 0x00220F27
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x06005372 RID: 21362 RVA: 0x00222D2A File Offset: 0x00220F2A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x06005373 RID: 21363 RVA: 0x00222D2D File Offset: 0x00220F2D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar("SelfDamage", 0m, ValueProp.Unblockable | ValueProp.Unpowered));
			}
		}

		// Token: 0x06005374 RID: 21364 RVA: 0x00222D44 File Offset: 0x00220F44
		public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Owner.Player)
			{
				NCombatRoom instance = NCombatRoom.Instance;
				if (instance != null)
				{
					instance.CombatVfxContainer.AddChildSafely(NFireSmokePuffVfx.Create(base.Owner));
				}
				await Cmd.CustomScaledWait(0.2f, 0.4f, false, default(CancellationToken));
				DamageVar damageVar = (DamageVar)base.DynamicVars["SelfDamage"];
				await CreatureCmd.Damage(choiceContext, base.Owner, damageVar.BaseValue, damageVar.Props, base.Owner, null);
			}
		}

		// Token: 0x06005375 RID: 21365 RVA: 0x00222D98 File Offset: 0x00220F98
		public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			if (target == base.Owner)
			{
				if (result.UnblockedDamage > 0)
				{
					if (base.Owner.CombatState.CurrentSide == base.Owner.Side)
					{
						foreach (Creature creature in base.CombatState.HittableEnemies)
						{
							NFireBurstVfx nfireBurstVfx = NFireBurstVfx.Create(creature, 0.75f);
							NCombatRoom instance = NCombatRoom.Instance;
							if (instance != null)
							{
								instance.CombatVfxContainer.AddChildSafely(nfireBurstVfx);
							}
						}
						await CreatureCmd.Damage(choiceContext, base.CombatState.HittableEnemies, base.Amount, ValueProp.Unpowered, base.Owner, null);
					}
				}
			}
		}

		// Token: 0x06005376 RID: 21366 RVA: 0x00222DF4 File Offset: 0x00220FF4
		public void IncrementSelfDamage()
		{
			base.AssertMutable();
			DynamicVar dynamicVar = base.DynamicVars["SelfDamage"];
			decimal baseValue = dynamicVar.BaseValue;
			dynamicVar.BaseValue = baseValue + 1m;
		}

		// Token: 0x04002259 RID: 8793
		private const string _selfDamageKey = "SelfDamage";
	}
}
