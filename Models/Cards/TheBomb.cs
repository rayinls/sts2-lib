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
	// Token: 0x02000A91 RID: 2705
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TheBomb : CardModel
	{
		// Token: 0x06007189 RID: 29065 RVA: 0x002696CF File Offset: 0x002678CF
		public TheBomb()
			: base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001F19 RID: 7961
		// (get) Token: 0x0600718A RID: 29066 RVA: 0x002696DC File Offset: 0x002678DC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("Turns", 3m),
					new DynamicVar("BombDamage", 40m)
				});
			}
		}

		// Token: 0x0600718B RID: 29067 RVA: 0x00269710 File Offset: 0x00267910
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			TheBombPower theBombPower = await PowerCmd.Apply<TheBombPower>(base.Owner.Creature, base.DynamicVars["Turns"].BaseValue, base.Owner.Creature, this, false);
			TheBombPower theBombPower2 = theBombPower;
			theBombPower2.SetDamage(base.DynamicVars["BombDamage"].BaseValue);
		}

		// Token: 0x0600718C RID: 29068 RVA: 0x00269753 File Offset: 0x00267953
		protected override void OnUpgrade()
		{
			base.DynamicVars["BombDamage"].UpgradeValueBy(10m);
		}

		// Token: 0x040025DA RID: 9690
		private const string _turnsKey = "Turns";

		// Token: 0x040025DB RID: 9691
		private const string _bombDamageKey = "BombDamage";
	}
}
