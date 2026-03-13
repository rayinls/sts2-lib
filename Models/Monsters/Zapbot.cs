using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Encounters;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x020007A5 RID: 1957
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Zapbot : MonsterModel
	{
		// Token: 0x170017D8 RID: 6104
		// (get) Token: 0x06006083 RID: 24707 RVA: 0x00242E3F File Offset: 0x0024103F
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 24, 23);
			}
		}

		// Token: 0x170017D9 RID: 6105
		// (get) Token: 0x06006084 RID: 24708 RVA: 0x00242E4B File Offset: 0x0024104B
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 29, 28);
			}
		}

		// Token: 0x170017DA RID: 6106
		// (get) Token: 0x06006085 RID: 24709 RVA: 0x00242E57 File Offset: 0x00241057
		private int ZapDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 15, 14);
			}
		}

		// Token: 0x170017DB RID: 6107
		// (get) Token: 0x06006086 RID: 24710 RVA: 0x00242E64 File Offset: 0x00241064
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x06006087 RID: 24711 RVA: 0x00242E68 File Offset: 0x00241068
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			if (TestMode.IsOff)
			{
				FabricatorNormal.SetBotFallPosition(NCombatRoom.Instance.GetCreatureNode(base.Creature));
			}
			await PowerCmd.Apply<HighVoltagePower>(base.Creature, 2m, base.Creature, null, false);
		}

		// Token: 0x06006088 RID: 24712 RVA: 0x00242EAC File Offset: 0x002410AC
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("ZAP", new Func<IReadOnlyList<Creature>, Task>(this.ZapMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.ZapDamage)
			});
			moveState.FollowUpState = moveState;
			list.Add(moveState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06006089 RID: 24713 RVA: 0x00242F00 File Offset: 0x00241100
		private async Task ZapMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.ZapDamage).FromMonster(this).WithAttackerAnim("Attack", 0.6f, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}
	}
}
