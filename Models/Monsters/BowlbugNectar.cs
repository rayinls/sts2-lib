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
using MegaCrit.Sts2.Core.Nodes.Combat;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000736 RID: 1846
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BowlbugNectar : MonsterModel
	{
		// Token: 0x170014D5 RID: 5333
		// (get) Token: 0x06005950 RID: 22864 RVA: 0x0022CF37 File Offset: 0x0022B137
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 36, 35);
			}
		}

		// Token: 0x170014D6 RID: 5334
		// (get) Token: 0x06005951 RID: 22865 RVA: 0x0022CF43 File Offset: 0x0022B143
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 39, 38);
			}
		}

		// Token: 0x170014D7 RID: 5335
		// (get) Token: 0x06005952 RID: 22866 RVA: 0x0022CF4F File Offset: 0x0022B14F
		private int ThrashDamage
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x170014D8 RID: 5336
		// (get) Token: 0x06005953 RID: 22867 RVA: 0x0022CF52 File Offset: 0x0022B152
		private int BuffStrengthGain
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 16, 15);
			}
		}

		// Token: 0x170014D9 RID: 5337
		// (get) Token: 0x06005954 RID: 22868 RVA: 0x0022CF5F File Offset: 0x0022B15F
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/workbug_goop/workbug_goop_die";
			}
		}

		// Token: 0x170014DA RID: 5338
		// (get) Token: 0x06005955 RID: 22869 RVA: 0x0022CF66 File Offset: 0x0022B166
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Insect;
			}
		}

		// Token: 0x06005956 RID: 22870 RVA: 0x0022CF6C File Offset: 0x0022B16C
		public override void SetupSkins(NCreatureVisuals visuals)
		{
			MegaSkeleton skeleton = visuals.SpineBody.GetSkeleton();
			skeleton.SetSkin(skeleton.GetData().FindSkin("goop"));
			skeleton.SetSlotsToSetupPose();
		}

		// Token: 0x06005957 RID: 22871 RVA: 0x0022CFA4 File Offset: 0x0022B1A4
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("THRASH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ThrashMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.ThrashDamage)
			});
			MoveState moveState2 = new MoveState("BUFF_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BuffMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			MoveState moveState3 = new MoveState("THRASH2_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ThrashMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.ThrashDamage)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState3;
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(moveState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005958 RID: 22872 RVA: 0x0022D064 File Offset: 0x0022B264
		private async Task ThrashMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.ThrashDamage).FromMonster(this).WithAttackerAnim("Attack", 0.3f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/workbug_goop/workbug_goop_spit", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005959 RID: 22873 RVA: 0x0022D0A8 File Offset: 0x0022B2A8
		private async Task BuffMove(IReadOnlyList<Creature> targets)
		{
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.6f);
			await PowerCmd.Apply<StrengthPower>(base.Creature, this.BuffStrengthGain, base.Creature, null, false);
		}

		// Token: 0x0600595A RID: 22874 RVA: 0x0022D0EC File Offset: 0x0022B2EC
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("spit", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Dead", animState5, null);
			creatureAnimator.AddAnyState("Hit", animState4, null);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			return creatureAnimator;
		}

		// Token: 0x0400228E RID: 8846
		private const string _spitSfx = "event:/sfx/enemy/enemy_attacks/workbug_goop/workbug_goop_spit";

		// Token: 0x0400228F RID: 8847
		private const string _spineSkin = "goop";
	}
}
