using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200081B RID: 2075
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FabricatorNormal : EncounterModel
	{
		// Token: 0x170018F3 RID: 6387
		// (get) Token: 0x060063F6 RID: 25590 RVA: 0x00250775 File Offset: 0x0024E975
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170018F4 RID: 6388
		// (get) Token: 0x060063F7 RID: 25591 RVA: 0x00250778 File Offset: 0x0024E978
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170018F5 RID: 6389
		// (get) Token: 0x060063F8 RID: 25592 RVA: 0x0025077B File Offset: 0x0024E97B
		public override IReadOnlyList<string> Slots
		{
			get
			{
				return new <>z__ReadOnlyArray<string>(new string[] { "bot1", "bot2", "fabricator", "bot3", "bot4" });
			}
		}

		// Token: 0x170018F6 RID: 6390
		// (get) Token: 0x060063F9 RID: 25593 RVA: 0x002507B0 File Offset: 0x0024E9B0
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				MonsterModel monsterModel = ModelDb.Monster<Fabricator>();
				HashSet<MonsterModel> defenseSpawns = Fabricator.defenseSpawns;
				HashSet<MonsterModel> aggroSpawns = Fabricator.aggroSpawns;
				int num = 0;
				MonsterModel[] array = new MonsterModel[1 + (defenseSpawns.Count + aggroSpawns.Count)];
				array[num] = monsterModel;
				num++;
				foreach (MonsterModel monsterModel2 in defenseSpawns)
				{
					array[num] = monsterModel2;
					num++;
				}
				foreach (MonsterModel monsterModel2 in aggroSpawns)
				{
					array[num] = monsterModel2;
					num++;
				}
				return new <>z__ReadOnlyArray<MonsterModel>(array);
			}
		}

		// Token: 0x060063FA RID: 25594 RVA: 0x00250880 File Offset: 0x0024EA80
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<Fabricator>().ToMutable(), "fabricator"));
		}

		// Token: 0x060063FB RID: 25595 RVA: 0x0025089B File Offset: 0x0024EA9B
		public override float GetCameraScaling()
		{
			return 0.85f;
		}

		// Token: 0x060063FC RID: 25596 RVA: 0x002508A2 File Offset: 0x0024EAA2
		public override Vector2 GetCameraOffset()
		{
			return Vector2.Down * 60f;
		}

		// Token: 0x060063FD RID: 25597 RVA: 0x002508B4 File Offset: 0x0024EAB4
		public static void SetBotFallPosition(NCreature creatureNode)
		{
			Node2D specialNode = creatureNode.GetSpecialNode<Node2D>("Visuals/FallControl");
			if (specialNode == null)
			{
				return;
			}
			float num = 125f;
			specialNode.Position = Vector2.Down * ((num - creatureNode.Position.Y) / creatureNode.Visuals.Body.Scale.Y);
		}

		// Token: 0x04002523 RID: 9507
		private const string _fabricatorSlot = "fabricator";

		// Token: 0x04002524 RID: 9508
		private const string _botSlotPrefix = "bot";
	}
}
