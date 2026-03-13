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
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000781 RID: 1921
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SkulkingColony : MonsterModel
	{
		// Token: 0x170016CD RID: 5837
		// (get) Token: 0x06005DFA RID: 24058 RVA: 0x0023B227 File Offset: 0x00239427
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 84, 79);
			}
		}

		// Token: 0x170016CE RID: 5838
		// (get) Token: 0x06005DFB RID: 24059 RVA: 0x0023B233 File Offset: 0x00239433
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x170016CF RID: 5839
		// (get) Token: 0x06005DFC RID: 24060 RVA: 0x0023B23B File Offset: 0x0023943B
		private int SuperCrabDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 7, 6);
			}
		}

		// Token: 0x170016D0 RID: 5840
		// (get) Token: 0x06005DFD RID: 24061 RVA: 0x0023B246 File Offset: 0x00239446
		private int SuperCrabRepeat
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170016D1 RID: 5841
		// (get) Token: 0x06005DFE RID: 24062 RVA: 0x0023B249 File Offset: 0x00239449
		private int ZoomDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 17, 16);
			}
		}

		// Token: 0x170016D2 RID: 5842
		// (get) Token: 0x06005DFF RID: 24063 RVA: 0x0023B256 File Offset: 0x00239456
		private int SmashDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 11, 9);
			}
		}

		// Token: 0x170016D3 RID: 5843
		// (get) Token: 0x06005E00 RID: 24064 RVA: 0x0023B263 File Offset: 0x00239463
		private int InertiaBlock
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 13, 10);
			}
		}

		// Token: 0x170016D4 RID: 5844
		// (get) Token: 0x06005E01 RID: 24065 RVA: 0x0023B26F File Offset: 0x0023946F
		public override bool HasDeathSfx
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170016D5 RID: 5845
		// (get) Token: 0x06005E02 RID: 24066 RVA: 0x0023B272 File Offset: 0x00239472
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x06005E03 RID: 24067 RVA: 0x0023B278 File Offset: 0x00239478
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<HardenedShellPower>(base.Creature, 20m, base.Creature, null, false);
		}

		// Token: 0x06005E04 RID: 24068 RVA: 0x0023B2BC File Offset: 0x002394BC
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("INERTIA_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.InertiaMove), new AbstractIntent[]
			{
				new DefendIntent(),
				new BuffIntent()
			});
			MoveState moveState2 = new MoveState("ZOOM_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ZoomMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.ZoomDamage)
			});
			MoveState moveState3 = new MoveState("SUPER_CRAB_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SuperCrabMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.SuperCrabDamage, this.SuperCrabRepeat)
			});
			MoveState moveState4 = new MoveState("SMASH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SmashMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SmashDamage),
				new StatusIntent(4)
			});
			moveState4.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState;
			moveState.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState4;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(moveState4);
			return new MonsterMoveStateMachine(list, moveState4);
		}

		// Token: 0x06005E05 RID: 24069 RVA: 0x0023B3D0 File Offset: 0x002395D0
		private async Task InertiaMove(IReadOnlyList<Creature> targets)
		{
			await CreatureCmd.TriggerAnim(base.Creature, "Attack", 0.15f);
			await CreatureCmd.GainBlock(base.Creature, this.InertiaBlock, ValueProp.Move, null, false);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 3m, base.Creature, null, false);
		}

		// Token: 0x06005E06 RID: 24070 RVA: 0x0023B414 File Offset: 0x00239614
		private async Task SuperCrabMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SuperCrabDamage).WithHitCount(this.SuperCrabRepeat).FromMonster(this)
				.WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005E07 RID: 24071 RVA: 0x0023B458 File Offset: 0x00239658
		private async Task ZoomMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.ZoomDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005E08 RID: 24072 RVA: 0x0023B49C File Offset: 0x0023969C
		private async Task SmashMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SmashDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			await CardPileCmd.AddToCombatAndPreview<Dazed>(targets, PileType.Discard, 4, false, CardPilePosition.Bottom);
		}

		// Token: 0x040023AB RID: 9131
		private const int _smashStatusCount = 4;
	}
}
