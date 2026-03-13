using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Nodes.Audio;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200068A RID: 1674
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SandpitPower : PowerModel
	{
		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x0600550A RID: 21770 RVA: 0x00225BE7 File Offset: 0x00223DE7
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x0600550B RID: 21771 RVA: 0x00225BEA File Offset: 0x00223DEA
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x0600550C RID: 21772 RVA: 0x00225BED File Offset: 0x00223DED
		public override bool IsInstanced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600550D RID: 21773 RVA: 0x00225BF0 File Offset: 0x00223DF0
		[NullableContext(2)]
		[return: Nullable(1)]
		public override Task AfterApplied(Creature applier, CardModel cardSource)
		{
			if (TestMode.IsOn)
			{
				return Task.CompletedTask;
			}
			NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(base.Target);
			this._initialAmount = base.Amount;
			this._initialTargetPosition = creatureNode.GlobalPosition.X;
			return Task.CompletedTask;
		}

		// Token: 0x0600550E RID: 21774 RVA: 0x00225C40 File Offset: 0x00223E40
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == CombatSide.Enemy)
			{
				await PowerCmd.Decrement(this);
			}
		}

		// Token: 0x0600550F RID: 21775 RVA: 0x00225C8C File Offset: 0x00223E8C
		public override async Task AfterPowerAmountChanged(PowerModel power, decimal _, [Nullable(2)] Creature __, [Nullable(2)] CardModel cardSource)
		{
			if (!TestMode.IsOn)
			{
				if (power == this)
				{
					await this.UpdateCreaturePositions();
					if (LocalContext.IsMe(base.Target))
					{
						int num = Mathf.Clamp(6 - base.Amount, 0, 5);
						NRunMusicController instance = NRunMusicController.Instance;
						if (instance != null)
						{
							instance.UpdateMusicParameter(TheInsatiable.TheInsatiableTrackName, (float)num);
						}
					}
				}
			}
		}

		// Token: 0x06005510 RID: 21776 RVA: 0x00225CD8 File Offset: 0x00223ED8
		public override async Task AfterRemoved(Creature oldOwner)
		{
			if (!oldOwner.IsDead)
			{
				if (!base.Target.IsDead)
				{
					if (TestMode.IsOff)
					{
						NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(base.Owner);
						Tween tween = NCombatRoom.Instance.CreateTween();
						float num = creatureNode.GlobalPosition.X - 450f;
						foreach (Creature creature in this.AllAffectedCreatures)
						{
							NCreature creatureNode2 = NCombatRoom.Instance.GetCreatureNode(creature);
							tween.Parallel().TweenProperty(creatureNode2, "global_position:x", num, 0.699999988079071).SetEase(Tween.EaseType.InOut)
								.SetTrans(Tween.TransitionType.Sine);
						}
					}
					SfxCmd.Play("event:/sfx/enemy/enemy_attacks/the_insatiable/the_insatiable_finisher", 1f);
					await CreatureCmd.TriggerAnim(base.Owner, "EatPlayerTrigger", 0f);
					await Cmd.Wait(0.5f, false);
					foreach (Creature creature2 in this.AllAffectedCreatures)
					{
						if (TestMode.IsOff)
						{
							NCombatRoom.Instance.GetCreatureNode(creature2).Visuals.Visible = false;
						}
						if (creature2.IsPlayer || creature2.Monster is Osty)
						{
							await CreatureCmd.Kill(creature2, true);
						}
					}
					IEnumerator<Creature> enumerator2 = null;
				}
			}
		}

		// Token: 0x06005511 RID: 21777 RVA: 0x00225D24 File Offset: 0x00223F24
		public override async Task AfterCreatureAddedToCombat(Creature creature)
		{
			if (creature.Side != base.Owner.Side)
			{
				await this.UpdateCreaturePositions();
			}
		}

		// Token: 0x06005512 RID: 21778 RVA: 0x00225D70 File Offset: 0x00223F70
		public override async Task AfterOstyRevived(Creature osty)
		{
			await this.UpdateCreaturePositions();
		}

		// Token: 0x06005513 RID: 21779 RVA: 0x00225DB4 File Offset: 0x00223FB4
		public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == CombatSide.Enemy)
			{
				await this.UpdateCreaturePositions();
			}
		}

		// Token: 0x06005514 RID: 21780 RVA: 0x00225E00 File Offset: 0x00224000
		private async Task UpdateCreaturePositions()
		{
			if (!TestMode.IsOn)
			{
				NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(base.Owner);
				NCreature creatureNode2 = NCombatRoom.Instance.GetCreatureNode(base.Target);
				float num = creatureNode.GlobalPosition.X - 450f;
				float num2 = this._initialTargetPosition + 50f;
				float num3 = (num2 - num) / (float)this._initialAmount;
				int num4 = Mathf.Min(base.Amount, this._initialAmount);
				int num5 = Mathf.Max(base.Amount - this._initialAmount, 0);
				NCreature ncreature = creatureNode2;
				float num6 = 0f;
				Creature target = base.Target;
				Player player = ((target != null) ? target.Player : null);
				if (player != null && player.IsOstyAlive && LocalContext.IsMe(base.Target))
				{
					NCreature creatureNode3 = NCombatRoom.Instance.GetCreatureNode(base.Target.Player.Osty);
					ncreature = creatureNode3;
					float x = NCreature.GetOstyOffsetFromPlayer(creatureNode3.Entity).X;
					num2 += x;
					num3 = (num2 - num) / (float)this._initialAmount;
					float num7 = 100f * (1f - (float)num4 / (float)this._initialAmount);
					num6 = x - num7;
				}
				float num8 = creatureNode.GlobalPosition.X - 400f + num3 * (float)num4 + num3 * ((float)num5 / ((float)num5 + 2f));
				Tween tween = null;
				foreach (Creature creature in this.AllAffectedCreatures)
				{
					float num9 = num8 - ncreature.GlobalPosition.X;
					if (creature != ncreature.Entity)
					{
						num9 = num8 - num6 - creatureNode2.GlobalPosition.X;
					}
					if (Math.Abs(num9) > 5f && !creature.IsDead)
					{
						NCreature creatureNode4 = NCombatRoom.Instance.GetCreatureNode(creature);
						if (creatureNode4 != null)
						{
							if (tween == null)
							{
								tween = NCombatRoom.Instance.CreateTween().SetParallel(true).SetEase(Tween.EaseType.Out)
									.SetTrans(Tween.TransitionType.Cubic);
							}
							tween.TweenProperty(creatureNode4, "global_position:x", creatureNode4.GlobalPosition.X + num9, 0.25);
						}
					}
				}
				if (tween != null)
				{
					await tween.ToSignal(tween, Tween.SignalName.Finished);
				}
			}
		}

		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x06005515 RID: 21781 RVA: 0x00225E44 File Offset: 0x00224044
		private IReadOnlyList<Creature> AllAffectedCreatures
		{
			get
			{
				Creature creature = base.Target.Player.Creature;
				IReadOnlyList<Creature> pets = base.Target.Pets;
				int num = 0;
				Creature[] array = new Creature[1 + pets.Count];
				array[num] = creature;
				num++;
				foreach (Creature creature2 in pets)
				{
					array[num] = creature2;
					num++;
				}
				return new <>z__ReadOnlyArray<Creature>(array);
			}
		}

		// Token: 0x04002267 RID: 8807
		private const float _paddingDistanceFromMonster = 450f;

		// Token: 0x04002268 RID: 8808
		private const float _paddingDistanceFromOriginal = 50f;

		// Token: 0x04002269 RID: 8809
		private const float _tweenTime = 0.25f;

		// Token: 0x0400226A RID: 8810
		private int _initialAmount;

		// Token: 0x0400226B RID: 8811
		private float _initialTargetPosition;
	}
}
