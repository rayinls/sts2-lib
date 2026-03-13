using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200060E RID: 1550
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DoomPower : PowerModel
	{
		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x0600523A RID: 21050 RVA: 0x00220A8F File Offset: 0x0021EC8F
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x0600523B RID: 21051 RVA: 0x00220A92 File Offset: 0x0021EC92
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x0600523C RID: 21052 RVA: 0x00220A95 File Offset: 0x0021EC95
		public override Color AmountLabelColor
		{
			get
			{
				return PowerModel._normalAmountLabelColor;
			}
		}

		// Token: 0x0600523D RID: 21053 RVA: 0x00220A9C File Offset: 0x0021EC9C
		public static async Task DoomKill(IReadOnlyList<Creature> creatures)
		{
			if (creatures.Count != 0)
			{
				CombatState combatState = creatures.First<Creature>().CombatState;
				foreach (Creature creature in creatures)
				{
					await DoomPower.PlayVfx(creature);
					await CreatureCmd.Kill(creature, false);
					creature = null;
				}
				IEnumerator<Creature> enumerator = null;
				await Hook.AfterDiedToDoom(combatState, creatures);
			}
		}

		// Token: 0x0600523E RID: 21054 RVA: 0x00220ADF File Offset: 0x0021ECDF
		public static IReadOnlyList<Creature> GetDoomedCreatures(IReadOnlyList<Creature> creatures)
		{
			return creatures.Where(delegate(Creature c)
			{
				DoomPower power = c.GetPower<DoomPower>();
				return power != null && power.IsOwnerDoomed();
			}).ToList<Creature>();
		}

		// Token: 0x0600523F RID: 21055 RVA: 0x00220B0B File Offset: 0x0021ED0B
		public bool IsOwnerDoomed()
		{
			return base.Owner.CurrentHp <= base.Amount;
		}

		// Token: 0x06005240 RID: 21056 RVA: 0x00220B24 File Offset: 0x0021ED24
		public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				if (!base.Owner.IsDead)
				{
					if (this.IsOwnerDoomed())
					{
						IReadOnlyList<Creature> doomedCreatures = DoomPower.GetDoomedCreatures(base.Owner.CombatState.GetCreaturesOnSide(side));
						if (doomedCreatures.First<Creature>() == base.Owner)
						{
							await DoomPower.DoomKill(doomedCreatures);
						}
					}
				}
			}
		}

		// Token: 0x06005241 RID: 21057 RVA: 0x00220B70 File Offset: 0x0021ED70
		private static async Task PlayVfx(Creature creature)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(creature) : null);
			if (ncreature != null)
			{
				bool flag = false;
				if (creature.IsMonster)
				{
					Player player = creature.Player;
					AbstractModel abstractModel;
					flag = Hook.ShouldDie(((player != null) ? player.RunState : null) ?? creature.CombatState.RunState, creature.CombatState, creature, out abstractModel);
					flag = flag && creature.Monster.ShouldDisappearFromDoom;
				}
				DoomPower.StartDoomAnim(ncreature, flag);
				NDoomOverlayVfx orCreate = NDoomOverlayVfx.GetOrCreate();
				if (orCreate != null && !orCreate.IsInsideTree())
				{
					NCombatRoom.Instance.CombatVfxContainer.AddChildSafely(orCreate);
				}
				List<Creature> list = (from c in creature.CombatState.GetTeammatesOf(creature)
					where c.IsAlive
					select c).ToList<Creature>();
				if (flag)
				{
					if (list.Count<Creature>() == 1 && list.First<Creature>() == creature)
					{
						await Cmd.Wait(1.5f, false);
					}
					else
					{
						await Cmd.Wait(0.25f, false);
					}
				}
			}
		}

		// Token: 0x06005242 RID: 21058 RVA: 0x00220BB4 File Offset: 0x0021EDB4
		private unsafe static void StartDoomAnim(NCreature creature, bool shouldDie)
		{
			Task task = null;
			if (shouldDie)
			{
				MonsterModel monster = creature.Entity.Monster;
				if (monster != null)
				{
					monster.OnDieToDoom();
				}
				Tween tween = creature.AnimDisableUi();
				tween.TweenCallback(Callable.From(new Action(creature.QueueFreeSafely)));
				task = DoomPower.WaitForTween(tween);
				if (creature.HasSpineAnimation)
				{
					creature.SetAnimationTrigger("Hit");
					if (creature.SpineController.GetAnimationState().GetCurrent(0).GetAnimation()
						.GetName() == "hurt")
					{
						MegaTrackEntry current = creature.SpineController.GetAnimationState().GetCurrent(0);
						current.SetTrackTime(0.1f);
						current.SetTimeScale(0f);
					}
				}
				NCombatRoom instance = NCombatRoom.Instance;
				if (instance != null)
				{
					instance.RemoveCreatureNode(creature);
				}
			}
			NDoomVfx ndoomVfx = NDoomVfx.Create(creature.Visuals, creature.Hitbox.GlobalPosition, creature.Hitbox.Size, shouldDie);
			NCombatRoom instance2 = NCombatRoom.Instance;
			if (instance2 != null)
			{
				instance2.CombatVfxContainer.AddChildSafely(ndoomVfx);
			}
			if (shouldDie)
			{
				<>y__InlineArray2<Task> <>y__InlineArray = default(<>y__InlineArray2<Task>);
				*<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray2<Task>, Task>(ref <>y__InlineArray, 0) = task;
				*<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray2<Task>, Task>(ref <>y__InlineArray, 1) = ndoomVfx.VfxTask;
				creature.DeathAnimationTask = Task.WhenAll(<PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray2<Task>, Task>(in <>y__InlineArray, 2));
			}
		}

		// Token: 0x06005243 RID: 21059 RVA: 0x00220CEC File Offset: 0x0021EEEC
		private static async Task WaitForTween(Tween t)
		{
			if (t.IsValid() && t.IsRunning())
			{
				await t.ToSignal(t, Tween.SignalName.Finished);
			}
		}
	}
}
