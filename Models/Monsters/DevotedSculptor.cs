using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000749 RID: 1865
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DevotedSculptor : MonsterModel
	{
		// Token: 0x17001552 RID: 5458
		// (get) Token: 0x06005A7C RID: 23164 RVA: 0x00230592 File Offset: 0x0022E792
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 172, 162);
			}
		}

		// Token: 0x17001553 RID: 5459
		// (get) Token: 0x06005A7D RID: 23165 RVA: 0x002305A4 File Offset: 0x0022E7A4
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001554 RID: 5460
		// (get) Token: 0x06005A7E RID: 23166 RVA: 0x002305AC File Offset: 0x0022E7AC
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Fur;
			}
		}

		// Token: 0x17001555 RID: 5461
		// (get) Token: 0x06005A7F RID: 23167 RVA: 0x002305AF File Offset: 0x0022E7AF
		private int SavageDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 15, 12);
			}
		}

		// Token: 0x06005A80 RID: 23168 RVA: 0x002305BC File Offset: 0x0022E7BC
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("FORBIDDEN_INCANTATION_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ForbiddenIncantationMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			MoveState moveState2 = new MoveState("SAVAGE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SavageMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SavageDamage)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState2;
			list.Add(moveState);
			list.Add(moveState2);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005A81 RID: 23169 RVA: 0x00230644 File Offset: 0x0022E844
		private async Task ForbiddenIncantationMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play(this.CastSfx, 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0f);
			await Cmd.Wait(0.3f, false);
			VfxCmd.PlayOnCreatureCenter(base.Creature, "vfx/vfx_scream");
			TalkCmd.Play(DevotedSculptor._forbiddenIncantationDialogue, base.Creature, -1.0, VfxColor.White);
			await Cmd.CustomScaledWait(0.75f, 1f, false, default(CancellationToken));
			await PowerCmd.Apply<RitualPower>(base.Creature, this._ritualGain, null, null, false);
		}

		// Token: 0x06005A82 RID: 23170 RVA: 0x00230688 File Offset: 0x0022E888
		private async Task SavageMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SavageDamage).FromMonster(this).WithAttackerAnim("Attack", 0.3f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x040022CD RID: 8909
		private static readonly LocString _forbiddenIncantationDialogue = new LocString("monsters", "DEVOTED_SCULPTOR.moves.FORBIDDEN_INCANTATION.banter");

		// Token: 0x040022CE RID: 8910
		private readonly int _ritualGain = 9;
	}
}
