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
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000777 RID: 1911
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PhantasmalGardener : MonsterModel
	{
		// Token: 0x17001683 RID: 5763
		// (get) Token: 0x06005D4B RID: 23883 RVA: 0x002391DB File Offset: 0x002373DB
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 29, 28);
			}
		}

		// Token: 0x17001684 RID: 5764
		// (get) Token: 0x06005D4C RID: 23884 RVA: 0x002391E7 File Offset: 0x002373E7
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 33, 32);
			}
		}

		// Token: 0x17001685 RID: 5765
		// (get) Token: 0x06005D4D RID: 23885 RVA: 0x002391F3 File Offset: 0x002373F3
		private int BiteDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 5, 5);
			}
		}

		// Token: 0x17001686 RID: 5766
		// (get) Token: 0x06005D4E RID: 23886 RVA: 0x002391FE File Offset: 0x002373FE
		private int LashDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 7, 7);
			}
		}

		// Token: 0x17001687 RID: 5767
		// (get) Token: 0x06005D4F RID: 23887 RVA: 0x00239209 File Offset: 0x00237409
		private int FlailDamage
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17001688 RID: 5768
		// (get) Token: 0x06005D50 RID: 23888 RVA: 0x0023920C File Offset: 0x0023740C
		private int FlailRepeat
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 3, 3);
			}
		}

		// Token: 0x17001689 RID: 5769
		// (get) Token: 0x06005D51 RID: 23889 RVA: 0x00239217 File Offset: 0x00237417
		private int EnlargeStr
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 3, 2);
			}
		}

		// Token: 0x1700168A RID: 5770
		// (get) Token: 0x06005D52 RID: 23890 RVA: 0x00239222 File Offset: 0x00237422
		private int SkittishAmount
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 7, 6);
			}
		}

		// Token: 0x1700168B RID: 5771
		// (get) Token: 0x06005D53 RID: 23891 RVA: 0x0023922C File Offset: 0x0023742C
		// (set) Token: 0x06005D54 RID: 23892 RVA: 0x00239234 File Offset: 0x00237434
		public int EnlargeTriggers
		{
			get
			{
				return this._enlargeTriggers;
			}
			set
			{
				base.AssertMutable();
				this._enlargeTriggers = value;
			}
		}

		// Token: 0x1700168C RID: 5772
		// (get) Token: 0x06005D55 RID: 23893 RVA: 0x00239243 File Offset: 0x00237443
		public override bool ShouldFadeAfterDeath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700168D RID: 5773
		// (get) Token: 0x06005D56 RID: 23894 RVA: 0x00239246 File Offset: 0x00237446
		// (set) Token: 0x06005D57 RID: 23895 RVA: 0x0023924E File Offset: 0x0023744E
		public float CurrentScale { get; private set; } = 1f;

		// Token: 0x1700168E RID: 5774
		// (get) Token: 0x06005D58 RID: 23896 RVA: 0x00239257 File Offset: 0x00237457
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Magic;
			}
		}

		// Token: 0x1700168F RID: 5775
		// (get) Token: 0x06005D59 RID: 23897 RVA: 0x0023925A File Offset: 0x0023745A
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/phantasmal_gardeners/phantasmal_gardeners_die";
			}
		}

		// Token: 0x06005D5A RID: 23898 RVA: 0x00239264 File Offset: 0x00237464
		public override void SetupSkins(NCreatureVisuals visuals)
		{
			MegaSkeleton skeleton = visuals.SpineBody.GetSkeleton();
			MegaSkin megaSkin = visuals.SpineBody.NewSkin("custom-skin");
			MegaSkeletonDataResource data = skeleton.GetData();
			string slotName = base.Creature.SlotName;
			bool flag = slotName == "first" || slotName == "third";
			if (flag)
			{
				megaSkin.AddSkin(data.FindSkin("tall"));
			}
			else
			{
				megaSkin.AddSkin(data.FindSkin("short"));
			}
			skeleton.SetSkin(megaSkin);
			skeleton.SetSlotsToSetupPose();
		}

		// Token: 0x06005D5B RID: 23899 RVA: 0x002392F8 File Offset: 0x002374F8
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<SkittishPower>(base.Creature, this.SkittishAmount, base.Creature, null, false);
		}

		// Token: 0x06005D5C RID: 23900 RVA: 0x0023933C File Offset: 0x0023753C
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("BITE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BiteMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.BiteDamage)
			});
			MoveState moveState2 = new MoveState("LASH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.LashMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.LashDamage)
			});
			MoveState moveState3 = new MoveState("FLAIL_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.FlailMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.FlailDamage, this.FlailRepeat)
			});
			MoveState moveState4 = new MoveState("ENLARGE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.EnlargeMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			ConditionalBranchState conditionalBranchState = new ConditionalBranchState("INIT_MOVE");
			conditionalBranchState.AddState(moveState3, () => base.Creature.SlotName == "first");
			conditionalBranchState.AddState(moveState, () => base.Creature.SlotName == "second");
			conditionalBranchState.AddState(moveState2, () => base.Creature.SlotName == "third");
			conditionalBranchState.AddState(moveState4, () => base.Creature.SlotName == "fourth");
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState4;
			moveState4.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState4);
			list.Add(moveState3);
			return new MonsterMoveStateMachine(list, conditionalBranchState);
		}

		// Token: 0x06005D5D RID: 23901 RVA: 0x0023949C File Offset: 0x0023769C
		private async Task BiteMove(IReadOnlyList<Creature> targets)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				NCreature creatureNode = instance.GetCreatureNode(base.Creature);
				if (creatureNode != null)
				{
					creatureNode.SetDefaultScaleTo(this.CurrentScale, 0.75f);
				}
			}
			await DamageCmd.Attack(this.BiteDamage).FromMonster(this).WithAttackerAnim("Attack", 0.3f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/phantasmal_gardeners/phantasmal_gardeners_attack_bite", null)
				.WithHitFx("vfx/vfx_bite", null, null)
				.Execute(null);
		}

		// Token: 0x06005D5E RID: 23902 RVA: 0x002394E0 File Offset: 0x002376E0
		private async Task LashMove(IReadOnlyList<Creature> targets)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				NCreature creatureNode = instance.GetCreatureNode(base.Creature);
				if (creatureNode != null)
				{
					creatureNode.SetDefaultScaleTo(this.CurrentScale, 0.75f);
				}
			}
			await DamageCmd.Attack(this.LashDamage).FromMonster(this).WithAttackerAnim("Attack", 0.3f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/phantasmal_gardeners/phantasmal_gardeners_attack_bite", null)
				.WithHitFx("vfx/vfx_bite", null, null)
				.Execute(null);
		}

		// Token: 0x06005D5F RID: 23903 RVA: 0x00239524 File Offset: 0x00237724
		private async Task FlailMove(IReadOnlyList<Creature> targets)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				NCreature creatureNode = instance.GetCreatureNode(base.Creature);
				if (creatureNode != null)
				{
					creatureNode.SetDefaultScaleTo(this.CurrentScale, 0.35f);
				}
			}
			await DamageCmd.Attack(this.FlailDamage).WithHitCount(this.FlailRepeat).OnlyPlayAnimOnce()
				.FromMonster(this)
				.WithAttackerAnim("AttackMulti", 0.3f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/phantasmal_gardeners/phantasmal_gardeners_attack_lick", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005D60 RID: 23904 RVA: 0x00239568 File Offset: 0x00237768
		private async Task EnlargeMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/phantasmal_gardeners/phantasmal_gardeners_buff", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 1.5f);
			await PowerCmd.Apply<StrengthPower>(base.Creature, this.EnlargeStr, base.Creature, null, false);
			this.EnlargeTriggers++;
			this.CurrentScale = 1f + 0.1f * (float)Math.Log((double)(this.EnlargeTriggers + 1));
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				NCreature creatureNode = instance.GetCreatureNode(base.Creature);
				if (creatureNode != null)
				{
					creatureNode.SetDefaultScaleTo(this.CurrentScale, 0.75f);
				}
			}
		}

		// Token: 0x06005D61 RID: 23905 RVA: 0x002395AC File Offset: 0x002377AC
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("buff", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("attack_multi", false);
			AnimState animState5 = new AnimState("hurt_extended", false);
			AnimState animState6 = new AnimState("hurt", false);
			AnimState animState7 = new AnimState("die", false);
			AnimState animState8 = new AnimState("block_loop", true);
			AnimState animState9 = new AnimState("block_start", false);
			AnimState animState10 = new AnimState("block_end", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			animState5.NextState = animState;
			animState6.NextState = animState8;
			animState9.NextState = animState8;
			animState10.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Idle", animState, null);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("AttackMulti", animState4, null);
			creatureAnimator.AddAnyState("Dead", animState7, null);
			creatureAnimator.AddAnyState("Hit", animState5, () => !base.Creature.GetPower<SkittishPower>().HasGainedBlockThisTurn);
			creatureAnimator.AddAnyState("Hit", animState6, () => base.Creature.GetPower<SkittishPower>().HasGainedBlockThisTurn);
			creatureAnimator.AddAnyState("BlockStart", animState9, null);
			creatureAnimator.AddAnyState("BlockEnd", animState10, null);
			return creatureAnimator;
		}

		// Token: 0x04002386 RID: 9094
		private int _enlargeTriggers;

		// Token: 0x04002388 RID: 9096
		private const string _attackMultiTrigger = "AttackMulti";

		// Token: 0x04002389 RID: 9097
		public const string blockStartTrigger = "BlockStart";

		// Token: 0x0400238A RID: 9098
		public const string blockEndTrigger = "BlockEnd";

		// Token: 0x0400238B RID: 9099
		private const string _biteSfx = "event:/sfx/enemy/enemy_attacks/phantasmal_gardeners/phantasmal_gardeners_attack_bite";

		// Token: 0x0400238C RID: 9100
		private const string _lickSfx = "event:/sfx/enemy/enemy_attacks/phantasmal_gardeners/phantasmal_gardeners_attack_lick";

		// Token: 0x0400238D RID: 9101
		private const string _buffSfx = "event:/sfx/enemy/enemy_attacks/phantasmal_gardeners/phantasmal_gardeners_buff";
	}
}
