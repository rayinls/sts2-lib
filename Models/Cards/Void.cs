using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000AB5 RID: 2741
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Void : CardModel
	{
		// Token: 0x0600724A RID: 29258 RVA: 0x0026ACD2 File Offset: 0x00268ED2
		public Void()
			: base(-1, CardType.Status, CardRarity.Status, TargetType.None, true)
		{
		}

		// Token: 0x17001F65 RID: 8037
		// (get) Token: 0x0600724B RID: 29259 RVA: 0x0026ACDF File Offset: 0x00268EDF
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001F66 RID: 8038
		// (get) Token: 0x0600724C RID: 29260 RVA: 0x0026ACE2 File Offset: 0x00268EE2
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(1));
			}
		}

		// Token: 0x17001F67 RID: 8039
		// (get) Token: 0x0600724D RID: 29261 RVA: 0x0026ACEF File Offset: 0x00268EEF
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlyArray<CardKeyword>(new CardKeyword[]
				{
					CardKeyword.Unplayable,
					CardKeyword.Ethereal
				});
			}
		}

		// Token: 0x0600724E RID: 29262 RVA: 0x0026AD04 File Offset: 0x00268F04
		public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
		{
			if (card == this)
			{
				await Cmd.Wait(0.25f, false);
				await PlayerCmd.LoseEnergy(base.DynamicVars.Energy.IntValue, base.Owner);
			}
		}
	}
}
