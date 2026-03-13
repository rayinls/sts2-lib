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
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200077D RID: 1917
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Seapunk : MonsterModel
	{
		// Token: 0x170016B8 RID: 5816
		// (get) Token: 0x06005DCE RID: 24014 RVA: 0x0023AA64 File Offset: 0x00238C64
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 47, 44);
			}
		}

		// Token: 0x170016B9 RID: 5817
		// (get) Token: 0x06005DCF RID: 24015 RVA: 0x0023AA70 File Offset: 0x00238C70
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 49, 46);
			}
		}

		// Token: 0x170016BA RID: 5818
		// (get) Token: 0x06005DD0 RID: 24016 RVA: 0x0023AA7C File Offset: 0x00238C7C
		private int SeaKickDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 12, 11);
			}
		}

		// Token: 0x170016BB RID: 5819
		// (get) Token: 0x06005DD1 RID: 24017 RVA: 0x0023AA89 File Offset: 0x00238C89
		private int SpinningKickDamage
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170016BC RID: 5820
		// (get) Token: 0x06005DD2 RID: 24018 RVA: 0x0023AA8C File Offset: 0x00238C8C
		private int SpinningKickRepeat
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x170016BD RID: 5821
		// (get) Token: 0x06005DD3 RID: 24019 RVA: 0x0023AA8F File Offset: 0x00238C8F
		private int BubbleBlock
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 8, 7);
			}
		}

		// Token: 0x170016BE RID: 5822
		// (get) Token: 0x06005DD4 RID: 24020 RVA: 0x0023AA99 File Offset: 0x00238C99
		private int BubbleStr
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 2, 1);
			}
		}

		// Token: 0x170016BF RID: 5823
		// (get) Token: 0x06005DD5 RID: 24021 RVA: 0x0023AAA4 File Offset: 0x00238CA4
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Fur;
			}
		}

		// Token: 0x170016C0 RID: 5824
		// (get) Token: 0x06005DD6 RID: 24022 RVA: 0x0023AAA7 File Offset: 0x00238CA7
		public override string HurtSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/seapunk/seapunk_hurt";
			}
		}

		// Token: 0x06005DD7 RID: 24023 RVA: 0x0023AAB0 File Offset: 0x00238CB0
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
		}

		// Token: 0x06005DD8 RID: 24024 RVA: 0x0023AAF4 File Offset: 0x00238CF4
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("SEA_KICK_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SeaKickMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SeaKickDamage)
			});
			MoveState moveState2 = new MoveState("SPINNING_KICK_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SpinningKickMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.SpinningKickDamage, this.SpinningKickRepeat)
			});
			MoveState moveState3 = new MoveState("BUBBLE_BURP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BubbleBurpMove), new AbstractIntent[]
			{
				new BuffIntent(),
				new DefendIntent()
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005DD9 RID: 24025 RVA: 0x0023ABC4 File Offset: 0x00238DC4
		private async Task SeaKickMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SeaKickDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/seapunk/seapunk_kick", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005DDA RID: 24026 RVA: 0x0023AC08 File Offset: 0x00238E08
		private async Task SpinningKickMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SpinningKickDamage).WithHitCount(this.SpinningKickRepeat).FromMonster(this)
				.WithAttackerAnim("MultiAttack", 0.15f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/seapunk/seapunk_kick_multi", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005DDB RID: 24027 RVA: 0x0023AC4C File Offset: 0x00238E4C
		private async Task BubbleBurpMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/seapunk/seapunk_buff", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.75f);
			await CreatureCmd.GainBlock(base.Creature, this.BubbleBlock, ValueProp.Move, null, false);
			await PowerCmd.Apply<StrengthPower>(base.Creature, this.BubbleStr, base.Creature, null, false);
		}

		// Token: 0x06005DDC RID: 24028 RVA: 0x0023AC90 File Offset: 0x00238E90
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("attack_multi", false);
			AnimState animState3 = new AnimState("cast", false);
			AnimState animState4 = new AnimState("attack", false);
			AnimState animState5 = new AnimState("hurt", false);
			AnimState animState6 = new AnimState("die", false);
			animState3.NextState = animState;
			animState4.NextState = animState;
			animState5.NextState = animState;
			animState2.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Cast", animState3, null);
			creatureAnimator.AddAnyState("Attack", animState4, null);
			creatureAnimator.AddAnyState("Dead", animState6, null);
			creatureAnimator.AddAnyState("Hit", animState5, null);
			creatureAnimator.AddAnyState("MultiAttack", animState2, null);
			return creatureAnimator;
		}

		// Token: 0x040023A6 RID: 9126
		private const string _multiAttackTrigger = "MultiAttack";

		// Token: 0x040023A7 RID: 9127
		private const string _buffSfx = "event:/sfx/enemy/enemy_attacks/seapunk/seapunk_buff";

		// Token: 0x040023A8 RID: 9128
		private const string _kickSfx = "event:/sfx/enemy/enemy_attacks/seapunk/seapunk_kick";

		// Token: 0x040023A9 RID: 9129
		private const string _kickMultiSfx = "event:/sfx/enemy/enemy_attacks/seapunk/seapunk_kick_multi";
	}
}
