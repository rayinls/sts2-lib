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
using MegaCrit.Sts2.Core.MonsterMoves;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Random;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x020007A0 RID: 1952
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TwoTailedRat : MonsterModel
	{
		// Token: 0x170017AB RID: 6059
		// (get) Token: 0x0600600C RID: 24588 RVA: 0x002415CB File Offset: 0x0023F7CB
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/two_tail_rats/two_tail_rats_die";
			}
		}

		// Token: 0x170017AC RID: 6060
		// (get) Token: 0x0600600D RID: 24589 RVA: 0x002415D2 File Offset: 0x0023F7D2
		public override string HurtSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/two_tail_rats/two_tail_rats_hurt";
			}
		}

		// Token: 0x170017AD RID: 6061
		// (get) Token: 0x0600600E RID: 24590 RVA: 0x002415D9 File Offset: 0x0023F7D9
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 18, 17);
			}
		}

		// Token: 0x170017AE RID: 6062
		// (get) Token: 0x0600600F RID: 24591 RVA: 0x002415E5 File Offset: 0x0023F7E5
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 22, 21);
			}
		}

		// Token: 0x170017AF RID: 6063
		// (get) Token: 0x06006010 RID: 24592 RVA: 0x002415F1 File Offset: 0x0023F7F1
		private int ScratchDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 9, 8);
			}
		}

		// Token: 0x170017B0 RID: 6064
		// (get) Token: 0x06006011 RID: 24593 RVA: 0x002415FD File Offset: 0x0023F7FD
		private int DiseaseBiteDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 7, 6);
			}
		}

		// Token: 0x170017B1 RID: 6065
		// (get) Token: 0x06006012 RID: 24594 RVA: 0x00241608 File Offset: 0x0023F808
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Slime;
			}
		}

		// Token: 0x170017B2 RID: 6066
		// (get) Token: 0x06006013 RID: 24595 RVA: 0x0024160B File Offset: 0x0023F80B
		// (set) Token: 0x06006014 RID: 24596 RVA: 0x00241613 File Offset: 0x0023F813
		public int StarterMoveIndex
		{
			get
			{
				return this._starterMoveIndex;
			}
			set
			{
				base.AssertMutable();
				this._starterMoveIndex = value;
			}
		}

		// Token: 0x170017B3 RID: 6067
		// (get) Token: 0x06006015 RID: 24597 RVA: 0x00241622 File Offset: 0x0023F822
		// (set) Token: 0x06006016 RID: 24598 RVA: 0x0024162A File Offset: 0x0023F82A
		private int TurnsUntilSummonable
		{
			get
			{
				return this._turnsUntilSummonable;
			}
			set
			{
				base.AssertMutable();
				this._turnsUntilSummonable = value;
			}
		}

		// Token: 0x170017B4 RID: 6068
		// (get) Token: 0x06006017 RID: 24599 RVA: 0x00241639 File Offset: 0x0023F839
		// (set) Token: 0x06006018 RID: 24600 RVA: 0x00241641 File Offset: 0x0023F841
		public int CallForBackupCount
		{
			get
			{
				return this._callForBackupCount;
			}
			set
			{
				base.AssertMutable();
				this._callForBackupCount = value;
			}
		}

		// Token: 0x06006019 RID: 24601 RVA: 0x00241650 File Offset: 0x0023F850
		public override void SetupSkins(NCreatureVisuals visuals)
		{
			MegaSkeleton skeleton = visuals.SpineBody.GetSkeleton();
			MegaSkin megaSkin = visuals.SpineBody.NewSkin("custom-skin");
			MegaSkeletonDataResource data = skeleton.GetData();
			megaSkin.AddSkin(data.FindSkin(Rng.Chaotic.NextItem<string>(TwoTailedRat._barnacleOptions)));
			megaSkin.AddSkin(data.FindSkin(Rng.Chaotic.NextItem<string>(TwoTailedRat._headOptions)));
			skeleton.SetSkin(megaSkin);
			skeleton.SetSlotsToSetupPose();
		}

		// Token: 0x0600601A RID: 24602 RVA: 0x002416C4 File Offset: 0x0023F8C4
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
		}

		// Token: 0x0600601B RID: 24603 RVA: 0x00241708 File Offset: 0x0023F908
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("SCRATCH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ScratchMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.ScratchDamage)
			});
			MoveState moveState2 = new MoveState("DISEASE_BITE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.DiseaseBiteMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.DiseaseBiteDamage)
			});
			MoveState moveState3 = new MoveState("SCREECH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ScreechMove), new AbstractIntent[]
			{
				new DebuffIntent(false)
			});
			MoveState moveState4 = new MoveState("CALL_FOR_BACKUP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.CallForBackup), new AbstractIntent[]
			{
				new SummonIntent()
			});
			RandomBranchState randomBranchState = new RandomBranchState("RAND");
			moveState.FollowUpState = randomBranchState;
			moveState2.FollowUpState = randomBranchState;
			moveState3.FollowUpState = randomBranchState;
			moveState4.FollowUpState = randomBranchState;
			randomBranchState.AddBranch(moveState, MoveRepeatType.CannotRepeat, delegate
			{
				if (!this.CanSummon())
				{
					return 1f;
				}
				return 0.083333336f;
			});
			randomBranchState.AddBranch(moveState2, MoveRepeatType.CannotRepeat, delegate
			{
				if (!this.CanSummon())
				{
					return 1f;
				}
				return 0.083333336f;
			});
			randomBranchState.AddBranch(moveState3, 3, MoveRepeatType.CannotRepeat, delegate
			{
				if (!this.CanSummon())
				{
					return 1f;
				}
				return 0.083333336f;
			});
			randomBranchState.AddBranch(moveState4, MoveRepeatType.UseOnlyOnce, delegate
			{
				if (!this.CanSummon())
				{
					return 0f;
				}
				return 0.75f;
			});
			list.Add(randomBranchState);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(moveState4);
			if (this.StarterMoveIndex == -1)
			{
				return new MonsterMoveStateMachine(list, randomBranchState);
			}
			int num = this.StarterMoveIndex % 3;
			MoveState moveState5;
			if (num != 0)
			{
				if (num != 1)
				{
					moveState5 = moveState3;
				}
				else
				{
					moveState5 = moveState2;
				}
			}
			else
			{
				moveState5 = moveState;
			}
			MoveState moveState6 = moveState5;
			return new MonsterMoveStateMachine(list, moveState6);
		}

		// Token: 0x0600601C RID: 24604 RVA: 0x002418A8 File Offset: 0x0023FAA8
		private async Task ScratchMove(IReadOnlyList<Creature> targets)
		{
			int turnsUntilSummonable = this.TurnsUntilSummonable;
			this.TurnsUntilSummonable = turnsUntilSummonable - 1;
			await DamageCmd.Attack(this.ScratchDamage).FromMonster(this).WithAttackerAnim("Attack", 0.25f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/two_tail_rats/two_tail_rats_attack_hands", null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x0600601D RID: 24605 RVA: 0x002418EC File Offset: 0x0023FAEC
		private async Task DiseaseBiteMove(IReadOnlyList<Creature> targets)
		{
			int turnsUntilSummonable = this.TurnsUntilSummonable;
			this.TurnsUntilSummonable = turnsUntilSummonable - 1;
			await DamageCmd.Attack(this.DiseaseBiteDamage).FromMonster(this).WithAttackerAnim("Cast", 0.25f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/two_tail_rats/two_tail_rats_attack_bite", null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x0600601E RID: 24606 RVA: 0x00241930 File Offset: 0x0023FB30
		private async Task ScreechMove(IReadOnlyList<Creature> targets)
		{
			int turnsUntilSummonable = this.TurnsUntilSummonable;
			this.TurnsUntilSummonable = turnsUntilSummonable - 1;
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/two_tail_rats/two_tail_rats_attack_bite", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.3f);
			await PowerCmd.Apply<FrailPower>(targets, 1m, base.Creature, null, false);
		}

		// Token: 0x0600601F RID: 24607 RVA: 0x0024197C File Offset: 0x0023FB7C
		private async Task CallForBackup(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/two_tail_rats/two_tail_rats_summon", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Summon", 0.3f);
			string nextSlot = base.CombatState.Encounter.Slots.LastOrDefault((string s) => this.CombatState.Enemies.All((Creature c) => c.SlotName != s), string.Empty);
			if (!string.IsNullOrEmpty(nextSlot))
			{
				await Cmd.Wait(0.5f, false);
				await CreatureCmd.Add<TwoTailedRat>(base.CombatState, nextSlot);
			}
			List<TwoTailedRat> list = base.Creature.CombatState.Enemies.Select((Creature c) => c.Monster).OfType<TwoTailedRat>().ToList<TwoTailedRat>();
			int maxCallForBackupCount = list.Max((TwoTailedRat c) => c.CallForBackupCount + 1);
			list.ForEach(delegate(TwoTailedRat r)
			{
				r.CallForBackupCount = maxCallForBackupCount;
			});
		}

		// Token: 0x06006020 RID: 24608 RVA: 0x002419C0 File Offset: 0x0023FBC0
		private bool CanSummon()
		{
			if (this.TurnsUntilSummonable > 0)
			{
				return false;
			}
			if (this.CallForBackupCount >= 3)
			{
				return false;
			}
			EncounterModel encounter = base.CombatState.Encounter;
			if (string.IsNullOrEmpty((encounter != null) ? encounter.GetNextSlot(base.CombatState) : null))
			{
				return false;
			}
			List<Creature> list = (from c in base.Creature.CombatState.GetTeammatesOf(base.Creature)
				where c != base.Creature
				select c).ToList<Creature>();
			foreach (Creature creature in list)
			{
				if (creature.Monster.NextMove.Id.Equals("CALL_FOR_BACKUP_MOVE"))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006021 RID: 24609 RVA: 0x00241A94 File Offset: 0x0023FC94
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("debuff", false);
			AnimState animState3 = new AnimState("summon", false);
			AnimState animState4 = new AnimState("attack", false);
			AnimState animState5 = new AnimState("hurt", false);
			AnimState animState6 = new AnimState("die", false);
			animState3.NextState = animState;
			animState2.NextState = animState;
			animState4.NextState = animState;
			animState5.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Summon", animState3, null);
			creatureAnimator.AddAnyState("Attack", animState4, null);
			creatureAnimator.AddAnyState("Dead", animState6, null);
			creatureAnimator.AddAnyState("Hit", animState5, null);
			return creatureAnimator;
		}

		// Token: 0x04002428 RID: 9256
		private const string _attackHandsSfx = "event:/sfx/enemy/enemy_attacks/two_tail_rats/two_tail_rats_attack_hands";

		// Token: 0x04002429 RID: 9257
		private const string _summonSfx = "event:/sfx/enemy/enemy_attacks/two_tail_rats/two_tail_rats_summon";

		// Token: 0x0400242A RID: 9258
		private const string _attackBite = "event:/sfx/enemy/enemy_attacks/two_tail_rats/two_tail_rats_attack_bite";

		// Token: 0x0400242B RID: 9259
		private static readonly string[] _barnacleOptions = new string[] { "barnacle1", "barnacle1", "barnacle3" };

		// Token: 0x0400242C RID: 9260
		private static readonly string[] _headOptions = new string[] { "head1", "head2", "head3" };

		// Token: 0x0400242D RID: 9261
		private const string _callForBackupMoveId = "CALL_FOR_BACKUP_MOVE";

		// Token: 0x0400242E RID: 9262
		private const float _callForBackupChance = 0.75f;

		// Token: 0x0400242F RID: 9263
		private const int _callForBackupLimit = 3;

		// Token: 0x04002430 RID: 9264
		private int _starterMoveIndex = -1;

		// Token: 0x04002431 RID: 9265
		private int _turnsUntilSummonable = 2;

		// Token: 0x04002432 RID: 9266
		private int _callForBackupCount;

		// Token: 0x04002433 RID: 9267
		private const string _summonTrigger = "Summon";
	}
}
