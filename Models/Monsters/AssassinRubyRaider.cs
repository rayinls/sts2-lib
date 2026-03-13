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
	// Token: 0x0200072E RID: 1838
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class AssassinRubyRaider : MonsterModel
	{
		// Token: 0x170014B3 RID: 5299
		// (get) Token: 0x06005904 RID: 22788 RVA: 0x0022C19F File Offset: 0x0022A39F
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 19, 18);
			}
		}

		// Token: 0x170014B4 RID: 5300
		// (get) Token: 0x06005905 RID: 22789 RVA: 0x0022C1AB File Offset: 0x0022A3AB
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 24, 23);
			}
		}

		// Token: 0x170014B5 RID: 5301
		// (get) Token: 0x06005906 RID: 22790 RVA: 0x0022C1B7 File Offset: 0x0022A3B7
		private static int KillshotDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 12, 11);
			}
		}

		// Token: 0x170014B6 RID: 5302
		// (get) Token: 0x06005907 RID: 22791 RVA: 0x0022C1C4 File Offset: 0x0022A3C4
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x06005908 RID: 22792 RVA: 0x0022C1C8 File Offset: 0x0022A3C8
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("KILLSHOT_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.KillshotMove), new AbstractIntent[]
			{
				new SingleAttackIntent(AssassinRubyRaider.KillshotDamage)
			});
			moveState.FollowUpState = moveState;
			list.Add(moveState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005909 RID: 22793 RVA: 0x0022C21C File Offset: 0x0022A41C
		private async Task KillshotMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(AssassinRubyRaider.KillshotDamage).FromMonster(this).WithAttackerAnim("Attack", 0.5f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}
	}
}
