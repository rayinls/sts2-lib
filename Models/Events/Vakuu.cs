using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Entities.Ancients;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.Relics;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007FD RID: 2045
	[NullableContext(1)]
	[Nullable(0)]
	public class Vakuu : AncientEventModel
	{
		// Token: 0x17001893 RID: 6291
		// (get) Token: 0x06006325 RID: 25381 RVA: 0x0024E5AF File Offset: 0x0024C7AF
		public override Color ButtonColor
		{
			get
			{
				return new Color(0.05f, 0.06f, 0.12f, 0.8f);
			}
		}

		// Token: 0x17001894 RID: 6292
		// (get) Token: 0x06006326 RID: 25382 RVA: 0x0024E5CA File Offset: 0x0024C7CA
		public override Color DialogueColor
		{
			get
			{
				return new Color("3C1931");
			}
		}

		// Token: 0x06006327 RID: 25383 RVA: 0x0024E5D8 File Offset: 0x0024C7D8
		protected override AncientDialogueSet DefineDialogues()
		{
			AncientDialogueSet ancientDialogueSet = new AncientDialogueSet();
			ancientDialogueSet.FirstVisitEverDialogue = new AncientDialogue(new string[] { "" });
			AncientDialogueSet ancientDialogueSet2 = ancientDialogueSet;
			Dictionary<string, IReadOnlyList<AncientDialogue>> dictionary = new Dictionary<string, IReadOnlyList<AncientDialogue>>();
			string text = AncientEventModel.CharKey<Ironclad>();
			dictionary[text] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
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
				new AncientDialogue(new string[] { "" }),
				new AncientDialogue(new string[] { "" })
			});
			return ancientDialogueSet;
		}

		// Token: 0x17001895 RID: 6293
		// (get) Token: 0x06006328 RID: 25384 RVA: 0x0024E969 File Offset: 0x0024CB69
		public override IEnumerable<EventOption> AllPossibleOptions
		{
			get
			{
				return this.Pool1.Concat(this.Pool2).Concat(this.Pool3);
			}
		}

		// Token: 0x17001896 RID: 6294
		// (get) Token: 0x06006329 RID: 25385 RVA: 0x0024E987 File Offset: 0x0024CB87
		private IEnumerable<EventOption> Pool1
		{
			get
			{
				return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
				{
					base.RelicOption<BloodSoakedRose>("INITIAL", null),
					base.RelicOption<WhisperingEarring>("INITIAL", null),
					base.RelicOption<Fiddle>("INITIAL", null)
				});
			}
		}

		// Token: 0x17001897 RID: 6295
		// (get) Token: 0x0600632A RID: 25386 RVA: 0x0024E9C4 File Offset: 0x0024CBC4
		private IEnumerable<EventOption> Pool2
		{
			get
			{
				return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
				{
					base.RelicOption<PreservedFog>("INITIAL", null),
					base.RelicOption<SereTalon>("INITIAL", null),
					base.RelicOption<DistinguishedCape>("INITIAL", null).ThatDoesDamage(9m)
				});
			}
		}

		// Token: 0x17001898 RID: 6296
		// (get) Token: 0x0600632B RID: 25387 RVA: 0x0024EA18 File Offset: 0x0024CC18
		private IEnumerable<EventOption> Pool3
		{
			get
			{
				return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
				{
					base.RelicOption<ChoicesParadox>("INITIAL", null),
					base.RelicOption<MusicBox>("INITIAL", null),
					base.RelicOption<LordsParasol>("INITIAL", null),
					base.RelicOption<JeweledMask>("INITIAL", null)
				});
			}
		}

		// Token: 0x0600632C RID: 25388 RVA: 0x0024EA6C File Offset: 0x0024CC6C
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			List<EventOption> list = this.Pool1.ToList<EventOption>();
			List<EventOption> list2 = this.Pool2.ToList<EventOption>();
			List<EventOption> list3 = this.Pool3.ToList<EventOption>();
			list.UnstableShuffle(base.Rng);
			list2.UnstableShuffle(base.Rng);
			list3.UnstableShuffle(base.Rng);
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				list[0],
				list2[0],
				list3[0]
			});
		}
	}
}
