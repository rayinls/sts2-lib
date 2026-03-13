using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008A9 RID: 2217
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Beckon : CardModel
	{
		// Token: 0x0600675F RID: 26463 RVA: 0x0025547B File Offset: 0x0025367B
		public Beckon()
			: base(1, CardType.Status, CardRarity.Status, TargetType.None, true)
		{
		}

		// Token: 0x17001AD7 RID: 6871
		// (get) Token: 0x06006760 RID: 26464 RVA: 0x00255488 File Offset: 0x00253688
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001AD8 RID: 6872
		// (get) Token: 0x06006761 RID: 26465 RVA: 0x0025548B File Offset: 0x0025368B
		public override bool HasTurnEndInHandEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001AD9 RID: 6873
		// (get) Token: 0x06006762 RID: 26466 RVA: 0x0025548E File Offset: 0x0025368E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new HpLossVar(6m));
			}
		}

		// Token: 0x06006763 RID: 26467 RVA: 0x002554A0 File Offset: 0x002536A0
		public override async Task OnTurnEndInHand(PlayerChoiceContext choiceContext)
		{
			await CreatureCmd.Damage(choiceContext, base.Owner.Creature, base.DynamicVars.HpLoss.BaseValue, ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move, this);
		}
	}
}
