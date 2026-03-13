using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Orbs;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200097D RID: 2429
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Hailstorm : CardModel
	{
		// Token: 0x06006BC4 RID: 27588 RVA: 0x0025DC4A File Offset: 0x0025BE4A
		public Hailstorm()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001CBE RID: 7358
		// (get) Token: 0x06006BC5 RID: 27589 RVA: 0x0025DC57 File Offset: 0x0025BE57
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromOrb<FrostOrb>());
			}
		}

		// Token: 0x17001CBF RID: 7359
		// (get) Token: 0x06006BC6 RID: 27590 RVA: 0x0025DC63 File Offset: 0x0025BE63
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<HailstormPower>(6m));
			}
		}

		// Token: 0x06006BC7 RID: 27591 RVA: 0x0025DC78 File Offset: 0x0025BE78
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<HailstormPower>(base.Owner.Creature, base.DynamicVars["HailstormPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006BC8 RID: 27592 RVA: 0x0025DCBB File Offset: 0x0025BEBB
		protected override void OnUpgrade()
		{
			base.DynamicVars["HailstormPower"].UpgradeValueBy(2m);
		}
	}
}
