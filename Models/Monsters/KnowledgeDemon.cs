using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Audio;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000762 RID: 1890
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class KnowledgeDemon : MonsterModel
	{
		// Token: 0x17001601 RID: 5633
		// (get) Token: 0x06005C0B RID: 23563 RVA: 0x002354EE File Offset: 0x002336EE
		// (set) Token: 0x06005C0C RID: 23564 RVA: 0x002354F6 File Offset: 0x002336F6
		private int CurseOfKnowledgeCounter
		{
			get
			{
				return this._curseOfKnowledgeCounter;
			}
			set
			{
				base.AssertMutable();
				this._curseOfKnowledgeCounter = value;
			}
		}

		// Token: 0x17001602 RID: 5634
		// (get) Token: 0x06005C0D RID: 23565 RVA: 0x00235505 File Offset: 0x00233705
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 399, 379);
			}
		}

		// Token: 0x17001603 RID: 5635
		// (get) Token: 0x06005C0E RID: 23566 RVA: 0x00235517 File Offset: 0x00233717
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001604 RID: 5636
		// (get) Token: 0x06005C0F RID: 23567 RVA: 0x0023551F File Offset: 0x0023371F
		private int SlapDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 18, 17);
			}
		}

		// Token: 0x17001605 RID: 5637
		// (get) Token: 0x06005C10 RID: 23568 RVA: 0x0023552C File Offset: 0x0023372C
		private int PonderDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 13, 11);
			}
		}

		// Token: 0x17001606 RID: 5638
		// (get) Token: 0x06005C11 RID: 23569 RVA: 0x00235539 File Offset: 0x00233739
		private int KnowledgeOverwhelmingDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 9, 8);
			}
		}

		// Token: 0x17001607 RID: 5639
		// (get) Token: 0x06005C12 RID: 23570 RVA: 0x00235545 File Offset: 0x00233745
		private int PonderStrength
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 3, 2);
			}
		}

		// Token: 0x17001608 RID: 5640
		// (get) Token: 0x06005C13 RID: 23571 RVA: 0x00235550 File Offset: 0x00233750
		// (set) Token: 0x06005C14 RID: 23572 RVA: 0x00235558 File Offset: 0x00233758
		public bool IsBurnt
		{
			get
			{
				return this._isBurnt;
			}
			set
			{
				base.AssertMutable();
				this._isBurnt = value;
			}
		}

		// Token: 0x06005C15 RID: 23573 RVA: 0x00235568 File Offset: 0x00233768
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("CURSE_OF_KNOWLEDGE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.CurseOfKnowledge), new AbstractIntent[]
			{
				new DebuffIntent(false)
			});
			MoveState moveState2 = new MoveState("SLAP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SlapMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SlapDamage)
			});
			MoveState moveState3 = new MoveState("KNOWLEDGE_OVERWHELMING_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.KnowledgeOverwhelmingMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.KnowledgeOverwhelmingDamage, 3)
			});
			MoveState moveState4 = new MoveState("PONDER_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.PonderMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.PonderDamage),
				new HealIntent(),
				new BuffIntent()
			});
			ConditionalBranchState conditionalBranchState = new ConditionalBranchState("CurseOfKnowledgeBranch");
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState4;
			moveState4.FollowUpState = conditionalBranchState;
			conditionalBranchState.AddState(moveState, () => this._curseOfKnowledgeCounter < 3);
			conditionalBranchState.AddState(moveState2, () => this._curseOfKnowledgeCounter >= 3);
			list.Add(conditionalBranchState);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState4);
			list.Add(moveState3);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005C16 RID: 23574 RVA: 0x002356B4 File Offset: 0x002338B4
		private async Task CurseOfKnowledge(IReadOnlyList<Creature> targets)
		{
			if (this.CurseOfKnowledgeCounter >= KnowledgeDemon._curseOfKnowledgeSets.Count)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 1);
				defaultInterpolatedStringHandler.AppendLiteral("There are no valid sets at this index ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.CurseOfKnowledgeCounter);
				throw new InvalidOperationException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			TalkCmd.Play(KnowledgeDemon._curseOfKnowledgeStartLine, base.Creature, 1.0, VfxColor.White);
			await CreatureCmd.TriggerAnim(base.Creature, "MindRotTrigger", 1f);
			List<Task> list = new List<Task>();
			foreach (Creature creature in targets)
			{
				list.Add(this.ChooseCurse(creature));
			}
			await Task.WhenAll(list);
			TalkCmd.Play(KnowledgeDemon._curseOfKnowledgeDoneLine, base.Creature, 1.0, VfxColor.White);
			this.CurseOfKnowledgeCounter++;
		}

		// Token: 0x06005C17 RID: 23575 RVA: 0x00235700 File Offset: 0x00233900
		private async Task ChooseCurse(Creature target)
		{
			if (!target.IsDead)
			{
				int disintegrationDamage = KnowledgeDemon._disintegrationDamageValues[this.CurseOfKnowledgeCounter];
				List<CardModel> list = KnowledgeDemon._curseOfKnowledgeSets[this.CurseOfKnowledgeCounter].Select(delegate(KnowledgeDemon.IChoosable c)
				{
					CardModel cardModel3 = this.CombatState.CreateCard((CardModel)c, target.Player);
					if (cardModel3 is Disintegration)
					{
						cardModel3.DynamicVars["DisintegrationPower"].BaseValue = disintegrationDamage;
					}
					return cardModel3;
				}).ToList<CardModel>();
				CardModel cardModel = await CardSelectCmd.FromChooseACardScreen(new BlockingPlayerChoiceContext(), list, target.Player, false);
				CardModel cardModel2 = cardModel;
				await ((KnowledgeDemon.IChoosable)cardModel2).OnChosen();
			}
		}

		// Token: 0x06005C18 RID: 23576 RVA: 0x0023574C File Offset: 0x0023394C
		private async Task SlapMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SlapDamage).FromMonster(this).WithAttackerAnim("MediumAttackTrigger", 0.5f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/knowledge_demon/knowledge_demon_slap", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(null);
		}

		// Token: 0x06005C19 RID: 23577 RVA: 0x00235790 File Offset: 0x00233990
		private async Task KnowledgeOverwhelmingMove(IReadOnlyList<Creature> targets)
		{
			this.IsBurnt = true;
			NRunMusicController instance = NRunMusicController.Instance;
			if (instance != null)
			{
				instance.UpdateMusicParameter("knowledge_demon_progress", 2f);
			}
			await DamageCmd.Attack(this.KnowledgeOverwhelmingDamage).WithHitCount(3).FromMonster(this)
				.WithAttackerAnim("HeavyAttackTrigger", 0.85f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/knowledge_demon/knowledge_demon_clap", null)
				.OnlyPlayAnimOnce()
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(null);
		}

		// Token: 0x06005C1A RID: 23578 RVA: 0x002357D4 File Offset: 0x002339D4
		private async Task PonderMove(IReadOnlyList<Creature> targets)
		{
			await CreatureCmd.TriggerAnim(base.Creature, "HealTrigger", 1.8f);
			NRunMusicController instance = NRunMusicController.Instance;
			if (instance != null)
			{
				instance.UpdateMusicParameter("knowledge_demon_progress", 1f);
			}
			this.IsBurnt = false;
			await DamageCmd.Attack(this.PonderDamage).FromMonster(this).WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(null);
			await CreatureCmd.Heal(base.Creature, 30 * base.Creature.CombatState.Players.Count, true);
			await PowerCmd.Apply<StrengthPower>(base.Creature, this.PonderStrength, base.Creature, null, false);
		}

		// Token: 0x06005C1B RID: 23579 RVA: 0x00235818 File Offset: 0x00233A18
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("attack_light", false);
			AnimState animState3 = new AnimState("attack_medium", false);
			AnimState animState4 = new AnimState("attack_heavy", false);
			AnimState animState5 = new AnimState("brain_rot", false);
			AnimState animState6 = new AnimState("heal", false);
			AnimState animState7 = new AnimState("burnt_loop", true);
			AnimState animState8 = new AnimState("hurt", false);
			AnimState animState9 = new AnimState("die", false);
			AnimState animState10 = new AnimState("hurt_burnt", false);
			AnimState animState11 = new AnimState("die_burnt", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState7;
			animState5.NextState = animState;
			animState6.NextState = animState;
			animState8.NextState = animState;
			animState10.NextState = animState7;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("LightAttackTrigger", animState2, null);
			creatureAnimator.AddAnyState("MediumAttackTrigger", animState3, null);
			creatureAnimator.AddAnyState("HeavyAttackTrigger", animState4, null);
			creatureAnimator.AddAnyState("MindRotTrigger", animState5, null);
			creatureAnimator.AddAnyState("HealTrigger", animState6, null);
			creatureAnimator.AddAnyState("Dead", animState9, () => !this._isBurnt);
			creatureAnimator.AddAnyState("Hit", animState8, () => !this._isBurnt);
			creatureAnimator.AddAnyState("Dead", animState11, () => this._isBurnt);
			creatureAnimator.AddAnyState("Hit", animState10, () => this._isBurnt);
			return creatureAnimator;
		}

		// Token: 0x0400232D RID: 9005
		private const string _knowledgeDemonCustomTrackName = "knowledge_demon_progress";

		// Token: 0x0400232E RID: 9006
		private const string _clapSfx = "event:/sfx/enemy/enemy_attacks/knowledge_demon/knowledge_demon_clap";

		// Token: 0x0400232F RID: 9007
		private const string _flameSfx = "event:/sfx/enemy/enemy_attacks/knowledge_demon/knowledge_demon_flame";

		// Token: 0x04002330 RID: 9008
		private const string _slapSfx = "event:/sfx/enemy/enemy_attacks/knowledge_demon/knowledge_demon_slap";

		// Token: 0x04002331 RID: 9009
		private static readonly LocString _curseOfKnowledgeStartLine = MonsterModel.L10NMonsterLookup("KNOWLEDGE_DEMON.moves.CURSE_OF_KNOWLEDGE.startLine");

		// Token: 0x04002332 RID: 9010
		private static readonly LocString _curseOfKnowledgeDoneLine = MonsterModel.L10NMonsterLookup("KNOWLEDGE_DEMON.moves.CURSE_OF_KNOWLEDGE.doneLine");

		// Token: 0x04002333 RID: 9011
		private static readonly int[] _disintegrationDamageValues = new int[] { 6, 7, 8 };

		// Token: 0x04002334 RID: 9012
		private static readonly IReadOnlyList<IReadOnlyList<KnowledgeDemon.IChoosable>> _curseOfKnowledgeSets = new <>z__ReadOnlyArray<IReadOnlyList<KnowledgeDemon.IChoosable>>(new IReadOnlyList<KnowledgeDemon.IChoosable>[]
		{
			new <>z__ReadOnlyArray<KnowledgeDemon.IChoosable>(new KnowledgeDemon.IChoosable[]
			{
				ModelDb.Card<Disintegration>(),
				ModelDb.Card<MindRot>()
			}),
			new <>z__ReadOnlyArray<KnowledgeDemon.IChoosable>(new KnowledgeDemon.IChoosable[]
			{
				ModelDb.Card<Disintegration>(),
				ModelDb.Card<Sloth>()
			}),
			new <>z__ReadOnlyArray<KnowledgeDemon.IChoosable>(new KnowledgeDemon.IChoosable[]
			{
				ModelDb.Card<Disintegration>(),
				ModelDb.Card<WasteAway>()
			})
		});

		// Token: 0x04002335 RID: 9013
		private int _curseOfKnowledgeCounter;

		// Token: 0x04002336 RID: 9014
		private const int _knowledgeOverwhelmingRepeat = 3;

		// Token: 0x04002337 RID: 9015
		private const int _ponderHeal = 30;

		// Token: 0x04002338 RID: 9016
		private bool _isBurnt;

		// Token: 0x04002339 RID: 9017
		private const string _mindRotTrigger = "MindRotTrigger";

		// Token: 0x0400233A RID: 9018
		private const string _lightAttackTrigger = "LightAttackTrigger";

		// Token: 0x0400233B RID: 9019
		private const string _mediumAttackTrigger = "MediumAttackTrigger";

		// Token: 0x0400233C RID: 9020
		private const string _heavyAttackTrigger = "HeavyAttackTrigger";

		// Token: 0x0400233D RID: 9021
		private const string _healTrigger = "HealTrigger";

		// Token: 0x02001BEA RID: 7146
		public interface IChoosable
		{
			// Token: 0x0600A7D6 RID: 42966
			Task OnChosen();
		}
	}
}
