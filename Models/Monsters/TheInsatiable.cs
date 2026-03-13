using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
using MegaCrit.Sts2.Core.Nodes.Audio;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000794 RID: 1940
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TheInsatiable : MonsterModel
	{
		// Token: 0x1700175B RID: 5979
		// (get) Token: 0x06005F41 RID: 24385 RVA: 0x0023EE27 File Offset: 0x0023D027
		public static string TheInsatiableTrackName
		{
			get
			{
				return "insatiable_progress";
			}
		}

		// Token: 0x1700175C RID: 5980
		// (get) Token: 0x06005F42 RID: 24386 RVA: 0x0023EE2E File Offset: 0x0023D02E
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 341, 321);
			}
		}

		// Token: 0x1700175D RID: 5981
		// (get) Token: 0x06005F43 RID: 24387 RVA: 0x0023EE40 File Offset: 0x0023D040
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x1700175E RID: 5982
		// (get) Token: 0x06005F44 RID: 24388 RVA: 0x0023EE48 File Offset: 0x0023D048
		private int ThrashDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 9, 8);
			}
		}

		// Token: 0x1700175F RID: 5983
		// (get) Token: 0x06005F45 RID: 24389 RVA: 0x0023EE54 File Offset: 0x0023D054
		private int BiteDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 31, 28);
			}
		}

		// Token: 0x17001760 RID: 5984
		// (get) Token: 0x06005F46 RID: 24390 RVA: 0x0023EE61 File Offset: 0x0023D061
		private int SalivateStrength
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 3, 2);
			}
		}

		// Token: 0x17001761 RID: 5985
		// (get) Token: 0x06005F47 RID: 24391 RVA: 0x0023EE6C File Offset: 0x0023D06C
		// (set) Token: 0x06005F48 RID: 24392 RVA: 0x0023EE74 File Offset: 0x0023D074
		private bool HasLiquified
		{
			get
			{
				return this._hasLiquified;
			}
			set
			{
				base.AssertMutable();
				this._hasLiquified = value;
			}
		}

		// Token: 0x17001762 RID: 5986
		// (get) Token: 0x06005F49 RID: 24393 RVA: 0x0023EE83 File Offset: 0x0023D083
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Insect;
			}
		}

		// Token: 0x06005F4A RID: 24394 RVA: 0x0023EE88 File Offset: 0x0023D088
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			base.Creature.Died += this.AfterDeath;
		}

		// Token: 0x06005F4B RID: 24395 RVA: 0x0023EECB File Offset: 0x0023D0CB
		private void AfterDeath(Creature _)
		{
			base.Creature.Died -= this.AfterDeath;
			NRunMusicController instance = NRunMusicController.Instance;
			if (instance == null)
			{
				return;
			}
			instance.UpdateMusicParameter(TheInsatiable.TheInsatiableTrackName, 10f);
		}

		// Token: 0x06005F4C RID: 24396 RVA: 0x0023EF00 File Offset: 0x0023D100
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("LIQUIFY_GROUND_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.LiquifyMove), new AbstractIntent[]
			{
				new BuffIntent(),
				new StatusIntent(6)
			});
			MoveState moveState2 = new MoveState("THRASH_MOVE_1", new Func<IReadOnlyList<Creature>, Task>(this.ThrashMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.ThrashDamage, 2)
			});
			MoveState moveState3 = new MoveState("THRASH_MOVE_2", new Func<IReadOnlyList<Creature>, Task>(this.ThrashMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.ThrashDamage, 2)
			});
			MoveState moveState4 = new MoveState("LUNGING_BITE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BiteMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.BiteDamage)
			});
			MoveState moveState5 = new MoveState("SALIVATE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SalivateMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState4;
			moveState4.FollowUpState = moveState5;
			moveState5.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState2;
			list.Add(moveState);
			list.Add(moveState4);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(moveState5);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005F4D RID: 24397 RVA: 0x0023F040 File Offset: 0x0023D240
		private async Task LiquifyMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/the_insatiable/the_insatiable_liquify_ground", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "LiquifySand", 0f);
			await Cmd.Wait(0.5f, false);
			VfxCmd.PlayOnCreatureCenter(base.Creature, "vfx/vfx_scream");
			await Cmd.Wait(0.75f, false);
			foreach (Creature creature in targets)
			{
				SandpitPower sandpitPower = (SandpitPower)ModelDb.Power<SandpitPower>().ToMutable(0);
				sandpitPower.Target = creature;
				await PowerCmd.Apply(sandpitPower, base.Creature, 4m, base.Creature, null, false);
			}
			IEnumerator<Creature> enumerator = null;
			foreach (Creature creature2 in targets)
			{
				Player player = creature2.Player ?? creature2.PetOwner;
				List<CardPileAddResult> statusCards = new List<CardPileAddResult>();
				for (int i = 0; i < 6; i++)
				{
					CardModel cardModel = base.CombatState.CreateCard<FranticEscape>(player);
					PileType pileType = ((i < 3) ? PileType.Draw : PileType.Discard);
					List<CardPileAddResult> list = statusCards;
					list.Add(await CardPileCmd.AddGeneratedCardToCombat(cardModel, pileType, false, CardPilePosition.Random));
					list = null;
				}
				if (LocalContext.IsMe(player))
				{
					CardCmd.PreviewCardPileAdd(statusCards, 1.2f, CardPreviewStyle.HorizontalLayout);
					await Cmd.Wait(1f, false);
				}
				player = null;
				statusCards = null;
			}
			enumerator = null;
			this.HasLiquified = true;
		}

		// Token: 0x06005F4E RID: 24398 RVA: 0x0023F08C File Offset: 0x0023D28C
		private async Task ThrashMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.ThrashDamage).WithHitCount(2).FromMonster(this)
				.WithHitFx("vfx/vfx_scratch", null, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/the_insatiable/the_insatiable_thrash", null)
				.WithAttackerAnim("Thrash", 0.3f, null)
				.OnlyPlayAnimOnce()
				.Execute(null);
		}

		// Token: 0x06005F4F RID: 24399 RVA: 0x0023F0D0 File Offset: 0x0023D2D0
		private async Task BiteMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.BiteDamage).FromMonster(this).WithAttackerAnim("Bite", 0.25f, null)
				.OnlyPlayAnimOnce()
				.WithHitFx("vfx/vfx_bite", null, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/the_insatiable/the_insatiable_lunging_bite", null)
				.Execute(null);
		}

		// Token: 0x06005F50 RID: 24400 RVA: 0x0023F114 File Offset: 0x0023D314
		private async Task SalivateMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/the_insatiable/the_insatiable_salivate", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Salivate", 0.5f);
			await PowerCmd.Apply<StrengthPower>(base.Creature, this.SalivateStrength, base.Creature, null, false);
		}

		// Token: 0x06005F51 RID: 24401 RVA: 0x0023F158 File Offset: 0x0023D358
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("intro_loop", true);
			AnimState animState2 = new AnimState("liquify_sand", false);
			AnimState animState3 = new AnimState("idle_loop", true);
			AnimState animState4 = new AnimState("salivate", false);
			AnimState animState5 = new AnimState("attack_thrash", false);
			AnimState animState6 = new AnimState("attack_bite", false);
			AnimState animState7 = new AnimState("eat_player", false);
			AnimState animState8 = new AnimState("intro_hurt", false);
			AnimState animState9 = new AnimState("hurt", false);
			AnimState animState10 = new AnimState("die", false);
			animState2.NextState = animState3;
			animState4.NextState = animState3;
			animState5.NextState = animState3;
			animState6.NextState = animState3;
			animState9.NextState = animState3;
			animState7.NextState = animState3;
			animState8.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Cast", animState4, null);
			creatureAnimator.AddAnyState("Attack", animState5, null);
			creatureAnimator.AddAnyState("Dead", animState10, null);
			creatureAnimator.AddAnyState("EatPlayerTrigger", animState7, null);
			creatureAnimator.AddAnyState("LiquifySand", animState2, null);
			creatureAnimator.AddAnyState("Salivate", animState4, null);
			creatureAnimator.AddAnyState("Thrash", animState5, null);
			creatureAnimator.AddAnyState("Bite", animState6, null);
			creatureAnimator.AddAnyState("Hit", animState8, () => !this.HasLiquified);
			creatureAnimator.AddAnyState("Hit", animState9, () => this.HasLiquified);
			return creatureAnimator;
		}

		// Token: 0x040023EF RID: 9199
		private const int _liquifyStatusDrawCount = 3;

		// Token: 0x040023F0 RID: 9200
		private const int _liquifyStatusDiscardCount = 3;

		// Token: 0x040023F1 RID: 9201
		private const int _thrashRepeat = 2;

		// Token: 0x040023F2 RID: 9202
		private const string _liquifySandTrigger = "LiquifySand";

		// Token: 0x040023F3 RID: 9203
		private const string _salivateTrigger = "Salivate";

		// Token: 0x040023F4 RID: 9204
		private const string _biteTrigger = "Bite";

		// Token: 0x040023F5 RID: 9205
		private const string _thrashTrigger = "Thrash";

		// Token: 0x040023F6 RID: 9206
		public const string eatPlayerTrigger = "EatPlayerTrigger";

		// Token: 0x040023F7 RID: 9207
		public const string finisherSfx = "event:/sfx/enemy/enemy_attacks/the_insatiable/the_insatiable_finisher";

		// Token: 0x040023F8 RID: 9208
		private const string _liquifyGroundSfx = "event:/sfx/enemy/enemy_attacks/the_insatiable/the_insatiable_liquify_ground";

		// Token: 0x040023F9 RID: 9209
		private const string _lungingBiteSfx = "event:/sfx/enemy/enemy_attacks/the_insatiable/the_insatiable_lunging_bite";

		// Token: 0x040023FA RID: 9210
		private const string _salivateSfx = "event:/sfx/enemy/enemy_attacks/the_insatiable/the_insatiable_salivate";

		// Token: 0x040023FB RID: 9211
		private const string _thrashSfx = "event:/sfx/enemy/enemy_attacks/the_insatiable/the_insatiable_thrash";

		// Token: 0x040023FC RID: 9212
		private bool _hasLiquified;
	}
}
