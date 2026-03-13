using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.MonsterMoves;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000765 RID: 1893
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LeafSlimeS : MonsterModel
	{
		// Token: 0x17001617 RID: 5655
		// (get) Token: 0x06005C4C RID: 23628 RVA: 0x00236267 File Offset: 0x00234467
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 12, 11);
			}
		}

		// Token: 0x17001618 RID: 5656
		// (get) Token: 0x06005C4D RID: 23629 RVA: 0x00236273 File Offset: 0x00234473
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 16, 15);
			}
		}

		// Token: 0x17001619 RID: 5657
		// (get) Token: 0x06005C4E RID: 23630 RVA: 0x0023627F File Offset: 0x0023447F
		private int TackleDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 4, 3);
			}
		}

		// Token: 0x1700161A RID: 5658
		// (get) Token: 0x06005C4F RID: 23631 RVA: 0x0023628A File Offset: 0x0023448A
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Slime;
			}
		}

		// Token: 0x06005C50 RID: 23632 RVA: 0x00236290 File Offset: 0x00234490
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("BUTT_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.TackleMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.TackleDamage)
			});
			MoveState moveState2 = new MoveState("GOOP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.GoopMove), new AbstractIntent[]
			{
				new StatusIntent(1)
			});
			RandomBranchState randomBranchState = new RandomBranchState("RAND");
			moveState.FollowUpState = randomBranchState;
			moveState2.FollowUpState = randomBranchState;
			randomBranchState.AddBranch(moveState, MoveRepeatType.CannotRepeat);
			randomBranchState.AddBranch(moveState2, MoveRepeatType.CannotRepeat);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(randomBranchState);
			return new MonsterMoveStateMachine(list, randomBranchState);
		}

		// Token: 0x06005C51 RID: 23633 RVA: 0x0023633C File Offset: 0x0023453C
		private async Task TackleMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.TackleDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_slime_impact", null, null)
				.Execute(null);
		}

		// Token: 0x06005C52 RID: 23634 RVA: 0x00236380 File Offset: 0x00234580
		private async Task GoopMove(IReadOnlyList<Creature> targets)
		{
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.5f);
			SfxCmd.Play(this.AttackSfx, 1f);
			VfxCmd.PlayOnCreatureCenters(targets, "vfx/vfx_slime_impact");
			await CardPileCmd.AddToCombatAndPreview<Slimed>(targets, PileType.Discard, 1, false, CardPilePosition.Bottom);
		}

		// Token: 0x0400234A RID: 9034
		private const int _goopAmount = 1;
	}
}
