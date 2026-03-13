using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000786 RID: 1926
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SnappingJaxfruit : MonsterModel
	{
		// Token: 0x170016EF RID: 5871
		// (get) Token: 0x06005E45 RID: 24133 RVA: 0x0023C126 File Offset: 0x0023A326
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 34, 31);
			}
		}

		// Token: 0x170016F0 RID: 5872
		// (get) Token: 0x06005E46 RID: 24134 RVA: 0x0023C132 File Offset: 0x0023A332
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 36, 33);
			}
		}

		// Token: 0x170016F1 RID: 5873
		// (get) Token: 0x06005E47 RID: 24135 RVA: 0x0023C13E File Offset: 0x0023A33E
		private int EnergyDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 4, 3);
			}
		}

		// Token: 0x170016F2 RID: 5874
		// (get) Token: 0x06005E48 RID: 24136 RVA: 0x0023C149 File Offset: 0x0023A349
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Plant;
			}
		}

		// Token: 0x170016F3 RID: 5875
		// (get) Token: 0x06005E49 RID: 24137 RVA: 0x0023C14C File Offset: 0x0023A34C
		// (set) Token: 0x06005E4A RID: 24138 RVA: 0x0023C154 File Offset: 0x0023A354
		private bool IsCharged
		{
			get
			{
				return this._isCharged;
			}
			set
			{
				base.AssertMutable();
				this._isCharged = value;
			}
		}

		// Token: 0x06005E4B RID: 24139 RVA: 0x0023C164 File Offset: 0x0023A364
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			SfxCmd.PlayLoop("event:/sfx/enemy/enemy_attacks/orb_plant/orb_plant_idle_loop", true);
		}

		// Token: 0x06005E4C RID: 24140 RVA: 0x0023C1A7 File Offset: 0x0023A3A7
		public override void BeforeRemovedFromRoom()
		{
			SfxCmd.StopLoop("event:/sfx/enemy/enemy_attacks/orb_plant/orb_plant_idle_loop");
		}

		// Token: 0x06005E4D RID: 24141 RVA: 0x0023C1B4 File Offset: 0x0023A3B4
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("ENERGY_ORB_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.EnergyOrb), new AbstractIntent[]
			{
				new SingleAttackIntent(this.EnergyDamage),
				new BuffIntent()
			});
			moveState.FollowUpState = moveState;
			list.Add(moveState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005E4E RID: 24142 RVA: 0x0023C210 File Offset: 0x0023A410
		public async Task EnergyOrb(IReadOnlyList<Creature> targets)
		{
			if (TestMode.IsOff)
			{
				NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(base.Creature);
				Player me = LocalContext.GetMe(base.CombatState);
				Creature creature = ((me != null) ? me.Creature : null);
				NSnappingJaxfruitVfx specialNode = creatureNode.GetSpecialNode<NSnappingJaxfruitVfx>("Visuals/NSnappingJaxfruitVfx");
				if (specialNode != null)
				{
					specialNode.SetTarget(creature);
				}
			}
			this.IsCharged = true;
			await DamageCmd.Attack(this.EnergyDamage).FromMonster(this).WithAttackerAnim("Cast", 0.25f, null)
				.Execute(null);
			this.IsCharged = false;
			await PowerCmd.Apply<StrengthPower>(base.Creature, 2m, base.Creature, null, false);
		}

		// Token: 0x06005E4F RID: 24143 RVA: 0x0023C254 File Offset: 0x0023A454
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("hurt", false);
			AnimState animState3 = new AnimState("die", false);
			AnimState animState4 = new AnimState("charge_up", false);
			AnimState animState5 = new AnimState("charged_loop", true);
			AnimState animState6 = new AnimState("hurt_charged", false);
			AnimState animState7 = new AnimState("cast", false);
			animState2.NextState = animState;
			animState4.NextState = animState5;
			animState6.NextState = animState5;
			animState7.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Dead", animState3, null);
			creatureAnimator.AddAnyState("Charge", animState4, null);
			creatureAnimator.AddAnyState("Cast", animState7, null);
			creatureAnimator.AddAnyState("Hit", animState6, () => this.IsCharged);
			creatureAnimator.AddAnyState("Hit", animState2, () => !this.IsCharged);
			return creatureAnimator;
		}

		// Token: 0x040023BF RID: 9151
		private const string _chargeTrigger = "Charge";

		// Token: 0x040023C0 RID: 9152
		private const string _chargedSfx = "event:/sfx/enemy/enemy_attacks/orb_plant/orb_plant_charged_loop";

		// Token: 0x040023C1 RID: 9153
		private const string _idleLoopSfx = "event:/sfx/enemy/enemy_attacks/orb_plant/orb_plant_idle_loop";

		// Token: 0x040023C2 RID: 9154
		private bool _isCharged;
	}
}
