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
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000743 RID: 1859
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CubexConstruct : MonsterModel
	{
		// Token: 0x17001533 RID: 5427
		// (get) Token: 0x06005A33 RID: 23091 RVA: 0x0022F77B File Offset: 0x0022D97B
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 70, 65);
			}
		}

		// Token: 0x17001534 RID: 5428
		// (get) Token: 0x06005A34 RID: 23092 RVA: 0x0022F787 File Offset: 0x0022D987
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001535 RID: 5429
		// (get) Token: 0x06005A35 RID: 23093 RVA: 0x0022F78F File Offset: 0x0022D98F
		public override string BestiaryAttackAnimId
		{
			get
			{
				return "charge_start";
			}
		}

		// Token: 0x17001536 RID: 5430
		// (get) Token: 0x06005A36 RID: 23094 RVA: 0x0022F796 File Offset: 0x0022D996
		private int BlastDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 8, 7);
			}
		}

		// Token: 0x17001537 RID: 5431
		// (get) Token: 0x06005A37 RID: 23095 RVA: 0x0022F7A1 File Offset: 0x0022D9A1
		private int ExpelDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 6, 5);
			}
		}

		// Token: 0x17001538 RID: 5432
		// (get) Token: 0x06005A38 RID: 23096 RVA: 0x0022F7AC File Offset: 0x0022D9AC
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Stone;
			}
		}

		// Token: 0x06005A39 RID: 23097 RVA: 0x0022F7B0 File Offset: 0x0022D9B0
		public override void SetupSkins(NCreatureVisuals visuals)
		{
			MegaSkeleton skeleton = visuals.SpineBody.GetSkeleton();
			MegaSkin megaSkin = visuals.SpineBody.NewSkin("custom-skin");
			MegaSkeletonDataResource data = skeleton.GetData();
			megaSkin.AddSkin(data.FindSkin(Rng.Chaotic.NextItem<string>(CubexConstruct._eyeOptions)));
			megaSkin.AddSkin(data.FindSkin(Rng.Chaotic.NextItem<string>(CubexConstruct._mossOptions)));
			skeleton.SetSkin(megaSkin);
			skeleton.SetSlotsToSetupPose();
		}

		// Token: 0x06005A3A RID: 23098 RVA: 0x0022F824 File Offset: 0x0022DA24
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await CreatureCmd.GainBlock(base.Creature, 13m, ValueProp.Move, null, false);
			await PowerCmd.Apply<ArtifactPower>(base.Creature, 1m, base.Creature, null, false);
			base.Creature.CurrentHpChanged += this.OnHpChanged;
		}

		// Token: 0x06005A3B RID: 23099 RVA: 0x0022F867 File Offset: 0x0022DA67
		public override void BeforeRemovedFromRoom()
		{
			SfxCmd.SetParam("event:/sfx/enemy/enemy_attacks/cubex_construct/cubex_construct_charge_attack", "loop", 2f);
			SfxCmd.StopLoop("event:/sfx/enemy/enemy_attacks/cubex_construct/cubex_construct_charge_attack");
			base.Creature.CurrentHpChanged -= this.OnHpChanged;
		}

		// Token: 0x06005A3C RID: 23100 RVA: 0x0022F89E File Offset: 0x0022DA9E
		public void OnHpChanged(int oldHp, int newHp)
		{
			if (newHp < oldHp)
			{
				SfxCmd.SetParam("event:/sfx/enemy/enemy_attacks/cubex_construct/cubex_construct_charge_attack", "enemy_hurt", 1f);
			}
		}

		// Token: 0x06005A3D RID: 23101 RVA: 0x0022F8B8 File Offset: 0x0022DAB8
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("CHARGE_UP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ChargeUpMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			MoveState moveState2 = new MoveState("REPEATER_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.RepeaterBlastMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.BlastDamage),
				new BuffIntent()
			});
			MoveState moveState3 = new MoveState("REPEATER_MOVE_2", new Func<IReadOnlyList<Creature>, Task>(this.RepeaterBlastMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.BlastDamage),
				new BuffIntent()
			});
			MoveState moveState4 = new MoveState("EXPEL_BLAST", new Func<IReadOnlyList<Creature>, Task>(this.ExpelBlastMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.ExpelDamage, 2)
			});
			MoveState moveState5 = new MoveState("SUBMERGE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SubmergeMove), new AbstractIntent[]
			{
				new DefendIntent()
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState4;
			moveState4.FollowUpState = moveState2;
			moveState5.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(moveState4);
			list.Add(moveState5);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005A3E RID: 23102 RVA: 0x0022F9FC File Offset: 0x0022DBFC
		private async Task ChargeUpMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.PlayLoop("event:/sfx/enemy/enemy_attacks/cubex_construct/cubex_construct_charge_attack", false);
			await CreatureCmd.TriggerAnim(base.Creature, "Charge", 0f);
			await Cmd.Wait(0.75f, false);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 2m, base.Creature, null, false);
		}

		// Token: 0x06005A3F RID: 23103 RVA: 0x0022FA40 File Offset: 0x0022DC40
		private async Task RepeaterBlastMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.SetParam("event:/sfx/enemy/enemy_attacks/cubex_construct/cubex_construct_charge_attack", "loop", 1f);
			await Cmd.Wait(0.4f, false);
			await DamageCmd.Attack(this.BlastDamage).FromMonster(this).WithAttackerAnim("Attack", 0f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(null);
			SfxCmd.SetParam("event:/sfx/enemy/enemy_attacks/cubex_construct/cubex_construct_charge_attack", "loop", 0f);
			await Cmd.Wait(0.2f, false);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 2m, base.Creature, null, false);
			await CreatureCmd.TriggerAnim(base.Creature, "AttackEnd", 0f);
		}

		// Token: 0x06005A40 RID: 23104 RVA: 0x0022FA84 File Offset: 0x0022DC84
		private async Task ExpelBlastMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.SetParam("event:/sfx/enemy/enemy_attacks/cubex_construct/cubex_construct_charge_attack", "loop", 1f);
			await Cmd.Wait(0.4f, false);
			await DamageCmd.Attack(this.ExpelDamage).WithHitCount(2).FromMonster(this)
				.WithAttackerAnim("Attack", 0f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(null);
			await Cmd.Wait(0.2f, false);
			await CreatureCmd.TriggerAnim(base.Creature, "AttackEnd", 0f);
		}

		// Token: 0x06005A41 RID: 23105 RVA: 0x0022FAC8 File Offset: 0x0022DCC8
		private async Task SubmergeMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/cubex_construct/cubex_construct_burrow", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Burrow", 0f);
			await Cmd.Wait(1.25f, false);
			await CreatureCmd.GainBlock(base.Creature, 15m, ValueProp.Move, null, false);
		}

		// Token: 0x06005A42 RID: 23106 RVA: 0x0022FB0C File Offset: 0x0022DD0C
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("burrowed_loop", true)
			{
				BoundsContainer = "BurrowedBounds"
			};
			AnimState animState2 = new AnimState("burrow", false);
			AnimState animState3 = new AnimState("unburrow", false);
			AnimState animState4 = new AnimState("idle_loop", true)
			{
				BoundsContainer = "IdleBounds"
			};
			AnimState animState5 = new AnimState("hurt", false);
			AnimState animState6 = new AnimState("die", false);
			AnimState animState7 = new AnimState("hurt", false);
			AnimState animState8 = new AnimState("charge_start", false)
			{
				BoundsContainer = "ChargingBounds"
			};
			AnimState animState9 = new AnimState("charge_loop", true);
			AnimState animState10 = new AnimState("attack_loop", true);
			AnimState animState11 = new AnimState("attack_finish", false);
			animState2.NextState = animState;
			animState.AddBranch("Charge", animState3, null);
			animState3.NextState = animState8;
			animState4.AddBranch("Hit", animState5, null);
			animState4.AddBranch("Burrow", animState2, null);
			animState5.NextState = animState4;
			animState5.AddBranch("Hit", animState5, null);
			animState5.AddBranch("Burrow", animState2, null);
			animState8.NextState = animState9;
			animState9.AddBranch("Hit", animState7, null);
			animState9.AddBranch("Attack", animState10, null);
			animState10.AddBranch("AttackEnd", animState11, null);
			animState11.NextState = animState8;
			animState11.AddBranch("Hit", animState7, null);
			animState7.NextState = animState9;
			animState7.AddBranch("Hit", animState7, null);
			animState7.AddBranch("Attack", animState10, null);
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState2, controller);
			creatureAnimator.AddAnyState("Dead", animState6, null);
			return creatureAnimator;
		}

		// Token: 0x040022BA RID: 8890
		private static readonly string[] _eyeOptions = new string[] { "diamondeye", "circleeye", "squareeye" };

		// Token: 0x040022BB RID: 8891
		private static readonly string[] _mossOptions = new string[] { "moss1", "moss2", "moss3" };

		// Token: 0x040022BC RID: 8892
		private const string _burrowTrigger = "Burrow";

		// Token: 0x040022BD RID: 8893
		private const string _chargeTrigger = "Charge";

		// Token: 0x040022BE RID: 8894
		private const string _attackEndTrigger = "AttackEnd";

		// Token: 0x040022BF RID: 8895
		private const string _chargeStartAnimId = "charge_start";

		// Token: 0x040022C0 RID: 8896
		private const int _expelRepeats = 2;

		// Token: 0x040022C1 RID: 8897
		private const string _burrowSfx = "event:/sfx/enemy/enemy_attacks/cubex_construct/cubex_construct_burrow";

		// Token: 0x040022C2 RID: 8898
		private const string _chargedLoopSfx = "event:/sfx/enemy/enemy_attacks/cubex_construct/cubex_construct_charge_attack";
	}
}
