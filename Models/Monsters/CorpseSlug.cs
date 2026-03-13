using System;
using System.Collections.Generic;
using System.Linq;
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
using MegaCrit.Sts2.Core.Random;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000740 RID: 1856
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CorpseSlug : MonsterModel
	{
		// Token: 0x17001516 RID: 5398
		// (get) Token: 0x060059F1 RID: 23025 RVA: 0x0022EBFB File Offset: 0x0022CDFB
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 27, 25);
			}
		}

		// Token: 0x17001517 RID: 5399
		// (get) Token: 0x060059F2 RID: 23026 RVA: 0x0022EC07 File Offset: 0x0022CE07
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 29, 27);
			}
		}

		// Token: 0x17001518 RID: 5400
		// (get) Token: 0x060059F3 RID: 23027 RVA: 0x0022EC13 File Offset: 0x0022CE13
		private int WhipSlapDamage
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17001519 RID: 5401
		// (get) Token: 0x060059F4 RID: 23028 RVA: 0x0022EC16 File Offset: 0x0022CE16
		private int WhipSlapRepeat
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700151A RID: 5402
		// (get) Token: 0x060059F5 RID: 23029 RVA: 0x0022EC19 File Offset: 0x0022CE19
		private int GlompDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 9, 8);
			}
		}

		// Token: 0x1700151B RID: 5403
		// (get) Token: 0x060059F6 RID: 23030 RVA: 0x0022EC25 File Offset: 0x0022CE25
		private int GoopFrailAmt
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700151C RID: 5404
		// (get) Token: 0x060059F7 RID: 23031 RVA: 0x0022EC28 File Offset: 0x0022CE28
		private int RavenousStr
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 5, 4);
			}
		}

		// Token: 0x1700151D RID: 5405
		// (get) Token: 0x060059F8 RID: 23032 RVA: 0x0022EC33 File Offset: 0x0022CE33
		protected override string AttackSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/corpse_slugs/corpse_slugs_attack";
			}
		}

		// Token: 0x1700151E RID: 5406
		// (get) Token: 0x060059F9 RID: 23033 RVA: 0x0022EC3A File Offset: 0x0022CE3A
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/corpse_slugs/corpse_slugs_die";
			}
		}

		// Token: 0x1700151F RID: 5407
		// (get) Token: 0x060059FA RID: 23034 RVA: 0x0022EC41 File Offset: 0x0022CE41
		// (set) Token: 0x060059FB RID: 23035 RVA: 0x0022EC49 File Offset: 0x0022CE49
		public bool IsRavenous
		{
			get
			{
				return this._isRavenous;
			}
			set
			{
				base.AssertMutable();
				this._isRavenous = value;
			}
		}

		// Token: 0x17001520 RID: 5408
		// (get) Token: 0x060059FC RID: 23036 RVA: 0x0022EC58 File Offset: 0x0022CE58
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Slime;
			}
		}

		// Token: 0x17001521 RID: 5409
		// (get) Token: 0x060059FD RID: 23037 RVA: 0x0022EC5B File Offset: 0x0022CE5B
		// (set) Token: 0x060059FE RID: 23038 RVA: 0x0022EC63 File Offset: 0x0022CE63
		public int StarterMoveIdx
		{
			get
			{
				return this._starterMoveIdx;
			}
			set
			{
				base.AssertMutable();
				this._starterMoveIdx = value;
			}
		}

		// Token: 0x060059FF RID: 23039 RVA: 0x0022EC74 File Offset: 0x0022CE74
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<RavenousPower>(base.Creature, this.RavenousStr, base.Creature, null, false);
		}

		// Token: 0x06005A00 RID: 23040 RVA: 0x0022ECB8 File Offset: 0x0022CEB8
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("WHIP_SLAP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.WhipSlapMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.WhipSlapDamage, this.WhipSlapRepeat)
			});
			MoveState moveState2 = new MoveState("GLOMP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.GlompMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.GlompDamage)
			});
			MoveState moveState3 = new MoveState("GOOP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.GoopMove), new AbstractIntent[]
			{
				new DebuffIntent(false)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			int num = this.StarterMoveIdx % 3;
			MoveState moveState4;
			if (num != 0)
			{
				if (num != 1)
				{
					moveState4 = moveState3;
				}
				else
				{
					moveState4 = moveState2;
				}
			}
			else
			{
				moveState4 = moveState;
			}
			MoveState moveState5 = moveState4;
			return new MonsterMoveStateMachine(list, moveState5);
		}

		// Token: 0x06005A01 RID: 23041 RVA: 0x0022EDA8 File Offset: 0x0022CFA8
		private async Task WhipSlapMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.WhipSlapDamage).WithHitCount(this.WhipSlapRepeat).FromMonster(this)
				.WithAttackerAnim("Attack", 0.5f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/corpse_slugs/corpse_slugs_attack_light", null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005A02 RID: 23042 RVA: 0x0022EDEC File Offset: 0x0022CFEC
		private async Task GlompMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.GlompDamage).FromMonster(this).WithAttackerAnim("HeavyAttackTrigger", 0.3f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005A03 RID: 23043 RVA: 0x0022EE30 File Offset: 0x0022D030
		private async Task GoopMove(IReadOnlyList<Creature> targets)
		{
			await CreatureCmd.TriggerAnim(base.Creature, "Attack", 0.2f);
			await PowerCmd.Apply<FrailPower>(targets, this.GoopFrailAmt, base.Creature, null, false);
		}

		// Token: 0x06005A04 RID: 23044 RVA: 0x0022EE7C File Offset: 0x0022D07C
		public static void EnsureCorpseSlugsStartWithDifferentMoves(IEnumerable<MonsterModel> monsters, Rng rng)
		{
			IEnumerable<CorpseSlug> enumerable = monsters.OfType<CorpseSlug>();
			int num = rng.NextInt(3);
			foreach (CorpseSlug corpseSlug in enumerable)
			{
				corpseSlug.StarterMoveIdx = num % 3;
				num++;
			}
		}

		// Token: 0x06005A05 RID: 23045 RVA: 0x0022EEDC File Offset: 0x0022D0DC
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("attack", false);
			AnimState animState3 = new AnimState("attack_heavy", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("die", false);
			AnimState animState6 = new AnimState("devour_loop", true);
			AnimState animState7 = new AnimState("devour_start", false);
			AnimState animState8 = new AnimState("devour_end", false);
			AnimState animState9 = new AnimState("hurt_devouring", false);
			AnimState animState10 = new AnimState("die_devouring", false);
			animState3.NextState = animState;
			animState2.NextState = animState;
			animState4.NextState = animState;
			animState7.NextState = animState6;
			animState8.NextState = animState;
			animState9.NextState = animState6;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("HeavyAttackTrigger", animState3, null);
			creatureAnimator.AddAnyState("DevourStartTrigger", animState7, () => !this._isRavenous);
			creatureAnimator.AddAnyState("DevourEndkTrigger", animState8, null);
			creatureAnimator.AddAnyState("Attack", animState2, null);
			creatureAnimator.AddAnyState("Dead", animState5, () => !this._isRavenous);
			creatureAnimator.AddAnyState("Hit", animState4, () => !this._isRavenous);
			creatureAnimator.AddAnyState("Dead", animState10, () => this._isRavenous);
			creatureAnimator.AddAnyState("Hit", animState9, () => this._isRavenous);
			return creatureAnimator;
		}

		// Token: 0x040022AD RID: 8877
		private const string _heavyAttackTrigger = "HeavyAttackTrigger";

		// Token: 0x040022AE RID: 8878
		public const string devourStartTrigger = "DevourStartTrigger";

		// Token: 0x040022AF RID: 8879
		public const string devourEndTrigger = "DevourEndkTrigger";

		// Token: 0x040022B0 RID: 8880
		private const string _attackLightSfx = "event:/sfx/enemy/enemy_attacks/corpse_slugs/corpse_slugs_attack_light";

		// Token: 0x040022B1 RID: 8881
		public const string ravenousSfx = "event:/sfx/enemy/enemy_attacks/corpse_slugs/corpse_slugs_ravenous";

		// Token: 0x040022B2 RID: 8882
		public const string ravenousUpSfxDouble = "event:/sfx/enemy/enemy_attacks/corpse_slugs/corpse_slugs_ravenous_up_double";

		// Token: 0x040022B3 RID: 8883
		private bool _isRavenous;

		// Token: 0x040022B4 RID: 8884
		private int _starterMoveIdx;
	}
}
