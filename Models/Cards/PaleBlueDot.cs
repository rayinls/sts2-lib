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
	// Token: 0x020009EF RID: 2543
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PaleBlueDot : CardModel
	{
		// Token: 0x06006E2A RID: 28202 RVA: 0x00262A59 File Offset: 0x00260C59
		public PaleBlueDot()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001DBB RID: 7611
		// (get) Token: 0x06006E2B RID: 28203 RVA: 0x00262A66 File Offset: 0x00260C66
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(1),
					new DynamicVar("CardPlay", 5m)
				});
			}
		}

		// Token: 0x06006E2C RID: 28204 RVA: 0x00262A90 File Offset: 0x00260C90
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<PaleBlueDotPower>(base.Owner.Creature, base.DynamicVars.Cards.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006E2D RID: 28205 RVA: 0x00262AD3 File Offset: 0x00260CD3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
