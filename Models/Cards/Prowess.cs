using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A0C RID: 2572
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Prowess : CardModel
	{
		// Token: 0x06006EBA RID: 28346 RVA: 0x00263CFF File Offset: 0x00261EFF
		public Prowess()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001DF2 RID: 7666
		// (get) Token: 0x06006EBB RID: 28347 RVA: 0x00263D0C File Offset: 0x00261F0C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<StrengthPower>(),
					HoverTipFactory.FromPower<DexterityPower>()
				});
			}
		}

		// Token: 0x17001DF3 RID: 7667
		// (get) Token: 0x06006EBC RID: 28348 RVA: 0x00263D29 File Offset: 0x00261F29
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<StrengthPower>(1m),
					new PowerVar<DexterityPower>(1m)
				});
			}
		}

		// Token: 0x06006EBD RID: 28349 RVA: 0x00263D50 File Offset: 0x00261F50
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, base.DynamicVars.Strength.BaseValue, base.Owner.Creature, this, false);
			await PowerCmd.Apply<DexterityPower>(base.Owner.Creature, base.DynamicVars.Dexterity.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006EBE RID: 28350 RVA: 0x00263D93 File Offset: 0x00261F93
		protected override void OnUpgrade()
		{
			base.DynamicVars.Dexterity.UpgradeValueBy(1m);
			base.DynamicVars.Strength.UpgradeValueBy(1m);
		}
	}
}
