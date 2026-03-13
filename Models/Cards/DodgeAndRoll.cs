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
	// Token: 0x02000924 RID: 2340
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DodgeAndRoll : CardModel
	{
		// Token: 0x060069D9 RID: 27097 RVA: 0x0025A021 File Offset: 0x00258221
		public DodgeAndRoll()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001BE7 RID: 7143
		// (get) Token: 0x060069DA RID: 27098 RVA: 0x0025A02E File Offset: 0x0025822E
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001BE8 RID: 7144
		// (get) Token: 0x060069DB RID: 27099 RVA: 0x0025A031 File Offset: 0x00258231
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(4m, ValueProp.Move));
			}
		}

		// Token: 0x060069DC RID: 27100 RVA: 0x0025A044 File Offset: 0x00258244
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			decimal num = await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			decimal num2 = num;
			await PowerCmd.Apply<BlockNextTurnPower>(base.Owner.Creature, num2, base.Owner.Creature, this, false);
		}

		// Token: 0x060069DD RID: 27101 RVA: 0x0025A08F File Offset: 0x0025828F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(2m);
		}
	}
}
