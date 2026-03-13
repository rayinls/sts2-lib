using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Entities.Ancients;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.Relics;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007DE RID: 2014
	[NullableContext(1)]
	[Nullable(0)]
	public class Orobas : AncientEventModel
	{
		// Token: 0x17001839 RID: 6201
		// (get) Token: 0x060061E7 RID: 25063 RVA: 0x002484BF File Offset: 0x002466BF
		public override Color ButtonColor
		{
			get
			{
				return new Color(0.05f, 0f, 0.1f, 0.35f);
			}
		}

		// Token: 0x1700183A RID: 6202
		// (get) Token: 0x060061E8 RID: 25064 RVA: 0x002484DA File Offset: 0x002466DA
		public override Color DialogueColor
		{
			get
			{
				return new Color("5C5F7A");
			}
		}

		// Token: 0x060061E9 RID: 25065 RVA: 0x002484E8 File Offset: 0x002466E8
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
				new AncientDialogue(new string[] { "", "" })
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

		// Token: 0x1700183B RID: 6203
		// (get) Token: 0x060061EA RID: 25066 RVA: 0x00248834 File Offset: 0x00246A34
		public override IEnumerable<EventOption> AllPossibleOptions
		{
			get
			{
				return new IEnumerable<EventOption>[]
				{
					this.OptionPool1,
					this.OptionPool2,
					this.OptionPool3,
					this.DiscoveryTotems,
					new <>z__ReadOnlySingleElementList<EventOption>(this.PrismaticGemOption)
				}.SelectMany((IEnumerable<EventOption> x) => x);
			}
		}

		// Token: 0x1700183C RID: 6204
		// (get) Token: 0x060061EB RID: 25067 RVA: 0x0024889D File Offset: 0x00246A9D
		private IEnumerable<EventOption> OptionPool1
		{
			get
			{
				return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
				{
					base.RelicOption<ElectricShrymp>("INITIAL", null),
					base.RelicOption<GlassEye>("INITIAL", null),
					base.RelicOption<SandCastle>("INITIAL", null)
				});
			}
		}

		// Token: 0x1700183D RID: 6205
		// (get) Token: 0x060061EC RID: 25068 RVA: 0x002488D7 File Offset: 0x00246AD7
		private IEnumerable<EventOption> OptionPool2
		{
			get
			{
				return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
				{
					base.RelicOption<AlchemicalCoffer>("INITIAL", null),
					base.RelicOption<Driftwood>("INITIAL", null),
					base.RelicOption<RadiantPearl>("INITIAL", null)
				});
			}
		}

		// Token: 0x1700183E RID: 6206
		// (get) Token: 0x060061ED RID: 25069 RVA: 0x00248914 File Offset: 0x00246B14
		private IEnumerable<EventOption> DiscoveryTotems
		{
			get
			{
				List<EventOption> list = new List<EventOption>();
				foreach (CharacterModel characterModel in ModelDb.AllCharacters)
				{
					SeaGlass seaGlass = (SeaGlass)ModelDb.Relic<SeaGlass>().ToMutable();
					seaGlass.CharacterId = characterModel.Id;
					list.Add(base.RelicOption(seaGlass, "INITIAL", null));
				}
				return list;
			}
		}

		// Token: 0x1700183F RID: 6207
		// (get) Token: 0x060061EE RID: 25070 RVA: 0x00248990 File Offset: 0x00246B90
		private EventOption PrismaticGemOption
		{
			get
			{
				return base.RelicOption<PrismaticGem>("INITIAL", null);
			}
		}

		// Token: 0x17001840 RID: 6208
		// (get) Token: 0x060061EF RID: 25071 RVA: 0x002489A0 File Offset: 0x00246BA0
		private IEnumerable<EventOption> OptionPool3
		{
			get
			{
				List<EventOption> list = new List<EventOption>();
				TouchOfOrobas touchOfOrobas = (TouchOfOrobas)ModelDb.Relic<TouchOfOrobas>().ToMutable();
				if (base.Owner != null)
				{
					if (touchOfOrobas.SetupForPlayer(base.Owner))
					{
						list.Add(base.RelicOption(touchOfOrobas, "INITIAL", null));
					}
				}
				else
				{
					list.Add(base.RelicOption(touchOfOrobas, "INITIAL", null));
				}
				ArchaicTooth archaicTooth = (ArchaicTooth)ModelDb.Relic<ArchaicTooth>().ToMutable();
				if (base.Owner != null)
				{
					if (archaicTooth.SetupForPlayer(base.Owner))
					{
						list.Add(base.RelicOption(archaicTooth, "INITIAL", null));
					}
				}
				else
				{
					list.Add(base.RelicOption(archaicTooth, "INITIAL", null));
				}
				if (list.Count == 0)
				{
					list.Add(new EventOption(this, null, "OROBAS.pages.INITIAL.options.OPTION_POOL_3_LOCKED", Array.Empty<IHoverTip>()));
				}
				return list;
			}
		}

		// Token: 0x060061F0 RID: 25072 RVA: 0x00248A70 File Offset: 0x00246C70
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			CharacterModel character = base.Owner.Character;
			CharacterModel characterModel = base.Rng.NextItem<CharacterModel>(base.Owner.UnlockState.Characters.Where((CharacterModel c) => c.Id != character.Id));
			if (characterModel == null)
			{
				characterModel = character;
			}
			List<EventOption> list = this.OptionPool1.ToList<EventOption>();
			EventOption eventOption;
			if (base.Rng.NextFloat(1f) < 0.3333333f)
			{
				eventOption = this.PrismaticGemOption;
			}
			else
			{
				SeaGlass seaGlass = (SeaGlass)ModelDb.Relic<SeaGlass>().ToMutable();
				seaGlass.CharacterId = characterModel.Id;
				eventOption = base.RelicOption(seaGlass, "INITIAL", null);
			}
			list.Add(eventOption);
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				base.Rng.NextItem<EventOption>(list),
				base.Rng.NextItem<EventOption>(this.OptionPool2),
				base.Rng.NextItem<EventOption>(this.OptionPool3)
			});
		}

		// Token: 0x040024B0 RID: 9392
		private const float _prismaticOdds = 0.3333333f;
	}
}
