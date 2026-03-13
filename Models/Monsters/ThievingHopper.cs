using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.UI;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000797 RID: 1943
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ThievingHopper : MonsterModel
	{
		// Token: 0x1700176F RID: 5999
		// (get) Token: 0x06005F74 RID: 24436 RVA: 0x0023F939 File Offset: 0x0023DB39
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 84, 79);
			}
		}

		// Token: 0x17001770 RID: 6000
		// (get) Token: 0x06005F75 RID: 24437 RVA: 0x0023F945 File Offset: 0x0023DB45
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001771 RID: 6001
		// (get) Token: 0x06005F76 RID: 24438 RVA: 0x0023F94D File Offset: 0x0023DB4D
		// (set) Token: 0x06005F77 RID: 24439 RVA: 0x0023F955 File Offset: 0x0023DB55
		public bool IsHovering
		{
			get
			{
				return this._isHovering;
			}
			set
			{
				base.AssertMutable();
				this._isHovering = value;
			}
		}

		// Token: 0x17001772 RID: 6002
		// (get) Token: 0x06005F78 RID: 24440 RVA: 0x0023F964 File Offset: 0x0023DB64
		protected override string AttackSfx
		{
			get
			{
				if (!this.IsHovering)
				{
					return "event:/sfx/enemy/enemy_attacks/thieving_hopper/thieving_hopper_attack";
				}
				return "event:/sfx/enemy/enemy_attacks/thieving_hopper/thieving_hopper_attack_hover";
			}
		}

		// Token: 0x17001773 RID: 6003
		// (get) Token: 0x06005F79 RID: 24441 RVA: 0x0023F979 File Offset: 0x0023DB79
		private string FleeSfx
		{
			get
			{
				if (!this.IsHovering)
				{
					return "event:/sfx/enemy/enemy_attacks/thieving_hopper/thieving_hopper_flee";
				}
				return "event:/sfx/enemy/enemy_attacks/thieving_hopper/thieving_hopper_flee_hover";
			}
		}

		// Token: 0x17001774 RID: 6004
		// (get) Token: 0x06005F7A RID: 24442 RVA: 0x0023F98E File Offset: 0x0023DB8E
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/thieving_hopper/thieving_hopper_die";
			}
		}

		// Token: 0x17001775 RID: 6005
		// (get) Token: 0x06005F7B RID: 24443 RVA: 0x0023F995 File Offset: 0x0023DB95
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Insect;
			}
		}

		// Token: 0x17001776 RID: 6006
		// (get) Token: 0x06005F7C RID: 24444 RVA: 0x0023F998 File Offset: 0x0023DB98
		public override string TakeDamageSfx
		{
			get
			{
				if (!this.IsHovering)
				{
					return base.TakeDamageSfx;
				}
				return "event:/sfx/enemy/enemy_attacks/thieving_hopper/thieving_hopper_hurt_hover";
			}
		}

		// Token: 0x06005F7D RID: 24445 RVA: 0x0023F9B0 File Offset: 0x0023DBB0
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<EscapeArtistPower>(base.Creature, 5m, base.Creature, null, false);
		}

		// Token: 0x06005F7E RID: 24446 RVA: 0x0023F9F3 File Offset: 0x0023DBF3
		public override void BeforeRemovedFromRoom()
		{
			SfxCmd.StopLoop("event:/sfx/enemy/enemy_attacks/thieving_hopper/thieving_hopper_hover_loop");
		}

		// Token: 0x17001777 RID: 6007
		// (get) Token: 0x06005F7F RID: 24447 RVA: 0x0023F9FF File Offset: 0x0023DBFF
		private int TheftDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 19, 17);
			}
		}

		// Token: 0x17001778 RID: 6008
		// (get) Token: 0x06005F80 RID: 24448 RVA: 0x0023FA0C File Offset: 0x0023DC0C
		private int HatTrickDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 23, 21);
			}
		}

		// Token: 0x17001779 RID: 6009
		// (get) Token: 0x06005F81 RID: 24449 RVA: 0x0023FA19 File Offset: 0x0023DC19
		private int NabDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 16, 14);
			}
		}

		// Token: 0x06005F82 RID: 24450 RVA: 0x0023FA28 File Offset: 0x0023DC28
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("THIEVERY_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ThieveryMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.TheftDamage),
				new CardDebuffIntent()
			});
			MoveState moveState2 = new MoveState("NAB_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.NabMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.NabDamage)
			});
			MoveState moveState3 = new MoveState("HAT_TRICK_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.HatTrickMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.HatTrickDamage)
			});
			MoveState moveState4 = new MoveState("FLUTTER_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.FlutterMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			MoveState moveState5 = new MoveState("ESCAPE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.EscapeMove), new AbstractIntent[]
			{
				new EscapeIntent()
			});
			moveState.FollowUpState = moveState4;
			moveState4.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState5;
			moveState5.FollowUpState = moveState5;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(moveState4);
			list.Add(moveState5);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005F83 RID: 24451 RVA: 0x0023FB64 File Offset: 0x0023DD64
		private async Task ThieveryMove(IReadOnlyList<Creature> targets)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			NCreature creatureNode = ((instance != null) ? instance.GetCreatureNode(base.Creature) : null);
			if (creatureNode != null)
			{
				Creature creature = LocalContext.GetMe(targets) ?? targets.First<Creature>();
				NCreature creatureNode2 = NCombatRoom.Instance.GetCreatureNode(creature);
				Node2D specialNode = creatureNode.GetSpecialNode<Node2D>("Visuals/SpineBoneNode");
				if (specialNode != null)
				{
					specialNode.Position = Vector2.Right * (creatureNode2.GlobalPosition.X - creatureNode.GlobalPosition.X);
				}
			}
			await CreatureCmd.TriggerAnim(base.Creature, "Steal", 0.25f);
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/thieving_hopper/thieving_hopper_steal", 1f);
			List<CardModel> cardsToSteal = new List<CardModel>();
			foreach (Creature creature2 in targets)
			{
				IEnumerable<CardModel> enumerable;
				List<CardModel> list = (enumerable = (from c in CardPile.GetCards(creature2.Player ?? creature2.PetOwner, new PileType[]
					{
						PileType.Draw,
						PileType.Discard
					})
					where c.DeckVersion != null
					select c).ToList<CardModel>());
				Func<CardModel, bool>[] stealPriorities = ThievingHopper._stealPriorities;
				for (int i = 0; i < stealPriorities.Length; i++)
				{
					IEnumerable<CardModel> enumerable2 = list.Where(stealPriorities[i]);
					if (enumerable2.Any<CardModel>())
					{
						enumerable = enumerable2;
						break;
					}
				}
				CardModel cardToSteal = base.RunRng.CombatCardGeneration.NextItem<CardModel>(enumerable);
				await CardPileCmd.RemoveFromCombat(cardToSteal, false, false);
				cardsToSteal.Add(cardToSteal);
				cardToSteal = null;
			}
			IEnumerator<Creature> enumerator = null;
			await Cmd.Wait(0.6f, false);
			foreach (CardModel cardModel in cardsToSteal)
			{
				if (creatureNode != null && LocalContext.IsMine(cardModel))
				{
					Marker2D specialNode2 = creatureNode.GetSpecialNode<Marker2D>("%StolenCardPos");
					if (specialNode2 != null)
					{
						NCard ncard = NCard.Create(cardModel, ModelVisibility.Visible);
						specialNode2.AddChildSafely(ncard);
						ncard.Position += ncard.Size * 0.5f;
						ncard.UpdateVisuals(PileType.Deck, CardPreviewMode.Normal);
					}
				}
				SwipePower swipe = (SwipePower)ModelDb.Power<SwipePower>().ToMutable(0);
				await swipe.Steal(cardModel);
				await PowerCmd.Apply(swipe, base.Creature, 1m, base.Creature, null, false);
				swipe = null;
			}
			List<CardModel>.Enumerator enumerator2 = default(List<CardModel>.Enumerator);
			await DamageCmd.Attack(this.TheftDamage).FromMonster(this).WithNoAttackerAnim()
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005F84 RID: 24452 RVA: 0x0023FBB0 File Offset: 0x0023DDB0
		private async Task NabMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.NabDamage).FromMonster(this).WithAttackerAnim("Attack", 0.3f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005F85 RID: 24453 RVA: 0x0023FBF4 File Offset: 0x0023DDF4
		private async Task HatTrickMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.HatTrickDamage).FromMonster(this).WithAttackerAnim("Attack", 0.3f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005F86 RID: 24454 RVA: 0x0023FC38 File Offset: 0x0023DE38
		private async Task FlutterMove(IReadOnlyList<Creature> targets)
		{
			this.IsHovering = true;
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/thieving_hopper/thieving_hopper_take_off", 1f);
			SfxCmd.PlayLoop("event:/sfx/enemy/enemy_attacks/thieving_hopper/thieving_hopper_hover_loop", true);
			await CreatureCmd.TriggerAnim(base.Creature, "Hover", 0f);
			await Cmd.Wait(1.25f, false);
			await PowerCmd.Apply<FlutterPower>(base.Creature, 5m, base.Creature, null, false);
		}

		// Token: 0x06005F87 RID: 24455 RVA: 0x0023FC7C File Offset: 0x0023DE7C
		private async Task EscapeMove(IReadOnlyList<Creature> targets)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				NCreature creatureNode = instance.GetCreatureNode(base.Creature);
				if (creatureNode != null)
				{
					creatureNode.ToggleIsInteractable(false);
				}
			}
			SfxCmd.Play(this.FleeSfx, 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Flee", 0.85f);
			if (this.IsHovering)
			{
				SfxCmd.StopLoop("event:/sfx/enemy/enemy_attacks/thieving_hopper/thieving_hopper_hover_loop");
				this.IsHovering = false;
			}
			await Cmd.Wait(1.5f, false);
			await CreatureCmd.Escape(base.Creature, true);
		}

		// Token: 0x06005F88 RID: 24456 RVA: 0x0023FCC0 File Offset: 0x0023DEC0
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true)
			{
				BoundsContainer = "GroundedBounds"
			};
			AnimState animState2 = new AnimState("flee", false);
			AnimState animState3 = new AnimState("flee_hover", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("hurt_hover", false);
			AnimState animState6 = new AnimState("attack", false);
			AnimState animState7 = new AnimState("attack_hover", false);
			AnimState animState8 = new AnimState("die", false);
			AnimState animState9 = new AnimState("take_off", false);
			AnimState animState10 = new AnimState("hover_loop", true)
			{
				BoundsContainer = "FlyingBounds"
			};
			AnimState animState11 = new AnimState("steal", false);
			animState9.NextState = animState10;
			animState11.NextState = animState;
			animState4.NextState = animState;
			animState5.NextState = animState10;
			animState6.NextState = animState;
			animState7.NextState = animState10;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("StunTrigger", animState, null);
			creatureAnimator.AddAnyState("Hover", animState9, null);
			creatureAnimator.AddAnyState("Steal", animState11, null);
			creatureAnimator.AddAnyState("Dead", animState8, null);
			creatureAnimator.AddAnyState("Hit", animState5, () => this.IsHovering);
			creatureAnimator.AddAnyState("Hit", animState4, () => !this.IsHovering);
			creatureAnimator.AddAnyState("Attack", animState7, () => this.IsHovering);
			creatureAnimator.AddAnyState("Attack", animState6, () => !this.IsHovering);
			creatureAnimator.AddAnyState("Flee", animState3, () => this.IsHovering);
			creatureAnimator.AddAnyState("Flee", animState2, () => !this.IsHovering);
			return creatureAnimator;
		}

		// Token: 0x04002402 RID: 9218
		private static readonly Func<CardModel, bool>[] _stealPriorities = new Func<CardModel, bool>[]
		{
			(CardModel c) => !(c.Enchantment is Imbued) && c.Rarity == CardRarity.Uncommon,
			delegate(CardModel c)
			{
				bool flag = !(c.Enchantment is Imbued);
				bool flag2 = flag;
				if (flag2)
				{
					bool flag3;
					switch (c.Rarity)
					{
					case CardRarity.Common:
					case CardRarity.Rare:
					case CardRarity.Event:
						flag3 = true;
						goto IL_0042;
					}
					flag3 = false;
					IL_0042:
					flag2 = flag3;
				}
				return flag2;
			},
			delegate(CardModel c)
			{
				bool flag4 = !(c.Enchantment is Imbued);
				bool flag5 = flag4;
				if (flag5)
				{
					CardRarity rarity = c.Rarity;
					bool flag6 = rarity == CardRarity.Basic || rarity == CardRarity.Quest;
					flag5 = flag6;
				}
				return flag5;
			},
			(CardModel c) => c.Rarity == CardRarity.Ancient || c.Enchantment is Imbued
		};

		// Token: 0x04002403 RID: 9219
		public const string stunTrigger = "StunTrigger";

		// Token: 0x04002404 RID: 9220
		private bool _isHovering;

		// Token: 0x04002405 RID: 9221
		private const string _fleeTrigger = "Flee";

		// Token: 0x04002406 RID: 9222
		private const string _hoverTrigger = "Hover";

		// Token: 0x04002407 RID: 9223
		private const string _stealTrigger = "Steal";

		// Token: 0x04002408 RID: 9224
		private const string _escapeMoveId = "ESCAPE_MOVE";

		// Token: 0x04002409 RID: 9225
		private const string _stealSfx = "event:/sfx/enemy/enemy_attacks/thieving_hopper/thieving_hopper_steal";

		// Token: 0x0400240A RID: 9226
		private const string _takeOffSfx = "event:/sfx/enemy/enemy_attacks/thieving_hopper/thieving_hopper_take_off";

		// Token: 0x0400240B RID: 9227
		public const string hoverLoop = "event:/sfx/enemy/enemy_attacks/thieving_hopper/thieving_hopper_hover_loop";
	}
}
