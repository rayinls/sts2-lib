using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Godot;
using MegaCrit.Sts2.Core.Entities.Ancients;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Models.Relics;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007DF RID: 2015
	[NullableContext(1)]
	[Nullable(0)]
	public class Pael : AncientEventModel
	{
		// Token: 0x17001841 RID: 6209
		// (get) Token: 0x060061F2 RID: 25074 RVA: 0x00248B78 File Offset: 0x00246D78
		public override IEnumerable<EventOption> AllPossibleOptions
		{
			get
			{
				return this.OptionPool1.Concat(this.OptionPool2).Concat(this.OptionPool3).Concat(new <>z__ReadOnlyArray<EventOption>(new EventOption[] { this.PaelsClawOption, this.PaelsToothOption, this.PaelsLegionOption, this.PaelsGrowthOption }));
			}
		}

		// Token: 0x060061F3 RID: 25075 RVA: 0x00248BD8 File Offset: 0x00246DD8
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
			string text3 = AncientEventModel.CharKey<Defect>();
			dictionary[text3] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
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
			ancientDialogueSet2.CharacterDialogues = dictionary;
			ancientDialogueSet.AgnosticDialogues = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "" }),
				new AncientDialogue(new string[] { "" })
			});
			return ancientDialogueSet;
		}

		// Token: 0x17001842 RID: 6210
		// (get) Token: 0x060061F4 RID: 25076 RVA: 0x00248F03 File Offset: 0x00247103
		public override Color ButtonColor
		{
			get
			{
				return new Color(0.03f, 0.08f, 0f, 0.75f);
			}
		}

		// Token: 0x17001843 RID: 6211
		// (get) Token: 0x060061F5 RID: 25077 RVA: 0x00248F1E File Offset: 0x0024711E
		public override Color DialogueColor
		{
			get
			{
				return new Color("332C29");
			}
		}

		// Token: 0x17001844 RID: 6212
		// (get) Token: 0x060061F6 RID: 25078 RVA: 0x00248F2A File Offset: 0x0024712A
		private EventOption PaelsClawOption
		{
			get
			{
				return base.RelicOption<PaelsClaw>("INITIAL", null);
			}
		}

		// Token: 0x17001845 RID: 6213
		// (get) Token: 0x060061F7 RID: 25079 RVA: 0x00248F38 File Offset: 0x00247138
		private EventOption PaelsToothOption
		{
			get
			{
				return base.RelicOption<PaelsTooth>("INITIAL", null);
			}
		}

		// Token: 0x17001846 RID: 6214
		// (get) Token: 0x060061F8 RID: 25080 RVA: 0x00248F46 File Offset: 0x00247146
		private EventOption PaelsGrowthOption
		{
			get
			{
				return base.RelicOption<PaelsGrowth>("INITIAL", null);
			}
		}

		// Token: 0x17001847 RID: 6215
		// (get) Token: 0x060061F9 RID: 25081 RVA: 0x00248F54 File Offset: 0x00247154
		private EventOption PaelsLegionOption
		{
			get
			{
				return base.RelicOption<PaelsLegion>("INITIAL", null);
			}
		}

		// Token: 0x17001848 RID: 6216
		// (get) Token: 0x060061FA RID: 25082 RVA: 0x00248F62 File Offset: 0x00247162
		private IEnumerable<EventOption> OptionPool1
		{
			get
			{
				return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
				{
					base.RelicOption<PaelsFlesh>("INITIAL", null),
					base.RelicOption<PaelsHorn>("INITIAL", null),
					base.RelicOption<PaelsTears>("INITIAL", null)
				});
			}
		}

		// Token: 0x17001849 RID: 6217
		// (get) Token: 0x060061FB RID: 25083 RVA: 0x00248F9C File Offset: 0x0024719C
		private unsafe List<EventOption> OptionPool2
		{
			get
			{
				int num = 1;
				List<EventOption> list = new List<EventOption>(num);
				CollectionsMarshal.SetCount<EventOption>(list, num);
				Span<EventOption> span = CollectionsMarshal.AsSpan<EventOption>(list);
				int num2 = 0;
				*span[num2] = base.RelicOption<PaelsWing>("INITIAL", null);
				return list;
			}
		}

		// Token: 0x1700184A RID: 6218
		// (get) Token: 0x060061FC RID: 25084 RVA: 0x00248FD8 File Offset: 0x002471D8
		private unsafe List<EventOption> OptionPool3
		{
			get
			{
				int num = 2;
				List<EventOption> list = new List<EventOption>(num);
				CollectionsMarshal.SetCount<EventOption>(list, num);
				Span<EventOption> span = CollectionsMarshal.AsSpan<EventOption>(list);
				int num2 = 0;
				*span[num2] = base.RelicOption<PaelsEye>("INITIAL", null);
				num2++;
				*span[num2] = base.RelicOption<PaelsBlood>("INITIAL", null);
				return list;
			}
		}

		// Token: 0x060061FD RID: 25085 RVA: 0x0024902C File Offset: 0x0024722C
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			EventOption eventOption = base.Rng.NextItem<EventOption>(this.OptionPool1);
			List<EventOption> list = this.OptionPool2.ToList<EventOption>();
			IReadOnlyList<CardModel> cards = base.Owner.Deck.Cards;
			if (cards.Count((CardModel c) => ModelDb.Enchantment<Goopy>().CanEnchant(c)) >= 3)
			{
				list.Add(this.PaelsClawOption);
			}
			if (cards.Count((CardModel c) => c.IsRemovable) >= 5)
			{
				list.Add(this.PaelsToothOption);
			}
			list.AddRange(list);
			list.Add(this.PaelsGrowthOption);
			EventOption eventOption2 = base.Rng.NextItem<EventOption>(list);
			List<EventOption> list2 = this.OptionPool3.ToList<EventOption>();
			if (!base.Owner.HasEventPet())
			{
				list2.Add(this.PaelsLegionOption);
			}
			EventOption eventOption3 = base.Rng.NextItem<EventOption>(list2);
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[] { eventOption, eventOption2, eventOption3 });
		}
	}
}
