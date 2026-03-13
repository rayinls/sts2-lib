using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Godot;
using MegaCrit.Sts2.Core.Entities.Ancients;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.Relics;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007CB RID: 1995
	[NullableContext(1)]
	[Nullable(0)]
	public class Darv : AncientEventModel
	{
		// Token: 0x17001808 RID: 6152
		// (get) Token: 0x06006144 RID: 24900 RVA: 0x002451BB File Offset: 0x002433BB
		public override Color ButtonColor
		{
			get
			{
				return new Color(0.06f, 0f, 0.08f, 0.5f);
			}
		}

		// Token: 0x17001809 RID: 6153
		// (get) Token: 0x06006145 RID: 24901 RVA: 0x002451D6 File Offset: 0x002433D6
		public override Color DialogueColor
		{
			get
			{
				return new Color("512E66");
			}
		}

		// Token: 0x1700180A RID: 6154
		// (get) Token: 0x06006146 RID: 24902 RVA: 0x002451E4 File Offset: 0x002433E4
		public override IEnumerable<EventOption> AllPossibleOptions
		{
			get
			{
				return (from r in Darv._validRelicSets.SelectMany((Darv.ValidRelicSet s) => s.relics)
					select base.RelicOption(r.ToMutable(), "INITIAL", null)).Concat(new <>z__ReadOnlySingleElementList<EventOption>(base.RelicOption<DustyTome>("INITIAL", null)));
			}
		}

		// Token: 0x06006147 RID: 24903 RVA: 0x00245244 File Offset: 0x00243444
		protected override AncientDialogueSet DefineDialogues()
		{
			AncientDialogueSet ancientDialogueSet = new AncientDialogueSet();
			ancientDialogueSet.FirstVisitEverDialogue = new AncientDialogue(new string[] { "event:/sfx/npcs/darv/darv_introduction" });
			AncientDialogueSet ancientDialogueSet2 = ancientDialogueSet;
			Dictionary<string, IReadOnlyList<AncientDialogue>> dictionary = new Dictionary<string, IReadOnlyList<AncientDialogue>>();
			string text = AncientEventModel.CharKey<Ironclad>();
			dictionary[text] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "event:/sfx/npcs/darv/darv_introduction" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/darv/darv_endeared" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "", "", "" })
				{
					VisitIndex = new int?(4)
				}
			});
			string text2 = AncientEventModel.CharKey<Silent>();
			dictionary[text2] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "event:/sfx/npcs/darv/darv_introduction" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/darv/darv_excited" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/darv/darv_pain", "", "event:/sfx/npcs/darv/darv_outta_the_way" })
				{
					VisitIndex = new int?(4)
				}
			});
			string text3 = AncientEventModel.CharKey<Defect>();
			dictionary[text3] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "event:/sfx/npcs/darv/darv_introduction", "" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/darv/darv_endeared" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/darv/darv_fear", "", "event:/sfx/npcs/darv/darv_fear" })
				{
					VisitIndex = new int?(4)
				}
			});
			string text4 = AncientEventModel.CharKey<Necrobinder>();
			dictionary[text4] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "event:/sfx/npcs/darv/darv_introduction", "", "event:/sfx/npcs/darv/darv_excited" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/darv/darv_endeared" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/darv/darv_excited", "", "event:/sfx/npcs/darv/darv_fear" })
				{
					VisitIndex = new int?(4)
				}
			});
			string text5 = AncientEventModel.CharKey<Regent>();
			dictionary[text5] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "event:/sfx/npcs/darv/darv_introduction", "", "event:/sfx/npcs/darv/darv_excited" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/darv/darv_introduction" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/darv/darv_excited", "", "event:/sfx/npcs/darv/darv_pain" })
				{
					VisitIndex = new int?(4)
				}
			});
			ancientDialogueSet2.CharacterDialogues = dictionary;
			ancientDialogueSet.AgnosticDialogues = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "event:/sfx/npcs/darv/darv_excited" }),
				new AncientDialogue(new string[] { "event:/sfx/npcs/darv/darv_outta_the_way" })
			});
			return ancientDialogueSet;
		}

		// Token: 0x06006148 RID: 24904 RVA: 0x002455B0 File Offset: 0x002437B0
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			List<EventOption> list = (from rs in Darv._validRelicSets
				where rs.filter(base.Owner)
				select base.RelicOption(base.Rng.NextItem<RelicModel>(rs.relics).ToMutable(), "INITIAL", null)).ToList<EventOption>().UnstableShuffle(base.Rng);
			List<EventOption> list2;
			if (base.Rng.NextBool())
			{
				list2 = list.Take(2).ToList<EventOption>();
				DustyTome dustyTome = (DustyTome)ModelDb.Relic<DustyTome>().ToMutable();
				if (base.Owner != null)
				{
					dustyTome.SetupForPlayer(base.Owner);
				}
				list2.Add(base.RelicOption(dustyTome, "INITIAL", null));
			}
			else
			{
				list2 = list.Take(3).ToList<EventOption>();
			}
			return list2;
		}

		// Token: 0x0600614A RID: 24906 RVA: 0x00245660 File Offset: 0x00243860
		// Note: this type is marked as 'beforefieldinit'.
		unsafe static Darv()
		{
			int num = 9;
			List<Darv.ValidRelicSet> list = new List<Darv.ValidRelicSet>(num);
			CollectionsMarshal.SetCount<Darv.ValidRelicSet>(list, num);
			Span<Darv.ValidRelicSet> span = CollectionsMarshal.AsSpan<Darv.ValidRelicSet>(list);
			int num2 = 0;
			*span[num2] = new Darv.ValidRelicSet(new RelicModel[] { ModelDb.Relic<Astrolabe>() });
			num2++;
			*span[num2] = new Darv.ValidRelicSet(new RelicModel[] { ModelDb.Relic<BlackStar>() });
			num2++;
			*span[num2] = new Darv.ValidRelicSet(new RelicModel[] { ModelDb.Relic<CallingBell>() });
			num2++;
			*span[num2] = new Darv.ValidRelicSet(new RelicModel[] { ModelDb.Relic<EmptyCage>() });
			num2++;
			*span[num2] = new Darv.ValidRelicSet((Player owner) => !owner.RunState.Modifiers.Any((ModifierModel m) => m.ClearsPlayerDeck), new RelicModel[] { ModelDb.Relic<PandorasBox>() });
			num2++;
			*span[num2] = new Darv.ValidRelicSet(new RelicModel[] { ModelDb.Relic<RunicPyramid>() });
			num2++;
			*span[num2] = new Darv.ValidRelicSet(new RelicModel[] { ModelDb.Relic<SneckoEye>() });
			num2++;
			*span[num2] = new Darv.ValidRelicSet((Player owner) => owner.RunState.CurrentActIndex == 1, new RelicModel[]
			{
				ModelDb.Relic<Ectoplasm>(),
				ModelDb.Relic<Sozu>()
			});
			num2++;
			*span[num2] = new Darv.ValidRelicSet((Player owner) => owner.RunState.CurrentActIndex == 2, new RelicModel[]
			{
				ModelDb.Relic<PhilosophersStone>(),
				ModelDb.Relic<VelvetChoker>()
			});
			Darv._validRelicSets = list;
		}

		// Token: 0x04002481 RID: 9345
		private const string _sfxExcited = "event:/sfx/npcs/darv/darv_excited";

		// Token: 0x04002482 RID: 9346
		private const string _sfxOuttaTheWay = "event:/sfx/npcs/darv/darv_outta_the_way";

		// Token: 0x04002483 RID: 9347
		private const string _sfxFear = "event:/sfx/npcs/darv/darv_fear";

		// Token: 0x04002484 RID: 9348
		private const string _sfxPain = "event:/sfx/npcs/darv/darv_pain";

		// Token: 0x04002485 RID: 9349
		private const string _sfxEndeared = "event:/sfx/npcs/darv/darv_endeared";

		// Token: 0x04002486 RID: 9350
		private const string _sfxIntroduction = "event:/sfx/npcs/darv/darv_introduction";

		// Token: 0x04002487 RID: 9351
		private static readonly List<Darv.ValidRelicSet> _validRelicSets;

		// Token: 0x02001D1B RID: 7451
		[Nullable(0)]
		private struct ValidRelicSet
		{
			// Token: 0x0600AA54 RID: 43604 RVA: 0x00378212 File Offset: 0x00376412
			public ValidRelicSet(Func<Player, bool> filter, RelicModel[] relics)
			{
				this.filter = filter;
				this.relics = relics;
			}

			// Token: 0x0600AA55 RID: 43605 RVA: 0x00378222 File Offset: 0x00376422
			public ValidRelicSet(RelicModel[] relics)
			{
				this.filter = (Player _) => true;
				this.relics = relics;
			}

			// Token: 0x040074D8 RID: 29912
			public readonly Func<Player, bool> filter;

			// Token: 0x040074D9 RID: 29913
			public readonly RelicModel[] relics;
		}
	}
}
