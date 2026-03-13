using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200066C RID: 1644
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PanachePower : PowerModel
	{
		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x0600544D RID: 21581 RVA: 0x00224440 File Offset: 0x00222640
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x0600544E RID: 21582 RVA: 0x00224443 File Offset: 0x00222643
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x0600544F RID: 21583 RVA: 0x00224446 File Offset: 0x00222646
		public override int DisplayAmount
		{
			get
			{
				return base.DynamicVars["CardsLeft"].IntValue;
			}
		}

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x06005450 RID: 21584 RVA: 0x0022445D File Offset: 0x0022265D
		public override bool IsInstanced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x06005451 RID: 21585 RVA: 0x00224460 File Offset: 0x00222660
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("CardsLeft", 5m));
			}
		}

		// Token: 0x06005452 RID: 21586 RVA: 0x00224477 File Offset: 0x00222677
		protected override object InitInternalData()
		{
			return new PanachePower.Data();
		}

		// Token: 0x06005453 RID: 21587 RVA: 0x00224480 File Offset: 0x00222680
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner.Player)
			{
				PanachePower.Data data = base.GetInternalData<PanachePower.Data>();
				if (data.alreadyApplied)
				{
					DynamicVar dynamicVar = base.DynamicVars["CardsLeft"];
					decimal baseValue = dynamicVar.BaseValue;
					dynamicVar.BaseValue = baseValue - 1m;
					base.InvokeDisplayAmountChanged();
					if (base.DynamicVars["CardsLeft"].IntValue <= 0)
					{
						await Cmd.Wait(0.5f, false);
						await CreatureCmd.Damage(context, base.CombatState.HittableEnemies, base.Amount, ValueProp.Unpowered, base.Owner);
						base.DynamicVars["CardsLeft"].BaseValue = 5m;
						base.InvokeDisplayAmountChanged();
					}
				}
				data.alreadyApplied = true;
			}
		}

		// Token: 0x06005454 RID: 21588 RVA: 0x002244D3 File Offset: 0x002226D3
		public override Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side != base.Owner.Side)
			{
				return Task.CompletedTask;
			}
			base.DynamicVars["CardsLeft"].BaseValue = 5m;
			base.InvokeDisplayAmountChanged();
			return Task.CompletedTask;
		}

		// Token: 0x04002263 RID: 8803
		private const int _baseCardsLeft = 5;

		// Token: 0x04002264 RID: 8804
		private const string _cardsLeftKey = "CardsLeft";

		// Token: 0x02001A4B RID: 6731
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x04006731 RID: 26417
			public bool alreadyApplied;
		}
	}
}
