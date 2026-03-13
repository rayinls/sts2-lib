using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000807 RID: 2055
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BattlewornDummyEventEncounter : EncounterModel
	{
		// Token: 0x170018A8 RID: 6312
		// (get) Token: 0x06006375 RID: 25461 RVA: 0x0024FBB6 File Offset: 0x0024DDB6
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170018A9 RID: 6313
		// (get) Token: 0x06006376 RID: 25462 RVA: 0x0024FBB9 File Offset: 0x0024DDB9
		public override bool ShouldGiveRewards
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170018AA RID: 6314
		// (get) Token: 0x06006377 RID: 25463 RVA: 0x0024FBBC File Offset: 0x0024DDBC
		// (set) Token: 0x06006378 RID: 25464 RVA: 0x0024FBC4 File Offset: 0x0024DDC4
		public BattlewornDummyEventEncounter.DummySetting Setting
		{
			get
			{
				return this._setting;
			}
			set
			{
				base.AssertMutable();
				this._setting = value;
			}
		}

		// Token: 0x170018AB RID: 6315
		// (get) Token: 0x06006379 RID: 25465 RVA: 0x0024FBD3 File Offset: 0x0024DDD3
		// (set) Token: 0x0600637A RID: 25466 RVA: 0x0024FBDB File Offset: 0x0024DDDB
		public bool RanOutOfTime
		{
			get
			{
				return this._ranOutOfTime;
			}
			set
			{
				base.AssertMutable();
				this._ranOutOfTime = value;
			}
		}

		// Token: 0x170018AC RID: 6316
		// (get) Token: 0x0600637B RID: 25467 RVA: 0x0024FBEA File Offset: 0x0024DDEA
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlyArray<MonsterModel>(new MonsterModel[]
				{
					ModelDb.Monster<BattleFriendV1>(),
					ModelDb.Monster<BattleFriendV2>(),
					ModelDb.Monster<BattleFriendV3>()
				});
			}
		}

		// Token: 0x0600637C RID: 25468 RVA: 0x0024FC10 File Offset: 0x0024DE10
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			MonsterModel monsterModel;
			switch (this.Setting)
			{
			case BattlewornDummyEventEncounter.DummySetting.Setting1:
				monsterModel = ModelDb.Monster<BattleFriendV1>();
				break;
			case BattlewornDummyEventEncounter.DummySetting.Setting2:
				monsterModel = ModelDb.Monster<BattleFriendV2>();
				break;
			case BattlewornDummyEventEncounter.DummySetting.Setting3:
				monsterModel = ModelDb.Monster<BattleFriendV3>();
				break;
			default:
				throw new InvalidOperationException("Setting must be set!");
			}
			MonsterModel monsterModel2 = monsterModel;
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(monsterModel2.ToMutable(), null));
		}

		// Token: 0x0600637D RID: 25469 RVA: 0x0024FC70 File Offset: 0x0024DE70
		public override Dictionary<string, string> SaveCustomState()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["Setting"] = this.Setting.ToString();
			dictionary["RanOutOfTime"] = this.RanOutOfTime.ToString();
			return dictionary;
		}

		// Token: 0x0600637E RID: 25470 RVA: 0x0024FCBA File Offset: 0x0024DEBA
		public override void LoadCustomState(Dictionary<string, string> state)
		{
			this.Setting = Enum.Parse<BattlewornDummyEventEncounter.DummySetting>(state["Setting"]);
			this.RanOutOfTime = bool.Parse(state["RanOutOfTime"]);
		}

		// Token: 0x04002514 RID: 9492
		private const string _settingKey = "Setting";

		// Token: 0x04002515 RID: 9493
		private const string _ranOutOfTimeKey = "RanOutOfTime";

		// Token: 0x04002516 RID: 9494
		private BattlewornDummyEventEncounter.DummySetting _setting;

		// Token: 0x04002517 RID: 9495
		private bool _ranOutOfTime;

		// Token: 0x02001DC4 RID: 7620
		[NullableContext(0)]
		public enum DummySetting
		{
			// Token: 0x040077BB RID: 30651
			None,
			// Token: 0x040077BC RID: 30652
			Setting1,
			// Token: 0x040077BD RID: 30653
			Setting2,
			// Token: 0x040077BE RID: 30654
			Setting3
		}
	}
}
