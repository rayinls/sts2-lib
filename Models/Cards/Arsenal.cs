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
	// Token: 0x02000896 RID: 2198
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Arsenal : CardModel
	{
		// Token: 0x060066FD RID: 26365 RVA: 0x0025474B File Offset: 0x0025294B
		public Arsenal()
			: base(1, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001AAE RID: 6830
		// (get) Token: 0x060066FE RID: 26366 RVA: 0x00254758 File Offset: 0x00252958
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x17001AAF RID: 6831
		// (get) Token: 0x060066FF RID: 26367 RVA: 0x00254764 File Offset: 0x00252964
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<ArsenalPower>(1m));
			}
		}

		// Token: 0x06006700 RID: 26368 RVA: 0x00254778 File Offset: 0x00252978
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<ArsenalPower>(base.Owner.Creature, base.DynamicVars["ArsenalPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006701 RID: 26369 RVA: 0x002547BB File Offset: 0x002529BB
		protected override void OnUpgrade()
		{
			base.DynamicVars["ArsenalPower"].UpgradeValueBy(1m);
		}
	}
}
