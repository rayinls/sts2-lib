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
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000741 RID: 1857
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CrossbowRubyRaider : MonsterModel
	{
		// Token: 0x17001522 RID: 5410
		// (get) Token: 0x06005A0D RID: 23053 RVA: 0x0022F08E File Offset: 0x0022D28E
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 19, 18);
			}
		}

		// Token: 0x17001523 RID: 5411
		// (get) Token: 0x06005A0E RID: 23054 RVA: 0x0022F09A File Offset: 0x0022D29A
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 22, 21);
			}
		}

		// Token: 0x17001524 RID: 5412
		// (get) Token: 0x06005A0F RID: 23055 RVA: 0x0022F0A6 File Offset: 0x0022D2A6
		private int FireDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 16, 14);
			}
		}

		// Token: 0x17001525 RID: 5413
		// (get) Token: 0x06005A10 RID: 23056 RVA: 0x0022F0B3 File Offset: 0x0022D2B3
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x17001526 RID: 5414
		// (get) Token: 0x06005A11 RID: 23057 RVA: 0x0022F0B6 File Offset: 0x0022D2B6
		// (set) Token: 0x06005A12 RID: 23058 RVA: 0x0022F0BE File Offset: 0x0022D2BE
		private bool IsCrossbowReloaded
		{
			get
			{
				return this._isCrossbowReloaded;
			}
			set
			{
				base.AssertMutable();
				this._isCrossbowReloaded = value;
			}
		}

		// Token: 0x06005A13 RID: 23059 RVA: 0x0022F0D0 File Offset: 0x0022D2D0
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("FIRE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.FireMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.FireDamage)
			});
			MoveState moveState2 = new MoveState("RELOAD_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ReloadMove), new AbstractIntent[]
			{
				new DefendIntent()
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState;
			list.Add(moveState2);
			list.Add(moveState);
			return new MonsterMoveStateMachine(list, moveState2);
		}

		// Token: 0x06005A14 RID: 23060 RVA: 0x0022F158 File Offset: 0x0022D358
		private async Task FireMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.FireDamage).FromMonster(this).WithAttackerAnim("Attack", 0.25f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			this.IsCrossbowReloaded = false;
		}

		// Token: 0x06005A15 RID: 23061 RVA: 0x0022F19C File Offset: 0x0022D39C
		private async Task ReloadMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/crossbow_ruby_raider/crossbow_ruby_raider_reload", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Reload", 0.25f);
			await CreatureCmd.GainBlock(base.Creature, 3m, ValueProp.Move, null, false);
			this.IsCrossbowReloaded = true;
		}

		// Token: 0x06005A16 RID: 23062 RVA: 0x0022F1E0 File Offset: 0x0022D3E0
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("hurt", false);
			AnimState animState3 = new AnimState("die", false);
			AnimState animState4 = new AnimState("attack", false);
			AnimState animState5 = new AnimState("hurt_empty", false);
			AnimState animState6 = new AnimState("idle_loop_empty", true);
			AnimState animState7 = new AnimState("hurt_empty", false);
			AnimState animState8 = new AnimState("die_empty", false);
			AnimState animState9 = new AnimState("reload", false);
			animState2.NextState = animState;
			animState4.NextState = animState6;
			animState5.NextState = animState6;
			animState7.NextState = animState6;
			animState9.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Attack", animState6, null);
			creatureAnimator.AddAnyState("Reload", animState9, null);
			creatureAnimator.AddAnyState("Hit", animState7, () => !this.IsCrossbowReloaded);
			creatureAnimator.AddAnyState("Hit", animState2, () => this.IsCrossbowReloaded);
			creatureAnimator.AddAnyState("Dead", animState8, () => !this.IsCrossbowReloaded);
			creatureAnimator.AddAnyState("Dead", animState3, () => this.IsCrossbowReloaded);
			return creatureAnimator;
		}

		// Token: 0x040022B5 RID: 8885
		private const string _reloadTrigger = "Reload";

		// Token: 0x040022B6 RID: 8886
		private const string _reloadSfx = "event:/sfx/enemy/enemy_attacks/crossbow_ruby_raider/crossbow_ruby_raider_reload";

		// Token: 0x040022B7 RID: 8887
		private bool _isCrossbowReloaded;
	}
}
