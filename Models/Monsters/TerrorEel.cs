using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
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
	// Token: 0x0200078E RID: 1934
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TerrorEel : MonsterModel
	{
		// Token: 0x17001722 RID: 5922
		// (get) Token: 0x06005EBA RID: 24250 RVA: 0x0023D5F0 File Offset: 0x0023B7F0
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 150, 140);
			}
		}

		// Token: 0x17001723 RID: 5923
		// (get) Token: 0x06005EBB RID: 24251 RVA: 0x0023D602 File Offset: 0x0023B802
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001724 RID: 5924
		// (get) Token: 0x06005EBC RID: 24252 RVA: 0x0023D60A File Offset: 0x0023B80A
		private int ShriekAmount
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 75, 70);
			}
		}

		// Token: 0x17001725 RID: 5925
		// (get) Token: 0x06005EBD RID: 24253 RVA: 0x0023D616 File Offset: 0x0023B816
		private int CrashDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 19, 17);
			}
		}

		// Token: 0x17001726 RID: 5926
		// (get) Token: 0x06005EBE RID: 24254 RVA: 0x0023D623 File Offset: 0x0023B823
		private int ThrashDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 4, 3);
			}
		}

		// Token: 0x17001727 RID: 5927
		// (get) Token: 0x06005EBF RID: 24255 RVA: 0x0023D62E File Offset: 0x0023B82E
		private int ThrashRepeat
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17001728 RID: 5928
		// (get) Token: 0x06005EC0 RID: 24256 RVA: 0x0023D631 File Offset: 0x0023B831
		// (set) Token: 0x06005EC1 RID: 24257 RVA: 0x0023D639 File Offset: 0x0023B839
		public MoveState TerrorState
		{
			get
			{
				return this._terrorState;
			}
			private set
			{
				base.AssertMutable();
				this._terrorState = value;
			}
		}

		// Token: 0x17001729 RID: 5929
		// (get) Token: 0x06005EC2 RID: 24258 RVA: 0x0023D648 File Offset: 0x0023B848
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Slime;
			}
		}

		// Token: 0x06005EC3 RID: 24259 RVA: 0x0023D64C File Offset: 0x0023B84C
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<ShriekPower>(base.Creature, this.ShriekAmount, base.Creature, null, false);
		}

		// Token: 0x06005EC4 RID: 24260 RVA: 0x0023D690 File Offset: 0x0023B890
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("CRASH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.CrashMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.CrashDamage)
			});
			MoveState moveState2 = new MoveState("ThrashMove", new Func<IReadOnlyList<Creature>, Task>(this.ThrashMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.ThrashDamage, this.ThrashRepeat),
				new BuffIntent()
			});
			MoveState moveState3 = new MoveState("STUN_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.StunMove), new AbstractIntent[]
			{
				new StunIntent()
			});
			this.TerrorState = new MoveState("TERROR_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.TerrorMove), new AbstractIntent[]
			{
				new DebuffIntent(false)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState;
			moveState3.FollowUpState = this.TerrorState;
			this.TerrorState.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(this.TerrorState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005EC5 RID: 24261 RVA: 0x0023D7A8 File Offset: 0x0023B9A8
		private async Task CrashMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.CrashDamage).FromMonster(this).WithAttackerAnim("Attack", 0.2f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005EC6 RID: 24262 RVA: 0x0023D7EC File Offset: 0x0023B9EC
		private async Task ThrashMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.ThrashDamage).WithHitCount(this.ThrashRepeat).FromMonster(this)
				.WithAttackerAnim("AttackTripleTrigger", 0.25f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/terror_eel/terror_eel_attack_multi", null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			await PowerCmd.Apply<VigorPower>(base.Creature, 7m, base.Creature, null, false);
		}

		// Token: 0x06005EC7 RID: 24263 RVA: 0x0023D82F File Offset: 0x0023BA2F
		private Task StunMove(IReadOnlyList<Creature> targets)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06005EC8 RID: 24264 RVA: 0x0023D838 File Offset: 0x0023BA38
		private async Task TerrorMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/terror_eel/terror_eel_debuff", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0f);
			await Cmd.Wait(0.7f, false);
			VfxCmd.PlayOnCreatureCenter(base.Creature, "vfx/vfx_scream");
			await Cmd.CustomScaledWait(0.1f, 0.3f, false, default(CancellationToken));
			await PowerCmd.Apply<VulnerablePower>(targets, 99m, base.Creature, null, false);
		}

		// Token: 0x06005EC9 RID: 24265 RVA: 0x0023D884 File Offset: 0x0023BA84
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("cast", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("attack_triple", false);
			AnimState animState5 = new AnimState("hurt", false);
			AnimState animState6 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState5.NextState = animState;
			animState4.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("AttackTripleTrigger", animState4, null);
			creatureAnimator.AddAnyState("Dead", animState6, null);
			creatureAnimator.AddAnyState("Hit", animState5, null);
			return creatureAnimator;
		}

		// Token: 0x040023D8 RID: 9176
		private const string _debuffSfx = "event:/sfx/enemy/enemy_attacks/terror_eel/terror_eel_debuff";

		// Token: 0x040023D9 RID: 9177
		private const string _attackMultiSfx = "event:/sfx/enemy/enemy_attacks/terror_eel/terror_eel_attack_multi";

		// Token: 0x040023DA RID: 9178
		private const string _thrashMoveId = "ThrashMove";

		// Token: 0x040023DB RID: 9179
		private const int _hpNormal = 140;

		// Token: 0x040023DC RID: 9180
		private const int _hpTough = 150;

		// Token: 0x040023DD RID: 9181
		private MoveState _terrorState;

		// Token: 0x040023DE RID: 9182
		private const string _attackTripleTrigger = "AttackTripleTrigger";
	}
}
