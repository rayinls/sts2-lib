using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000788 RID: 1928
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SoulFysh : MonsterModel
	{
		// Token: 0x170016FB RID: 5883
		// (get) Token: 0x06005E63 RID: 24163 RVA: 0x0023C5EF File Offset: 0x0023A7EF
		public override string HurtSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/soul_fysh/soul_fysh_hurt";
			}
		}

		// Token: 0x170016FC RID: 5884
		// (get) Token: 0x06005E64 RID: 24164 RVA: 0x0023C5F6 File Offset: 0x0023A7F6
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 221, 211);
			}
		}

		// Token: 0x170016FD RID: 5885
		// (get) Token: 0x06005E65 RID: 24165 RVA: 0x0023C608 File Offset: 0x0023A808
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x170016FE RID: 5886
		// (get) Token: 0x06005E66 RID: 24166 RVA: 0x0023C610 File Offset: 0x0023A810
		private int DeGasDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 17, 16);
			}
		}

		// Token: 0x170016FF RID: 5887
		// (get) Token: 0x06005E67 RID: 24167 RVA: 0x0023C61D File Offset: 0x0023A81D
		private int ScreamDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 12, 11);
			}
		}

		// Token: 0x17001700 RID: 5888
		// (get) Token: 0x06005E68 RID: 24168 RVA: 0x0023C62A File Offset: 0x0023A82A
		private int GazeDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 8, 7);
			}
		}

		// Token: 0x17001701 RID: 5889
		// (get) Token: 0x06005E69 RID: 24169 RVA: 0x0023C635 File Offset: 0x0023A835
		private int BeckonMoveAmount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17001702 RID: 5890
		// (get) Token: 0x06005E6A RID: 24170 RVA: 0x0023C638 File Offset: 0x0023A838
		private int GazeMoveAmount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17001703 RID: 5891
		// (get) Token: 0x06005E6B RID: 24171 RVA: 0x0023C63B File Offset: 0x0023A83B
		private int ScreamMoveAmount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17001704 RID: 5892
		// (get) Token: 0x06005E6C RID: 24172 RVA: 0x0023C63E File Offset: 0x0023A83E
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Magic;
			}
		}

		// Token: 0x17001705 RID: 5893
		// (get) Token: 0x06005E6D RID: 24173 RVA: 0x0023C641 File Offset: 0x0023A841
		// (set) Token: 0x06005E6E RID: 24174 RVA: 0x0023C649 File Offset: 0x0023A849
		public bool IsInvisible
		{
			get
			{
				return this._isInvisible;
			}
			set
			{
				base.AssertMutable();
				this._isInvisible = value;
			}
		}

		// Token: 0x06005E6F RID: 24175 RVA: 0x0023C658 File Offset: 0x0023A858
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("BECKON_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BeckonMove), new AbstractIntent[]
			{
				new StatusIntent(this.BeckonMoveAmount)
			});
			MoveState moveState2 = new MoveState("DE_GAS_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.DeGasMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.DeGasDamage)
			});
			MoveState moveState3 = new MoveState("GAZE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.GazeMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.GazeDamage),
				new StatusIntent(this.GazeMoveAmount)
			});
			MoveState moveState4 = new MoveState("FADE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.FadeMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			MoveState moveState5 = new MoveState("SCREAM_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ScreamMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.ScreamDamage),
				new DebuffIntent(false)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState4;
			moveState4.FollowUpState = moveState5;
			moveState5.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(moveState5);
			list.Add(moveState4);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005E70 RID: 24176 RVA: 0x0023C7A8 File Offset: 0x0023A9A8
		private async Task BeckonMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/soul_fysh/soul_fysh_beckon", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "AttackBeckon", 0f);
			await Cmd.Wait(0.3f, false);
			VfxCmd.PlayOnCreatureCenter(base.Creature, "vfx/vfx_spooky_scream");
			await Cmd.CustomScaledWait(0f, 0.3f, false, default(CancellationToken));
			foreach (Creature creature in targets)
			{
				Player player = creature.Player ?? creature.PetOwner;
				CardPileAddResult[] statusCards = new CardPileAddResult[this.BeckonMoveAmount];
				CardModel cardModel = base.CombatState.CreateCard<Beckon>(player);
				CardPileAddResult[] array = statusCards;
				array[0] = await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Draw, false, CardPilePosition.Random);
				array = null;
				CardModel cardModel2 = base.CombatState.CreateCard<Beckon>(player);
				array = statusCards;
				array[1] = await CardPileCmd.AddGeneratedCardToCombat(cardModel2, PileType.Discard, false, CardPilePosition.Bottom);
				array = null;
				if (LocalContext.IsMe(player))
				{
					CardCmd.PreviewCardPileAdd(statusCards, 1.2f, CardPreviewStyle.HorizontalLayout);
					await Cmd.Wait(1f, false);
				}
				player = null;
				statusCards = null;
			}
			IEnumerator<Creature> enumerator = null;
		}

		// Token: 0x06005E71 RID: 24177 RVA: 0x0023C7F4 File Offset: 0x0023A9F4
		private async Task GazeMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.GazeDamage).FromMonster(this).WithAttackerAnim("AttackBeckon", 0.6f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/soul_fysh/soul_fysh_beckon", null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			foreach (Creature creature in targets)
			{
				Player player = creature.Player ?? creature.PetOwner;
				CardPileAddResult[] statusCards = new CardPileAddResult[1];
				CardModel cardModel = base.CombatState.CreateCard<Beckon>(player);
				CardPileAddResult[] array = statusCards;
				array[0] = await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Discard, false, CardPilePosition.Bottom);
				array = null;
				if (LocalContext.IsMe(player))
				{
					CardCmd.PreviewCardPileAdd(statusCards, 1.2f, CardPreviewStyle.HorizontalLayout);
					await Cmd.Wait(1f, false);
				}
				player = null;
				statusCards = null;
			}
			IEnumerator<Creature> enumerator = null;
		}

		// Token: 0x06005E72 RID: 24178 RVA: 0x0023C840 File Offset: 0x0023AA40
		private async Task DeGasMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.DeGasDamage).FromMonster(this).WithAttackerAnim("Attack", 0.45f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005E73 RID: 24179 RVA: 0x0023C884 File Offset: 0x0023AA84
		private async Task ScreamMove(IReadOnlyList<Creature> targets)
		{
			this.IsInvisible = false;
			await DamageCmd.Attack(this.ScreamDamage).FromMonster(this).WithAttackerAnim("AttackDebuffTrigger", 0.65f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/soul_fysh/soul_fysh_wave", null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			await PowerCmd.Apply<VulnerablePower>(targets, this.ScreamMoveAmount, base.Creature, null, false);
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/soul_fysh/soul_fysh_reappear", 1f);
		}

		// Token: 0x06005E74 RID: 24180 RVA: 0x0023C8D0 File Offset: 0x0023AAD0
		private async Task FadeMove(IReadOnlyList<Creature> targets)
		{
			this.IsInvisible = true;
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/soul_fysh/soul_fysh_intangible", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "IntangibleStart", 0.8f);
			await PowerCmd.Apply<IntangiblePower>(base.Creature, 2m, base.Creature, null, false);
		}

		// Token: 0x06005E75 RID: 24181 RVA: 0x0023C914 File Offset: 0x0023AB14
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("cast", false);
			AnimState animState3 = new AnimState("attack_heavy", false);
			AnimState animState4 = new AnimState("attack_beckon", false);
			AnimState animState5 = new AnimState("hurt", false);
			AnimState animState6 = new AnimState("die", false);
			AnimState animState7 = new AnimState("intangible_loop", true);
			AnimState animState8 = new AnimState("intangible_start", false);
			AnimState animState9 = new AnimState("intangible_end", false);
			AnimState animState10 = new AnimState("hurt_intangible", false);
			AnimState animState11 = new AnimState("die_intangible", false);
			AnimState animState12 = new AnimState("attack_debuff", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			animState5.NextState = animState;
			animState8.NextState = animState7;
			animState10.NextState = animState7;
			animState12.NextState = animState9;
			animState9.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("AttackBeckon", animState4, null);
			creatureAnimator.AddAnyState("IntangibleStart", animState8, null);
			creatureAnimator.AddAnyState("AttackDebuffTrigger", animState12, null);
			creatureAnimator.AddAnyState("Dead", animState6, () => !this.IsInvisible);
			creatureAnimator.AddAnyState("Hit", animState5, () => !this.IsInvisible);
			creatureAnimator.AddAnyState("Dead", animState11, () => this.IsInvisible);
			creatureAnimator.AddAnyState("Hit", animState10, () => this.IsInvisible);
			return creatureAnimator;
		}

		// Token: 0x040023C5 RID: 9157
		private const string _intangibleSfx = "event:/sfx/enemy/enemy_attacks/soul_fysh/soul_fysh_intangible";

		// Token: 0x040023C6 RID: 9158
		private const string _beckonSfx = "event:/sfx/enemy/enemy_attacks/soul_fysh/soul_fysh_beckon";

		// Token: 0x040023C7 RID: 9159
		private const string _waveSfx = "event:/sfx/enemy/enemy_attacks/soul_fysh/soul_fysh_wave";

		// Token: 0x040023C8 RID: 9160
		private const string _reappearSfx = "event:/sfx/enemy/enemy_attacks/soul_fysh/soul_fysh_reappear";

		// Token: 0x040023C9 RID: 9161
		private const string _attackBeckonTrigger = "AttackBeckon";

		// Token: 0x040023CA RID: 9162
		private const string _intangibleStartTrigger = "IntangibleStart";

		// Token: 0x040023CB RID: 9163
		private const string _attackDebuffTrigger = "AttackDebuffTrigger";

		// Token: 0x040023CC RID: 9164
		private bool _isInvisible;
	}
}
