using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000735 RID: 1845
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BowlbugEgg : MonsterModel
	{
		// Token: 0x170014CE RID: 5326
		// (get) Token: 0x06005942 RID: 22850 RVA: 0x0022CCD4 File Offset: 0x0022AED4
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 23, 21);
			}
		}

		// Token: 0x170014CF RID: 5327
		// (get) Token: 0x06005943 RID: 22851 RVA: 0x0022CCE0 File Offset: 0x0022AEE0
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 24, 22);
			}
		}

		// Token: 0x170014D0 RID: 5328
		// (get) Token: 0x06005944 RID: 22852 RVA: 0x0022CCEC File Offset: 0x0022AEEC
		private int BiteDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 8, 7);
			}
		}

		// Token: 0x170014D1 RID: 5329
		// (get) Token: 0x06005945 RID: 22853 RVA: 0x0022CCF7 File Offset: 0x0022AEF7
		private int ProtectBlock
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 8, 7);
			}
		}

		// Token: 0x170014D2 RID: 5330
		// (get) Token: 0x06005946 RID: 22854 RVA: 0x0022CD02 File Offset: 0x0022AF02
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/workbug_egg/workbug_egg_die";
			}
		}

		// Token: 0x170014D3 RID: 5331
		// (get) Token: 0x06005947 RID: 22855 RVA: 0x0022CD09 File Offset: 0x0022AF09
		protected override string AttackSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/workbug_egg/workbug_egg_attack";
			}
		}

		// Token: 0x170014D4 RID: 5332
		// (get) Token: 0x06005948 RID: 22856 RVA: 0x0022CD10 File Offset: 0x0022AF10
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Insect;
			}
		}

		// Token: 0x06005949 RID: 22857 RVA: 0x0022CD14 File Offset: 0x0022AF14
		public override void SetupSkins(NCreatureVisuals visuals)
		{
			MegaSkeleton skeleton = visuals.SpineBody.GetSkeleton();
			skeleton.SetSkin(skeleton.GetData().FindSkin("cocoon"));
			skeleton.SetSlotsToSetupPose();
			Node node = visuals.GetNode("Visuals/CocoonSlotNode/Cocoon");
			if (node != null)
			{
				MegaSprite megaSprite = new MegaSprite(node);
				MegaSkeleton skeleton2 = megaSprite.GetSkeleton();
				skeleton2.SetSkin(skeleton2.GetData().FindSkin("egg1"));
				megaSprite.GetAnimationState().SetAnimation("egg_idle_loop", true, 0);
				skeleton2.SetSlotsToSetupPose();
			}
		}

		// Token: 0x0600594A RID: 22858 RVA: 0x0022CDA0 File Offset: 0x0022AFA0
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
		}

		// Token: 0x0600594B RID: 22859 RVA: 0x0022CDE4 File Offset: 0x0022AFE4
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("BITE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BiteMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.BiteDamage),
				new DefendIntent()
			});
			moveState.FollowUpState = moveState;
			list.Add(moveState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x0600594C RID: 22860 RVA: 0x0022CE40 File Offset: 0x0022B040
		private async Task BiteMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.BiteDamage).FromMonster(this).WithAttackerAnim("Attack", 0.3f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			await CreatureCmd.GainBlock(base.Creature, this.ProtectBlock, ValueProp.Move, null, false);
		}

		// Token: 0x0600594D RID: 22861 RVA: 0x0022CE84 File Offset: 0x0022B084
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("spit", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Dead", animState5, null);
			creatureAnimator.AddAnyState("Hit", animState4, null);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			return creatureAnimator;
		}
	}
}
