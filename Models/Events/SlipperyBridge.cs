using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007E9 RID: 2025
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SlipperyBridge : EventModel
	{
		// Token: 0x1700185C RID: 6236
		// (get) Token: 0x06006253 RID: 25171 RVA: 0x0024A6A0 File Offset: 0x002488A0
		// (set) Token: 0x06006254 RID: 25172 RVA: 0x0024A6A8 File Offset: 0x002488A8
		private int NumberOfHoldOns
		{
			get
			{
				return this._numberOfHoldOns;
			}
			set
			{
				base.AssertMutable();
				this._numberOfHoldOns = value;
			}
		}

		// Token: 0x1700185D RID: 6237
		// (get) Token: 0x06006255 RID: 25173 RVA: 0x0024A6B7 File Offset: 0x002488B7
		// (set) Token: 0x06006256 RID: 25174 RVA: 0x0024A6BF File Offset: 0x002488BF
		[Nullable(2)]
		private CardModel RandomCardToLose
		{
			[NullableContext(2)]
			get
			{
				return this._randomCardToLose;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				this._randomCardToLose = value;
			}
		}

		// Token: 0x1700185E RID: 6238
		// (get) Token: 0x06006257 RID: 25175 RVA: 0x0024A6CE File Offset: 0x002488CE
		private int CurrentHpLoss
		{
			get
			{
				return 3 + this.NumberOfHoldOns;
			}
		}

		// Token: 0x1700185F RID: 6239
		// (get) Token: 0x06006258 RID: 25176 RVA: 0x0024A6D8 File Offset: 0x002488D8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new StringVar("RandomCard", ""),
					new DynamicVar("HpLoss", this.CurrentHpLoss)
				});
			}
		}

		// Token: 0x06006259 RID: 25177 RVA: 0x0024A70F File Offset: 0x0024890F
		public override bool IsAllowed(RunState runState)
		{
			if (runState.TotalFloor > 6)
			{
				return runState.Players.All((Player p) => p.Deck.Cards.Any((CardModel c) => c.IsRemovable));
			}
			return false;
		}

		// Token: 0x0600625A RID: 25178 RVA: 0x0024A748 File Offset: 0x00248948
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			this.GetNewRandomCard();
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.Overcome), "SLIPPERY_BRIDGE.pages.INITIAL.options.OVERCOME", new IHoverTip[] { HoverTipFactory.FromCard(this.RandomCardToLose, false) }),
				new EventOption(this, new Func<Task>(this.HoldOn), "SLIPPERY_BRIDGE.pages.INITIAL.options.HOLD_ON_0", Array.Empty<IHoverTip>()).ThatDoesDamage(this.CurrentHpLoss)
			});
		}

		// Token: 0x0600625B RID: 25179 RVA: 0x0024A7C4 File Offset: 0x002489C4
		public override void OnRoomEnter()
		{
			NEventRoom instance = NEventRoom.Instance;
			if (instance == null)
			{
				return;
			}
			Control vfxContainer = instance.VfxContainer;
			if (vfxContainer == null)
			{
				return;
			}
			vfxContainer.AddChildSafely(NRainVfx.Create());
		}

		// Token: 0x0600625C RID: 25180 RVA: 0x0024A7E4 File Offset: 0x002489E4
		private void GetNewRandomCard()
		{
			List<CardModel> list;
			if (this.RandomCardToLose == null)
			{
				list = base.Owner.Deck.Cards.Where((CardModel c) => c.Rarity != CardRarity.Basic).ToList<CardModel>();
			}
			else
			{
				list = base.Owner.Deck.Cards.Where((CardModel c) => c.GetType() != this.RandomCardToLose.GetType()).ToList<CardModel>();
			}
			list.RemoveAll((CardModel c) => !c.IsRemovable);
			if (list.Count == 0)
			{
				list = base.Owner.Deck.Cards.Where((CardModel c) => c.IsRemovable).ToList<CardModel>();
			}
			this.RandomCardToLose = base.Rng.NextItem<CardModel>(list);
			StringVar stringVar = (StringVar)base.DynamicVars["RandomCard"];
			stringVar.StringValue = this.RandomCardToLose.Title;
		}

		// Token: 0x0600625D RID: 25181 RVA: 0x0024A900 File Offset: 0x00248B00
		private async Task Overcome()
		{
			await CardPileCmd.RemoveFromDeck(this.RandomCardToLose, true);
			base.SetEventFinished(base.L10NLookup("SLIPPERY_BRIDGE.pages.OVERCOME.description"));
		}

		// Token: 0x0600625E RID: 25182 RVA: 0x0024A944 File Offset: 0x00248B44
		private async Task HoldOn()
		{
			await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner.Creature, this.CurrentHpLoss, ValueProp.Unblockable | ValueProp.Unpowered, null, null);
			this.NumberOfHoldOns++;
			base.DynamicVars["HpLoss"].BaseValue = this.CurrentHpLoss;
			this.GetNewRandomCard();
			string holdOnSuffix = this.GetHoldOnSuffix(this.NumberOfHoldOns - 1);
			string holdOnSuffix2 = this.GetHoldOnSuffix(this.NumberOfHoldOns);
			string text = "SLIPPERY_BRIDGE.pages.HOLD_ON_" + holdOnSuffix + ".options.HOLD_ON_" + holdOnSuffix2;
			this.SetEventState(base.L10NLookup("SLIPPERY_BRIDGE.pages.HOLD_ON_" + holdOnSuffix + ".description"), new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.Overcome), "SLIPPERY_BRIDGE.pages.INITIAL.options.OVERCOME", new IHoverTip[] { HoverTipFactory.FromCard(this.RandomCardToLose, false) }),
				new EventOption(this, new Func<Task>(this.HoldOn), text, Array.Empty<IHoverTip>()).ThatDoesDamage(this.CurrentHpLoss)
			}));
		}

		// Token: 0x0600625F RID: 25183 RVA: 0x0024A987 File Offset: 0x00248B87
		private string GetHoldOnSuffix(int holdOnNumber)
		{
			if (holdOnNumber >= 7)
			{
				return "LOOP";
			}
			return holdOnNumber.ToString();
		}

		// Token: 0x040024C6 RID: 9414
		private const string _randomCardKey = "RandomCard";

		// Token: 0x040024C7 RID: 9415
		private const string _hpLossKey = "HpLoss";

		// Token: 0x040024C8 RID: 9416
		private const string _overcomeLocKey = "SLIPPERY_BRIDGE.pages.INITIAL.options.OVERCOME";

		// Token: 0x040024C9 RID: 9417
		private const int _initialHpLoss = 3;

		// Token: 0x040024CA RID: 9418
		private int _numberOfHoldOns;

		// Token: 0x040024CB RID: 9419
		[Nullable(2)]
		private CardModel _randomCardToLose;
	}
}
