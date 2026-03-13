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

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000773 RID: 1907
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Ovicopter : MonsterModel
	{
		// Token: 0x17001667 RID: 5735
		// (get) Token: 0x06005D0C RID: 23820 RVA: 0x002384E6 File Offset: 0x002366E6
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 126, 124);
			}
		}

		// Token: 0x17001668 RID: 5736
		// (get) Token: 0x06005D0D RID: 23821 RVA: 0x002384F2 File Offset: 0x002366F2
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 132, 130);
			}
		}

		// Token: 0x17001669 RID: 5737
		// (get) Token: 0x06005D0E RID: 23822 RVA: 0x00238504 File Offset: 0x00236704
		private int SmashDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 17, 16);
			}
		}

		// Token: 0x1700166A RID: 5738
		// (get) Token: 0x06005D0F RID: 23823 RVA: 0x00238511 File Offset: 0x00236711
		private int TenderizerDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 8, 7);
			}
		}

		// Token: 0x1700166B RID: 5739
		// (get) Token: 0x06005D10 RID: 23824 RVA: 0x0023851C File Offset: 0x0023671C
		private int NutritionalPasteStrengthAmount
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 4, 3);
			}
		}

		// Token: 0x1700166C RID: 5740
		// (get) Token: 0x06005D11 RID: 23825 RVA: 0x00238527 File Offset: 0x00236727
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/egg_layer/egg_layer_die";
			}
		}

		// Token: 0x1700166D RID: 5741
		// (get) Token: 0x06005D12 RID: 23826 RVA: 0x0023852E File Offset: 0x0023672E
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Slime;
			}
		}

		// Token: 0x06005D13 RID: 23827 RVA: 0x00238534 File Offset: 0x00236734
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			SfxCmd.PlayLoop("event:/sfx/enemy/enemy_attacks/egg_layer/egg_layer_idle_loop", true);
		}

		// Token: 0x06005D14 RID: 23828 RVA: 0x00238577 File Offset: 0x00236777
		public override void BeforeRemovedFromRoom()
		{
			SfxCmd.StopLoop("event:/sfx/enemy/enemy_attacks/egg_layer/egg_layer_idle_loop");
		}

		// Token: 0x1700166E RID: 5742
		// (get) Token: 0x06005D15 RID: 23829 RVA: 0x00238584 File Offset: 0x00236784
		private bool CanLay
		{
			get
			{
				return base.Creature.CombatState.GetTeammatesOf(base.Creature).Count((Creature c) => c.IsAlive) <= 3;
			}
		}

		// Token: 0x06005D16 RID: 23830 RVA: 0x002385D4 File Offset: 0x002367D4
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("LAY_EGGS_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.LayEggsMove), new AbstractIntent[]
			{
				new SummonIntent()
			});
			MoveState moveState2 = new MoveState("SMASH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SmashMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SmashDamage)
			});
			MoveState moveState3 = new MoveState("TENDERIZER_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.TenderizerMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.TenderizerDamage),
				new DebuffIntent(false)
			});
			MoveState moveState4 = new MoveState("NUTRITIONAL_PASTE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.NutritionalPasteMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			ConditionalBranchState conditionalBranchState = new ConditionalBranchState("SUMMON_BRANCH_STATE");
			moveState.FollowUpState = moveState2;
			moveState4.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = conditionalBranchState;
			conditionalBranchState.AddState(moveState, () => this.CanLay);
			conditionalBranchState.AddState(moveState4, () => !this.CanLay);
			list.Add(moveState4);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(conditionalBranchState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005D17 RID: 23831 RVA: 0x00238710 File Offset: 0x00236910
		private async Task LayEggsMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/egg_layer/egg_layer_lay", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "lay", 1f);
			for (int i = 0; i < 3; i++)
			{
				string text = base.CombatState.Encounter.Slots.LastOrDefault((string s) => base.CombatState.Enemies.All((Creature c) => c.SlotName != s), string.Empty);
				await PowerCmd.Apply<MinionPower>(await CreatureCmd.Add<ToughEgg>(base.CombatState, text), 1m, base.Creature, null, false);
			}
		}

		// Token: 0x06005D18 RID: 23832 RVA: 0x00238754 File Offset: 0x00236954
		private async Task NutritionalPasteMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/egg_layer/egg_layer_lay", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "lay", 1f);
			await PowerCmd.Apply<StrengthPower>(base.Creature, this.NutritionalPasteStrengthAmount, base.Creature, null, false);
		}

		// Token: 0x06005D19 RID: 23833 RVA: 0x00238798 File Offset: 0x00236998
		private async Task SmashMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SmashDamage).FromMonster(this).WithAttackerAnim("Attack", 0.3f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005D1A RID: 23834 RVA: 0x002387DC File Offset: 0x002369DC
		private async Task TenderizerMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.TenderizerDamage).FromMonster(this).WithAttackerAnim("Attack", 0.3f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			await PowerCmd.Apply<VulnerablePower>(targets, 2m, base.Creature, null, false);
		}

		// Token: 0x06005D1B RID: 23835 RVA: 0x00238828 File Offset: 0x00236A28
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("cast", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("die", false);
			AnimState animState6 = new AnimState("lay", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState6.NextState = animState;
			animState4.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Dead", animState5, null);
			creatureAnimator.AddAnyState("Hit", animState4, null);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("lay", animState6, null);
			return creatureAnimator;
		}

		// Token: 0x04002377 RID: 9079
		private const string _layMove = "lay";

		// Token: 0x04002378 RID: 9080
		private const string _idleLoop = "event:/sfx/enemy/enemy_attacks/egg_layer/egg_layer_idle_loop";

		// Token: 0x04002379 RID: 9081
		private const string _laySfx = "event:/sfx/enemy/enemy_attacks/egg_layer/egg_layer_lay";
	}
}
