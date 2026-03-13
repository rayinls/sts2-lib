using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A9E RID: 2718
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ToricToughness : CardModel
	{
		// Token: 0x060071D5 RID: 29141 RVA: 0x00269F9D File Offset: 0x0026819D
		public ToricToughness()
			: base(2, CardType.Skill, CardRarity.Event, TargetType.Self, true)
		{
		}

		// Token: 0x17001F35 RID: 7989
		// (get) Token: 0x060071D6 RID: 29142 RVA: 0x00269FAA File Offset: 0x002681AA
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001F36 RID: 7990
		// (get) Token: 0x060071D7 RID: 29143 RVA: 0x00269FAD File Offset: 0x002681AD
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("Turns", 2m),
					new BlockVar(5m, ValueProp.Move)
				});
			}
		}

		// Token: 0x17001F37 RID: 7991
		// (get) Token: 0x060071D8 RID: 29144 RVA: 0x00269FDC File Offset: 0x002681DC
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x060071D9 RID: 29145 RVA: 0x00269FF0 File Offset: 0x002681F0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			decimal num = await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			decimal blockAmount = num;
			ToricToughnessPower toricToughnessPower = await PowerCmd.Apply<ToricToughnessPower>(base.Owner.Creature, base.DynamicVars["Turns"].BaseValue, base.Owner.Creature, this, false);
			if (toricToughnessPower != null)
			{
				toricToughnessPower.SetBlock(blockAmount);
			}
		}

		// Token: 0x060071DA RID: 29146 RVA: 0x0026A03B File Offset: 0x0026823B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(2m);
		}

		// Token: 0x040025E1 RID: 9697
		private const string _turnsKey = "Turns";
	}
}
