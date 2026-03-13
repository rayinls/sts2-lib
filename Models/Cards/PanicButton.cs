using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009F1 RID: 2545
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PanicButton : CardModel
	{
		// Token: 0x06006E32 RID: 28210 RVA: 0x00262B70 File Offset: 0x00260D70
		public PanicButton()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001DBD RID: 7613
		// (get) Token: 0x06006E33 RID: 28211 RVA: 0x00262B7D File Offset: 0x00260D7D
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001DBE RID: 7614
		// (get) Token: 0x06006E34 RID: 28212 RVA: 0x00262B80 File Offset: 0x00260D80
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001DBF RID: 7615
		// (get) Token: 0x06006E35 RID: 28213 RVA: 0x00262B88 File Offset: 0x00260D88
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(30m, ValueProp.Move),
					new DynamicVar("Turns", 2m)
				});
			}
		}

		// Token: 0x06006E36 RID: 28214 RVA: 0x00262BB8 File Offset: 0x00260DB8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await PowerCmd.Apply<NoBlockPower>(base.Owner.Creature, base.DynamicVars["Turns"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006E37 RID: 28215 RVA: 0x00262C03 File Offset: 0x00260E03
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(10m);
		}

		// Token: 0x040025BC RID: 9660
		private const string _turnsKey = "Turns";
	}
}
