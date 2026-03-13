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
	// Token: 0x02000A92 RID: 2706
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TheGambit : CardModel
	{
		// Token: 0x0600718D RID: 29069 RVA: 0x00269771 File Offset: 0x00267971
		public TheGambit()
			: base(0, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001F1A RID: 7962
		// (get) Token: 0x0600718E RID: 29070 RVA: 0x0026977E File Offset: 0x0026797E
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001F1B RID: 7963
		// (get) Token: 0x0600718F RID: 29071 RVA: 0x00269781 File Offset: 0x00267981
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(50m, ValueProp.Move));
			}
		}

		// Token: 0x06007190 RID: 29072 RVA: 0x00269798 File Offset: 0x00267998
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await PowerCmd.Apply<TheGambitPower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x06007191 RID: 29073 RVA: 0x002697E3 File Offset: 0x002679E3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(25m);
		}
	}
}
