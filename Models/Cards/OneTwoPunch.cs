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
	// Token: 0x020009E8 RID: 2536
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class OneTwoPunch : CardModel
	{
		// Token: 0x06006E07 RID: 28167 RVA: 0x00262663 File Offset: 0x00260863
		public OneTwoPunch()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001DAD RID: 7597
		// (get) Token: 0x06006E08 RID: 28168 RVA: 0x00262670 File Offset: 0x00260870
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Attacks", 1m));
			}
		}

		// Token: 0x06006E09 RID: 28169 RVA: 0x00262688 File Offset: 0x00260888
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<OneTwoPunchPower>(base.Owner.Creature, base.DynamicVars["Attacks"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006E0A RID: 28170 RVA: 0x002626CB File Offset: 0x002608CB
		protected override void OnUpgrade()
		{
			base.DynamicVars["Attacks"].UpgradeValueBy(1m);
		}

		// Token: 0x040025BA RID: 9658
		private const string _attacksKey = "Attacks";
	}
}
