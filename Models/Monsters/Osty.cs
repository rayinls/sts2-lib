using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000772 RID: 1906
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Osty : MonsterModel
	{
		// Token: 0x1700165F RID: 5727
		// (get) Token: 0x06005D00 RID: 23808 RVA: 0x002382EC File Offset: 0x002364EC
		public static Vector2 MinOffset
		{
			get
			{
				return new Vector2(150f, -75f);
			}
		}

		// Token: 0x17001660 RID: 5728
		// (get) Token: 0x06005D01 RID: 23809 RVA: 0x002382FD File Offset: 0x002364FD
		public static Vector2 MaxOffset
		{
			get
			{
				return new Vector2(250f, -75f);
			}
		}

		// Token: 0x17001661 RID: 5729
		// (get) Token: 0x06005D02 RID: 23810 RVA: 0x0023830E File Offset: 0x0023650E
		public static Vector2 ScaleRange
		{
			get
			{
				return new Vector2(1f, 2f);
			}
		}

		// Token: 0x17001662 RID: 5730
		// (get) Token: 0x06005D03 RID: 23811 RVA: 0x0023831F File Offset: 0x0023651F
		public override int MinInitialHp
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17001663 RID: 5731
		// (get) Token: 0x06005D04 RID: 23812 RVA: 0x00238322 File Offset: 0x00236522
		public override int MaxInitialHp
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17001664 RID: 5732
		// (get) Token: 0x06005D05 RID: 23813 RVA: 0x00238325 File Offset: 0x00236525
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/characters/osty/osty_die";
			}
		}

		// Token: 0x17001665 RID: 5733
		// (get) Token: 0x06005D06 RID: 23814 RVA: 0x0023832C File Offset: 0x0023652C
		public override bool HasDeathSfx
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001666 RID: 5734
		// (get) Token: 0x06005D07 RID: 23815 RVA: 0x0023832F File Offset: 0x0023652F
		public override bool IsHealthBarVisible
		{
			get
			{
				return base.Creature.IsAlive;
			}
		}

		// Token: 0x06005D08 RID: 23816 RVA: 0x0023833C File Offset: 0x0023653C
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			MoveState moveState = new MoveState("NOTHING_MOVE", (IReadOnlyList<Creature> _) => Task.CompletedTask, Array.Empty<AbstractIntent>());
			moveState.FollowUpState = moveState;
			return new MonsterMoveStateMachine(new <>z__ReadOnlySingleElementList<MonsterState>(moveState), moveState);
		}

		// Token: 0x06005D09 RID: 23817 RVA: 0x0023838C File Offset: 0x0023658C
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("cast", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("attack_poke", false);
			AnimState animState5 = new AnimState("hurt", false);
			AnimState animState6 = new AnimState("die", false);
			AnimState animState7 = new AnimState("dead_loop", true);
			AnimState animState8 = new AnimState("revive", false);
			animState.AddBranch("Hit", animState5, null);
			animState2.NextState = animState;
			animState2.AddBranch("Hit", animState5, null);
			animState3.NextState = animState;
			animState3.AddBranch("Hit", animState5, null);
			animState4.NextState = animState;
			animState4.AddBranch("Hit", animState5, null);
			animState5.NextState = animState;
			animState5.AddBranch("Hit", animState5, null);
			animState6.NextState = animState7;
			animState8.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Dead", animState6, null);
			creatureAnimator.AddAnyState("attack_poke", animState4, null);
			creatureAnimator.AddAnyState("Revive", animState8, null);
			return creatureAnimator;
		}

		// Token: 0x06005D0A RID: 23818 RVA: 0x002384C5 File Offset: 0x002366C5
		public static bool CheckMissingWithAnim(Player owner)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.ShakeOstyIfDead(owner);
			}
			return owner.IsOstyMissing;
		}

		// Token: 0x04002374 RID: 9076
		public const float attackerAnimDelay = 0.3f;

		// Token: 0x04002375 RID: 9077
		public const string pokeAnim = "attack_poke";

		// Token: 0x04002376 RID: 9078
		public const string ostyAttackSfx = "event:/sfx/characters/osty/osty_attack";
	}
}
