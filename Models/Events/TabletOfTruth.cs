using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007F0 RID: 2032
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TabletOfTruth : EventModel
	{
		// Token: 0x17001867 RID: 6247
		// (get) Token: 0x0600628B RID: 25227 RVA: 0x0024B3F4 File Offset: 0x002495F4
		// (set) Token: 0x0600628C RID: 25228 RVA: 0x0024B3FC File Offset: 0x002495FC
		private int DecipherCount
		{
			get
			{
				return this._decipherCount;
			}
			set
			{
				base.AssertMutable();
				this._decipherCount = value;
			}
		}

		// Token: 0x0600628D RID: 25229 RVA: 0x0024B40C File Offset: 0x0024960C
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.Decipher), "TABLET_OF_TRUTH.pages.INITIAL.options.DECIPHER_1", Array.Empty<IHoverTip>()).ThatWillKillPlayerIf((Player p) => p.Creature.MaxHp <= base.DynamicVars["DecipherMaxHpLoss"].BaseValue),
				new EventOption(this, new Func<Task>(this.Smash), "TABLET_OF_TRUTH.pages.INITIAL.options.SMASH", Array.Empty<IHoverTip>())
			});
		}

		// Token: 0x17001868 RID: 6248
		// (get) Token: 0x0600628E RID: 25230 RVA: 0x0024B473 File Offset: 0x00249673
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("SmashHPGain", 20m),
					new DynamicVar("DecipherMaxHpLoss", 3m)
				});
			}
		}

		// Token: 0x0600628F RID: 25231 RVA: 0x0024B4A8 File Offset: 0x002496A8
		private async Task Smash()
		{
			Creature creature = base.Owner.Creature;
			await CreatureCmd.Heal(creature, base.DynamicVars["SmashHPGain"].BaseValue, true);
			base.SetEventFinished(base.L10NLookup("TABLET_OF_TRUTH.pages.SMASH.description"));
		}

		// Token: 0x06006290 RID: 25232 RVA: 0x0024B4EC File Offset: 0x002496EC
		private async Task Decipher()
		{
			await this.LoseMaxHpAndUpgrade(base.DynamicVars["DecipherMaxHpLoss"].BaseValue);
			this.DecipherCount++;
			if (this.DecipherCount == 5)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 1);
				defaultInterpolatedStringHandler.AppendLiteral("TABLET_OF_TRUTH.pages.DECIPHER_");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.DecipherCount);
				defaultInterpolatedStringHandler.AppendLiteral(".description");
				base.SetEventFinished(base.L10NLookup(defaultInterpolatedStringHandler.ToStringAndClear()));
			}
			else
			{
				base.DynamicVars["DecipherMaxHpLoss"].BaseValue = this.GetDecipherCost();
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 1);
				defaultInterpolatedStringHandler.AppendLiteral("TABLET_OF_TRUTH.pages.DECIPHER_");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.DecipherCount);
				defaultInterpolatedStringHandler.AppendLiteral(".description");
				LocString locString = base.L10NLookup(defaultInterpolatedStringHandler.ToStringAndClear());
				EventOption[] array = new EventOption[2];
				int num = 0;
				Func<Task> func = new Func<Task>(this.Decipher);
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(48, 1);
				defaultInterpolatedStringHandler.AppendLiteral("TABLET_OF_TRUTH.pages.DECIPHER_");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.DecipherCount);
				defaultInterpolatedStringHandler.AppendLiteral(".options.DECIPHER");
				array[num] = new EventOption(this, func, defaultInterpolatedStringHandler.ToStringAndClear(), Array.Empty<IHoverTip>()).ThatWillKillPlayerIf((Player p) => p.Creature.MaxHp <= base.DynamicVars["DecipherMaxHpLoss"].BaseValue);
				array[1] = new EventOption(this, new Func<Task>(this.GiveUp), "TABLET_OF_TRUTH.pages.DECIPHER.options.GIVE_UP", Array.Empty<IHoverTip>());
				this.SetEventState(locString, new <>z__ReadOnlyArray<EventOption>(array));
			}
		}

		// Token: 0x06006291 RID: 25233 RVA: 0x0024B530 File Offset: 0x00249730
		public int GetDecipherCost()
		{
			Player owner = base.Owner;
			switch (this.DecipherCount)
			{
			case 1:
				return 6;
			case 2:
				return 12;
			case 3:
				return 24;
			case 4:
				return owner.Creature.MaxHp - 1;
			default:
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(37, 1);
				defaultInterpolatedStringHandler.AppendLiteral("DecipherCount: ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.DecipherCount);
				defaultInterpolatedStringHandler.AppendLiteral(" should not be called.");
				Log.Error(defaultInterpolatedStringHandler.ToStringAndClear(), 2);
				return 999;
			}
			}
		}

		// Token: 0x06006292 RID: 25234 RVA: 0x0024B5BC File Offset: 0x002497BC
		private Task GiveUp()
		{
			base.SetEventFinished(base.L10NLookup("TABLET_OF_TRUTH.pages.GIVE_UP.description"));
			return Task.CompletedTask;
		}

		// Token: 0x06006293 RID: 25235 RVA: 0x0024B5D4 File Offset: 0x002497D4
		private async Task LoseMaxHpAndUpgrade(decimal hp)
		{
			if (hp < base.Owner.Creature.MaxHp)
			{
				await CreatureCmd.LoseMaxHp(new ThrowingPlayerChoiceContext(), base.Owner.Creature, hp, false);
				List<CardModel> list = PileType.Deck.GetPile(base.Owner).Cards.Where((CardModel c) => c.IsUpgradable).ToList<CardModel>();
				if (this._decipherCount == 4)
				{
					foreach (CardModel cardModel in list)
					{
						CardCmd.Upgrade(cardModel, CardPreviewStyle.MessyLayout);
						await Cmd.CustomScaledWait(0.1f, 0.2f, false, default(CancellationToken));
					}
					List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
					await Cmd.CustomScaledWait(0.6f, 1.2f, false, default(CancellationToken));
				}
				else if (list.Count != 0)
				{
					CardCmd.Upgrade(base.Rng.NextItem<CardModel>(list), CardPreviewStyle.EventLayout);
				}
			}
			else
			{
				await CreatureCmd.LoseMaxHp(new ThrowingPlayerChoiceContext(), base.Owner.Creature, base.Owner.Creature.MaxHp - 1, false);
				await CreatureCmd.Kill(base.Owner.Creature, false);
			}
		}

		// Token: 0x040024D9 RID: 9433
		private const string _smashHpGainKey = "SmashHPGain";

		// Token: 0x040024DA RID: 9434
		private const string _decipherHpLossKey = "DecipherMaxHpLoss";

		// Token: 0x040024DB RID: 9435
		private int _decipherCount;
	}
}
