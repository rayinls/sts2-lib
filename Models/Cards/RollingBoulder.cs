using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A2D RID: 2605
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RollingBoulder : CardModel
	{
		// Token: 0x06006F70 RID: 28528 RVA: 0x0026544E File Offset: 0x0026364E
		public RollingBoulder()
			: base(3, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001E3F RID: 7743
		// (get) Token: 0x06006F71 RID: 28529 RVA: 0x0026545B File Offset: 0x0026365B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<RollingBoulderPower>(5m),
					new DynamicVar("IncrementAmount", 5m)
				});
			}
		}

		// Token: 0x06006F72 RID: 28530 RVA: 0x0026548C File Offset: 0x0026368C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<RollingBoulderPower>(base.Owner.Creature, base.DynamicVars["RollingBoulderPower"].IntValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006F73 RID: 28531 RVA: 0x002654CF File Offset: 0x002636CF
		protected override void OnUpgrade()
		{
			base.DynamicVars["RollingBoulderPower"].UpgradeValueBy(5m);
		}

		// Token: 0x040025C7 RID: 9671
		public const int incrementAmount = 5;

		// Token: 0x040025C8 RID: 9672
		private const string _incrementAmountKey = "IncrementAmount";
	}
}
