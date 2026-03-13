using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Godot;
using MegaCrit.Sts2.Core.Entities.Ancients;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.Relics;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007F3 RID: 2035
	[NullableContext(1)]
	[Nullable(0)]
	public class Tezcatara : AncientEventModel
	{
		// Token: 0x1700186F RID: 6255
		// (get) Token: 0x060062A6 RID: 25254 RVA: 0x0024BDF3 File Offset: 0x00249FF3
		public override IEnumerable<CharacterModel> AnyCharacterDialogueBlacklist
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CharacterModel>(ModelDb.Character<Defect>());
			}
		}

		// Token: 0x17001870 RID: 6256
		// (get) Token: 0x060062A7 RID: 25255 RVA: 0x0024BDFF File Offset: 0x00249FFF
		public override Color ButtonColor
		{
			get
			{
				return new Color(0.08f, 0.04f, 0f, 0.75f);
			}
		}

		// Token: 0x17001871 RID: 6257
		// (get) Token: 0x060062A8 RID: 25256 RVA: 0x0024BE1A File Offset: 0x0024A01A
		public override Color DialogueColor
		{
			get
			{
				return new Color("33251E");
			}
		}

		// Token: 0x17001872 RID: 6258
		// (get) Token: 0x060062A9 RID: 25257 RVA: 0x0024BE26 File Offset: 0x0024A026
		public override IEnumerable<EventOption> AllPossibleOptions
		{
			get
			{
				return this.OptionPool1.Concat(this.OptionPool2).Concat(this.OptionPool3);
			}
		}

		// Token: 0x17001873 RID: 6259
		// (get) Token: 0x060062AA RID: 25258 RVA: 0x0024BE44 File Offset: 0x0024A044
		private unsafe List<EventOption> OptionPool1
		{
			get
			{
				int num = 3;
				List<EventOption> list = new List<EventOption>(num);
				CollectionsMarshal.SetCount<EventOption>(list, num);
				Span<EventOption> span = CollectionsMarshal.AsSpan<EventOption>(list);
				int num2 = 0;
				*span[num2] = base.RelicOption<NutritiousSoup>("INITIAL", null);
				num2++;
				*span[num2] = base.RelicOption<VeryHotCocoa>("INITIAL", null);
				num2++;
				*span[num2] = base.RelicOption<YummyCookie>("INITIAL", null);
				return list;
			}
		}

		// Token: 0x060062AB RID: 25259 RVA: 0x0024BEB0 File Offset: 0x0024A0B0
		protected override AncientDialogueSet DefineDialogues()
		{
			AncientDialogueSet ancientDialogueSet = new AncientDialogueSet();
			ancientDialogueSet.FirstVisitEverDialogue = new AncientDialogue(new string[] { "" });
			AncientDialogueSet ancientDialogueSet2 = ancientDialogueSet;
			Dictionary<string, IReadOnlyList<AncientDialogue>> dictionary = new Dictionary<string, IReadOnlyList<AncientDialogue>>();
			string text = AncientEventModel.CharKey<Ironclad>();
			dictionary[text] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "" })
				{
					VisitIndex = new int?(4)
				}
			});
			string text2 = AncientEventModel.CharKey<Silent>();
			dictionary[text2] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "", "", "" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "", "", "" })
				{
					VisitIndex = new int?(4)
				}
			});
			string text3 = AncientEventModel.CharKey<Defect>();
			dictionary[text3] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "", "" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "", "", "" })
				{
					VisitIndex = new int?(4)
				}
			});
			string text4 = AncientEventModel.CharKey<Necrobinder>();
			dictionary[text4] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "", "", "" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "", "", "" })
				{
					VisitIndex = new int?(4)
				}
			});
			string text5 = AncientEventModel.CharKey<Regent>();
			dictionary[text5] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "", "", "" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "", "", "" })
				{
					VisitIndex = new int?(4)
				}
			});
			ancientDialogueSet2.CharacterDialogues = dictionary;
			ancientDialogueSet.AgnosticDialogues = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "" }),
				new AncientDialogue(new string[] { "" })
			});
			return ancientDialogueSet;
		}

		// Token: 0x17001874 RID: 6260
		// (get) Token: 0x060062AC RID: 25260 RVA: 0x0024C21C File Offset: 0x0024A41C
		private unsafe List<EventOption> OptionPool2
		{
			get
			{
				int num = 4;
				List<EventOption> list = new List<EventOption>(num);
				CollectionsMarshal.SetCount<EventOption>(list, num);
				Span<EventOption> span = CollectionsMarshal.AsSpan<EventOption>(list);
				int num2 = 0;
				*span[num2] = base.RelicOption<BiiigHug>("INITIAL", null);
				num2++;
				*span[num2] = base.RelicOption<Storybook>("INITIAL", null);
				num2++;
				*span[num2] = base.RelicOption<SealOfGold>("INITIAL", null);
				num2++;
				*span[num2] = base.RelicOption<ToastyMittens>("INITIAL", null);
				return list;
			}
		}

		// Token: 0x17001875 RID: 6261
		// (get) Token: 0x060062AD RID: 25261 RVA: 0x0024C2A4 File Offset: 0x0024A4A4
		private unsafe List<EventOption> OptionPool3
		{
			get
			{
				int num = 3;
				List<EventOption> list = new List<EventOption>(num);
				CollectionsMarshal.SetCount<EventOption>(list, num);
				Span<EventOption> span = CollectionsMarshal.AsSpan<EventOption>(list);
				int num2 = 0;
				*span[num2] = base.RelicOption<GoldenCompass>("INITIAL", null);
				num2++;
				*span[num2] = base.RelicOption<PumpkinCandle>("INITIAL", null);
				num2++;
				*span[num2] = base.RelicOption<ToyBox>("INITIAL", null);
				return list;
			}
		}

		// Token: 0x060062AE RID: 25262 RVA: 0x0024C310 File Offset: 0x0024A510
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			EventOption eventOption = base.Rng.NextItem<EventOption>(this.OptionPool1);
			EventOption eventOption2 = base.Rng.NextItem<EventOption>(this.OptionPool2);
			EventOption eventOption3 = base.Rng.NextItem<EventOption>(this.OptionPool3);
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[] { eventOption, eventOption2, eventOption3 });
		}
	}
}
