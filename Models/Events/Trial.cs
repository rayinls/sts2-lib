using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007FB RID: 2043
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Trial : EventModel
	{
		// Token: 0x0600630C RID: 25356 RVA: 0x0024DE50 File Offset: 0x0024C050
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.Accept), "TRIAL.pages.INITIAL.options.ACCEPT", Array.Empty<IHoverTip>()),
				new EventOption(this, new Func<Task>(this.Reject), "TRIAL.pages.INITIAL.options.REJECT", Array.Empty<IHoverTip>())
			});
		}

		// Token: 0x17001890 RID: 6288
		// (get) Token: 0x0600630D RID: 25357 RVA: 0x0024DEA6 File Offset: 0x0024C0A6
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("EntrantNumber", -1m));
			}
		}

		// Token: 0x17001891 RID: 6289
		// (get) Token: 0x0600630E RID: 25358 RVA: 0x0024DEBC File Offset: 0x0024C0BC
		private static string TrialStartedPath
		{
			get
			{
				return ImageHelper.GetImagePath("events/trial_started.png");
			}
		}

		// Token: 0x0600630F RID: 25359 RVA: 0x0024DEC8 File Offset: 0x0024C0C8
		public override IEnumerable<string> GetAssetPaths(IRunState runState)
		{
			List<string> list = new List<string>();
			list.AddRange(base.GetAssetPaths(runState));
			list.Add(Trial.TrialStartedPath);
			list.Add(Trial._trialMerchantVfx);
			list.Add(Trial._trialNobleVfx);
			list.Add(Trial._trialNondescriptVfx);
			return new <>z__ReadOnlyList<string>(list);
		}

		// Token: 0x06006310 RID: 25360 RVA: 0x0024DF18 File Offset: 0x0024C118
		private Task Accept()
		{
			if (LocalContext.IsMe(base.Owner))
			{
				NEventRoom.Instance.Layout.RemoveNodesOnPortrait();
			}
			string text;
			string text2;
			EventOption[] array;
			switch (base.Rng.NextInt(3))
			{
			case 0:
				text = Trial._trialMerchantVfx;
				text2 = "TRIAL.pages.MERCHANT.description";
				array = new EventOption[]
				{
					new EventOption(this, new Func<Task>(this.MerchantGuilty), "TRIAL.pages.MERCHANT.options.GUILTY", HoverTipFactory.FromCardWithCardHoverTips<Regret>(false)),
					new EventOption(this, new Func<Task>(this.MerchantInnocent), "TRIAL.pages.MERCHANT.options.INNOCENT", HoverTipFactory.FromCardWithCardHoverTips<Shame>(false))
				};
				break;
			case 1:
				text = Trial._trialNobleVfx;
				text2 = "TRIAL.pages.NOBLE.description";
				array = new EventOption[]
				{
					new EventOption(this, new Func<Task>(this.NobleGuilty), "TRIAL.pages.NOBLE.options.GUILTY", Array.Empty<IHoverTip>()),
					new EventOption(this, new Func<Task>(this.NobleInnocent), "TRIAL.pages.NOBLE.options.INNOCENT", HoverTipFactory.FromCardWithCardHoverTips<Regret>(false))
				};
				break;
			case 2:
				text = Trial._trialNondescriptVfx;
				text2 = "TRIAL.pages.NONDESCRIPT.description";
				array = new EventOption[]
				{
					new EventOption(this, new Func<Task>(this.NondescriptGuilty), "TRIAL.pages.NONDESCRIPT.options.GUILTY", HoverTipFactory.FromCardWithCardHoverTips<Doubt>(false)),
					new EventOption(this, new Func<Task>(this.NondescriptInnocent), "TRIAL.pages.NONDESCRIPT.options.INNOCENT", HoverTipFactory.FromCardWithCardHoverTips<Doubt>(false).Concat(new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Transform, Array.Empty<DynamicVar>()))))
				};
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			this.AddVfxAnchoredToPortrait(text);
			if (LocalContext.IsMe(base.Owner))
			{
				NEventRoom.Instance.SetPortrait(PreloadManager.Cache.GetTexture2D(Trial.TrialStartedPath));
			}
			LocString locString = base.L10NLookup("TRIAL.trialFormat");
			locString.Add(new StringVar("TrialStory", base.L10NLookup(text2).GetRawText()));
			this.SetEventState(locString, array);
			return Task.CompletedTask;
		}

		// Token: 0x06006311 RID: 25361 RVA: 0x0024E0E8 File Offset: 0x0024C2E8
		private Task Reject()
		{
			EventOption[] array = new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.Accept), "TRIAL.pages.REJECT.options.ACCEPT", Array.Empty<IHoverTip>()),
				new EventOption(this, new Func<Task>(this.DoubleDown), "TRIAL.pages.REJECT.options.DOUBLE_DOWN", false, true, Array.Empty<IHoverTip>()).ThatDoesDamage(9999m)
			};
			LocString locString = base.L10NLookup("TRIAL.pages.REJECT.description");
			this.SetEventState(locString, array);
			return Task.CompletedTask;
		}

		// Token: 0x06006312 RID: 25362 RVA: 0x0024E164 File Offset: 0x0024C364
		private Task DoubleDown()
		{
			NModalContainer.Instance.Add(NAbandonRunConfirmPopup.Create(null), true);
			return Task.CompletedTask;
		}

		// Token: 0x06006313 RID: 25363 RVA: 0x0024E17C File Offset: 0x0024C37C
		private void AddVfxAnchoredToPortrait(string portraitPath)
		{
			if (LocalContext.IsMe(base.Owner))
			{
				Node2D node2D = PreloadManager.Cache.GetScene(portraitPath).Instantiate<Node2D>(PackedScene.GenEditState.Disabled);
				node2D.Position = new Vector2(292f, 68f);
				NEventRoom.Instance.Layout.AddVfxAnchoredToPortrait(node2D);
			}
		}

		// Token: 0x06006314 RID: 25364 RVA: 0x0024E1D0 File Offset: 0x0024C3D0
		private async Task MerchantGuilty()
		{
			await CardPileCmd.AddCurseToDeck<Regret>(base.Owner);
			for (int i = 0; i < 2; i++)
			{
				await RelicCmd.Obtain(RelicFactory.PullNextRelicFromFront(base.Owner).ToMutable(), base.Owner, -1);
			}
			this.SetTrialFinished("TRIAL.pages.MERCHANT_GUILTY.description");
		}

		// Token: 0x06006315 RID: 25365 RVA: 0x0024E214 File Offset: 0x0024C414
		private async Task MerchantInnocent()
		{
			await CardPileCmd.AddCurseToDeck<Shame>(base.Owner);
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.UpgradeSelectionPrompt, 2);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForUpgrade(base.Owner, cardSelectorPrefs);
			foreach (CardModel cardModel in enumerable)
			{
				CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
			}
			this.SetTrialFinished("TRIAL.pages.MERCHANT_INNOCENT.description");
		}

		// Token: 0x06006316 RID: 25366 RVA: 0x0024E258 File Offset: 0x0024C458
		private async Task NobleGuilty()
		{
			await CreatureCmd.Heal(base.Owner.Creature, 10m, true);
			this.SetTrialFinished("TRIAL.pages.NOBLE_GUILTY.description");
		}

		// Token: 0x06006317 RID: 25367 RVA: 0x0024E29C File Offset: 0x0024C49C
		private async Task NobleInnocent()
		{
			await CardPileCmd.AddCurseToDeck<Regret>(base.Owner);
			await PlayerCmd.GainGold(300m, base.Owner, false);
			this.SetTrialFinished("TRIAL.pages.NOBLE_INNOCENT.description");
		}

		// Token: 0x06006318 RID: 25368 RVA: 0x0024E2E0 File Offset: 0x0024C4E0
		private async Task NondescriptGuilty()
		{
			await CardPileCmd.AddCurseToDeck<Doubt>(base.Owner);
			List<Reward> list = new List<Reward>();
			for (int i = 0; i < 2; i++)
			{
				list.Add(new CardReward(CardCreationOptions.ForNonCombatWithDefaultOdds(new <>z__ReadOnlySingleElementList<CardPoolModel>(base.Owner.Character.CardPool), null), 3, base.Owner));
			}
			await RewardsCmd.OfferCustom(base.Owner, list);
			this.SetTrialFinished("TRIAL.pages.NONDESCRIPT_GUILTY.description");
		}

		// Token: 0x06006319 RID: 25369 RVA: 0x0024E324 File Offset: 0x0024C524
		private async Task NondescriptInnocent()
		{
			await CardPileCmd.AddCurseToDeck<Doubt>(base.Owner);
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.TransformSelectionPrompt, 2);
			List<CardModel> list = (await CardSelectCmd.FromDeckForTransformation(base.Owner, cardSelectorPrefs, null)).ToList<CardModel>();
			foreach (CardModel cardModel in list)
			{
				await CardCmd.TransformToRandom(cardModel, base.Owner.RunState.Rng.Niche, CardPreviewStyle.EventLayout);
			}
			List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
			this.SetTrialFinished("TRIAL.pages.NONDESCRIPT_INNOCENT.description");
		}

		// Token: 0x0600631A RID: 25370 RVA: 0x0024E368 File Offset: 0x0024C568
		private void SetTrialFinished(string trialResultLoc)
		{
			LocString locString = base.L10NLookup("TRIAL.trialResult");
			locString.Add(new StringVar("TrialResult", base.L10NLookup(trialResultLoc).GetRawText()));
			base.SetEventFinished(locString);
		}

		// Token: 0x0600631B RID: 25371 RVA: 0x0024E3A4 File Offset: 0x0024C5A4
		public override void CalculateVars()
		{
			if (base.DynamicVars["EntrantNumber"].BaseValue == -1m)
			{
				base.DynamicVars["EntrantNumber"].BaseValue = Rng.Chaotic.NextInt(101, 999);
			}
		}

		// Token: 0x040024F4 RID: 9460
		private static readonly string _trialMerchantVfx = SceneHelper.GetScenePath("vfx/events/trial_merchant_vfx");

		// Token: 0x040024F5 RID: 9461
		private static readonly string _trialNobleVfx = SceneHelper.GetScenePath("vfx/events/trial_noble_vfx");

		// Token: 0x040024F6 RID: 9462
		private static readonly string _trialNondescriptVfx = SceneHelper.GetScenePath("vfx/events/trial_nondescript_vfx");

		// Token: 0x040024F7 RID: 9463
		private const string _entrantNumberKey = "EntrantNumber";

		// Token: 0x040024F8 RID: 9464
		private const string _trialResultKey = "TrialResult";

		// Token: 0x040024F9 RID: 9465
		private const string _trialStoryKey = "TrialStory";
	}
}
