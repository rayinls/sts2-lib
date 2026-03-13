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
	// Token: 0x020009CA RID: 2506
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Mimic : CardModel
	{
		// Token: 0x06006D65 RID: 28005 RVA: 0x0026126F File Offset: 0x0025F46F
		public Mimic()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.AnyAlly, true)
		{
		}

		// Token: 0x17001D64 RID: 7524
		// (get) Token: 0x06006D66 RID: 28006 RVA: 0x0026127C File Offset: 0x0025F47C
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.MultiplayerOnly;
			}
		}

		// Token: 0x17001D65 RID: 7525
		// (get) Token: 0x06006D67 RID: 28007 RVA: 0x0026127F File Offset: 0x0025F47F
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001D66 RID: 7526
		// (get) Token: 0x06006D68 RID: 28008 RVA: 0x00261288 File Offset: 0x0025F488
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(0m);
				array[1] = new CalculationExtraVar(1m);
				array[2] = new CalculatedBlockVar(ValueProp.Move).WithMultiplier((CardModel card, [Nullable(2)] Creature target) => (target != null) ? target.Block : 0);
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x17001D67 RID: 7527
		// (get) Token: 0x06006D69 RID: 28009 RVA: 0x002612E7 File Offset: 0x0025F4E7
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006D6A RID: 28010 RVA: 0x002612EC File Offset: 0x0025F4EC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.CalculatedBlock.Calculate(cardPlay.Target), base.DynamicVars.CalculatedBlock.Props, cardPlay, false);
		}

		// Token: 0x06006D6B RID: 28011 RVA: 0x00261337 File Offset: 0x0025F537
		protected override void OnUpgrade()
		{
			base.RemoveKeyword(CardKeyword.Exhaust);
		}
	}
}
