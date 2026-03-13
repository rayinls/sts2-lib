using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200073F RID: 1855
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Chomper : MonsterModel
	{
		// Token: 0x17001511 RID: 5393
		// (get) Token: 0x060059E5 RID: 23013 RVA: 0x0022EA41 File Offset: 0x0022CC41
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 63, 60);
			}
		}

		// Token: 0x17001512 RID: 5394
		// (get) Token: 0x060059E6 RID: 23014 RVA: 0x0022EA4D File Offset: 0x0022CC4D
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 67, 64);
			}
		}

		// Token: 0x17001513 RID: 5395
		// (get) Token: 0x060059E7 RID: 23015 RVA: 0x0022EA59 File Offset: 0x0022CC59
		private static int ClampDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 9, 8);
			}
		}

		// Token: 0x17001514 RID: 5396
		// (get) Token: 0x060059E8 RID: 23016 RVA: 0x0022EA65 File Offset: 0x0022CC65
		// (set) Token: 0x060059E9 RID: 23017 RVA: 0x0022EA6D File Offset: 0x0022CC6D
		public bool ScreamFirst
		{
			get
			{
				return this._screamFirst;
			}
			set
			{
				base.AssertMutable();
				this._screamFirst = value;
			}
		}

		// Token: 0x17001515 RID: 5397
		// (get) Token: 0x060059EA RID: 23018 RVA: 0x0022EA7C File Offset: 0x0022CC7C
		public override string TakeDamageSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/chomper/chomper_hurt";
			}
		}

		// Token: 0x060059EB RID: 23019 RVA: 0x0022EA84 File Offset: 0x0022CC84
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<ArtifactPower>(base.Creature, 2m, base.Creature, null, false);
		}

		// Token: 0x060059EC RID: 23020 RVA: 0x0022EAC8 File Offset: 0x0022CCC8
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("CLAMP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ClampMove), new AbstractIntent[]
			{
				new MultiAttackIntent(Chomper.ClampDamage, 2)
			});
			MoveState moveState2 = new MoveState("SCREECH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ScreechMove), new AbstractIntent[]
			{
				new StatusIntent(3)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			MoveState moveState3 = (this._screamFirst ? moveState2 : moveState);
			return new MonsterMoveStateMachine(list, moveState3);
		}

		// Token: 0x060059ED RID: 23021 RVA: 0x0022EB5C File Offset: 0x0022CD5C
		private async Task ClampMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(Chomper.ClampDamage).WithHitCount(2).FromMonster(this)
				.WithAttackerAnim("Attack", 0.3f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x060059EE RID: 23022 RVA: 0x0022EBA0 File Offset: 0x0022CDA0
		private async Task ScreechMove(IReadOnlyList<Creature> targets)
		{
			LocString locString = MonsterModel.L10NMonsterLookup("CHOMPER.moves.SCREECH.title");
			TalkCmd.Play(locString, base.Creature, -1.0, VfxColor.White);
			SfxCmd.Play(this.CastSfx, 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 1f);
			await CardPileCmd.AddToCombatAndPreview<Dazed>(targets, PileType.Discard, 3, false, CardPilePosition.Bottom);
		}

		// Token: 0x040022A9 RID: 8873
		public const string screechMoveId = "SCREECH_MOVE";

		// Token: 0x040022AA RID: 8874
		private const int _screechStatusCount = 3;

		// Token: 0x040022AB RID: 8875
		private const int _clampRepeat = 2;

		// Token: 0x040022AC RID: 8876
		private bool _screamFirst;
	}
}
