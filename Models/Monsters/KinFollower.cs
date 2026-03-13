using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Audio;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000760 RID: 1888
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class KinFollower : MonsterModel
	{
		// Token: 0x170015EB RID: 5611
		// (get) Token: 0x06005BDB RID: 23515 RVA: 0x00234B94 File Offset: 0x00232D94
		public unsafe override IEnumerable<string> AssetPaths
		{
			get
			{
				int num = 1;
				List<string> list = new List<string>(num);
				CollectionsMarshal.SetCount<string>(list, num);
				Span<string> span = CollectionsMarshal.AsSpan<string>(list);
				int num2 = 0;
				*span[num2] = SceneHelper.GetScenePath("vfx/vfx_kin_poof");
				List<string> list2 = list;
				return list2.Concat(base.AssetPaths);
			}
		}

		// Token: 0x170015EC RID: 5612
		// (get) Token: 0x06005BDC RID: 23516 RVA: 0x00234BD9 File Offset: 0x00232DD9
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/the_kin_minion/the_kin_minion_die";
			}
		}

		// Token: 0x170015ED RID: 5613
		// (get) Token: 0x06005BDD RID: 23517 RVA: 0x00234BE0 File Offset: 0x00232DE0
		public override string HurtSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/the_kin_priest/the_kin_priest_hurt";
			}
		}

		// Token: 0x170015EE RID: 5614
		// (get) Token: 0x06005BDE RID: 23518 RVA: 0x00234BE7 File Offset: 0x00232DE7
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 62, 58);
			}
		}

		// Token: 0x170015EF RID: 5615
		// (get) Token: 0x06005BDF RID: 23519 RVA: 0x00234BF3 File Offset: 0x00232DF3
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 63, 59);
			}
		}

		// Token: 0x170015F0 RID: 5616
		// (get) Token: 0x06005BE0 RID: 23520 RVA: 0x00234BFF File Offset: 0x00232DFF
		private int QuickSlashDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 5, 5);
			}
		}

		// Token: 0x170015F1 RID: 5617
		// (get) Token: 0x06005BE1 RID: 23521 RVA: 0x00234C0A File Offset: 0x00232E0A
		private int BoomerangDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 2, 2);
			}
		}

		// Token: 0x170015F2 RID: 5618
		// (get) Token: 0x06005BE2 RID: 23522 RVA: 0x00234C15 File Offset: 0x00232E15
		private int DanceStrength
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 3, 2);
			}
		}

		// Token: 0x170015F3 RID: 5619
		// (get) Token: 0x06005BE3 RID: 23523 RVA: 0x00234C20 File Offset: 0x00232E20
		// (set) Token: 0x06005BE4 RID: 23524 RVA: 0x00234C28 File Offset: 0x00232E28
		public bool StartsWithDance
		{
			get
			{
				return this._startsWithDance;
			}
			set
			{
				base.AssertMutable();
				this._startsWithDance = value;
			}
		}

		// Token: 0x170015F4 RID: 5620
		// (get) Token: 0x06005BE5 RID: 23525 RVA: 0x00234C37 File Offset: 0x00232E37
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Fur;
			}
		}

		// Token: 0x06005BE6 RID: 23526 RVA: 0x00234C3C File Offset: 0x00232E3C
		public override void SetupSkins(NCreatureVisuals visuals)
		{
			MegaSkeleton skeleton = visuals.SpineBody.GetSkeleton();
			MegaSkin megaSkin = visuals.SpineBody.NewSkin("custom-skin");
			MegaSkeletonDataResource data = skeleton.GetData();
			MegaSkin megaSkin2 = data.FindSkin(Rng.Chaotic.NextItem<string>(KinFollower._hairOptions));
			megaSkin.AddSkin(megaSkin2);
			skeleton.SetSkin(megaSkin);
			skeleton.SetSlotsToSetupPose();
		}

		// Token: 0x06005BE7 RID: 23527 RVA: 0x00234C98 File Offset: 0x00232E98
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<MinionPower>(base.Creature, 1m, base.Creature, null, false);
			NRunMusicController instance = NRunMusicController.Instance;
			if (instance != null)
			{
				instance.UpdateMusicParameter("the_kin_progress", 0f);
			}
			base.Creature.Died += this.AfterDeath;
		}

		// Token: 0x06005BE8 RID: 23528 RVA: 0x00234CDC File Offset: 0x00232EDC
		private void AfterDeath(Creature _)
		{
			base.Creature.Died -= this.AfterDeath;
			IReadOnlyList<Creature> teammatesOf = base.Creature.CombatState.GetTeammatesOf(base.Creature);
			if (teammatesOf.Any((Creature c) => c != null && c.Monster is KinPriest && c.IsAlive))
			{
				NRunMusicController instance = NRunMusicController.Instance;
				if (instance != null)
				{
					instance.UpdateMusicParameter("the_kin_progress", 1f);
				}
			}
			if (!teammatesOf.Any((Creature c) => c != null && c.Monster is KinFollower && c.IsAlive))
			{
				Creature creature = teammatesOf.FirstOrDefault((Creature c) => c != null && c.Monster is KinPriest && c.IsAlive);
				if (creature != null)
				{
					KinPriest kinPriest = creature.Monster as KinPriest;
					if (kinPriest != null)
					{
						kinPriest.AllFollowerDeathResponse();
					}
				}
			}
		}

		// Token: 0x06005BE9 RID: 23529 RVA: 0x00234DC0 File Offset: 0x00232FC0
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("QUICK_SLASH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.QuickSlashMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.QuickSlashDamage)
			});
			MoveState moveState2 = new MoveState("BOOMERANG_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BoomerangMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.BoomerangDamage, 2)
			});
			MoveState moveState3 = new MoveState("POWER_DANCE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.PowerDanceMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			MoveState moveState4 = (this.StartsWithDance ? moveState3 : moveState);
			return new MonsterMoveStateMachine(list, moveState4);
		}

		// Token: 0x06005BEA RID: 23530 RVA: 0x00234E90 File Offset: 0x00233090
		private async Task QuickSlashMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.QuickSlashDamage).FromMonster(this).WithAttackerAnim("SlashTrigger", 0.2f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/the_kin_minion/the_kin_minion_quick_slash", null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005BEB RID: 23531 RVA: 0x00234ED4 File Offset: 0x002330D4
		private async Task PowerDanceMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/the_kin_minion/the_kin_minion_buff", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.9f);
			await PowerCmd.Apply<StrengthPower>(base.Creature, this.DanceStrength, base.Creature, null, false);
		}

		// Token: 0x06005BEC RID: 23532 RVA: 0x00234F18 File Offset: 0x00233118
		private async Task BoomerangMove(IReadOnlyList<Creature> targets)
		{
			if (TestMode.IsOff)
			{
				Vector2? vector = null;
				foreach (Creature creature in targets)
				{
					NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(creature);
					if (vector == null || vector.Value.X > creatureNode.GlobalPosition.X)
					{
						vector = new Vector2?(creatureNode.GlobalPosition);
					}
				}
				NCreature creatureNode2 = NCombatRoom.Instance.GetCreatureNode(base.Creature);
				Node2D specialNode = creatureNode2.GetSpecialNode<Node2D>("Visuals/AttackDistanceControl");
				if (specialNode != null)
				{
					specialNode.Position = Vector2.Left * (creatureNode2.GlobalPosition.X - vector.Value.X) / creatureNode2.Body.Scale;
				}
			}
			await DamageCmd.Attack(this.BoomerangDamage).WithHitCount(2).FromMonster(this)
				.WithAttackerAnim("BoomerangTrigger", 0.2f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/the_kin_minion/the_kin_minion_boomerang_slashh", null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.OnlyPlayAnimOnce()
				.Execute(null);
		}

		// Token: 0x06005BED RID: 23533 RVA: 0x00234F64 File Offset: 0x00233164
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("attack_slash", false);
			AnimState animState3 = new AnimState("attack_boomerang", false);
			AnimState animState4 = new AnimState("buff", false);
			AnimState animState5 = new AnimState("hurt", false);
			AnimState animState6 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState5.NextState = animState;
			animState4.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("BoomerangTrigger", animState3, null);
			creatureAnimator.AddAnyState("SlashTrigger", animState2, null);
			creatureAnimator.AddAnyState("Cast", animState4, null);
			creatureAnimator.AddAnyState("Dead", animState6, null);
			creatureAnimator.AddAnyState("Hit", animState5, null);
			return creatureAnimator;
		}

		// Token: 0x04002318 RID: 8984
		private static readonly string[] _hairOptions = new string[] { "hair_1", "hair_2", "hair_3" };

		// Token: 0x04002319 RID: 8985
		private const string _kinPoofPath = "vfx/vfx_kin_poof";

		// Token: 0x0400231A RID: 8986
		private const string _slashTrigger = "SlashTrigger";

		// Token: 0x0400231B RID: 8987
		private const string _boomerangTrigger = "BoomerangTrigger";

		// Token: 0x0400231C RID: 8988
		private const string _quickSlashSfx = "event:/sfx/enemy/enemy_attacks/the_kin_minion/the_kin_minion_quick_slash";

		// Token: 0x0400231D RID: 8989
		private const string _boomerangSfx = "event:/sfx/enemy/enemy_attacks/the_kin_minion/the_kin_minion_boomerang_slashh";

		// Token: 0x0400231E RID: 8990
		private const string _buffSfx = "event:/sfx/enemy/enemy_attacks/the_kin_minion/the_kin_minion_buff";

		// Token: 0x0400231F RID: 8991
		private const int _boomerangRepeat = 2;

		// Token: 0x04002320 RID: 8992
		private bool _startsWithDance;
	}
}
