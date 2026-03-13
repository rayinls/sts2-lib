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
	// Token: 0x0200078B RID: 1931
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SpinyToad : MonsterModel
	{
		// Token: 0x17001713 RID: 5907
		// (get) Token: 0x06005E98 RID: 24216 RVA: 0x0023D0E7 File Offset: 0x0023B2E7
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 121, 116);
			}
		}

		// Token: 0x17001714 RID: 5908
		// (get) Token: 0x06005E99 RID: 24217 RVA: 0x0023D0F3 File Offset: 0x0023B2F3
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 124, 119);
			}
		}

		// Token: 0x17001715 RID: 5909
		// (get) Token: 0x06005E9A RID: 24218 RVA: 0x0023D0FF File Offset: 0x0023B2FF
		private int LashDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 19, 17);
			}
		}

		// Token: 0x17001716 RID: 5910
		// (get) Token: 0x06005E9B RID: 24219 RVA: 0x0023D10C File Offset: 0x0023B30C
		private int ExplosionDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 25, 23);
			}
		}

		// Token: 0x17001717 RID: 5911
		// (get) Token: 0x06005E9C RID: 24220 RVA: 0x0023D119 File Offset: 0x0023B319
		protected override string AttackSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/spiny_toad/spiny_toad_lick";
			}
		}

		// Token: 0x17001718 RID: 5912
		// (get) Token: 0x06005E9D RID: 24221 RVA: 0x0023D120 File Offset: 0x0023B320
		// (set) Token: 0x06005E9E RID: 24222 RVA: 0x0023D128 File Offset: 0x0023B328
		public bool IsSpiny
		{
			get
			{
				return this._isSpiny;
			}
			set
			{
				base.AssertMutable();
				this._isSpiny = value;
			}
		}

		// Token: 0x17001719 RID: 5913
		// (get) Token: 0x06005E9F RID: 24223 RVA: 0x0023D137 File Offset: 0x0023B337
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/spiny_toad/spiny_toad_die";
			}
		}

		// Token: 0x1700171A RID: 5914
		// (get) Token: 0x06005EA0 RID: 24224 RVA: 0x0023D13E File Offset: 0x0023B33E
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x06005EA1 RID: 24225 RVA: 0x0023D144 File Offset: 0x0023B344
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("PROTRUDING_SPIKES_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SpikesMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			MoveState moveState2 = new MoveState("SPIKE_EXPLOSION_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ExplosionMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.ExplosionDamage)
			});
			MoveState moveState3 = new MoveState("TONGUE_LASH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.LashMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.LashDamage)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005EA2 RID: 24226 RVA: 0x0023D203 File Offset: 0x0023B403
		public override Task AfterAddedToRoom()
		{
			base.AfterAddedToRoom();
			return Task.CompletedTask;
		}

		// Token: 0x06005EA3 RID: 24227 RVA: 0x0023D214 File Offset: 0x0023B414
		private async Task SpikesMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/spiny_toad/spiny_toad_protrude", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Spiked", 0.5f);
			this.IsSpiny = true;
			await PowerCmd.Apply<ThornsPower>(base.Creature, 5m, base.Creature, null, false);
		}

		// Token: 0x06005EA4 RID: 24228 RVA: 0x0023D258 File Offset: 0x0023B458
		private async Task ExplosionMove(IReadOnlyList<Creature> targets)
		{
			this.IsSpiny = false;
			await DamageCmd.Attack(this.ExplosionDamage).FromMonster(this).WithAttackerAnim("Unspiked", 0.7f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/spiny_toad/spiny_toad_explode", null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			await PowerCmd.Apply<ThornsPower>(base.Creature, -5m, base.Creature, null, false);
			await Cmd.Wait(1f, false);
		}

		// Token: 0x06005EA5 RID: 24229 RVA: 0x0023D29C File Offset: 0x0023B49C
		private async Task LashMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.LashDamage).FromMonster(this).WithAttackerAnim("Attack", 0.3f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005EA6 RID: 24230 RVA: 0x0023D2E0 File Offset: 0x0023B4E0
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("hurt", false);
			AnimState animState3 = new AnimState("die", false);
			AnimState animState4 = new AnimState("protrude", false);
			AnimState animState5 = new AnimState("lick", false);
			AnimState animState6 = new AnimState("explode", false);
			AnimState animState7 = new AnimState("idle_naked_loop", true);
			AnimState animState8 = new AnimState("hurt_naked", false);
			AnimState animState9 = new AnimState("die_naked", false);
			animState7.AddBranch("Spiked", animState4, null);
			animState.AddBranch("Unspiked", animState6, null);
			animState4.NextState = animState;
			animState8.NextState = animState7;
			animState8.AddBranch("Spiked", animState4, null);
			animState2.NextState = animState;
			animState2.AddBranch("Unspiked", animState6, null);
			animState5.NextState = animState7;
			animState6.NextState = animState7;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState7, controller);
			creatureAnimator.AddAnyState("Attack", animState5, null);
			creatureAnimator.AddAnyState("Hit", animState8, () => !this.IsSpiny);
			creatureAnimator.AddAnyState("Hit", animState2, () => this.IsSpiny);
			creatureAnimator.AddAnyState("Dead", animState9, () => !this.IsSpiny);
			creatureAnimator.AddAnyState("Dead", animState3, () => this.IsSpiny);
			return creatureAnimator;
		}

		// Token: 0x040023D3 RID: 9171
		private const string _spikeTrigger = "Spiked";

		// Token: 0x040023D4 RID: 9172
		private const string _unSpikeTrigger = "Unspiked";

		// Token: 0x040023D5 RID: 9173
		private const string _attackHeavySfx = "event:/sfx/enemy/enemy_attacks/spiny_toad/spiny_toad_explode";

		// Token: 0x040023D6 RID: 9174
		private const string _buffSfx = "event:/sfx/enemy/enemy_attacks/spiny_toad/spiny_toad_protrude";

		// Token: 0x040023D7 RID: 9175
		private bool _isSpiny;
	}
}
