using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200091B RID: 2331
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DemonicShield : CardModel
	{
		// Token: 0x060069AA RID: 27050 RVA: 0x00259B0F File Offset: 0x00257D0F
		public DemonicShield()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.AnyAlly, true)
		{
		}

		// Token: 0x17001BD1 RID: 7121
		// (get) Token: 0x060069AB RID: 27051 RVA: 0x00259B1C File Offset: 0x00257D1C
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.MultiplayerOnly;
			}
		}

		// Token: 0x17001BD2 RID: 7122
		// (get) Token: 0x060069AC RID: 27052 RVA: 0x00259B1F File Offset: 0x00257D1F
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001BD3 RID: 7123
		// (get) Token: 0x060069AD RID: 27053 RVA: 0x00259B22 File Offset: 0x00257D22
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001BD4 RID: 7124
		// (get) Token: 0x060069AE RID: 27054 RVA: 0x00259B2C File Offset: 0x00257D2C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[4];
				array[0] = new CalculationBaseVar(0m);
				array[1] = new HpLossVar(1m);
				array[2] = new CalculationExtraVar(1m);
				array[3] = new CalculatedBlockVar(ValueProp.Move).WithMultiplier((CardModel card, [Nullable(2)] Creature _) => card.Owner.Creature.Block);
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x060069AF RID: 27055 RVA: 0x00259B98 File Offset: 0x00257D98
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			VfxCmd.PlayOnCreatureCenter(base.Owner.Creature, "vfx/vfx_bloody_impact");
			await CreatureCmd.Damage(choiceContext, base.Owner.Creature, base.DynamicVars.HpLoss.BaseValue, ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move, this);
			await CreatureCmd.GainBlock(cardPlay.Target, base.DynamicVars.CalculatedBlock.Calculate(cardPlay.Target), base.DynamicVars.CalculatedBlock.Props, cardPlay, false);
		}

		// Token: 0x060069B0 RID: 27056 RVA: 0x00259BEB File Offset: 0x00257DEB
		protected override void OnUpgrade()
		{
			base.RemoveKeyword(CardKeyword.Exhaust);
		}
	}
}
