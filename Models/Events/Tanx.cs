using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Entities.Ancients;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Models.Relics;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007F1 RID: 2033
	[NullableContext(1)]
	[Nullable(0)]
	public class Tanx : AncientEventModel
	{
		// Token: 0x17001869 RID: 6249
		// (get) Token: 0x06006297 RID: 25239 RVA: 0x0024B67F File Offset: 0x0024987F
		public override Color ButtonColor
		{
			get
			{
				return new Color(0.05f, 0.02f, 0f, 0.5f);
			}
		}

		// Token: 0x1700186A RID: 6250
		// (get) Token: 0x06006298 RID: 25240 RVA: 0x0024B69A File Offset: 0x0024989A
		public override Color DialogueColor
		{
			get
			{
				return new Color("731717");
			}
		}

		// Token: 0x06006299 RID: 25241 RVA: 0x0024B6A8 File Offset: 0x002498A8
		protected override AncientDialogueSet DefineDialogues()
		{
			AncientDialogueSet ancientDialogueSet = new AncientDialogueSet();
			ancientDialogueSet.FirstVisitEverDialogue = new AncientDialogue(new string[] { "event:/sfx/npcs/tanx/tanx_laugh" });
			AncientDialogueSet ancientDialogueSet2 = ancientDialogueSet;
			Dictionary<string, IReadOnlyList<AncientDialogue>> dictionary = new Dictionary<string, IReadOnlyList<AncientDialogue>>();
			string text = AncientEventModel.CharKey<Ironclad>();
			dictionary[text] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "event:/sfx/npcs/tanx/tanx_roar" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/tanx/tanx_laugh" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/tanx/tanx_roar", "", "event:/sfx/npcs/tanx/tanx_laugh" })
				{
					VisitIndex = new int?(4)
				}
			});
			string text2 = AncientEventModel.CharKey<Silent>();
			dictionary[text2] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "event:/sfx/npcs/tanx/tanx_roar" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/tanx/tanx_curiosity" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/tanx/tanx_roar", "", "event:/sfx/npcs/tanx/tanx_curiosity" })
				{
					VisitIndex = new int?(4)
				}
			});
			string text3 = AncientEventModel.CharKey<Defect>();
			dictionary[text3] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "event:/sfx/npcs/tanx/tanx_curiosity" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/tanx/tanx_laugh" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/tanx/tanx_roar", "", "event:/sfx/npcs/tanx/tanx_curiosity" })
				{
					VisitIndex = new int?(4)
				}
			});
			string text4 = AncientEventModel.CharKey<Necrobinder>();
			dictionary[text4] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "event:/sfx/npcs/tanx/tanx_roar", "", "event:/sfx/npcs/tanx/tanx_curiosity" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/tanx/tanx_roar" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/tanx/tanx_roar", "", "event:/sfx/npcs/tanx/tanx_laugh" })
				{
					VisitIndex = new int?(4)
				}
			});
			string text5 = AncientEventModel.CharKey<Regent>();
			dictionary[text5] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "event:/sfx/npcs/tanx/tanx_curiosity", "", "event:/sfx/npcs/tanx/tanx_laugh" })
				{
					VisitIndex = new int?(0)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/tanx/tanx_roar" })
				{
					VisitIndex = new int?(1)
				},
				new AncientDialogue(new string[] { "event:/sfx/npcs/tanx/tanx_roar", "", "event:/sfx/npcs/tanx/tanx_roar" })
				{
					VisitIndex = new int?(4)
				}
			});
			ancientDialogueSet2.CharacterDialogues = dictionary;
			ancientDialogueSet.AgnosticDialogues = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "event:/sfx/npcs/tanx/tanx_roar" }),
				new AncientDialogue(new string[] { "event:/sfx/npcs/tanx/tanx_laugh" })
			});
			return ancientDialogueSet;
		}

		// Token: 0x1700186B RID: 6251
		// (get) Token: 0x0600629A RID: 25242 RVA: 0x0024BA0B File Offset: 0x00249C0B
		public override IEnumerable<EventOption> AllPossibleOptions
		{
			get
			{
				return this.BaseOptionPool.Append(this.ApexOption);
			}
		}

		// Token: 0x1700186C RID: 6252
		// (get) Token: 0x0600629B RID: 25243 RVA: 0x0024BA20 File Offset: 0x00249C20
		private IEnumerable<EventOption> BaseOptionPool
		{
			get
			{
				return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
				{
					base.RelicOption<Claws>("INITIAL", null),
					base.RelicOption<Crossbow>("INITIAL", null),
					base.RelicOption<IronClub>("INITIAL", null),
					base.RelicOption<MeatCleaver>("INITIAL", null),
					base.RelicOption<Sai>("INITIAL", null),
					base.RelicOption<SpikedGauntlets>("INITIAL", null),
					base.RelicOption<TanxsWhistle>("INITIAL", null),
					base.RelicOption<ThrowingAxe>("INITIAL", null),
					base.RelicOption<WarHammer>("INITIAL", null)
				});
			}
		}

		// Token: 0x1700186D RID: 6253
		// (get) Token: 0x0600629C RID: 25244 RVA: 0x0024BAC0 File Offset: 0x00249CC0
		private EventOption ApexOption
		{
			get
			{
				return base.RelicOption<TriBoomerang>("INITIAL", null);
			}
		}

		// Token: 0x0600629D RID: 25245 RVA: 0x0024BAD0 File Offset: 0x00249CD0
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			List<EventOption> list = this.BaseOptionPool.ToList<EventOption>();
			if (base.Owner.Deck.Cards.Count((CardModel c) => ModelDb.Enchantment<Instinct>().CanEnchant(c)) >= 3)
			{
				list.Add(this.ApexOption);
			}
			return list.UnstableShuffle(base.Rng).Take(3).ToList<EventOption>();
		}

		// Token: 0x040024DC RID: 9436
		private const string _sfxLaugh = "event:/sfx/npcs/tanx/tanx_laugh";

		// Token: 0x040024DD RID: 9437
		private const string _sfxRoar = "event:/sfx/npcs/tanx/tanx_roar";

		// Token: 0x040024DE RID: 9438
		private const string _sfxCuriosity = "event:/sfx/npcs/tanx/tanx_curiosity";

		// Token: 0x040024DF RID: 9439
		private const int _apexCount = 3;
	}
}
