using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Entities.Ancients;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Models.Relics;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007DD RID: 2013
	[NullableContext(1)]
	[Nullable(0)]
	public class Nonupeipe : AncientEventModel
	{
		// Token: 0x17001832 RID: 6194
		// (get) Token: 0x060061DD RID: 25053 RVA: 0x0024800F File Offset: 0x0024620F
		public override Color ButtonColor
		{
			get
			{
				return new Color(0f, 0.1f, 0.16f, 0.75f);
			}
		}

		// Token: 0x17001833 RID: 6195
		// (get) Token: 0x060061DE RID: 25054 RVA: 0x0024802A File Offset: 0x0024622A
		public override Color DialogueColor
		{
			get
			{
				return new Color("0A494D");
			}
		}

		// Token: 0x060061DF RID: 25055 RVA: 0x00248038 File Offset: 0x00246238
		protected override AncientDialogueSet DefineDialogues()
		{
			AncientDialogueSet ancientDialogueSet = new AncientDialogueSet();
			ancientDialogueSet.FirstVisitEverDialogue = new AncientDialogue(new string[] { "event:/sfx/npcs/nonupeipe/nonupeipe_welcome" });
			AncientDialogueSet ancientDialogueSet2 = ancientDialogueSet;
			Dictionary<string, IReadOnlyList<AncientDialogue>> dictionary = new Dictionary<string, IReadOnlyList<AncientDialogue>>();
			string text = AncientEventModel.CharKey<Ironclad>();
			dictionary[text] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "event:/sfx/npcs/nonupeipe/nonupeipe_welcome" })
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
				new AncientDialogue(new string[] { "event:/sfx/npcs/nonupeipe/nonupeipe_grossed_out", "", "event:/sfx/npcs/nonupeipe/nonupeipe_welcome" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/nonupeipe/nonupeipe_giggle" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/nonupeipe/nonupeipe_eeked", "", "event:/sfx/npcs/nonupeipe/nonupeipe_eeked" })
				{
					VisitIndex = new int?(4)
				}
			});
			string text3 = AncientEventModel.CharKey<Defect>();
			dictionary[text3] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "event:/sfx/npcs/nonupeipe/nonupeipe_welcome" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/nonupeipe/nonupeipe_welcome" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/nonupeipe/nonupeipe_giggle", "" })
				{
					VisitIndex = new int?(4)
				}
			});
			string text4 = AncientEventModel.CharKey<Necrobinder>();
			dictionary[text4] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "", "event:/sfx/npcs/nonupeipe/nonupeipe_eeked" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/nonupeipe/nonupeipe_welcome" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/nonupeipe/nonupeipe_grossed_out", "" })
				{
					VisitIndex = new int?(4)
				}
			});
			string text5 = AncientEventModel.CharKey<Regent>();
			dictionary[text5] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "", "event:/sfx/npcs/nonupeipe/nonupeipe_eeked" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/nonupeipe/nonupeipe_welcome" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/nonupeipe/nonupeipe_grossed_out", "" })
				{
					VisitIndex = new int?(4)
				}
			});
			ancientDialogueSet2.CharacterDialogues = dictionary;
			ancientDialogueSet.AgnosticDialogues = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "event:/sfx/npcs/nonupeipe/nonupeipe_welcome" }),
				new AncientDialogue(new string[] { "event:/sfx/npcs/nonupeipe/nonupeipe_eeked" })
			});
			return ancientDialogueSet;
		}

		// Token: 0x17001834 RID: 6196
		// (get) Token: 0x060061E0 RID: 25056 RVA: 0x00248373 File Offset: 0x00246573
		public override IEnumerable<EventOption> AllPossibleOptions
		{
			get
			{
				return this.OptionPool.Concat(new <>z__ReadOnlySingleElementList<EventOption>(this.BeautifulBraceletEventOption));
			}
		}

		// Token: 0x17001835 RID: 6197
		// (get) Token: 0x060061E1 RID: 25057 RVA: 0x0024838B File Offset: 0x0024658B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return Array.Empty<DynamicVar>();
			}
		}

		// Token: 0x17001836 RID: 6198
		// (get) Token: 0x060061E2 RID: 25058 RVA: 0x00248394 File Offset: 0x00246594
		private IEnumerable<EventOption> OptionPool
		{
			get
			{
				return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
				{
					base.RelicOption<BlessedAntler>("INITIAL", null),
					base.RelicOption<BrilliantScarf>("INITIAL", null),
					base.RelicOption<DelicateFrond>("INITIAL", null),
					base.RelicOption<DiamondDiadem>("INITIAL", null),
					base.RelicOption<FurCoat>("INITIAL", null),
					base.RelicOption<Glitter>("INITIAL", null),
					base.RelicOption<JewelryBox>("INITIAL", null),
					base.RelicOption<LoomingFruit>("INITIAL", null),
					base.RelicOption<SignetRing>("INITIAL", null)
				});
			}
		}

		// Token: 0x17001837 RID: 6199
		// (get) Token: 0x060061E3 RID: 25059 RVA: 0x00248434 File Offset: 0x00246634
		private EventOption BeautifulBraceletEventOption
		{
			get
			{
				return base.RelicOption<BeautifulBracelet>("INITIAL", null);
			}
		}

		// Token: 0x060061E4 RID: 25060 RVA: 0x00248444 File Offset: 0x00246644
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			List<EventOption> list = this.OptionPool.ToList<EventOption>();
			if (base.Owner.Deck.Cards.Count(new Func<CardModel, bool>(ModelDb.Enchantment<Swift>().CanEnchant)) >= 4)
			{
				list.Add(this.BeautifulBraceletEventOption);
			}
			list.UnstableShuffle(base.Rng);
			return list.Take(3).ToList<EventOption>();
		}

		// Token: 0x17001838 RID: 6200
		// (get) Token: 0x060061E5 RID: 25061 RVA: 0x002484AB File Offset: 0x002466AB
		protected override Color EventButtonColor
		{
			get
			{
				return new Color("000000BF");
			}
		}

		// Token: 0x040024AC RID: 9388
		private const string _sfxEeked = "event:/sfx/npcs/nonupeipe/nonupeipe_eeked";

		// Token: 0x040024AD RID: 9389
		private const string _sfxWelcome = "event:/sfx/npcs/nonupeipe/nonupeipe_welcome";

		// Token: 0x040024AE RID: 9390
		private const string _sfxGrossedOut = "event:/sfx/npcs/nonupeipe/nonupeipe_grossed_out";

		// Token: 0x040024AF RID: 9391
		private const string _sfxGiggle = "event:/sfx/npcs/nonupeipe/nonupeipe_giggle";
	}
}
