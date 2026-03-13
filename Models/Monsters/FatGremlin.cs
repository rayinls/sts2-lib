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
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000751 RID: 1873
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FatGremlin : MonsterModel
	{
		// Token: 0x17001589 RID: 5513
		// (get) Token: 0x06005B02 RID: 23298 RVA: 0x00231F96 File Offset: 0x00230196
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 14, 13);
			}
		}

		// Token: 0x1700158A RID: 5514
		// (get) Token: 0x06005B03 RID: 23299 RVA: 0x00231FA2 File Offset: 0x002301A2
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 18, 17);
			}
		}

		// Token: 0x1700158B RID: 5515
		// (get) Token: 0x06005B04 RID: 23300 RVA: 0x00231FAE File Offset: 0x002301AE
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Insect;
			}
		}

		// Token: 0x1700158C RID: 5516
		// (get) Token: 0x06005B05 RID: 23301 RVA: 0x00231FB1 File Offset: 0x002301B1
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/gremlin_merc/fat_gremlin_die";
			}
		}

		// Token: 0x1700158D RID: 5517
		// (get) Token: 0x06005B06 RID: 23302 RVA: 0x00231FB8 File Offset: 0x002301B8
		// (set) Token: 0x06005B07 RID: 23303 RVA: 0x00231FC0 File Offset: 0x002301C0
		private bool IsAwake
		{
			get
			{
				return this._isAwake;
			}
			set
			{
				base.AssertMutable();
				this._isAwake = value;
			}
		}

		// Token: 0x06005B08 RID: 23304 RVA: 0x00231FD0 File Offset: 0x002301D0
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("SPAWNED_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SpawnedMove), new AbstractIntent[]
			{
				new StunIntent()
			});
			MoveState moveState2 = new MoveState("FLEE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.FleeMove), new AbstractIntent[]
			{
				new EscapeIntent()
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState2;
			list.Add(moveState);
			list.Add(moveState2);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005B09 RID: 23305 RVA: 0x00232050 File Offset: 0x00230250
		private async Task SpawnedMove(IReadOnlyList<Creature> targets)
		{
			await CreatureCmd.TriggerAnim(base.Creature, "WakeUpTrigger", 0.8f);
			this.IsAwake = true;
		}

		// Token: 0x06005B0A RID: 23306 RVA: 0x00232094 File Offset: 0x00230294
		private async Task FleeMove(IReadOnlyList<Creature> targets)
		{
			LocString locString = MonsterModel.L10NMonsterLookup("FAT_GREMLIN.moves.FLEE.banter");
			TalkCmd.Play(locString, base.Creature, 1.25, VfxColor.White);
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				NCreature creatureNode = instance.GetCreatureNode(base.Creature);
				if (creatureNode != null)
				{
					creatureNode.ToggleIsInteractable(false);
				}
			}
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/gremlin_merc/fat_gremlin_escape", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "FleeTrigger", 0f);
			await Cmd.Wait(1.25f, false);
			await CreatureCmd.Escape(base.Creature, true);
		}

		// Token: 0x06005B0B RID: 23307 RVA: 0x002320D8 File Offset: 0x002302D8
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("awake_loop", true);
			AnimState animState2 = new AnimState("spawn", false);
			AnimState animState3 = new AnimState("flee", false);
			AnimState animState4 = new AnimState("stunned_loop", true);
			AnimState animState5 = new AnimState("wake_up", false);
			AnimState animState6 = new AnimState("hurt_stunned", false);
			AnimState animState7 = new AnimState("hurt_awake", false);
			AnimState animState8 = new AnimState("die", false);
			animState2.NextState = animState4;
			animState6.NextState = animState4;
			animState7.NextState = animState;
			animState5.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState2, controller);
			creatureAnimator.AddAnyState("Idle", animState, null);
			creatureAnimator.AddAnyState("FleeTrigger", animState3, null);
			creatureAnimator.AddAnyState("WakeUpTrigger", animState5, null);
			creatureAnimator.AddAnyState("Dead", animState8, null);
			creatureAnimator.AddAnyState("Hit", animState7, () => this.IsAwake);
			creatureAnimator.AddAnyState("Hit", animState6, () => !this.IsAwake);
			return creatureAnimator;
		}

		// Token: 0x040022E7 RID: 8935
		private const string _fleeTrigger = "FleeTrigger";

		// Token: 0x040022E8 RID: 8936
		private const string _wakeUpTrigger = "WakeUpTrigger";

		// Token: 0x040022E9 RID: 8937
		private const string _escapeSfx = "event:/sfx/enemy/enemy_attacks/gremlin_merc/fat_gremlin_escape";

		// Token: 0x040022EA RID: 8938
		private bool _isAwake;
	}
}
