using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200077E RID: 1918
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SewerClam : MonsterModel
	{
		// Token: 0x170016C1 RID: 5825
		// (get) Token: 0x06005DDF RID: 24031 RVA: 0x0023AD67 File Offset: 0x00238F67
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 58, 56);
			}
		}

		// Token: 0x170016C2 RID: 5826
		// (get) Token: 0x06005DE0 RID: 24032 RVA: 0x0023AD73 File Offset: 0x00238F73
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x170016C3 RID: 5827
		// (get) Token: 0x06005DE1 RID: 24033 RVA: 0x0023AD7B File Offset: 0x00238F7B
		private int JetDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 11, 10);
			}
		}

		// Token: 0x170016C4 RID: 5828
		// (get) Token: 0x06005DE2 RID: 24034 RVA: 0x0023AD88 File Offset: 0x00238F88
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Stone;
			}
		}

		// Token: 0x06005DE3 RID: 24035 RVA: 0x0023AD8C File Offset: 0x00238F8C
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			int valueIfAscension = AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 9, 8);
			await PowerCmd.Apply<PlatingPower>(base.Creature, valueIfAscension, base.Creature, null, false);
		}

		// Token: 0x06005DE4 RID: 24036 RVA: 0x0023ADD0 File Offset: 0x00238FD0
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("PRESSURIZE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.PressurizeMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			MoveState moveState2 = new MoveState("JET_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.JetMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.JetDamage)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			return new MonsterMoveStateMachine(list, moveState2);
		}

		// Token: 0x06005DE5 RID: 24037 RVA: 0x0023AE58 File Offset: 0x00239058
		private async Task PressurizeMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/sewer_clam/sewer_clam_buff", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 1f);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 4m, base.Creature, null, false);
		}

		// Token: 0x06005DE6 RID: 24038 RVA: 0x0023AE9C File Offset: 0x0023909C
		private async Task JetMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.JetDamage).FromMonster(this).WithAttackerAnim("Attack", 0.45f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005DE7 RID: 24039 RVA: 0x0023AEE0 File Offset: 0x002390E0
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("buff", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Idle", animState, null);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("Dead", animState5, null);
			creatureAnimator.AddAnyState("Hit", animState4, null);
			return creatureAnimator;
		}

		// Token: 0x040023AA RID: 9130
		private const string _buffSfx = "event:/sfx/enemy/enemy_attacks/sewer_clam/sewer_clam_buff";
	}
}
