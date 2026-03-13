using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.TestSupport;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007CF RID: 1999
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DollRoom : EventModel
	{
		// Token: 0x1700180E RID: 6158
		// (get) Token: 0x0600615C RID: 24924 RVA: 0x002459EA File Offset: 0x00243BEA
		// (set) Token: 0x0600615D RID: 24925 RVA: 0x002459F2 File Offset: 0x00243BF2
		private int? AmbienceHandle
		{
			get
			{
				return this._ambienceHandle;
			}
			set
			{
				base.AssertMutable();
				this._ambienceHandle = value;
			}
		}

		// Token: 0x0600615E RID: 24926 RVA: 0x00245A01 File Offset: 0x00243C01
		public override bool IsAllowed(RunState runState)
		{
			return runState.CurrentActIndex == 1;
		}

		// Token: 0x1700180F RID: 6159
		// (get) Token: 0x0600615F RID: 24927 RVA: 0x00245A0C File Offset: 0x00243C0C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar("TakeTimeHpLoss", 5m, ValueProp.Unblockable | ValueProp.Unpowered),
					new DamageVar("ExamineHpLoss", 15m, ValueProp.Unblockable | ValueProp.Unpowered)
				});
			}
		}

		// Token: 0x06006160 RID: 24928 RVA: 0x00245A44 File Offset: 0x00243C44
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.ChooseRandom), "DOLL_ROOM.pages.INITIAL.options.RANDOM", Array.Empty<IHoverTip>()),
				new EventOption(this, new Func<Task>(this.TakeSomeTime), "DOLL_ROOM.pages.INITIAL.options.TAKE_SOME_TIME", Array.Empty<IHoverTip>()).ThatDoesDamage(base.DynamicVars["TakeTimeHpLoss"].BaseValue),
				new EventOption(this, new Func<Task>(this.Examine), "DOLL_ROOM.pages.INITIAL.options.EXAMINE", Array.Empty<IHoverTip>()).ThatDoesDamage(base.DynamicVars["ExamineHpLoss"].BaseValue)
			});
		}

		// Token: 0x06006161 RID: 24929 RVA: 0x00245AED File Offset: 0x00243CED
		protected override Task BeforeEventStarted()
		{
			if (LocalContext.IsMe(base.Owner) && TestMode.IsOff)
			{
				this.AmbienceHandle = new int?(NDebugAudioManager.Instance.Play("doll_room_amb.mp3", 1f, PitchVariance.None));
			}
			return Task.CompletedTask;
		}

		// Token: 0x06006162 RID: 24930 RVA: 0x00245B28 File Offset: 0x00243D28
		private async Task ChooseRandom()
		{
			await this.ChooseDollAndShowDescription(base.Owner.RunState.Rng.Niche.NextItem<DollRoom.DollChoice>(DollRoom._dolls));
		}

		// Token: 0x06006163 RID: 24931 RVA: 0x00245B6C File Offset: 0x00243D6C
		private async Task TakeSomeTime()
		{
			await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner.Creature, (DamageVar)base.DynamicVars["TakeTimeHpLoss"], null, null);
			IEnumerable<DollRoom.DollChoice> enumerable = DollRoom._dolls.ToList<DollRoom.DollChoice>().StableShuffle(base.Rng).Take(2);
			List<EventOption> list = new List<EventOption>();
			foreach (DollRoom.DollChoice dollChoice in enumerable)
			{
				list.Add(this.OptionFromChoice(dollChoice));
			}
			this.SetEventState(base.L10NLookup("DOLL_ROOM.pages.TAKE_SOME_TIME.description"), list);
		}

		// Token: 0x06006164 RID: 24932 RVA: 0x00245BB0 File Offset: 0x00243DB0
		private async Task Examine()
		{
			await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner.Creature, (DamageVar)base.DynamicVars["ExamineHpLoss"], null, null);
			IEnumerable<DollRoom.DollChoice> enumerable = DollRoom._dolls.ToList<DollRoom.DollChoice>().StableShuffle(base.Rng);
			List<EventOption> list = new List<EventOption>();
			foreach (DollRoom.DollChoice dollChoice in enumerable)
			{
				list.Add(this.OptionFromChoice(dollChoice));
			}
			this.SetEventState(base.L10NLookup("DOLL_ROOM.pages.EXAMINE.description"), list);
		}

		// Token: 0x06006165 RID: 24933 RVA: 0x00245BF4 File Offset: 0x00243DF4
		private EventOption OptionFromChoice(DollRoom.DollChoice choice)
		{
			DollRoom.<>c__DisplayClass16_0 CS$<>8__locals1 = new DollRoom.<>c__DisplayClass16_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.choice = choice;
			LocString title = CS$<>8__locals1.choice.relic.Title;
			LocString locString = base.L10NLookup("DOLL_ROOM.pages.TAKE.options.TAKE.description");
			locString.Add("RelicName", CS$<>8__locals1.choice.relic.Title);
			return new EventOption(this, new Func<Task>(CS$<>8__locals1.<OptionFromChoice>g__Func|0), title, locString, CS$<>8__locals1.choice.relic.Title.GetRawText(), HoverTipFactory.FromRelic(CS$<>8__locals1.choice.relic)).WithOverridenHistoryName(CS$<>8__locals1.choice.relic.Title);
		}

		// Token: 0x06006166 RID: 24934 RVA: 0x00245C9C File Offset: 0x00243E9C
		private async Task ChooseDollAndShowDescription(DollRoom.DollChoice choice)
		{
			this.StopAudio();
			await RelicCmd.Obtain(choice.relic.ToMutable(), base.Owner, -1);
			base.SetEventFinished(base.L10NLookup(choice.descriptionKey));
		}

		// Token: 0x06006167 RID: 24935 RVA: 0x00245CE7 File Offset: 0x00243EE7
		protected override void OnEventFinished()
		{
			this.StopAudio();
		}

		// Token: 0x06006168 RID: 24936 RVA: 0x00245CF0 File Offset: 0x00243EF0
		private void StopAudio()
		{
			if (this.AmbienceHandle != null)
			{
				NDebugAudioManager.Instance.Stop(this.AmbienceHandle.Value, 0.5f);
				this.AmbienceHandle = null;
			}
		}

		// Token: 0x04002488 RID: 9352
		private const string _takeTimeHpLossKey = "TakeTimeHpLoss";

		// Token: 0x04002489 RID: 9353
		private const string _examineHpLossKey = "ExamineHpLoss";

		// Token: 0x0400248A RID: 9354
		private int? _ambienceHandle;

		// Token: 0x0400248B RID: 9355
		private static readonly DollRoom.DollChoice[] _dolls = new DollRoom.DollChoice[]
		{
			new DollRoom.DollChoice
			{
				relic = ModelDb.Relic<DaughterOfTheWind>(),
				descriptionKey = "DOLL_ROOM.pages.DAUGHTER_OF_WIND.description"
			},
			new DollRoom.DollChoice
			{
				relic = ModelDb.Relic<MrStruggles>(),
				descriptionKey = "DOLL_ROOM.pages.MR_STRUGGLES.description"
			},
			new DollRoom.DollChoice
			{
				relic = ModelDb.Relic<BingBong>(),
				descriptionKey = "DOLL_ROOM.pages.FABLE.description"
			}
		};

		// Token: 0x02001D1F RID: 7455
		[NullableContext(0)]
		private struct DollChoice : IComparable<DollRoom.DollChoice>
		{
			// Token: 0x0600AA61 RID: 43617 RVA: 0x00378866 File Offset: 0x00376A66
			public int CompareTo(DollRoom.DollChoice other)
			{
				return this.relic.CompareTo(other.relic);
			}

			// Token: 0x040074EA RID: 29930
			[Nullable(1)]
			public RelicModel relic;

			// Token: 0x040074EB RID: 29931
			[Nullable(1)]
			public string descriptionKey;
		}
	}
}
