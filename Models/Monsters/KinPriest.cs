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
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Audio;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000761 RID: 1889
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class KinPriest : MonsterModel
	{
		// Token: 0x170015F5 RID: 5621
		// (get) Token: 0x06005BF1 RID: 23537 RVA: 0x00235060 File Offset: 0x00233260
		public override string BestiaryAttackAnimId
		{
			get
			{
				return "attack_grenade";
			}
		}

		// Token: 0x170015F6 RID: 5622
		// (get) Token: 0x06005BF2 RID: 23538 RVA: 0x00235067 File Offset: 0x00233267
		protected override string CastSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/the_kin_priest/the_kin_priest_cast";
			}
		}

		// Token: 0x170015F7 RID: 5623
		// (get) Token: 0x06005BF3 RID: 23539 RVA: 0x0023506E File Offset: 0x0023326E
		public override string HurtSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/the_kin_priest/the_kin_priest_hurt";
			}
		}

		// Token: 0x170015F8 RID: 5624
		// (get) Token: 0x06005BF4 RID: 23540 RVA: 0x00235075 File Offset: 0x00233275
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/the_kin_priest/the_kin_priest_die";
			}
		}

		// Token: 0x170015F9 RID: 5625
		// (get) Token: 0x06005BF5 RID: 23541 RVA: 0x0023507C File Offset: 0x0023327C
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 199, 190);
			}
		}

		// Token: 0x170015FA RID: 5626
		// (get) Token: 0x06005BF6 RID: 23542 RVA: 0x0023508E File Offset: 0x0023328E
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x170015FB RID: 5627
		// (get) Token: 0x06005BF7 RID: 23543 RVA: 0x00235096 File Offset: 0x00233296
		private int OrbOfFrailtyDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 9, 8);
			}
		}

		// Token: 0x170015FC RID: 5628
		// (get) Token: 0x06005BF8 RID: 23544 RVA: 0x002350A2 File Offset: 0x002332A2
		private int OrbOfWeaknessDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 9, 8);
			}
		}

		// Token: 0x170015FD RID: 5629
		// (get) Token: 0x06005BF9 RID: 23545 RVA: 0x002350AE File Offset: 0x002332AE
		private int BeamDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 3, 3);
			}
		}

		// Token: 0x170015FE RID: 5630
		// (get) Token: 0x06005BFA RID: 23546 RVA: 0x002350B9 File Offset: 0x002332B9
		private int RitualStrength
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 3, 2);
			}
		}

		// Token: 0x170015FF RID: 5631
		// (get) Token: 0x06005BFB RID: 23547 RVA: 0x002350C4 File Offset: 0x002332C4
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Fur;
			}
		}

		// Token: 0x17001600 RID: 5632
		// (get) Token: 0x06005BFC RID: 23548 RVA: 0x002350C7 File Offset: 0x002332C7
		// (set) Token: 0x06005BFD RID: 23549 RVA: 0x002350CF File Offset: 0x002332CF
		private bool SpeechUsed
		{
			get
			{
				return this._speechUsed;
			}
			set
			{
				base.AssertMutable();
				this._speechUsed = value;
			}
		}

		// Token: 0x06005BFE RID: 23550 RVA: 0x002350E0 File Offset: 0x002332E0
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			base.Creature.Died += this.AfterDeath;
		}

		// Token: 0x06005BFF RID: 23551 RVA: 0x00235123 File Offset: 0x00233323
		private void AfterDeath(Creature _)
		{
			base.Creature.Died -= this.AfterDeath;
			NRunMusicController instance = NRunMusicController.Instance;
			if (instance == null)
			{
				return;
			}
			instance.UpdateMusicParameter("the_kin_progress", 5f);
		}

		// Token: 0x06005C00 RID: 23552 RVA: 0x00235158 File Offset: 0x00233358
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("ORB_OF_FRAILTY_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.OrbOfFrailtyMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.OrbOfFrailtyDamage),
				new DebuffIntent(false)
			});
			MoveState moveState2 = new MoveState("ORB_OF_WEAKNESS_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.OrbOfWeaknessMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.OrbOfWeaknessDamage),
				new DebuffIntent(false)
			});
			MoveState moveState3 = new MoveState("BEAM_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BeamMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.BeamDamage, 3)
			});
			MoveState moveState4 = new MoveState("RITUAL_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.RitualMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState4;
			moveState4.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(moveState4);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005C01 RID: 23553 RVA: 0x00235268 File Offset: 0x00233468
		private async Task OrbOfFrailtyMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.OrbOfFrailtyDamage).FromMonster(this).WithAttackerAnim("AttackGrenade", 0f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/the_kin_priest/the_kin_priest_soul_grenade", null)
				.WithWaitBeforeHit(1f, 1f)
				.WithHitVfxNode((Creature t) => NKinPriestGrenadeVfx.Create(t))
				.Execute(null);
			await PowerCmd.Apply<FrailPower>(targets, 1m, base.Creature, null, false);
		}

		// Token: 0x06005C02 RID: 23554 RVA: 0x002352B4 File Offset: 0x002334B4
		private async Task OrbOfWeaknessMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.OrbOfWeaknessDamage).FromMonster(this).WithAttackerAnim("AttackGrenade", 0f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/the_kin_priest/the_kin_priest_soul_grenade", null)
				.WithWaitBeforeHit(1f, 1f)
				.WithHitVfxNode((Creature t) => NKinPriestGrenadeVfx.Create(t))
				.Execute(null);
			await PowerCmd.Apply<WeakPower>(targets, 1m, base.Creature, null, false);
		}

		// Token: 0x06005C03 RID: 23555 RVA: 0x00235300 File Offset: 0x00233500
		private async Task BeamMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.BeamDamage).WithHitCount(3).FromMonster(this)
				.WithAttackerAnim("AttackLaser", 0.4f, null)
				.AfterAttackerAnim(delegate
				{
					NCombatRoom instance = NCombatRoom.Instance;
					NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(base.Creature) : null);
					if (ncreature != null)
					{
						NKinPriestBeamVfx specialNode = ncreature.GetSpecialNode<NKinPriestBeamVfx>("Visuals/Beam");
						if (specialNode != null)
						{
							specialNode.Fire();
						}
					}
					SfxCmd.Play("event:/sfx/enemy/enemy_attacks/the_kin_priest/the_kin_priest_soul_beam", 1f);
					return Task.CompletedTask;
				})
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.OnlyPlayAnimOnce()
				.Execute(null);
		}

		// Token: 0x06005C04 RID: 23556 RVA: 0x00235344 File Offset: 0x00233544
		private async Task RitualMove(IReadOnlyList<Creature> targets)
		{
			if (!this.SpeechUsed)
			{
				this.SpeechUsed = true;
				TalkCmd.Play(KinPriest._ritualApplyLine, base.Creature, 1.0, VfxColor.White);
			}
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/the_kin_priest/the_kin_priest_rally", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Rally", 1f);
			await PowerCmd.Apply<StrengthPower>(base.Creature, this.RitualStrength, base.Creature, null, false);
		}

		// Token: 0x06005C05 RID: 23557 RVA: 0x00235388 File Offset: 0x00233588
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("rally", false);
			AnimState animState3 = new AnimState("attack_grenade", false);
			AnimState animState4 = new AnimState("attack_laser", false);
			AnimState animState5 = new AnimState("hurt", false);
			AnimState animState6 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			animState5.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Rally", animState2, null);
			creatureAnimator.AddAnyState("AttackGrenade", animState3, null);
			creatureAnimator.AddAnyState("AttackLaser", animState4, null);
			creatureAnimator.AddAnyState("Dead", animState6, null);
			creatureAnimator.AddAnyState("Hit", animState5, null);
			return creatureAnimator;
		}

		// Token: 0x06005C06 RID: 23558 RVA: 0x0023544F File Offset: 0x0023364F
		public void AllFollowerDeathResponse()
		{
			TalkCmd.Play(KinPriest._followersDeathLine, base.Creature, 1.0, VfxColor.White);
		}

		// Token: 0x04002321 RID: 8993
		public const string theKinCustomTrackName = "the_kin_progress";

		// Token: 0x04002322 RID: 8994
		private static readonly LocString _ritualApplyLine = MonsterModel.L10NMonsterLookup("KIN_PRIEST.moves.RITUAL.speakLine1");

		// Token: 0x04002323 RID: 8995
		private static readonly LocString _followersDeathLine = MonsterModel.L10NMonsterLookup("KIN_PRIEST.followersDeathLine");

		// Token: 0x04002324 RID: 8996
		private const string _grenadeTrigger = "AttackGrenade";

		// Token: 0x04002325 RID: 8997
		private const string _laserTrigger = "AttackLaser";

		// Token: 0x04002326 RID: 8998
		private const string _rallyTrigger = "Rally";

		// Token: 0x04002327 RID: 8999
		private const string _attackGrenadeAnimId = "attack_grenade";

		// Token: 0x04002328 RID: 9000
		private const string _soulBeamSfx = "event:/sfx/enemy/enemy_attacks/the_kin_priest/the_kin_priest_soul_beam";

		// Token: 0x04002329 RID: 9001
		private const string _soulGrenadeSfx = "event:/sfx/enemy/enemy_attacks/the_kin_priest/the_kin_priest_soul_grenade";

		// Token: 0x0400232A RID: 9002
		private const string _rallySfx = "event:/sfx/enemy/enemy_attacks/the_kin_priest/the_kin_priest_rally";

		// Token: 0x0400232B RID: 9003
		private const int _beamRepeat = 3;

		// Token: 0x0400232C RID: 9004
		private bool _speechUsed;
	}
}
