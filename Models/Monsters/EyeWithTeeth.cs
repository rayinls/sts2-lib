using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200074E RID: 1870
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EyeWithTeeth : MonsterModel
	{
		// Token: 0x17001577 RID: 5495
		// (get) Token: 0x06005AD4 RID: 23252 RVA: 0x00231594 File Offset: 0x0022F794
		public override int MinInitialHp
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x17001578 RID: 5496
		// (get) Token: 0x06005AD5 RID: 23253 RVA: 0x00231597 File Offset: 0x0022F797
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001579 RID: 5497
		// (get) Token: 0x06005AD6 RID: 23254 RVA: 0x0023159F File Offset: 0x0022F79F
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Magic;
			}
		}

		// Token: 0x1700157A RID: 5498
		// (get) Token: 0x06005AD7 RID: 23255 RVA: 0x002315A2 File Offset: 0x0022F7A2
		public override bool ShouldDisappearFromDoom
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005AD8 RID: 23256 RVA: 0x002315A8 File Offset: 0x0022F7A8
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<IllusionPower>(base.Creature, 1m, base.Creature, null, false);
		}

		// Token: 0x06005AD9 RID: 23257 RVA: 0x002315EC File Offset: 0x0022F7EC
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("DISTRACT_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.DistractMove), new AbstractIntent[]
			{
				new StatusIntent(3)
			});
			moveState.FollowUpState = moveState;
			list.Add(moveState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005ADA RID: 23258 RVA: 0x0023163C File Offset: 0x0022F83C
		private async Task DistractMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play(this.AttackSfx, 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Attack", 0.7f);
			VfxCmd.PlayOnCreatureCenters(targets, "vfx/vfx_attack_slash");
			await CardPileCmd.AddToCombatAndPreview<Dazed>(targets, PileType.Discard, 3, false, CardPilePosition.Bottom);
		}

		// Token: 0x06005ADB RID: 23259 RVA: 0x00231688 File Offset: 0x0022F888
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("attack", false);
			AnimState animState3 = new AnimState("die", false);
			animState2.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Attack", animState2, null);
			creatureAnimator.AddAnyState("Dead", animState3, () => !base.CombatState.GetTeammatesOf(base.Creature).Any((Creature t) => t != null && t.IsPrimaryEnemy && t.IsAlive));
			return creatureAnimator;
		}

		// Token: 0x040022DE RID: 8926
		private const int _distractAmount = 3;
	}
}
