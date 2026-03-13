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
	// Token: 0x02000758 RID: 1880
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GasBomb : MonsterModel
	{
		// Token: 0x170015B0 RID: 5552
		// (get) Token: 0x06005B60 RID: 23392 RVA: 0x002334ED File Offset: 0x002316ED
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 12, 10);
			}
		}

		// Token: 0x170015B1 RID: 5553
		// (get) Token: 0x06005B61 RID: 23393 RVA: 0x002334F9 File Offset: 0x002316F9
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x170015B2 RID: 5554
		// (get) Token: 0x06005B62 RID: 23394 RVA: 0x00233501 File Offset: 0x00231701
		private int ExplodeDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 9, 8);
			}
		}

		// Token: 0x170015B3 RID: 5555
		// (get) Token: 0x06005B63 RID: 23395 RVA: 0x0023350D File Offset: 0x0023170D
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Magic;
			}
		}

		// Token: 0x06005B64 RID: 23396 RVA: 0x00233510 File Offset: 0x00231710
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<MinionPower>(base.Creature, 1m, base.Creature, null, false);
		}

		// Token: 0x170015B4 RID: 5556
		// (get) Token: 0x06005B65 RID: 23397 RVA: 0x00233553 File Offset: 0x00231753
		// (set) Token: 0x06005B66 RID: 23398 RVA: 0x0023355B File Offset: 0x0023175B
		private bool HasExploded
		{
			get
			{
				return this._hasExploded;
			}
			set
			{
				base.AssertMutable();
				this._hasExploded = value;
			}
		}

		// Token: 0x170015B5 RID: 5557
		// (get) Token: 0x06005B67 RID: 23399 RVA: 0x0023356A File Offset: 0x0023176A
		public override bool ShouldFadeAfterDeath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170015B6 RID: 5558
		// (get) Token: 0x06005B68 RID: 23400 RVA: 0x0023356D File Offset: 0x0023176D
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/living_fog/living_fog_minion_die";
			}
		}

		// Token: 0x06005B69 RID: 23401 RVA: 0x00233574 File Offset: 0x00231774
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("EXPLODE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ExplodeMove), new AbstractIntent[]
			{
				new DeathBlowIntent(() => this.ExplodeDamage)
			});
			list.Add(moveState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005B6A RID: 23402 RVA: 0x002335C8 File Offset: 0x002317C8
		private async Task ExplodeMove(IReadOnlyList<Creature> targets)
		{
			this.HasExploded = true;
			await DamageCmd.Attack(this.ExplodeDamage).FromMonster(this).WithAttackerAnim("ExplodeTrigger", 0.1f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/living_fog/living_fog_explode", null)
				.WithHitVfxNode((Creature _) => NGaseousImpactVfx.Create(CombatSide.Player, base.CombatState, new Color("#402f45")))
				.Execute(null);
			await CreatureCmd.Kill(base.Creature, false);
		}

		// Token: 0x06005B6B RID: 23403 RVA: 0x0023360C File Offset: 0x0023180C
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("spawn", false);
			AnimState animState3 = new AnimState("explode", false);
			AnimState animState4 = new AnimState("attack", false);
			AnimState animState5 = new AnimState("hurt", false);
			AnimState animState6 = new AnimState("die", false);
			animState4.NextState = animState;
			animState5.NextState = animState;
			animState2.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState2, controller);
			creatureAnimator.AddAnyState("Idle", animState, null);
			creatureAnimator.AddAnyState("Attack", animState4, null);
			creatureAnimator.AddAnyState("Dead", animState6, () => !this.HasExploded);
			creatureAnimator.AddAnyState("Hit", animState5, null);
			creatureAnimator.AddAnyState("ExplodeTrigger", animState3, null);
			return creatureAnimator;
		}

		// Token: 0x04002301 RID: 8961
		private const string _explodeTrigger = "ExplodeTrigger";

		// Token: 0x04002302 RID: 8962
		private bool _hasExploded;

		// Token: 0x04002303 RID: 8963
		private const string _explodeSfx = "event:/sfx/enemy/enemy_attacks/living_fog/living_fog_explode";
	}
}
