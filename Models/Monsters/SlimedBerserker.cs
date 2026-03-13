using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000782 RID: 1922
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SlimedBerserker : MonsterModel
	{
		// Token: 0x170016D6 RID: 5846
		// (get) Token: 0x06005E0B RID: 24075 RVA: 0x0023B4F7 File Offset: 0x002396F7
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 276, 266);
			}
		}

		// Token: 0x170016D7 RID: 5847
		// (get) Token: 0x06005E0C RID: 24076 RVA: 0x0023B509 File Offset: 0x00239709
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x170016D8 RID: 5848
		// (get) Token: 0x06005E0D RID: 24077 RVA: 0x0023B511 File Offset: 0x00239711
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Slime;
			}
		}

		// Token: 0x170016D9 RID: 5849
		// (get) Token: 0x06005E0E RID: 24078 RVA: 0x0023B514 File Offset: 0x00239714
		public override bool HasDeathSfx
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170016DA RID: 5850
		// (get) Token: 0x06005E0F RID: 24079 RVA: 0x0023B517 File Offset: 0x00239717
		protected override string CastSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/slimed_berserker/slimed_berserker_buff";
			}
		}

		// Token: 0x170016DB RID: 5851
		// (get) Token: 0x06005E10 RID: 24080 RVA: 0x0023B51E File Offset: 0x0023971E
		private string SlimeSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/slimed_berserker/slimed_berserker_slime";
			}
		}

		// Token: 0x170016DC RID: 5852
		// (get) Token: 0x06005E11 RID: 24081 RVA: 0x0023B525 File Offset: 0x00239725
		private int PummelingDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 5, 4);
			}
		}

		// Token: 0x170016DD RID: 5853
		// (get) Token: 0x06005E12 RID: 24082 RVA: 0x0023B530 File Offset: 0x00239730
		private int SmotherDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 33, 30);
			}
		}

		// Token: 0x06005E13 RID: 24083 RVA: 0x0023B540 File Offset: 0x00239740
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("VOMIT_ICHOR_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.VomitIchorMove), new AbstractIntent[]
			{
				new StatusIntent(10)
			});
			MoveState moveState2 = new MoveState("LEECHING_HUG_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.LeechingHugMove), new AbstractIntent[]
			{
				new DebuffIntent(false),
				new BuffIntent()
			});
			MoveState moveState3 = new MoveState("SMOTHER_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SmotherMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SmotherDamage)
			});
			MoveState moveState4 = new MoveState("FURIOUS_PUMMELING_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.FuriousPummelingMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.PummelingDamage, 4)
			});
			moveState.FollowUpState = moveState4;
			moveState4.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState3);
			list.Add(moveState2);
			list.Add(moveState4);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005E14 RID: 24084 RVA: 0x0023B644 File Offset: 0x00239844
		private async Task VomitIchorMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play(this.SlimeSfx, 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Vomit", 0.7f);
			await CardPileCmd.AddToCombatAndPreview<Slimed>(targets, PileType.Discard, 10, false, CardPilePosition.Bottom);
		}

		// Token: 0x06005E15 RID: 24085 RVA: 0x0023B690 File Offset: 0x00239890
		private async Task LeechingHugMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play(this.CastSfx, 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Hug", 0.65f);
			await PowerCmd.Apply<WeakPower>(targets, 3m, null, null, false);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 3m, base.Creature, null, false);
		}

		// Token: 0x06005E16 RID: 24086 RVA: 0x0023B6DC File Offset: 0x002398DC
		private async Task FuriousPummelingMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.PummelingDamage).WithHitCount(4).OnlyPlayAnimOnce()
				.FromMonster(this)
				.WithAttackerAnim("Attack", 0.2f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.Execute(null);
		}

		// Token: 0x06005E17 RID: 24087 RVA: 0x0023B720 File Offset: 0x00239920
		private async Task SmotherMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SmotherDamage).FromMonster(this).WithAttackerAnim("Attack", 0.2f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.Execute(null);
		}

		// Token: 0x06005E18 RID: 24088 RVA: 0x0023B764 File Offset: 0x00239964
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("hug", false);
			AnimState animState3 = new AnimState("vomit", false);
			AnimState animState4 = new AnimState("attack", false);
			AnimState animState5 = new AnimState("hurt", false);
			AnimState animState6 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			animState5.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Hug", animState2, null);
			creatureAnimator.AddAnyState("Vomit", animState3, null);
			creatureAnimator.AddAnyState("Attack", animState4, null);
			creatureAnimator.AddAnyState("Dead", animState6, null);
			creatureAnimator.AddAnyState("Hit", animState5, null);
			return creatureAnimator;
		}

		// Token: 0x040023AC RID: 9132
		private const int _pummelingRepeat = 4;

		// Token: 0x040023AD RID: 9133
		private const int _leechingDrain = 3;

		// Token: 0x040023AE RID: 9134
		private const int _vomitSlimeInDiscard = 10;

		// Token: 0x040023AF RID: 9135
		private const string _hugTrigger = "Hug";

		// Token: 0x040023B0 RID: 9136
		private const string _vomitTrigger = "Vomit";
	}
}
