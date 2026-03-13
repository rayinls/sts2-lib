using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Audio;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200078F RID: 1935
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TestSubject : MonsterModel
	{
		// Token: 0x1700172A RID: 5930
		// (get) Token: 0x06005ECC RID: 24268 RVA: 0x0023D95C File Offset: 0x0023BB5C
		public override LocString Title
		{
			get
			{
				LocString title = base.Title;
				title.Add("Count", SaveManager.Instance.Progress.TestSubjectKills + 8);
				return title;
			}
		}

		// Token: 0x1700172B RID: 5931
		// (get) Token: 0x06005ECD RID: 24269 RVA: 0x0023D992 File Offset: 0x0023BB92
		public override int MinInitialHp
		{
			get
			{
				return this.FirstFormHp;
			}
		}

		// Token: 0x1700172C RID: 5932
		// (get) Token: 0x06005ECE RID: 24270 RVA: 0x0023D99A File Offset: 0x0023BB9A
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x1700172D RID: 5933
		// (get) Token: 0x06005ECF RID: 24271 RVA: 0x0023D9A2 File Offset: 0x0023BBA2
		public int FirstFormHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 111, 100);
			}
		}

		// Token: 0x1700172E RID: 5934
		// (get) Token: 0x06005ED0 RID: 24272 RVA: 0x0023D9AE File Offset: 0x0023BBAE
		public int SecondFormHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 212, 200);
			}
		}

		// Token: 0x1700172F RID: 5935
		// (get) Token: 0x06005ED1 RID: 24273 RVA: 0x0023D9C0 File Offset: 0x0023BBC0
		public int ThirdFormHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 313, 300);
			}
		}

		// Token: 0x17001730 RID: 5936
		// (get) Token: 0x06005ED2 RID: 24274 RVA: 0x0023D9D2 File Offset: 0x0023BBD2
		private int EnrageAmount
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 3, 2);
			}
		}

		// Token: 0x17001731 RID: 5937
		// (get) Token: 0x06005ED3 RID: 24275 RVA: 0x0023D9DD File Offset: 0x0023BBDD
		private int BiteDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 22, 20);
			}
		}

		// Token: 0x17001732 RID: 5938
		// (get) Token: 0x06005ED4 RID: 24276 RVA: 0x0023D9EA File Offset: 0x0023BBEA
		private int SkullBashDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 16, 14);
			}
		}

		// Token: 0x17001733 RID: 5939
		// (get) Token: 0x06005ED5 RID: 24277 RVA: 0x0023D9F7 File Offset: 0x0023BBF7
		private int PounceDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 32, 30);
			}
		}

		// Token: 0x17001734 RID: 5940
		// (get) Token: 0x06005ED6 RID: 24278 RVA: 0x0023DA04 File Offset: 0x0023BC04
		private int MultiClawDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 11, 10);
			}
		}

		// Token: 0x17001735 RID: 5941
		// (get) Token: 0x06005ED7 RID: 24279 RVA: 0x0023DA11 File Offset: 0x0023BC11
		private int BaseMultiClawCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17001736 RID: 5942
		// (get) Token: 0x06005ED8 RID: 24280 RVA: 0x0023DA14 File Offset: 0x0023BC14
		private int Phase3LacerateDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 11, 10);
			}
		}

		// Token: 0x17001737 RID: 5943
		// (get) Token: 0x06005ED9 RID: 24281 RVA: 0x0023DA21 File Offset: 0x0023BC21
		private int BigPounceDamage
		{
			get
			{
				return 45;
			}
		}

		// Token: 0x17001738 RID: 5944
		// (get) Token: 0x06005EDA RID: 24282 RVA: 0x0023DA25 File Offset: 0x0023BC25
		private int BurningGrowlBurnCount
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 5, 3);
			}
		}

		// Token: 0x17001739 RID: 5945
		// (get) Token: 0x06005EDB RID: 24283 RVA: 0x0023DA30 File Offset: 0x0023BC30
		private int BurningGrowlStrengthGain
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 3, 2);
			}
		}

		// Token: 0x1700173A RID: 5946
		// (get) Token: 0x06005EDC RID: 24284 RVA: 0x0023DA3B File Offset: 0x0023BC3B
		public override string DeathSfx
		{
			get
			{
				if (!base.IsMutable)
				{
					return base.DeathSfx;
				}
				if (!base.Creature.HasPower<AdaptablePower>())
				{
					return base.DeathSfx;
				}
				return "event:/sfx/enemy/enemy_attacks/test_subject/test_subject_knock_out";
			}
		}

		// Token: 0x1700173B RID: 5947
		// (get) Token: 0x06005EDD RID: 24285 RVA: 0x0023DA65 File Offset: 0x0023BC65
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x1700173C RID: 5948
		// (get) Token: 0x06005EDE RID: 24286 RVA: 0x0023DA68 File Offset: 0x0023BC68
		// (set) Token: 0x06005EDF RID: 24287 RVA: 0x0023DA70 File Offset: 0x0023BC70
		private MoveState DeadState
		{
			get
			{
				return this._deadState;
			}
			set
			{
				base.AssertMutable();
				this._deadState = value;
			}
		}

		// Token: 0x1700173D RID: 5949
		// (get) Token: 0x06005EE0 RID: 24288 RVA: 0x0023DA7F File Offset: 0x0023BC7F
		// (set) Token: 0x06005EE1 RID: 24289 RVA: 0x0023DA87 File Offset: 0x0023BC87
		private int Respawns
		{
			get
			{
				return this._respawns;
			}
			set
			{
				base.AssertMutable();
				this._respawns = value;
			}
		}

		// Token: 0x1700173E RID: 5950
		// (get) Token: 0x06005EE2 RID: 24290 RVA: 0x0023DA96 File Offset: 0x0023BC96
		// (set) Token: 0x06005EE3 RID: 24291 RVA: 0x0023DA9E File Offset: 0x0023BC9E
		private int ExtraMultiClawCount
		{
			get
			{
				return this._extraMultiClawCount;
			}
			set
			{
				base.AssertMutable();
				this._extraMultiClawCount = value;
			}
		}

		// Token: 0x1700173F RID: 5951
		// (get) Token: 0x06005EE4 RID: 24292 RVA: 0x0023DAAD File Offset: 0x0023BCAD
		private int MultiClawTotalCount
		{
			get
			{
				return this.BaseMultiClawCount + this.ExtraMultiClawCount;
			}
		}

		// Token: 0x17001740 RID: 5952
		// (get) Token: 0x06005EE5 RID: 24293 RVA: 0x0023DABC File Offset: 0x0023BCBC
		public override bool ShouldDisappearFromDoom
		{
			get
			{
				return this.Respawns >= 2;
			}
		}

		// Token: 0x06005EE6 RID: 24294 RVA: 0x0023DACC File Offset: 0x0023BCCC
		public async Task TriggerDeadState()
		{
			NRunMusicController instance = NRunMusicController.Instance;
			if (instance != null)
			{
				instance.UpdateMusicParameter("test_subject_progress", 1f);
			}
			ExtraRunFields extraFields = base.CombatState.RunState.ExtraFields;
			int testSubjectKills = extraFields.TestSubjectKills;
			extraFields.TestSubjectKills = testSubjectKills + 1;
			await CreatureCmd.TriggerAnim(base.Creature, "DeadTrigger", 0f);
			base.SetMoveImmediate(this.DeadState, true);
		}

		// Token: 0x06005EE7 RID: 24295 RVA: 0x0023DB10 File Offset: 0x0023BD10
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<AdaptablePower>(base.Creature, 1m, base.Creature, null, false);
			await PowerCmd.Apply<EnragePower>(base.Creature, this.EnrageAmount, base.Creature, null, false);
			base.Creature.Died += this.AfterDeath;
			base.Creature.PowerApplied += this.AfterPowerApplied;
			base.Creature.PowerRemoved += this.AfterPowerRemoved;
		}

		// Token: 0x06005EE8 RID: 24296 RVA: 0x0023DB54 File Offset: 0x0023BD54
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			this.DeadState = new MoveState("RESPAWN_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.RespawnMove), new AbstractIntent[]
			{
				new HealIntent(),
				new BuffIntent()
			})
			{
				MustPerformOnceBeforeTransitioning = true
			};
			MoveState moveState = new MoveState("BITE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BiteMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.BiteDamage)
			});
			MoveState moveState2 = new MoveState("SKULL_BASH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SkullBashMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SkullBashDamage),
				new DebuffIntent(false)
			});
			MoveState moveState3 = new MoveState("POUNCE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.PounceMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.PounceDamage)
			});
			MoveState moveState4 = new MoveState("MULTI_CLAW_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.MultiClawMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.MultiClawDamage, () => this.MultiClawTotalCount)
			});
			MoveState moveState5 = new MoveState("PHASE3_LACERATE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.Phase3LacerateMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.Phase3LacerateDamage, 3)
			});
			MoveState moveState6 = new MoveState("BIG_POUNCE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BigPounceMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.BigPounceDamage)
			});
			MoveState moveState7 = new MoveState("BURNING_GROWL_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BurningGrowlMove), new AbstractIntent[]
			{
				new StatusIntent(this.BurningGrowlBurnCount),
				new BuffIntent()
			});
			ConditionalBranchState conditionalBranchState = new ConditionalBranchState("REVIVE_BRANCH");
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState;
			moveState3.FollowUpState = moveState4;
			moveState4.FollowUpState = moveState3;
			moveState5.FollowUpState = moveState6;
			moveState6.FollowUpState = moveState7;
			moveState7.FollowUpState = moveState5;
			this.DeadState.FollowUpState = conditionalBranchState;
			conditionalBranchState.AddState(moveState4, () => this.Respawns < 2);
			conditionalBranchState.AddState(moveState5, () => this.Respawns >= 2);
			list.Add(this.DeadState);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(moveState4);
			list.Add(moveState5);
			list.Add(moveState6);
			list.Add(moveState7);
			list.Add(conditionalBranchState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005EE9 RID: 24297 RVA: 0x0023DDBC File Offset: 0x0023BFBC
		private void AfterDeath(Creature _)
		{
			if (base.Creature.HasPower<AdaptablePower>())
			{
				return;
			}
			base.Creature.Died -= this.AfterDeath;
			base.Creature.PowerApplied -= this.AfterPowerApplied;
			base.Creature.PowerRemoved -= this.AfterPowerRemoved;
			NRunMusicController instance = NRunMusicController.Instance;
			if (instance != null)
			{
				instance.UpdateMusicParameter("test_subject_progress", 5f);
			}
			this.SetColor(Colors.White);
		}

		// Token: 0x06005EEA RID: 24298 RVA: 0x0023DE41 File Offset: 0x0023C041
		private void AfterPowerApplied(PowerModel power)
		{
			if (power is IntangiblePower)
			{
				this.SetColor(StsColors.halfTransparentWhite);
			}
		}

		// Token: 0x06005EEB RID: 24299 RVA: 0x0023DE56 File Offset: 0x0023C056
		private void AfterPowerRemoved(PowerModel power)
		{
			if (power is IntangiblePower)
			{
				this.SetColor(Colors.White);
			}
		}

		// Token: 0x06005EEC RID: 24300 RVA: 0x0023DE6C File Offset: 0x0023C06C
		private async Task RespawnMove(IReadOnlyList<Creature> targets)
		{
			int respawns = this.Respawns;
			this.Respawns = respawns + 1;
			NRunMusicController instance = NRunMusicController.Instance;
			if (instance != null)
			{
				instance.UpdateMusicParameter("test_subject_progress", 2f);
			}
			SfxCmd.Play((this.Respawns == 1) ? "event:/sfx/enemy/enemy_attacks/test_subject/test_subject_revive_two_heads" : "event:/sfx/enemy/enemy_attacks/test_subject/test_subject_revive_three_heads", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "RespawnTrigger", 0f);
			await Cmd.Wait(0.8f, false);
			NCombatRoom instance2 = NCombatRoom.Instance;
			if (instance2 != null)
			{
				NCreature creatureNode = instance2.GetCreatureNode(base.Creature);
				if (creatureNode != null)
				{
					creatureNode.SetDefaultScaleTo(1f + (float)this.Respawns * 0.1f, 0.1f);
				}
			}
			await Cmd.Wait(1.15f, false);
			base.Creature.GetPower<AdaptablePower>().DoRevive();
			int respawns2 = this.Respawns;
			if (respawns2 != 1)
			{
				if (respawns2 == 2)
				{
					await this.Revive(this.ThirdFormHp);
					await PowerCmd.Apply<NemesisPower>(base.Creature, 1m, base.Creature, null, false);
					await PowerCmd.Remove<AdaptablePower>(base.Creature);
					await PowerCmd.Remove<PainfulStabsPower>(base.Creature);
				}
			}
			else
			{
				await this.Revive(this.SecondFormHp);
				await PowerCmd.Apply<PainfulStabsPower>(base.Creature, 1m, base.Creature, null, false);
			}
		}

		// Token: 0x06005EED RID: 24301 RVA: 0x0023DEB0 File Offset: 0x0023C0B0
		private async Task BiteMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.BiteDamage).FromMonster(this).WithAttackerAnim("BiteTrigger", 0.25f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/test_subject/test_subject_bite", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005EEE RID: 24302 RVA: 0x0023DEF4 File Offset: 0x0023C0F4
		private async Task SkullBashMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SkullBashDamage).FromMonster(this).WithAttackerAnim("BiteTrigger", 0.25f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/test_subject/test_subject_bite", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			await PowerCmd.Apply<VulnerablePower>(targets, 1m, base.Creature, null, false);
		}

		// Token: 0x06005EEF RID: 24303 RVA: 0x0023DF40 File Offset: 0x0023C140
		private async Task PounceMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.PounceDamage).FromMonster(this).WithAttackerAnim("BiteTrigger", 0.25f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/test_subject/test_subject_bite", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005EF0 RID: 24304 RVA: 0x0023DF84 File Offset: 0x0023C184
		private async Task MultiClawMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.MultiClawDamage).WithHitCount(this.MultiClawTotalCount).FromMonster(this)
				.OnlyPlayAnimOnce()
				.WithAttackerAnim("MultiAttackTrigger", 0.2f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/test_subject/test_subject_slash", null)
				.Execute(null);
			this.ExtraMultiClawCount++;
		}

		// Token: 0x06005EF1 RID: 24305 RVA: 0x0023DFC8 File Offset: 0x0023C1C8
		private async Task Phase3LacerateMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.Phase3LacerateDamage).WithHitCount(3).FromMonster(this)
				.OnlyPlayAnimOnce()
				.WithAttackerAnim("MultiAttackTrigger", 0.2f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/test_subject/test_subject_slash", null)
				.Execute(null);
		}

		// Token: 0x06005EF2 RID: 24306 RVA: 0x0023E00C File Offset: 0x0023C20C
		private async Task BigPounceMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.BigPounceDamage).FromMonster(this).WithAttackerAnim("BiteTrigger", 0.25f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/test_subject/test_subject_bite", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005EF3 RID: 24307 RVA: 0x0023E050 File Offset: 0x0023C250
		private async Task BurningGrowlMove(IReadOnlyList<Creature> targets)
		{
			await CardPileCmd.AddToCombatAndPreview<Burn>(targets, PileType.Discard, this.BurningGrowlBurnCount, false, CardPilePosition.Bottom);
			await PowerCmd.Apply<StrengthPower>(base.Creature, this.BurningGrowlStrengthGain, base.Creature, null, false);
		}

		// Token: 0x06005EF4 RID: 24308 RVA: 0x0023E09C File Offset: 0x0023C29C
		private async Task Revive(int baseRespawnHp)
		{
			base.AssertMutable();
			int scaledHp = baseRespawnHp * base.Creature.CombatState.Players.Count;
			await CreatureCmd.SetMaxHp(base.Creature, scaledHp);
			await CreatureCmd.Heal(base.Creature, scaledHp, true);
		}

		// Token: 0x06005EF5 RID: 24309 RVA: 0x0023E0E8 File Offset: 0x0023C2E8
		private void SetColor(Color color)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(base.Creature) : null);
			if (ncreature != null)
			{
				CanvasGroup specialNode = ncreature.GetSpecialNode<CanvasGroup>("%CanvasGroup");
				if (specialNode == null)
				{
					return;
				}
				specialNode.SetSelfModulate(color);
			}
		}

		// Token: 0x06005EF6 RID: 24310 RVA: 0x0023E128 File Offset: 0x0023C328
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop1", true);
			AnimState animState2 = new AnimState("hurt1", false);
			AnimState animState3 = new AnimState("attack_double1", false);
			AnimState animState4 = new AnimState("attack_big1", false);
			AnimState animState5 = new AnimState("heal1", false);
			AnimState animState6 = new AnimState("knockout1", false);
			AnimState animState7 = new AnimState("knocked_out_loop1", true);
			AnimState animState8 = new AnimState("regenerate1", false)
			{
				BoundsContainer = "RespawnBounds1"
			};
			AnimState animState9 = new AnimState("idle_loop2", true);
			AnimState animState10 = new AnimState("hurt2", false);
			AnimState animState11 = new AnimState("attack_double2", false);
			AnimState animState12 = new AnimState("attack_big2", false);
			AnimState animState13 = new AnimState("heal2", false);
			AnimState animState14 = new AnimState("knockout2", false);
			AnimState animState15 = new AnimState("knocked_out_loop2", true);
			AnimState animState16 = new AnimState("regenerate2", false)
			{
				BoundsContainer = "RespawnBounds2"
			};
			AnimState animState17 = new AnimState("idle_loop3", true);
			AnimState animState18 = new AnimState("hurt3", false);
			AnimState animState19 = new AnimState("attack_double3", false);
			AnimState animState20 = new AnimState("attack_big3", false);
			AnimState animState21 = new AnimState("heal3", false);
			AnimState animState22 = new AnimState("die", false);
			animState5.NextState = animState;
			animState4.NextState = animState;
			animState3.NextState = animState;
			animState2.NextState = animState;
			animState6.NextState = animState7;
			animState8.NextState = animState9;
			animState13.NextState = animState9;
			animState12.NextState = animState9;
			animState11.NextState = animState9;
			animState10.NextState = animState9;
			animState14.NextState = animState15;
			animState16.NextState = animState17;
			animState21.NextState = animState17;
			animState20.NextState = animState17;
			animState19.NextState = animState17;
			animState18.NextState = animState17;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Hit", animState2, () => this.Respawns == 0);
			creatureAnimator.AddAnyState("BiteTrigger", animState4, () => this.Respawns == 0);
			creatureAnimator.AddAnyState("MultiAttackTrigger", animState3, () => this.Respawns == 0);
			creatureAnimator.AddAnyState("GrowthSpurtTrigger", animState5, () => this.Respawns == 0);
			creatureAnimator.AddAnyState("DeadTrigger", animState6, () => this.Respawns == 0);
			creatureAnimator.AddAnyState("RespawnTrigger", animState8, () => this.Respawns == 1);
			creatureAnimator.AddAnyState("Hit", animState10, () => this.Respawns == 1);
			creatureAnimator.AddAnyState("BiteTrigger", animState12, () => this.Respawns == 1);
			creatureAnimator.AddAnyState("MultiAttackTrigger", animState11, () => this.Respawns == 1);
			creatureAnimator.AddAnyState("GrowthSpurtTrigger", animState13, () => this.Respawns == 1);
			creatureAnimator.AddAnyState("DeadTrigger", animState14, () => this.Respawns == 1);
			creatureAnimator.AddAnyState("RespawnTrigger", animState16, () => this.Respawns >= 2);
			creatureAnimator.AddAnyState("Hit", animState18, () => this.Respawns >= 2);
			creatureAnimator.AddAnyState("BiteTrigger", animState20, () => this.Respawns >= 2);
			creatureAnimator.AddAnyState("MultiAttackTrigger", animState19, () => this.Respawns >= 2);
			creatureAnimator.AddAnyState("GrowthSpurtTrigger", animState21, () => this.Respawns >= 2);
			creatureAnimator.AddAnyState("Dead", animState22, null);
			return creatureAnimator;
		}

		// Token: 0x040023DF RID: 9183
		private const string _testSubjectCustomTrackName = "test_subject_progress";

		// Token: 0x040023E0 RID: 9184
		private const int _baseTestSubjectNum = 8;

		// Token: 0x040023E1 RID: 9185
		private const int _phase3LacerateRepeat = 3;

		// Token: 0x040023E2 RID: 9186
		private const string _growthSpurtTrigger = "GrowthSpurtTrigger";

		// Token: 0x040023E3 RID: 9187
		private const string _bigAttackTrigger = "BiteTrigger";

		// Token: 0x040023E4 RID: 9188
		private const string _multiAttackTrigger = "MultiAttackTrigger";

		// Token: 0x040023E5 RID: 9189
		private const string _deadTrigger = "DeadTrigger";

		// Token: 0x040023E6 RID: 9190
		private const string _respawnTrigger = "RespawnTrigger";

		// Token: 0x040023E7 RID: 9191
		private const string _biteSfx = "event:/sfx/enemy/enemy_attacks/test_subject/test_subject_bite";

		// Token: 0x040023E8 RID: 9192
		private const string _slashSfx = "event:/sfx/enemy/enemy_attacks/test_subject/test_subject_slash";

		// Token: 0x040023E9 RID: 9193
		private const string _knockOutSfx = "event:/sfx/enemy/enemy_attacks/test_subject/test_subject_knock_out";

		// Token: 0x040023EA RID: 9194
		private const string _reviveTwoHeadsSfx = "event:/sfx/enemy/enemy_attacks/test_subject/test_subject_revive_two_heads";

		// Token: 0x040023EB RID: 9195
		private const string _reviveThreeHeadsSfx = "event:/sfx/enemy/enemy_attacks/test_subject/test_subject_revive_three_heads";

		// Token: 0x040023EC RID: 9196
		private MoveState _deadState;

		// Token: 0x040023ED RID: 9197
		private int _respawns;

		// Token: 0x040023EE RID: 9198
		private int _extraMultiClawCount;
	}
}
