using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006B3 RID: 1715
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SurroundedPower : PowerModel
	{
		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x060055F3 RID: 22003 RVA: 0x002274EA File Offset: 0x002256EA
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x060055F4 RID: 22004 RVA: 0x002274ED File Offset: 0x002256ED
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x170012EA RID: 4842
		// (get) Token: 0x060055F5 RID: 22005 RVA: 0x002274F0 File Offset: 0x002256F0
		// (set) Token: 0x060055F6 RID: 22006 RVA: 0x002274F8 File Offset: 0x002256F8
		public SurroundedPower.Direction Facing
		{
			get
			{
				return this._facing;
			}
			private set
			{
				base.AssertMutable();
				this._facing = value;
			}
		}

		// Token: 0x060055F7 RID: 22007 RVA: 0x00227508 File Offset: 0x00225708
		[NullableContext(2)]
		public override decimal ModifyDamageMultiplicative(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (dealer == null)
			{
				return 1m;
			}
			if (target != base.Owner)
			{
				return 1m;
			}
			SurroundedPower.Direction facing = this.Facing;
			if (facing != SurroundedPower.Direction.Right)
			{
				if (facing == SurroundedPower.Direction.Left)
				{
					if (!dealer.HasPower<BackAttackRightPower>())
					{
						return 1m;
					}
				}
			}
			else if (!dealer.HasPower<BackAttackLeftPower>())
			{
				return 1m;
			}
			return 1.5m;
		}

		// Token: 0x060055F8 RID: 22008 RVA: 0x00227570 File Offset: 0x00225770
		public override async Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (cardPlay.Target != null)
			{
				if (cardPlay.Card.Owner == base.Owner.Player)
				{
					await this.UpdateDirection(cardPlay.Target);
				}
			}
		}

		// Token: 0x060055F9 RID: 22009 RVA: 0x002275BC File Offset: 0x002257BC
		public override async Task BeforePotionUsed(PotionModel potion, [Nullable(2)] Creature target)
		{
			if (CombatManager.Instance.IsInProgress)
			{
				if (target != null)
				{
					if (potion.Owner == base.Owner.Player)
					{
						await this.UpdateDirection(target);
					}
				}
			}
		}

		// Token: 0x060055FA RID: 22010 RVA: 0x00227610 File Offset: 0x00225810
		public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (!wasRemovalPrevented)
			{
				if (creature.Side != base.Owner.Side)
				{
					IReadOnlyList<Creature> hittableEnemies = base.Owner.CombatState.HittableEnemies;
					if (hittableEnemies.Count != 0)
					{
						if (!hittableEnemies.All((Creature e) => e.HasPower<BackAttackLeftPower>()))
						{
							if (!hittableEnemies.All((Creature e) => e.HasPower<BackAttackRightPower>()))
							{
								goto IL_0109;
							}
						}
						await this.UpdateDirection(hittableEnemies[0]);
						IL_0109:;
					}
				}
			}
		}

		// Token: 0x060055FB RID: 22011 RVA: 0x00227664 File Offset: 0x00225864
		private async Task UpdateDirection(Creature target)
		{
			SurroundedPower.Direction facing = this.Facing;
			if (facing != SurroundedPower.Direction.Right)
			{
				if (facing == SurroundedPower.Direction.Left)
				{
					if (target.HasPower<BackAttackRightPower>())
					{
						await this.FaceDirection(SurroundedPower.Direction.Right);
					}
				}
			}
			else if (target.HasPower<BackAttackLeftPower>())
			{
				await this.FaceDirection(SurroundedPower.Direction.Left);
			}
		}

		// Token: 0x060055FC RID: 22012 RVA: 0x002276B0 File Offset: 0x002258B0
		private async Task FaceDirection(SurroundedPower.Direction direction)
		{
			this.Facing = direction;
			Creature owner = base.Owner;
			IReadOnlyList<Creature> pets = base.Owner.Pets;
			int num = 0;
			Creature[] array = new Creature[1 + pets.Count];
			array[num] = owner;
			num++;
			foreach (Creature creature in pets)
			{
				array[num] = creature;
				num++;
			}
			IEnumerable<Creature> enumerable = new <>z__ReadOnlyArray<Creature>(array);
			IEnumerable<Node2D> enumerable2 = enumerable.Select(delegate(Creature c)
			{
				NCombatRoom instance = NCombatRoom.Instance;
				if (instance == null)
				{
					return null;
				}
				NCreature creatureNode = instance.GetCreatureNode(c);
				if (creatureNode == null)
				{
					return null;
				}
				return creatureNode.Body;
			});
			foreach (Node2D node2D in enumerable2)
			{
				await this.FlipScale(node2D);
			}
			IEnumerator<Node2D> enumerator2 = null;
		}

		// Token: 0x060055FD RID: 22013 RVA: 0x002276FC File Offset: 0x002258FC
		private Task FlipScale([Nullable(2)] Node2D body)
		{
			if (body == null)
			{
				return Task.CompletedTask;
			}
			float x = body.Scale.X;
			if ((this.Facing == SurroundedPower.Direction.Right && x < 0f) || (this.Facing == SurroundedPower.Direction.Left && x > 0f))
			{
				body.Scale *= new Vector2(-1f, 1f);
			}
			return Task.CompletedTask;
		}

		// Token: 0x04002274 RID: 8820
		private SurroundedPower.Direction _facing;

		// Token: 0x02001AAE RID: 6830
		[NullableContext(0)]
		public enum Direction
		{
			// Token: 0x04006934 RID: 26932
			Right,
			// Token: 0x04006935 RID: 26933
			Left
		}
	}
}
