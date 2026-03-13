using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000624 RID: 1572
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FlutterPower : PowerModel
	{
		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x060052B4 RID: 21172 RVA: 0x0022184A File Offset: 0x0021FA4A
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x060052B5 RID: 21173 RVA: 0x0022184D File Offset: 0x0021FA4D
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x060052B6 RID: 21174 RVA: 0x00221850 File Offset: 0x0021FA50
		public override bool ShouldScaleInMultiplayer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x060052B7 RID: 21175 RVA: 0x00221853 File Offset: 0x0021FA53
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("DamageDecrease", 50m));
			}
		}

		// Token: 0x060052B8 RID: 21176 RVA: 0x0022186B File Offset: 0x0021FA6B
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
			return base.DynamicVars["DamageDecrease"].BaseValue / 100m;
		}

		// Token: 0x060052B9 RID: 21177 RVA: 0x002218AC File Offset: 0x0021FAAC
		public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			if (target == base.Owner)
			{
				if (result.UnblockedDamage != 0)
				{
					if (props.IsPoweredAttack())
					{
						await PowerCmd.Decrement(this);
						if (base.Amount <= 0)
						{
							await CreatureCmd.TriggerAnim(base.Owner, "StunTrigger", 0.6f);
							string nextState = base.Owner.Monster.MoveStateMachine.StateLog.Last<MonsterState>().GetNextState(base.Owner, base.Owner.Monster.RunRng.MonsterAi);
							await CreatureCmd.Stun(base.Owner, new Func<IReadOnlyList<Creature>, Task>(this.StunnedMove), nextState);
							((ThievingHopper)base.Owner.Monster).IsHovering = false;
							SfxCmd.StopLoop("event:/sfx/enemy/enemy_attacks/thieving_hopper/thieving_hopper_hover_loop");
							base.Flash();
							await Cmd.Wait(0.25f, false);
						}
					}
				}
			}
		}

		// Token: 0x060052BA RID: 21178 RVA: 0x00221908 File Offset: 0x0021FB08
		private Task StunnedMove(IReadOnlyList<Creature> targets)
		{
			return Task.CompletedTask;
		}

		// Token: 0x04002251 RID: 8785
		private const string _damageDecreaseKey = "DamageDecrease";
	}
}
