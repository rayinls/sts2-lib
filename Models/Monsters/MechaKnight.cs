using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Nodes.Vfx.Cards;
using MegaCrit.Sts2.Core.Nodes.Vfx.Utilities;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200076B RID: 1899
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MechaKnight : MonsterModel
	{
		// Token: 0x17001640 RID: 5696
		// (get) Token: 0x06005CA5 RID: 23717 RVA: 0x002373BD File Offset: 0x002355BD
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 320, 300);
			}
		}

		// Token: 0x17001641 RID: 5697
		// (get) Token: 0x06005CA6 RID: 23718 RVA: 0x002373CF File Offset: 0x002355CF
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001642 RID: 5698
		// (get) Token: 0x06005CA7 RID: 23719 RVA: 0x002373D7 File Offset: 0x002355D7
		private static int ChargeDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 30, 25);
			}
		}

		// Token: 0x17001643 RID: 5699
		// (get) Token: 0x06005CA8 RID: 23720 RVA: 0x002373E4 File Offset: 0x002355E4
		private static int HeavyCleaveDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 40, 35);
			}
		}

		// Token: 0x17001644 RID: 5700
		// (get) Token: 0x06005CA9 RID: 23721 RVA: 0x002373F1 File Offset: 0x002355F1
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/mechaknight/mechaknight_die";
			}
		}

		// Token: 0x17001645 RID: 5701
		// (get) Token: 0x06005CAA RID: 23722 RVA: 0x002373F8 File Offset: 0x002355F8
		public override string HurtSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/mechaknight/mechaknight_hurt";
			}
		}

		// Token: 0x17001646 RID: 5702
		// (get) Token: 0x06005CAB RID: 23723 RVA: 0x002373FF File Offset: 0x002355FF
		// (set) Token: 0x06005CAC RID: 23724 RVA: 0x00237407 File Offset: 0x00235607
		private bool IsWoundUp
		{
			get
			{
				return this._isWoundUp;
			}
			set
			{
				base.AssertMutable();
				this._isWoundUp = value;
			}
		}

		// Token: 0x06005CAD RID: 23725 RVA: 0x00237418 File Offset: 0x00235618
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<ArtifactPower>(base.Creature, 3m, base.Creature, null, false);
		}

		// Token: 0x06005CAE RID: 23726 RVA: 0x0023745C File Offset: 0x0023565C
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("CHARGE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ChargeMove), new AbstractIntent[]
			{
				new SingleAttackIntent(MechaKnight.ChargeDamage)
			});
			MoveState moveState2 = new MoveState("FLAMETHROWER_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.FlamethrowerMove), new AbstractIntent[]
			{
				new StatusIntent(4)
			});
			MoveState moveState3 = new MoveState("WINDUP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.WindupMove), new AbstractIntent[]
			{
				new DefendIntent(),
				new BuffIntent()
			});
			MoveState moveState4 = new MoveState("HEAVY_CLEAVE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.HeavyCleaveMove), new AbstractIntent[]
			{
				new SingleAttackIntent(MechaKnight.HeavyCleaveDamage)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState4;
			moveState4.FollowUpState = moveState2;
			list.Add(moveState);
			list.Add(moveState4);
			list.Add(moveState3);
			list.Add(moveState2);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005CAF RID: 23727 RVA: 0x0023755C File Offset: 0x0023575C
		private async Task ChargeMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(MechaKnight.ChargeDamage).FromMonster(this).WithAttackerAnim("charge", 0.25f, null)
				.WithWaitBeforeHit(0.5f, 1f)
				.WithHitVfxNode((Creature _) => NSpikeSplashVfx.Create(base.Creature, VfxColor.Gold))
				.WithHitFx(null, "event:/sfx/enemy/enemy_attacks/mechaknight/mechaknight_dash", null)
				.WithHitVfxNode((Creature _) => NBigSlashImpactVfx.Create(VfxCmd.GetSideCenter(CombatSide.Player, base.CombatState).Value, 180f, new Color("#80dbff")))
				.Execute(null);
		}

		// Token: 0x06005CB0 RID: 23728 RVA: 0x002375A0 File Offset: 0x002357A0
		private async Task HeavyCleaveMove(IReadOnlyList<Creature> targets)
		{
			this.IsWoundUp = false;
			await DamageCmd.Attack(MechaKnight.HeavyCleaveDamage).FromMonster(this).WithAttackerAnim("Attack", 0.4f, null)
				.WithHitFx(null, "event:/sfx/enemy/enemy_attacks/mechaknight/mechaknight_heavy_attack", null)
				.AfterAttackerAnim(delegate
				{
					NCombatRoom instance = NCombatRoom.Instance;
					if (instance != null)
					{
						instance.RadialBlur(VfxPosition.Left);
					}
					NGame instance2 = NGame.Instance;
					if (instance2 != null)
					{
						instance2.DoHitStop(ShakeStrength.Strong, ShakeDuration.Normal);
					}
					return Task.CompletedTask;
				})
				.WithHitVfxNode((Creature _) => NBigSlashVfx.Create(VfxCmd.GetSideCenter(CombatSide.Player, base.CombatState).Value, false))
				.WithHitVfxNode((Creature _) => NBigSlashImpactVfx.Create(VfxCmd.GetSideCenter(CombatSide.Player, base.CombatState).Value, 180f, new Color("#80dbff")))
				.Execute(null);
		}

		// Token: 0x06005CB1 RID: 23729 RVA: 0x002375E4 File Offset: 0x002357E4
		private async Task WindupMove(IReadOnlyList<Creature> targets)
		{
			this.IsWoundUp = true;
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/mechaknight/mechaknight_buff", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "windUp", 0.5f);
			await CreatureCmd.GainBlock(base.Creature, 15m, ValueProp.Move, null, false);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 5m, base.Creature, null, false);
		}

		// Token: 0x06005CB2 RID: 23730 RVA: 0x00237628 File Offset: 0x00235828
		private async Task FlamethrowerMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/mechaknight/mechaknight_flamethrower", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "flamethrower", 1.5f);
			await CardPileCmd.AddToCombatAndPreview<Burn>(targets, PileType.Hand, 4, false, CardPilePosition.Bottom);
		}

		// Token: 0x06005CB3 RID: 23731 RVA: 0x00237674 File Offset: 0x00235874
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("hurt", false);
			AnimState animState3 = new AnimState("die", false);
			AnimState animState4 = new AnimState("attack_flame", false);
			AnimState animState5 = new AnimState("attack_cleave", false);
			AnimState animState6 = new AnimState("charge", false);
			AnimState animState7 = new AnimState("wind_up", false);
			AnimState animState8 = new AnimState("idle_loop_wound", true);
			AnimState animState9 = new AnimState("hurt_wound", false);
			animState5.NextState = animState;
			animState6.NextState = animState;
			animState4.NextState = animState;
			animState2.NextState = animState;
			animState7.NextState = animState8;
			animState9.NextState = animState8;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Dead", animState3, null);
			creatureAnimator.AddAnyState("Attack", animState5, null);
			creatureAnimator.AddAnyState("flamethrower", animState4, null);
			creatureAnimator.AddAnyState("charge", animState6, null);
			creatureAnimator.AddAnyState("windUp", animState7, null);
			animState.AddBranch("Hit", animState2, () => !this.IsWoundUp);
			animState4.AddBranch("Hit", animState2, () => !this.IsWoundUp);
			animState2.AddBranch("Hit", animState2, () => !this.IsWoundUp);
			animState5.AddBranch("Hit", animState2, () => !this.IsWoundUp);
			animState7.AddBranch("Hit", animState2, () => !this.IsWoundUp);
			animState8.AddBranch("Hit", animState2, () => !this.IsWoundUp);
			animState9.AddBranch("Hit", animState2, () => !this.IsWoundUp);
			animState.AddBranch("Hit", animState9, () => this.IsWoundUp);
			animState4.AddBranch("Hit", animState9, () => this.IsWoundUp);
			animState2.AddBranch("Hit", animState9, () => this.IsWoundUp);
			animState5.AddBranch("Hit", animState9, () => this.IsWoundUp);
			animState7.AddBranch("Hit", animState9, () => this.IsWoundUp);
			animState8.AddBranch("Hit", animState9, () => this.IsWoundUp);
			animState9.AddBranch("Hit", animState9, () => this.IsWoundUp);
			return creatureAnimator;
		}

		// Token: 0x04002361 RID: 9057
		private const int _flamethrowerCardCount = 4;

		// Token: 0x04002362 RID: 9058
		private const int _windupBlock = 15;

		// Token: 0x04002363 RID: 9059
		private const string _windUpTrigger = "windUp";

		// Token: 0x04002364 RID: 9060
		private const string _flameAttackTrigger = "flamethrower";

		// Token: 0x04002365 RID: 9061
		private const string _chargeTrigger = "charge";

		// Token: 0x04002366 RID: 9062
		private bool _isWoundUp;

		// Token: 0x04002367 RID: 9063
		private const string _buffSfx = "event:/sfx/enemy/enemy_attacks/mechaknight/mechaknight_buff";

		// Token: 0x04002368 RID: 9064
		private const string _dashSfx = "event:/sfx/enemy/enemy_attacks/mechaknight/mechaknight_dash";

		// Token: 0x04002369 RID: 9065
		private const string _flamethrowerSfx = "event:/sfx/enemy/enemy_attacks/mechaknight/mechaknight_flamethrower";

		// Token: 0x0400236A RID: 9066
		private const string _heavyAttackSfx = "event:/sfx/enemy/enemy_attacks/mechaknight/mechaknight_heavy_attack";
	}
}
