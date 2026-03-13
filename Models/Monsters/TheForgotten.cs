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
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000793 RID: 1939
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TheForgotten : MonsterModel
	{
		// Token: 0x17001757 RID: 5975
		// (get) Token: 0x06005F37 RID: 24375 RVA: 0x0023EBEB File Offset: 0x0023CDEB
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 111, 106);
			}
		}

		// Token: 0x17001758 RID: 5976
		// (get) Token: 0x06005F38 RID: 24376 RVA: 0x0023EBF7 File Offset: 0x0023CDF7
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001759 RID: 5977
		// (get) Token: 0x06005F39 RID: 24377 RVA: 0x0023EBFF File Offset: 0x0023CDFF
		private int DreadDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 17, 15);
			}
		}

		// Token: 0x1700175A RID: 5978
		// (get) Token: 0x06005F3A RID: 24378 RVA: 0x0023EC0C File Offset: 0x0023CE0C
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Stone;
			}
		}

		// Token: 0x06005F3B RID: 24379 RVA: 0x0023EC10 File Offset: 0x0023CE10
		public override async Task AfterAddedToRoom()
		{
			await PowerCmd.Apply<PossessSpeedPower>(base.Creature, 1m, null, null, false);
		}

		// Token: 0x06005F3C RID: 24380 RVA: 0x0023EC54 File Offset: 0x0023CE54
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("MIASMA", new Func<IReadOnlyList<Creature>, Task>(this.MiasmaMove), new AbstractIntent[]
			{
				new DebuffIntent(false),
				new DefendIntent(),
				new BuffIntent()
			});
			MoveState moveState2 = new MoveState("DREAD", new Func<IReadOnlyList<Creature>, Task>(this.DreadMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.DreadDamage)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005F3D RID: 24381 RVA: 0x0023ECEC File Offset: 0x0023CEEC
		private async Task MiasmaMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play(this.CastSfx, 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.5f);
			await PowerCmd.Apply<DexterityPower>(targets, -2m, base.Creature, null, false);
			await CreatureCmd.GainBlock(base.Creature, 8m, ValueProp.Move, null, false);
			await PowerCmd.Apply<DexterityPower>(base.Creature, 2m, base.Creature, null, false);
		}

		// Token: 0x06005F3E RID: 24382 RVA: 0x0023ED38 File Offset: 0x0023CF38
		private async Task DreadMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.DreadDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005F3F RID: 24383 RVA: 0x0023ED7C File Offset: 0x0023CF7C
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("debuff", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("Dead", animState5, null);
			creatureAnimator.AddAnyState("Hit", animState4, null);
			return creatureAnimator;
		}
	}
}
