using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Map;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000505 RID: 1285
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FurCoat : RelicModel
	{
		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06004BB7 RID: 19383 RVA: 0x0021433F File Offset: 0x0021253F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06004BB8 RID: 19384 RVA: 0x00214342 File Offset: 0x00212542
		// (set) Token: 0x06004BB9 RID: 19385 RVA: 0x0021434A File Offset: 0x0021254A
		[SavedProperty]
		public int FurCoatActIndex
		{
			get
			{
				return this._furCoatActIndex;
			}
			set
			{
				base.AssertMutable();
				this._furCoatActIndex = value;
			}
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06004BBA RID: 19386 RVA: 0x00214359 File Offset: 0x00212559
		// (set) Token: 0x06004BBB RID: 19387 RVA: 0x00214361 File Offset: 0x00212561
		[SavedProperty]
		private int[] FurCoatCoordCols { get; set; } = Array.Empty<int>();

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06004BBC RID: 19388 RVA: 0x0021436A File Offset: 0x0021256A
		// (set) Token: 0x06004BBD RID: 19389 RVA: 0x00214372 File Offset: 0x00212572
		[SavedProperty]
		private int[] FurCoatCoordRows { get; set; } = Array.Empty<int>();

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06004BBE RID: 19390 RVA: 0x0021437B File Offset: 0x0021257B
		// (set) Token: 0x06004BBF RID: 19391 RVA: 0x00214383 File Offset: 0x00212583
		[SavedProperty]
		private bool FurCoatCoordsSet { get; set; }

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06004BC0 RID: 19392 RVA: 0x0021438C File Offset: 0x0021258C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Combats", 7m));
			}
		}

		// Token: 0x06004BC1 RID: 19393 RVA: 0x002143A3 File Offset: 0x002125A3
		public override Task AfterObtained()
		{
			this.FurCoatActIndex = base.Owner.RunState.CurrentActIndex;
			this.AddMarkedRooms(base.Owner.RunState.Map);
			return Task.CompletedTask;
		}

		// Token: 0x06004BC2 RID: 19394 RVA: 0x002143D7 File Offset: 0x002125D7
		public override ActMap ModifyGeneratedMapLate(IRunState runState, ActMap map, int actIndex)
		{
			return this.AddMarkedRooms(map);
		}

		// Token: 0x06004BC3 RID: 19395 RVA: 0x002143E0 File Offset: 0x002125E0
		private ActMap AddMarkedRooms(ActMap map)
		{
			if (base.Owner.RunState.CurrentActIndex != this.FurCoatActIndex)
			{
				return map;
			}
			List<MapCoord> markedCoords = this.GetMarkedCoords();
			bool flag = markedCoords == null;
			if (!flag)
			{
				flag = !markedCoords.TrueForAll((MapCoord c) => map.HasPoint(c) && (map.GetPoint(c).PointType == MapPointType.Monster || map.GetPoint(c).PointType == MapPointType.Elite));
			}
			if (flag)
			{
				Rng rng = new Rng(base.Owner.RunState.Rng.Seed + (uint)base.Owner.NetId + (uint)StringHelper.GetDeterministicHashCode("FurCoat"), 0);
				List<MapPoint> list = map.GetAllMapPoints().Where(delegate(MapPoint p)
				{
					MapPointType pointType = p.PointType;
					bool flag2 = pointType - MapPointType.Monster <= 1;
					if (flag2)
					{
						return !p.Quests.Any((AbstractModel q) => q is FurCoat);
					}
					return false;
				}).ToList<MapPoint>();
				list.UnstableShuffle(rng);
				int intValue = base.DynamicVars["Combats"].IntValue;
				List<MapPoint> list2 = list.Take(intValue).ToList<MapPoint>();
				this.FurCoatCoordCols = new int[list2.Count];
				this.FurCoatCoordRows = new int[list2.Count];
				for (int i = 0; i < list2.Count; i++)
				{
					this.FurCoatCoordCols[i] = list2[i].coord.col;
					this.FurCoatCoordRows[i] = list2[i].coord.row;
				}
				this.FurCoatCoordsSet = true;
				using (List<MapPoint>.Enumerator enumerator = list2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MapPoint mapPoint = enumerator.Current;
						mapPoint.AddQuest(this);
					}
					goto IL_022D;
				}
			}
			foreach (MapCoord mapCoord in markedCoords)
			{
				MapPoint point = map.GetPoint(mapCoord);
				if (point == null)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(95, 1);
					defaultInterpolatedStringHandler.AppendLiteral("Loaded a fur coat map with coordinate ");
					defaultInterpolatedStringHandler.AppendFormatted<MapCoord>(mapCoord);
					defaultInterpolatedStringHandler.AppendLiteral(", but the generated map does not ");
					defaultInterpolatedStringHandler.AppendLiteral("contain that coordinate!");
					throw new InvalidOperationException(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				point.AddQuest(this);
			}
			IL_022D:
			return map;
		}

		// Token: 0x06004BC4 RID: 19396 RVA: 0x0021463C File Offset: 0x0021283C
		public override async Task BeforeCombatStart()
		{
			List<MapCoord> markedCoords = this.GetMarkedCoords();
			if (markedCoords != null && markedCoords.Contains(base.Owner.RunState.CurrentMapPoint.coord))
			{
				base.Flash();
				IReadOnlyList<Creature> hittableEnemies = base.Owner.Creature.CombatState.HittableEnemies;
				VfxCmd.PlayOnCreatureCenters(hittableEnemies, "vfx/vfx_bite");
				foreach (Creature creature in hittableEnemies)
				{
					await CreatureCmd.SetCurrentHp(creature, 1m);
				}
				IEnumerator<Creature> enumerator = null;
			}
		}

		// Token: 0x06004BC5 RID: 19397 RVA: 0x00214680 File Offset: 0x00212880
		[NullableContext(2)]
		public List<MapCoord> GetMarkedCoords()
		{
			if (!this.FurCoatCoordsSet)
			{
				return null;
			}
			List<MapCoord> list = new List<MapCoord>();
			for (int i = 0; i < this.FurCoatCoordCols.Length; i++)
			{
				list.Add(new MapCoord
				{
					col = this.FurCoatCoordCols[i],
					row = this.FurCoatCoordRows[i]
				});
			}
			return list;
		}

		// Token: 0x040021B4 RID: 8628
		private const string _combatsKey = "Combats";

		// Token: 0x040021B5 RID: 8629
		private int _furCoatActIndex = -1;
	}
}
