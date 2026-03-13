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
	// Token: 0x02000768 RID: 1896
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LouseProgenitor : MonsterModel
	{
		// Token: 0x1700162B RID: 5675
		// (get) Token: 0x06005C78 RID: 23672 RVA: 0x00236936 File Offset: 0x00234B36
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 138, 134);
			}
		}

		// Token: 0x1700162C RID: 5676
		// (get) Token: 0x06005C79 RID: 23673 RVA: 0x00236948 File Offset: 0x00234B48
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 141, 136);
			}
		}

		// Token: 0x1700162D RID: 5677
		// (get) Token: 0x06005C7A RID: 23674 RVA: 0x0023695A File Offset: 0x00234B5A
		protected override string AttackSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/giant_louse/giant_louse_attack";
			}
		}

		// Token: 0x1700162E RID: 5678
		// (get) Token: 0x06005C7B RID: 23675 RVA: 0x00236961 File Offset: 0x00234B61
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/giant_louse/giant_louse_die";
			}
		}

		// Token: 0x1700162F RID: 5679
		// (get) Token: 0x06005C7C RID: 23676 RVA: 0x00236968 File Offset: 0x00234B68
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Insect;
			}
		}

		// Token: 0x17001630 RID: 5680
		// (get) Token: 0x06005C7D RID: 23677 RVA: 0x0023696B File Offset: 0x00234B6B
		// (set) Token: 0x06005C7E RID: 23678 RVA: 0x00236973 File Offset: 0x00234B73
		public bool Curled
		{
			get
			{
				return this._curled;
			}
			set
			{
				base.AssertMutable();
				this._curled = value;
			}
		}

		// Token: 0x17001631 RID: 5681
		// (get) Token: 0x06005C7F RID: 23679 RVA: 0x00236982 File Offset: 0x00234B82
		private int WebDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 10, 9);
			}
		}

		// Token: 0x17001632 RID: 5682
		// (get) Token: 0x06005C80 RID: 23680 RVA: 0x0023698F File Offset: 0x00234B8F
		private int PounceDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 16, 14);
			}
		}

		// Token: 0x17001633 RID: 5683
		// (get) Token: 0x06005C81 RID: 23681 RVA: 0x0023699C File Offset: 0x00234B9C
		private int CurlBlock
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 18, 14);
			}
		}

		// Token: 0x06005C82 RID: 23682 RVA: 0x002369A8 File Offset: 0x00234BA8
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<CurlUpPower>(base.Creature, this.CurlBlock, base.Creature, null, false);
		}

		// Token: 0x06005C83 RID: 23683 RVA: 0x002369EC File Offset: 0x00234BEC
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("WEB_CANNON_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.WebMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.WebDamage),
				new DebuffIntent(false)
			});
			MoveState moveState2 = new MoveState("POUNCE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.PounceMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.PounceDamage)
			});
			MoveState moveState3 = new MoveState("CURL_AND_GROW_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.CurlAndGrowMove), new AbstractIntent[]
			{
				new DefendIntent(),
				new BuffIntent()
			});
			moveState.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState;
			list.Add(moveState3);
			list.Add(moveState);
			list.Add(moveState2);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005C84 RID: 23684 RVA: 0x00236ABC File Offset: 0x00234CBC
		private async Task WebMove(IReadOnlyList<Creature> targets)
		{
			if (this.Curled)
			{
				SfxCmd.Play("event:/sfx/enemy/enemy_attacks/giant_louse/giant_louse_uncurl", 1f);
				await CreatureCmd.TriggerAnim(base.Creature, "Uncurl", 0.9f);
				this.Curled = false;
			}
			await DamageCmd.Attack(this.WebDamage).FromMonster(this).WithAttackerAnim("Web", 0.2f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/giant_louse/giant_louse_attack_web", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			await PowerCmd.Apply<FrailPower>(targets, 2m, base.Creature, null, false);
		}

		// Token: 0x06005C85 RID: 23685 RVA: 0x00236B08 File Offset: 0x00234D08
		private async Task CurlAndGrowMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/giant_louse/giant_louse_curl", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Curl", 0.25f);
			await CreatureCmd.GainBlock(base.Creature, this.CurlBlock, ValueProp.Move, null, false);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 5m, base.Creature, null, false);
			this.Curled = true;
		}

		// Token: 0x06005C86 RID: 23686 RVA: 0x00236B4C File Offset: 0x00234D4C
		private async Task PounceMove(IReadOnlyList<Creature> targets)
		{
			if (this.Curled)
			{
				SfxCmd.Play("event:/sfx/enemy/enemy_attacks/giant_louse/giant_louse_uncurl", 1f);
				await CreatureCmd.TriggerAnim(base.Creature, "Uncurl", 0.9f);
				this.Curled = false;
			}
			await DamageCmd.Attack(this.PounceDamage).FromMonster(this).WithAttackerAnim("Attack", 0.2f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.Execute(null);
		}

		// Token: 0x06005C87 RID: 23687 RVA: 0x00236B90 File Offset: 0x00234D90
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("curl", false);
			AnimState animState3 = new AnimState("uncurl", false);
			AnimState animState4 = new AnimState("curled_loop", true);
			AnimState animState5 = new AnimState("attack", false);
			AnimState animState6 = new AnimState("attack_web", false);
			AnimState animState7 = new AnimState("hurt", false);
			AnimState animState8 = new AnimState("die", false);
			AnimState animState9 = new AnimState("die_curled", false);
			animState.AddBranch("Curl", animState2, null);
			animState.AddBranch("Attack", animState5, null);
			animState.AddBranch("Web", animState6, null);
			animState.AddBranch("Hit", animState7, null);
			animState.AddBranch("Dead", animState8, null);
			animState2.NextState = animState4;
			animState4.AddBranch("Uncurl", animState3, null);
			animState4.AddBranch("Dead", animState9, null);
			animState5.NextState = animState;
			animState5.AddBranch("Hit", animState7, null);
			animState5.AddBranch("Dead", animState8, null);
			animState6.NextState = animState;
			animState6.AddBranch("Hit", animState7, null);
			animState6.AddBranch("Dead", animState8, null);
			animState7.NextState = animState;
			animState7.AddBranch("Hit", animState7, null);
			animState7.AddBranch("Dead", animState8, null);
			animState7.AddBranch("Curl", animState2, null);
			animState3.NextState = animState;
			return new CreatureAnimator(animState, controller);
		}

		// Token: 0x04002350 RID: 9040
		public const string curlTrigger = "Curl";

		// Token: 0x04002351 RID: 9041
		private const string _uncurlTrigger = "Uncurl";

		// Token: 0x04002352 RID: 9042
		private const string _webTrigger = "Web";

		// Token: 0x04002353 RID: 9043
		private const string _webSfx = "event:/sfx/enemy/enemy_attacks/giant_louse/giant_louse_attack_web";

		// Token: 0x04002354 RID: 9044
		public const string curlSfx = "event:/sfx/enemy/enemy_attacks/giant_louse/giant_louse_curl";

		// Token: 0x04002355 RID: 9045
		private const string _uncurlSfx = "event:/sfx/enemy/enemy_attacks/giant_louse/giant_louse_uncurl";

		// Token: 0x04002356 RID: 9046
		private bool _curled;

		// Token: 0x04002357 RID: 9047
		private const int _webFrail = 2;

		// Token: 0x04002358 RID: 9048
		private const int _growStrength = 5;
	}
}
