using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005DB RID: 1499
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class AutomationPower : PowerModel
	{
		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x0600511E RID: 20766 RVA: 0x0021EDB7 File Offset: 0x0021CFB7
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x0600511F RID: 20767 RVA: 0x0021EDBA File Offset: 0x0021CFBA
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x06005120 RID: 20768 RVA: 0x0021EDBD File Offset: 0x0021CFBD
		public override int DisplayAmount
		{
			get
			{
				return base.GetInternalData<AutomationPower.Data>().cardsLeft;
			}
		}

		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x06005121 RID: 20769 RVA: 0x0021EDCA File Offset: 0x0021CFCA
		public override bool IsInstanced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x06005122 RID: 20770 RVA: 0x0021EDCD File Offset: 0x0021CFCD
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("BaseCards", 10m));
			}
		}

		// Token: 0x06005123 RID: 20771 RVA: 0x0021EDE5 File Offset: 0x0021CFE5
		protected override object InitInternalData()
		{
			return new AutomationPower.Data();
		}

		// Token: 0x06005124 RID: 20772 RVA: 0x0021EDEC File Offset: 0x0021CFEC
		public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
		{
			if (card.Owner == base.Owner.Player)
			{
				AutomationPower.Data data = base.GetInternalData<AutomationPower.Data>();
				data.cardsLeft--;
				base.InvokeDisplayAmountChanged();
				if (data.cardsLeft <= 0)
				{
					base.Flash();
					await PlayerCmd.GainEnergy(base.Amount, base.Owner.Player);
					data.cardsLeft = 10;
					base.InvokeDisplayAmountChanged();
				}
			}
		}

		// Token: 0x04002248 RID: 8776
		private const int _baseCardsLeft = 10;

		// Token: 0x04002249 RID: 8777
		private const string _baseCardsKey = "BaseCards";

		// Token: 0x020019A6 RID: 6566
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x04006412 RID: 25618
			public int cardsLeft = 10;
		}
	}
}
