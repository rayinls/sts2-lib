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
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200075A RID: 1882
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GremlinMerc : MonsterModel
	{
		// Token: 0x170015BD RID: 5565
		// (get) Token: 0x06005B7E RID: 23422 RVA: 0x0023395B File Offset: 0x00231B5B
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 51, 47);
			}
		}

		// Token: 0x170015BE RID: 5566
		// (get) Token: 0x06005B7F RID: 23423 RVA: 0x00233967 File Offset: 0x00231B67
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 53, 49);
			}
		}

		// Token: 0x170015BF RID: 5567
		// (get) Token: 0x06005B80 RID: 23424 RVA: 0x00233973 File Offset: 0x00231B73
		public override bool HasDeathSfx
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170015C0 RID: 5568
		// (get) Token: 0x06005B81 RID: 23425 RVA: 0x00233976 File Offset: 0x00231B76
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Fur;
			}
		}

		// Token: 0x170015C1 RID: 5569
		// (get) Token: 0x06005B82 RID: 23426 RVA: 0x00233979 File Offset: 0x00231B79
		private int GimmeDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 8, 7);
			}
		}

		// Token: 0x170015C2 RID: 5570
		// (get) Token: 0x06005B83 RID: 23427 RVA: 0x00233983 File Offset: 0x00231B83
		private int GimmeRepeat
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170015C3 RID: 5571
		// (get) Token: 0x06005B84 RID: 23428 RVA: 0x00233986 File Offset: 0x00231B86
		private int DoubleSmashDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 7, 6);
			}
		}

		// Token: 0x170015C4 RID: 5572
		// (get) Token: 0x06005B85 RID: 23429 RVA: 0x00233990 File Offset: 0x00231B90
		private int DoubleSmashRepeat
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170015C5 RID: 5573
		// (get) Token: 0x06005B86 RID: 23430 RVA: 0x00233993 File Offset: 0x00231B93
		public int HeheDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 9, 8);
			}
		}

		// Token: 0x06005B87 RID: 23431 RVA: 0x002339A0 File Offset: 0x00231BA0
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<SurprisePower>(base.Creature, 1m, base.Creature, null, false);
			foreach (Player player in base.Creature.CombatState.Players)
			{
				ThieveryPower thieveryPower = (ThieveryPower)ModelDb.Power<ThieveryPower>().ToMutable(0);
				thieveryPower.Target = player.Creature;
				await PowerCmd.Apply(thieveryPower, base.Creature, 20m, base.Creature, null, false);
			}
			IEnumerator<Player> enumerator = null;
		}

		// Token: 0x06005B88 RID: 23432 RVA: 0x002339E4 File Offset: 0x00231BE4
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("GIMME_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.GimmeMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.GimmeDamage, this.GimmeRepeat)
			});
			MoveState moveState2 = new MoveState("DOUBLE_SMASH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.DoubleSmashMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.DoubleSmashDamage, this.DoubleSmashRepeat),
				new DebuffIntent(false)
			});
			MoveState moveState3 = new MoveState("HEHE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.HeheMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.HeheDamage),
				new BuffIntent()
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005B89 RID: 23433 RVA: 0x00233AC8 File Offset: 0x00231CC8
		private async Task GimmeMove(IReadOnlyList<Creature> targets)
		{
			if (!this._hasSpoken)
			{
				this._hasSpoken = true;
				LocString locString = MonsterModel.L10NMonsterLookup("GREMLIN_MERC.moves.GIMME.banter");
				TalkCmd.Play(locString, base.Creature, -1.0, VfxColor.White);
			}
			VfxCmd.PlayOnCreatureCenters(targets, "vfx/vfx_coin_explosion_regular");
			await DamageCmd.Attack(this.GimmeDamage).WithHitCount(this.GimmeRepeat).FromMonster(this)
				.WithAttackerAnim("AttackDouble", 0.15f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			foreach (ThieveryPower thieveryPower in base.Creature.GetPowerInstances<ThieveryPower>())
			{
				await thieveryPower.Steal();
			}
			IEnumerator<ThieveryPower> enumerator = null;
		}

		// Token: 0x06005B8A RID: 23434 RVA: 0x00233B14 File Offset: 0x00231D14
		private async Task DoubleSmashMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play(this.AttackSfx, 1f);
			VfxCmd.PlayOnCreatureCenters(targets, "vfx/vfx_coin_explosion_regular");
			await DamageCmd.Attack(this.DoubleSmashDamage).WithHitCount(this.DoubleSmashRepeat).FromMonster(this)
				.WithAttackerAnim("AttackDouble", 0.15f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			foreach (ThieveryPower thieveryPower in base.Creature.GetPowerInstances<ThieveryPower>())
			{
				await thieveryPower.Steal();
			}
			IEnumerator<ThieveryPower> enumerator = null;
			await PowerCmd.Apply<WeakPower>(targets, 2m, base.Creature, null, false);
		}

		// Token: 0x06005B8B RID: 23435 RVA: 0x00233B60 File Offset: 0x00231D60
		private async Task HeheMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.HeheDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/gremlin_merc/gremlin_merc_attack_buff", null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			foreach (ThieveryPower thieveryPower in base.Creature.GetPowerInstances<ThieveryPower>())
			{
				await thieveryPower.Steal();
			}
			IEnumerator<ThieveryPower> enumerator = null;
			await PowerCmd.Apply<StrengthPower>(base.Creature, 2m, base.Creature, null, false);
		}

		// Token: 0x06005B8C RID: 23436 RVA: 0x00233BA4 File Offset: 0x00231DA4
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("attack_single", false);
			AnimState animState3 = new AnimState("attack_double", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Idle", animState, null);
			creatureAnimator.AddAnyState("Attack", animState2, null);
			creatureAnimator.AddAnyState("AttackDouble", animState3, null);
			creatureAnimator.AddAnyState("Dead", animState5, null);
			creatureAnimator.AddAnyState("Hit", animState4, null);
			return creatureAnimator;
		}

		// Token: 0x04002309 RID: 8969
		private const string _attackBuffSfx = "event:/sfx/enemy/enemy_attacks/gremlin_merc/gremlin_merc_attack_buff";

		// Token: 0x0400230A RID: 8970
		private bool _hasSpoken;

		// Token: 0x0400230B RID: 8971
		private const string _attackDoubleTrigger = "AttackDouble";
	}
}
