using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Entities.Ancients;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.Relics;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007DC RID: 2012
	[NullableContext(1)]
	[Nullable(0)]
	public class Neow : AncientEventModel
	{
		// Token: 0x17001823 RID: 6179
		// (get) Token: 0x060061CA RID: 25034 RVA: 0x002476A5 File Offset: 0x002458A5
		public override string AmbientBgm
		{
			get
			{
				return "event:/sfx/ambience/act1_neow";
			}
		}

		// Token: 0x17001824 RID: 6180
		// (get) Token: 0x060061CB RID: 25035 RVA: 0x002476AC File Offset: 0x002458AC
		public override Color ButtonColor
		{
			get
			{
				return new Color(0f, 0.1f, 0.2f, 0.5f);
			}
		}

		// Token: 0x17001825 RID: 6181
		// (get) Token: 0x060061CC RID: 25036 RVA: 0x002476C7 File Offset: 0x002458C7
		public override Color DialogueColor
		{
			get
			{
				return new Color("28454F");
			}
		}

		// Token: 0x17001826 RID: 6182
		// (get) Token: 0x060061CD RID: 25037 RVA: 0x002476D4 File Offset: 0x002458D4
		public override LocString InitialDescription
		{
			get
			{
				Player owner = base.Owner;
				if (owner != null && owner.RunState.Modifiers.Count <= 0)
				{
					return base.InitialDescription;
				}
				return base.L10NLookup(base.Id.Entry + ".EVENT.description");
			}
		}

		// Token: 0x17001827 RID: 6183
		// (get) Token: 0x060061CE RID: 25038 RVA: 0x00247728 File Offset: 0x00245928
		public override IEnumerable<EventOption> AllPossibleOptions
		{
			get
			{
				return this.PositiveOptions.Concat(this.CurseOptions).Concat(new <>z__ReadOnlyArray<EventOption>(new EventOption[] { this.ClericOption, this.BundleOption, this.EmpowerOption, this.ToughnessOption, this.SafetyOption, this.PatienceOption, this.ScavengerOption }));
			}
		}

		// Token: 0x17001828 RID: 6184
		// (get) Token: 0x060061CF RID: 25039 RVA: 0x00247798 File Offset: 0x00245998
		private IEnumerable<EventOption> PositiveOptions
		{
			get
			{
				return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
				{
					base.RelicOption<ArcaneScroll>("INITIAL", "NEOW.pages.DONE.POSITIVE.description"),
					base.RelicOption<BoomingConch>("INITIAL", "NEOW.pages.DONE.POSITIVE.description"),
					base.RelicOption<Pomander>("INITIAL", "NEOW.pages.DONE.POSITIVE.description"),
					base.RelicOption<GoldenPearl>("INITIAL", "NEOW.pages.DONE.POSITIVE.description"),
					base.RelicOption<LeadPaperweight>("INITIAL", "NEOW.pages.DONE.POSITIVE.description"),
					base.RelicOption<NewLeaf>("INITIAL", "NEOW.pages.DONE.POSITIVE.description"),
					base.RelicOption<NeowsTorment>("INITIAL", "NEOW.pages.DONE.POSITIVE.description"),
					base.RelicOption<PreciseScissors>("INITIAL", "NEOW.pages.DONE.POSITIVE.description"),
					base.RelicOption<LostCoffer>("INITIAL", "NEOW.pages.DONE.POSITIVE.description")
				});
			}
		}

		// Token: 0x17001829 RID: 6185
		// (get) Token: 0x060061D0 RID: 25040 RVA: 0x0024785C File Offset: 0x00245A5C
		private EventOption ToughnessOption
		{
			get
			{
				return base.RelicOption<NutritiousOyster>("INITIAL", "NEOW.pages.DONE.POSITIVE.description");
			}
		}

		// Token: 0x1700182A RID: 6186
		// (get) Token: 0x060061D1 RID: 25041 RVA: 0x0024786E File Offset: 0x00245A6E
		private EventOption SafetyOption
		{
			get
			{
				return base.RelicOption<StoneHumidifier>("INITIAL", "NEOW.pages.DONE.POSITIVE.description");
			}
		}

		// Token: 0x1700182B RID: 6187
		// (get) Token: 0x060061D2 RID: 25042 RVA: 0x00247880 File Offset: 0x00245A80
		private EventOption ClericOption
		{
			get
			{
				return base.RelicOption<MassiveScroll>("INITIAL", "NEOW.pages.DONE.POSITIVE.description");
			}
		}

		// Token: 0x1700182C RID: 6188
		// (get) Token: 0x060061D3 RID: 25043 RVA: 0x00247892 File Offset: 0x00245A92
		private EventOption PatienceOption
		{
			get
			{
				return base.RelicOption<LavaRock>("INITIAL", "NEOW.pages.DONE.POSITIVE.description");
			}
		}

		// Token: 0x1700182D RID: 6189
		// (get) Token: 0x060061D4 RID: 25044 RVA: 0x002478A4 File Offset: 0x00245AA4
		private EventOption ScavengerOption
		{
			get
			{
				return base.RelicOption<SmallCapsule>("INITIAL", "NEOW.pages.DONE.POSITIVE.description");
			}
		}

		// Token: 0x1700182E RID: 6190
		// (get) Token: 0x060061D5 RID: 25045 RVA: 0x002478B6 File Offset: 0x00245AB6
		private EventOption EmpowerOption
		{
			get
			{
				return base.RelicOption<SilverCrucible>("INITIAL", "NEOW.pages.DONE.CURSED.description");
			}
		}

		// Token: 0x1700182F RID: 6191
		// (get) Token: 0x060061D6 RID: 25046 RVA: 0x002478C8 File Offset: 0x00245AC8
		private IEnumerable<EventOption> CurseOptions
		{
			get
			{
				return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
				{
					base.RelicOption<CursedPearl>("INITIAL", "NEOW.pages.DONE.POSITIVE.description"),
					base.RelicOption<LargeCapsule>("INITIAL", "NEOW.pages.DONE.CURSED.description"),
					base.RelicOption<LeafyPoultice>("INITIAL", "NEOW.pages.DONE.CURSED.description"),
					base.RelicOption<PrecariousShears>("INITIAL", "NEOW.pages.DONE.CURSED.description")
				});
			}
		}

		// Token: 0x17001830 RID: 6192
		// (get) Token: 0x060061D7 RID: 25047 RVA: 0x0024792C File Offset: 0x00245B2C
		private EventOption BundleOption
		{
			get
			{
				return base.RelicOption<ScrollBoxes>("INITIAL", "NEOW.pages.DONE.CURSED.description");
			}
		}

		// Token: 0x060061D8 RID: 25048 RVA: 0x00247940 File Offset: 0x00245B40
		protected override AncientDialogueSet DefineDialogues()
		{
			AncientDialogueSet ancientDialogueSet = new AncientDialogueSet();
			ancientDialogueSet.FirstVisitEverDialogue = new AncientDialogue(new string[] { "event:/sfx/npcs/neow/neow_welcome" });
			AncientDialogueSet ancientDialogueSet2 = ancientDialogueSet;
			Dictionary<string, IReadOnlyList<AncientDialogue>> dictionary = new Dictionary<string, IReadOnlyList<AncientDialogue>>();
			string text = AncientEventModel.CharKey<Ironclad>();
			dictionary[text] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "event:/sfx/npcs/neow/neow_welcome" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/neow/neow_curious" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/neow/neow_sleepy", "event:/sfx/npcs/neow/neow_sleepy", "event:/sfx/npcs/neow/neow_sleepy" })
				{
					VisitIndex = new int?(4)
				}
			});
			string text2 = AncientEventModel.CharKey<Silent>();
			dictionary[text2] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "event:/sfx/npcs/neow/neow_curious" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/neow/neow_sleepy" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/neow/neow_sleepy", "", "" })
				{
					VisitIndex = new int?(4)
				}
			});
			string text3 = AncientEventModel.CharKey<Defect>();
			dictionary[text3] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "event:/sfx/npcs/neow/neow_curious" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/neow/neow_sleepy" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/neow/neow_sleepy", "", "event:/sfx/npcs/neow/neow_sleepy" })
				{
					VisitIndex = new int?(4)
				}
			});
			string text4 = AncientEventModel.CharKey<Necrobinder>();
			dictionary[text4] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "event:/sfx/npcs/neow/neow_welcome", "", "event:/sfx/npcs/neow/neow_sleepy" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/neow/neow_sleepy" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "", "event:/sfx/npcs/neow/neow_sleepy" })
				{
					VisitIndex = new int?(4)
				}
			});
			string text5 = AncientEventModel.CharKey<Regent>();
			dictionary[text5] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "", "event:/sfx/npcs/neow/neow_sleepy" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/neow/neow_curious" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "", "event:/sfx/npcs/neow/neow_sleepy", "" })
				{
					VisitIndex = new int?(4)
				}
			});
			ancientDialogueSet2.CharacterDialogues = dictionary;
			ancientDialogueSet.AgnosticDialogues = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "event:/sfx/npcs/neow/neow_welcome" }),
				new AncientDialogue(new string[] { "event:/sfx/npcs/neow/neow_welcome" }),
				new AncientDialogue(new string[] { "event:/sfx/npcs/neow/neow_welcome" }),
				new AncientDialogue(new string[] { "event:/sfx/npcs/neow/neow_welcome" }),
				new AncientDialogue(new string[] { "" })
			});
			return ancientDialogueSet;
		}

		// Token: 0x17001831 RID: 6193
		// (get) Token: 0x060061D9 RID: 25049 RVA: 0x00247CD5 File Offset: 0x00245ED5
		private List<EventOption> ModifierOptions
		{
			get
			{
				base.AssertMutable();
				if (this._modifierOptions == null)
				{
					this._modifierOptions = new List<EventOption>();
				}
				return this._modifierOptions;
			}
		}

		// Token: 0x060061DA RID: 25050 RVA: 0x00247CF8 File Offset: 0x00245EF8
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			if (base.Owner.RunState.Modifiers.Count <= 0)
			{
				List<EventOption> list = this.CurseOptions.ToList<EventOption>();
				if (ScrollBoxes.CanGenerateBundles(base.Owner))
				{
					list.Add(this.BundleOption);
				}
				if (base.Owner.RunState.Players.Count == 1)
				{
					list.Add(this.EmpowerOption);
				}
				EventOption eventOption = base.Rng.NextItem<EventOption>(list);
				List<EventOption> list2 = this.PositiveOptions.ToList<EventOption>();
				if (eventOption.Relic is CursedPearl)
				{
					list2.RemoveAll((EventOption o) => o.Relic is GoldenPearl);
				}
				if (eventOption.Relic is PrecariousShears)
				{
					list2.RemoveAll((EventOption o) => o.Relic is PreciseScissors);
				}
				if (eventOption.Relic is LeafyPoultice)
				{
					list2.RemoveAll((EventOption o) => o.Relic is NewLeaf);
				}
				if (base.Owner.RunState.Players.Count > 1)
				{
					list2.Add(this.ClericOption);
				}
				if (base.Rng.NextBool())
				{
					list2.Add(this.ToughnessOption);
				}
				else
				{
					list2.Add(this.SafetyOption);
				}
				if (!(eventOption.Relic is LargeCapsule))
				{
					if (base.Rng.NextBool())
					{
						list2.Add(this.PatienceOption);
					}
					else
					{
						list2.Add(this.ScavengerOption);
					}
				}
				List<EventOption> list3 = list2.ToList<EventOption>().UnstableShuffle(base.Rng).Take(2)
					.ToList<EventOption>();
				list3.Add(eventOption);
				return list3;
			}
			foreach (ModifierModel modifierModel in base.Owner.RunState.Modifiers)
			{
				Func<Task> neowOption = modifierModel.GenerateNeowOption(this);
				if (neowOption != null)
				{
					int optionIndex = this.ModifierOptions.Count;
					this.ModifierOptions.Add(new EventOption(this, () => this.OnModifierOptionSelected(neowOption, optionIndex), modifierModel.NeowOptionTitle, modifierModel.NeowOptionDescription, modifierModel.Id.Entry, modifierModel.HoverTips.ToArray<IHoverTip>()));
				}
			}
			if (this.ModifierOptions.Count > 0)
			{
				return new <>z__ReadOnlySingleElementList<EventOption>(this.ModifierOptions[0]);
			}
			return Array.Empty<EventOption>();
		}

		// Token: 0x060061DB RID: 25051 RVA: 0x00247FB4 File Offset: 0x002461B4
		private async Task OnModifierOptionSelected(Func<Task> modifierFunc, int index)
		{
			await modifierFunc();
			if (index + 1 >= this.ModifierOptions.Count)
			{
				base.SetEventFinished(base.L10NLookup(base.Id.Entry + ".pages.DONE.description"));
			}
			else
			{
				this.SetEventState(this.InitialDescription, new <>z__ReadOnlySingleElementList<EventOption>(this.ModifierOptions[index + 1]));
			}
		}

		// Token: 0x040024A6 RID: 9382
		private const string _cursedChoiceDoneDescriptionOverride = "NEOW.pages.DONE.CURSED.description";

		// Token: 0x040024A7 RID: 9383
		private const string _positiveChoiceDoneDescriptionOverride = "NEOW.pages.DONE.POSITIVE.description";

		// Token: 0x040024A8 RID: 9384
		private const string _sfxSleepy = "event:/sfx/npcs/neow/neow_sleepy";

		// Token: 0x040024A9 RID: 9385
		private const string _sfxWelcome = "event:/sfx/npcs/neow/neow_welcome";

		// Token: 0x040024AA RID: 9386
		private const string _sfxCurious = "event:/sfx/npcs/neow/neow_curious";

		// Token: 0x040024AB RID: 9387
		[Nullable(new byte[] { 2, 1 })]
		private List<EventOption> _modifierOptions;
	}
}
