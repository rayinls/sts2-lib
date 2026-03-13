using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000766 RID: 1894
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LivingFog : MonsterModel
	{
		// Token: 0x1700161B RID: 5659
		// (get) Token: 0x06005C54 RID: 23636 RVA: 0x002363D3 File Offset: 0x002345D3
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 82, 80);
			}
		}

		// Token: 0x1700161C RID: 5660
		// (get) Token: 0x06005C55 RID: 23637 RVA: 0x002363DF File Offset: 0x002345DF
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x1700161D RID: 5661
		// (get) Token: 0x06005C56 RID: 23638 RVA: 0x002363E7 File Offset: 0x002345E7
		private int AdvancedGasDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 9, 8);
			}
		}

		// Token: 0x1700161E RID: 5662
		// (get) Token: 0x06005C57 RID: 23639 RVA: 0x002363F3 File Offset: 0x002345F3
		private int BloatDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 6, 5);
			}
		}

		// Token: 0x1700161F RID: 5663
		// (get) Token: 0x06005C58 RID: 23640 RVA: 0x002363FE File Offset: 0x002345FE
		private int SuperGasBlastDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 9, 8);
			}
		}

		// Token: 0x17001620 RID: 5664
		// (get) Token: 0x06005C59 RID: 23641 RVA: 0x0023640A File Offset: 0x0023460A
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Magic;
			}
		}

		// Token: 0x17001621 RID: 5665
		// (get) Token: 0x06005C5A RID: 23642 RVA: 0x0023640D File Offset: 0x0023460D
		// (set) Token: 0x06005C5B RID: 23643 RVA: 0x00236415 File Offset: 0x00234615
		private int BloatAmount
		{
			get
			{
				return this._bloatAmount;
			}
			set
			{
				base.AssertMutable();
				this._bloatAmount = value;
			}
		}

		// Token: 0x17001622 RID: 5666
		// (get) Token: 0x06005C5C RID: 23644 RVA: 0x00236424 File Offset: 0x00234624
		public override bool ShouldFadeAfterDeath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001623 RID: 5667
		// (get) Token: 0x06005C5D RID: 23645 RVA: 0x00236427 File Offset: 0x00234627
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/living_fog/living_fog_die";
			}
		}

		// Token: 0x06005C5E RID: 23646 RVA: 0x00236430 File Offset: 0x00234630
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("ADVANCED_GAS_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.AdvancedGasMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.AdvancedGasDamage),
				new CardDebuffIntent()
			});
			MoveState moveState2 = new MoveState("BLOAT_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BloatMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.BloatDamage),
				new SummonIntent()
			});
			MoveState moveState3 = new MoveState("SUPER_GAS_BLAST_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SuperGasBlastMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SuperGasBlastDamage)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState2;
			list.Add(moveState);
			list.Add(moveState3);
			list.Add(moveState2);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005C5F RID: 23647 RVA: 0x00236508 File Offset: 0x00234708
		private async Task AdvancedGasMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.AdvancedGasDamage).FromMonster(this).WithAttackerAnim("Cast", 1.25f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/living_fog/living_fog_attack_blow", null)
				.WithHitVfxNode((Creature _) => NGaseousImpactVfx.Create(CombatSide.Player, base.CombatState, new Color("#402f45")))
				.Execute(null);
			await PowerCmd.Apply<SmoggyPower>(targets, 1m, base.Creature, null, false);
		}

		// Token: 0x06005C60 RID: 23648 RVA: 0x00236554 File Offset: 0x00234754
		private async Task BloatMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/living_fog/living_fog_summon", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "SpawnBomb", 0.35f);
			for (int i = 0; i < this.BloatAmount; i++)
			{
				string nextSlot = base.CombatState.Encounter.GetNextSlot(base.CombatState);
				if (nextSlot != "")
				{
					SfxCmd.Play("event:/sfx/enemy/enemy_attacks/living_fog/living_fog_minion_appear", 1f);
					await CreatureCmd.Add<GasBomb>(base.CombatState, nextSlot);
				}
			}
			this.BloatAmount = Math.Min(this.BloatAmount + 1, 5);
			await DamageCmd.Attack(this.BloatDamage).FromMonster(this).WithAttackerAnim("Attack", 0.1f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/living_fog/living_fog_attack_blow", null)
				.WithHitVfxNode((Creature _) => NGaseousImpactVfx.Create(CombatSide.Player, base.CombatState, new Color("#402f45")))
				.Execute(null);
		}

		// Token: 0x06005C61 RID: 23649 RVA: 0x00236598 File Offset: 0x00234798
		private async Task SuperGasBlastMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SuperGasBlastDamage).FromMonster(this).WithAttackerAnim("Attack", 0.1f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/living_fog/living_fog_attack_blow", null)
				.WithHitVfxNode((Creature _) => NGaseousImpactVfx.Create(CombatSide.Player, base.CombatState, new Color("#402f45")))
				.Execute(null);
		}

		// Token: 0x06005C62 RID: 23650 RVA: 0x002365DC File Offset: 0x002347DC
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("debuff", false);
			AnimState animState3 = new AnimState("spawn_bomb", false);
			AnimState animState4 = new AnimState("attack", false);
			AnimState animState5 = new AnimState("hurt", false);
			AnimState animState6 = new AnimState("die", false);
			animState2.NextState = animState;
			animState4.NextState = animState;
			animState5.NextState = animState;
			animState3.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Idle", animState, null);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState4, null);
			creatureAnimator.AddAnyState("Dead", animState6, null);
			creatureAnimator.AddAnyState("Hit", animState5, null);
			creatureAnimator.AddAnyState("SpawnBomb", animState3, null);
			return creatureAnimator;
		}

		// Token: 0x0400234B RID: 9035
		private int _bloatAmount = 1;

		// Token: 0x0400234C RID: 9036
		private const string _spawnBombTrigger = "SpawnBomb";

		// Token: 0x0400234D RID: 9037
		private const string _attackBlowSfx = "event:/sfx/enemy/enemy_attacks/living_fog/living_fog_attack_blow";

		// Token: 0x0400234E RID: 9038
		private const string _summonSfx = "event:/sfx/enemy/enemy_attacks/living_fog/living_fog_summon";

		// Token: 0x0400234F RID: 9039
		private const string _appearsSfx = "event:/sfx/enemy/enemy_attacks/living_fog/living_fog_minion_appear";
	}
}
