using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Godot;
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
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Nodes.Vfx.Cards;
using MegaCrit.Sts2.Core.Nodes.Vfx.Utilities;
using MegaCrit.Sts2.Core.Random;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200073E RID: 1854
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CeremonialBeast : MonsterModel
	{
		// Token: 0x17001501 RID: 5377
		// (get) Token: 0x060059B5 RID: 22965 RVA: 0x0022E145 File Offset: 0x0022C345
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 262, 252);
			}
		}

		// Token: 0x17001502 RID: 5378
		// (get) Token: 0x060059B6 RID: 22966 RVA: 0x0022E157 File Offset: 0x0022C357
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001503 RID: 5379
		// (get) Token: 0x060059B7 RID: 22967 RVA: 0x0022E15F File Offset: 0x0022C35F
		private int PlowAmount
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 160, 150);
			}
		}

		// Token: 0x17001504 RID: 5380
		// (get) Token: 0x060059B8 RID: 22968 RVA: 0x0022E172 File Offset: 0x0022C372
		private int PlowDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 20, 18);
			}
		}

		// Token: 0x17001505 RID: 5381
		// (get) Token: 0x060059B9 RID: 22969 RVA: 0x0022E17F File Offset: 0x0022C37F
		private int PlowStrength
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17001506 RID: 5382
		// (get) Token: 0x060059BA RID: 22970 RVA: 0x0022E182 File Offset: 0x0022C382
		private int StompDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 17, 15);
			}
		}

		// Token: 0x17001507 RID: 5383
		// (get) Token: 0x060059BB RID: 22971 RVA: 0x0022E18F File Offset: 0x0022C38F
		private int CrushDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 19, 17);
			}
		}

		// Token: 0x17001508 RID: 5384
		// (get) Token: 0x060059BC RID: 22972 RVA: 0x0022E19C File Offset: 0x0022C39C
		private int CrushStrength
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 4, 3);
			}
		}

		// Token: 0x17001509 RID: 5385
		// (get) Token: 0x060059BD RID: 22973 RVA: 0x0022E1A7 File Offset: 0x0022C3A7
		public override bool ShouldDisappearFromDoom
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700150A RID: 5386
		// (get) Token: 0x060059BE RID: 22974 RVA: 0x0022E1AA File Offset: 0x0022C3AA
		public override bool ShouldFadeAfterDeath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700150B RID: 5387
		// (get) Token: 0x060059BF RID: 22975 RVA: 0x0022E1AD File Offset: 0x0022C3AD
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/ceremonial_beast/ceremonial_beast_die";
			}
		}

		// Token: 0x1700150C RID: 5388
		// (get) Token: 0x060059C0 RID: 22976 RVA: 0x0022E1B4 File Offset: 0x0022C3B4
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Fur;
			}
		}

		// Token: 0x1700150D RID: 5389
		// (get) Token: 0x060059C1 RID: 22977 RVA: 0x0022E1B7 File Offset: 0x0022C3B7
		// (set) Token: 0x060059C2 RID: 22978 RVA: 0x0022E1BF File Offset: 0x0022C3BF
		private bool IsStunnedByPlowRemoval
		{
			get
			{
				return this._isStunnedByPlowRemoval;
			}
			set
			{
				base.AssertMutable();
				this._isStunnedByPlowRemoval = value;
			}
		}

		// Token: 0x1700150E RID: 5390
		// (get) Token: 0x060059C3 RID: 22979 RVA: 0x0022E1CE File Offset: 0x0022C3CE
		private bool ShouldPlayRegularHurtAnim
		{
			get
			{
				return !this.IsStunnedByPlowRemoval && !this.InMidCharge;
			}
		}

		// Token: 0x1700150F RID: 5391
		// (get) Token: 0x060059C4 RID: 22980 RVA: 0x0022E1E3 File Offset: 0x0022C3E3
		// (set) Token: 0x060059C5 RID: 22981 RVA: 0x0022E1EB File Offset: 0x0022C3EB
		private bool InMidCharge
		{
			get
			{
				return this._inMidCharge;
			}
			set
			{
				base.AssertMutable();
				this._inMidCharge = value;
			}
		}

		// Token: 0x17001510 RID: 5392
		// (get) Token: 0x060059C6 RID: 22982 RVA: 0x0022E1FA File Offset: 0x0022C3FA
		// (set) Token: 0x060059C7 RID: 22983 RVA: 0x0022E202 File Offset: 0x0022C402
		public MoveState BeastCryState
		{
			get
			{
				return this._beastCryState;
			}
			set
			{
				base.AssertMutable();
				this._beastCryState = value;
			}
		}

		// Token: 0x060059C8 RID: 22984 RVA: 0x0022E214 File Offset: 0x0022C414
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("STAMP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.StampMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			MoveState moveState2 = new MoveState("PLOW_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.PlowMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.PlowDamage),
				new BuffIntent()
			});
			MoveState moveState3 = new MoveState("STUN_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.StunnedMove), new AbstractIntent[]
			{
				new StunIntent()
			})
			{
				MustPerformOnceBeforeTransitioning = true
			};
			this.BeastCryState = new MoveState("BEAST_CRY_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BeastCryMove), new AbstractIntent[]
			{
				new DebuffIntent(false)
			});
			MoveState moveState4 = new MoveState("STOMP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.StompMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.StompDamage)
			});
			MoveState moveState5 = new MoveState("CRUSH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.CrushMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.CrushDamage),
				new BuffIntent()
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState2;
			moveState3.FollowUpState = this.BeastCryState;
			this.BeastCryState.FollowUpState = moveState4;
			moveState4.FollowUpState = moveState5;
			moveState5.FollowUpState = this.BeastCryState;
			list.Add(moveState2);
			list.Add(moveState);
			list.Add(moveState3);
			list.Add(this.BeastCryState);
			list.Add(moveState4);
			list.Add(moveState5);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x060059C9 RID: 22985 RVA: 0x0022E3AC File Offset: 0x0022C5AC
		private async Task StampMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play(this.AttackSfx, 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Attack", 0.6f);
			await Cmd.CustomScaledWait(0f, 0.4f, false, default(CancellationToken));
			await PowerCmd.Apply<PlowPower>(base.Creature, this.PlowAmount, base.Creature, null, false);
		}

		// Token: 0x060059CA RID: 22986 RVA: 0x0022E3F0 File Offset: 0x0022C5F0
		private async Task PlowMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/ceremonial_beast/ceremonial_beast_plow", 1f);
			this.InMidCharge = true;
			await CreatureCmd.TriggerAnim(base.Creature, "Plow", 0f);
			await Cmd.Wait(0.5f, false);
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.CombatVfxContainer.AddChildSafely(NHorizontalLinesVfx.Create(new Color("BFFFC880"), 1.2000000476837158, false));
			}
			await Cmd.Wait(0.5f, false);
			NCombatRoom instance2 = NCombatRoom.Instance;
			if (instance2 != null)
			{
				instance2.RadialBlur(VfxPosition.Left);
			}
			VfxCmd.PlayOnCreatureCenters(targets, "vfx/vfx_attack_blunt");
			using (IEnumerator<Creature> enumerator = targets.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					Creature creature = enumerator.Current;
					NCombatRoom instance3 = NCombatRoom.Instance;
					if (instance3 != null)
					{
						instance3.CombatVfxContainer.AddChildSafely(NLineBurstVfx.Create(creature));
					}
				}
			}
			NGame instance4 = NGame.Instance;
			if (instance4 != null)
			{
				instance4.ScreenShake(ShakeStrength.Strong, ShakeDuration.Normal, 180f + Rng.Chaotic.NextFloat(-10f, 10f));
			}
			await DamageCmd.Attack(this.PlowDamage).FromMonster(this).WithNoAttackerAnim()
				.Execute(null);
			NGame instance5 = NGame.Instance;
			if (instance5 != null)
			{
				instance5.DoHitStop(ShakeStrength.Strong, ShakeDuration.Normal);
			}
			this.InMidCharge = false;
			await Cmd.Wait(0.2f, false);
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/ceremonial_beast/ceremonial_beast_plow_end", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "EndPlow", 0f);
			await Cmd.Wait(0.5f, false);
			await PowerCmd.Apply<StrengthPower>(base.Creature, this.PlowStrength, base.Creature, null, false);
		}

		// Token: 0x060059CB RID: 22987 RVA: 0x0022E43C File Offset: 0x0022C63C
		public async Task SetStunned()
		{
			this.IsStunnedByPlowRemoval = true;
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/ceremonial_beast/ceremonial_beast_stun", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Stun", 0.6f);
		}

		// Token: 0x060059CC RID: 22988 RVA: 0x0022E480 File Offset: 0x0022C680
		public async Task StunnedMove(IReadOnlyList<Creature> targets)
		{
			this.IsStunnedByPlowRemoval = false;
			await CreatureCmd.TriggerAnim(base.Creature, "Unstun", 0.6f);
		}

		// Token: 0x060059CD RID: 22989 RVA: 0x0022E4C4 File Offset: 0x0022C6C4
		private async Task BeastCryMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/ceremonial_beast/ceremonial_beast_shrill", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0f);
			await Cmd.Wait(0.3f, false);
			VfxCmd.PlayOnCreatureCenter(base.Creature, "vfx/vfx_scream");
			await Cmd.Wait(0.75f, false);
			await PowerCmd.Apply<RingingPower>(targets, 1m, base.Creature, null, false);
		}

		// Token: 0x060059CE RID: 22990 RVA: 0x0022E510 File Offset: 0x0022C710
		private async Task StompMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.StompDamage).FromMonster(this).WithAttackerAnim("Attack", 1f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.AfterAttackerAnim(delegate
				{
					NGame instance = NGame.Instance;
					if (instance != null)
					{
						instance.ScreenShake(ShakeStrength.Strong, ShakeDuration.Normal, 180f + Rng.Chaotic.NextFloat(-10f, 10f));
					}
					return Task.CompletedTask;
				})
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.WithHitVfxNode((Creature _) => NSpikeSplashVfx.Create(base.Creature, VfxColor.Cyan))
				.Execute(null);
		}

		// Token: 0x060059CF RID: 22991 RVA: 0x0022E554 File Offset: 0x0022C754
		private async Task CrushMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play(this.AttackSfx, 1f);
			await DamageCmd.Attack(this.CrushDamage).FromMonster(this).WithAttackerAnim("Attack", 1f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.AfterAttackerAnim(delegate
				{
					NGame instance = NGame.Instance;
					if (instance != null)
					{
						instance.ScreenShake(ShakeStrength.Strong, ShakeDuration.Normal, 180f + Rng.Chaotic.NextFloat(-10f, 10f));
					}
					return Task.CompletedTask;
				})
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.WithHitVfxNode((Creature _) => NSpikeSplashVfx.Create(base.Creature, VfxColor.Cyan))
				.Execute(null);
			await PowerCmd.Apply<StrengthPower>(base.Creature, this.CrushStrength, base.Creature, null, false);
		}

		// Token: 0x060059D0 RID: 22992 RVA: 0x0022E598 File Offset: 0x0022C798
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("shrill", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("plow", false);
			AnimState animState5 = new AnimState("plow_end", false);
			AnimState animState6 = new AnimState("plow_end_die", false);
			AnimState animState7 = new AnimState("stun", false);
			AnimState animState8 = new AnimState("stun_loop", true);
			AnimState animState9 = new AnimState("wake_up", false);
			AnimState animState10 = new AnimState("hurt", false);
			AnimState animState11 = new AnimState("die", false);
			animState.AddBranch("Plow", animState4, null);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState10.NextState = animState;
			animState4.AddBranch("EndPlow", animState5, null);
			animState5.NextState = animState;
			animState7.NextState = animState8;
			animState9.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Unstun", animState9, null);
			creatureAnimator.AddAnyState("Dead", animState11, () => !this.InMidCharge);
			creatureAnimator.AddAnyState("Dead", animState6, () => this.InMidCharge);
			animState.AddBranch("Hit", animState10, () => this.ShouldPlayRegularHurtAnim);
			animState2.AddBranch("Hit", animState10, () => this.ShouldPlayRegularHurtAnim);
			animState10.AddBranch("Hit", animState10, () => this.ShouldPlayRegularHurtAnim);
			animState3.AddBranch("Hit", animState10, () => this.ShouldPlayRegularHurtAnim);
			animState7.AddBranch("Hit", animState10, () => this.ShouldPlayRegularHurtAnim);
			animState8.AddBranch("Hit", animState10, () => this.ShouldPlayRegularHurtAnim);
			animState9.AddBranch("Hit", animState10, () => this.ShouldPlayRegularHurtAnim);
			animState.AddBranch("Hit", animState7, () => this.IsStunnedByPlowRemoval);
			animState2.AddBranch("Hit", animState7, () => this.IsStunnedByPlowRemoval);
			animState10.AddBranch("Hit", animState7, () => this.IsStunnedByPlowRemoval);
			animState3.AddBranch("Hit", animState7, () => this.IsStunnedByPlowRemoval);
			animState7.AddBranch("Hit", animState7, () => this.IsStunnedByPlowRemoval);
			animState8.AddBranch("Hit", animState7, () => this.IsStunnedByPlowRemoval);
			animState9.AddBranch("Hit", animState7, () => this.IsStunnedByPlowRemoval);
			creatureAnimator.AddAnyState("Stun", animState7, null);
			creatureAnimator.AddAnyState("Plow", animState4, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("PlowHit", animState10, null);
			return creatureAnimator;
		}

		// Token: 0x060059D1 RID: 22993 RVA: 0x0022E878 File Offset: 0x0022CA78
		public unsafe override List<BestiaryMonsterMove> MonsterMoveList(NCreatureVisuals creatureVisuals)
		{
			creatureVisuals.SetUpSkin(this);
			int num = 6;
			List<BestiaryMonsterMove> list = new List<BestiaryMonsterMove>(num);
			CollectionsMarshal.SetCount<BestiaryMonsterMove>(list, num);
			Span<BestiaryMonsterMove> span = CollectionsMarshal.AsSpan<BestiaryMonsterMove>(list);
			int num2 = 0;
			*span[num2] = new BestiaryMonsterMove(base.GetBestiaryMoveName("STOMP"), this.BestiaryAttackAnimId, this.AttackSfx, 0f);
			num2++;
			*span[num2] = new BestiaryMonsterMove(base.GetBestiaryMoveName("PLOW"), "plow", "event:/sfx/enemy/enemy_attacks/ceremonial_beast/ceremonial_beast_plow", 0f);
			num2++;
			*span[num2] = new BestiaryMonsterMove(base.GetBestiaryMoveName("BEAST_CRY"), "shrill", "event:/sfx/enemy/enemy_attacks/ceremonial_beast/ceremonial_beast_shrill", 0f);
			num2++;
			*span[num2] = new BestiaryMonsterMove("Stun", "stun", "event:/sfx/enemy/enemy_attacks/ceremonial_beast/ceremonial_beast_stun", 0f);
			num2++;
			*span[num2] = new BestiaryMonsterMove("Hurt", "hurt", this.TakeDamageSfx, 0f);
			num2++;
			*span[num2] = new BestiaryMonsterMove("Die", "die", this.DeathSfx, 0f);
			return list;
		}

		// Token: 0x0400229D RID: 8861
		private const string _plowTrigger = "Plow";

		// Token: 0x0400229E RID: 8862
		private const string _plowEndTrigger = "EndPlow";

		// Token: 0x0400229F RID: 8863
		private const string _stunTrigger = "Stun";

		// Token: 0x040022A0 RID: 8864
		private const string _unStunTrigger = "Unstun";

		// Token: 0x040022A1 RID: 8865
		private const string _plowHitTrigger = "PlowHit";

		// Token: 0x040022A2 RID: 8866
		private const string _plowSfx = "event:/sfx/enemy/enemy_attacks/ceremonial_beast/ceremonial_beast_plow";

		// Token: 0x040022A3 RID: 8867
		private const string _plowEndSfx = "event:/sfx/enemy/enemy_attacks/ceremonial_beast/ceremonial_beast_plow_end";

		// Token: 0x040022A4 RID: 8868
		private const string _shrillSfx = "event:/sfx/enemy/enemy_attacks/ceremonial_beast/ceremonial_beast_shrill";

		// Token: 0x040022A5 RID: 8869
		private const string _stunSfx = "event:/sfx/enemy/enemy_attacks/ceremonial_beast/ceremonial_beast_stun";

		// Token: 0x040022A6 RID: 8870
		private bool _isStunnedByPlowRemoval;

		// Token: 0x040022A7 RID: 8871
		private bool _inMidCharge;

		// Token: 0x040022A8 RID: 8872
		private MoveState _beastCryState;
	}
}
