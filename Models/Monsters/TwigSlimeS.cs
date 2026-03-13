using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200079F RID: 1951
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TwigSlimeS : MonsterModel
	{
		// Token: 0x170017A7 RID: 6055
		// (get) Token: 0x06006005 RID: 24581 RVA: 0x00241507 File Offset: 0x0023F707
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 8, 7);
			}
		}

		// Token: 0x170017A8 RID: 6056
		// (get) Token: 0x06006006 RID: 24582 RVA: 0x00241511 File Offset: 0x0023F711
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 12, 11);
			}
		}

		// Token: 0x170017A9 RID: 6057
		// (get) Token: 0x06006007 RID: 24583 RVA: 0x0024151D File Offset: 0x0023F71D
		private int TackleDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 5, 4);
			}
		}

		// Token: 0x170017AA RID: 6058
		// (get) Token: 0x06006008 RID: 24584 RVA: 0x00241528 File Offset: 0x0023F728
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Slime;
			}
		}

		// Token: 0x06006009 RID: 24585 RVA: 0x0024152C File Offset: 0x0023F72C
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("BUTT_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.TackleMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.TackleDamage)
			});
			moveState.FollowUpState = moveState;
			list.Add(moveState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x0600600A RID: 24586 RVA: 0x00241580 File Offset: 0x0023F780
		private async Task TackleMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.TackleDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_slime_impact", null, null)
				.Execute(null);
		}
	}
}
