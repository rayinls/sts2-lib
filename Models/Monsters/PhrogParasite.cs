using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000778 RID: 1912
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PhrogParasite : MonsterModel
	{
		// Token: 0x17001690 RID: 5776
		// (get) Token: 0x06005D6A RID: 23914 RVA: 0x002397B0 File Offset: 0x002379B0
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 66, 61);
			}
		}

		// Token: 0x17001691 RID: 5777
		// (get) Token: 0x06005D6B RID: 23915 RVA: 0x002397BC File Offset: 0x002379BC
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 68, 64);
			}
		}

		// Token: 0x17001692 RID: 5778
		// (get) Token: 0x06005D6C RID: 23916 RVA: 0x002397C8 File Offset: 0x002379C8
		private int LashDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 5, 4);
			}
		}

		// Token: 0x17001693 RID: 5779
		// (get) Token: 0x06005D6D RID: 23917 RVA: 0x002397D3 File Offset: 0x002379D3
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Plant;
			}
		}

		// Token: 0x06005D6E RID: 23918 RVA: 0x002397D8 File Offset: 0x002379D8
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<InfestedPower>(base.Creature, 4m, base.Creature, null, false);
		}

		// Token: 0x06005D6F RID: 23919 RVA: 0x0023981C File Offset: 0x00237A1C
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("INFECT_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.InfectMove), new AbstractIntent[]
			{
				new StatusIntent(3)
			});
			MoveState moveState2 = new MoveState("LASH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.LashMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.LashDamage, 4)
			});
			RandomBranchState randomBranchState = new RandomBranchState("RAND");
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState;
			randomBranchState.AddBranch(moveState, MoveRepeatType.CannotRepeat);
			randomBranchState.AddBranch(moveState2, MoveRepeatType.CannotRepeat);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(randomBranchState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005D70 RID: 23920 RVA: 0x002398C8 File Offset: 0x00237AC8
		private async Task LashMove(IReadOnlyList<Creature> targets)
		{
			AttackCommand attackCommand = DamageCmd.Attack(this.LashDamage).WithHitCount(4).FromMonster(this)
				.WithAttackerAnim("Attack", 0.55f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, this.AttackSfx, null);
			Func<Creature, Node2D> func;
			if ((func = PhrogParasite.<>O.<0>__Create) == null)
			{
				func = (PhrogParasite.<>O.<0>__Create = new Func<Creature, Node2D>(NWormyImpactVfx.Create));
			}
			await attackCommand.WithHitVfxNode(func).Execute(null);
		}

		// Token: 0x06005D71 RID: 23921 RVA: 0x0023990C File Offset: 0x00237B0C
		private async Task InfectMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play(this.CastSfx, 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.75f);
			foreach (Creature creature in targets)
			{
				NWormyImpactVfx nwormyImpactVfx = NWormyImpactVfx.Create(creature);
				if (nwormyImpactVfx != null)
				{
					NCombatRoom.Instance.CombatVfxContainer.AddChildSafely(nwormyImpactVfx);
				}
			}
			await CardPileCmd.AddToCombatAndPreview<Infection>(targets, PileType.Discard, 3, false, CardPilePosition.Bottom);
		}

		// Token: 0x0400238E RID: 9102
		private const int _lashRepeat = 4;

		// Token: 0x0400238F RID: 9103
		private const int _infestAmt = 3;

		// Token: 0x02001C31 RID: 7217
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400708C RID: 28812
			[Nullable(new byte[] { 0, 1, 2 })]
			public static Func<Creature, Node2D> <0>__Create;
		}
	}
}
