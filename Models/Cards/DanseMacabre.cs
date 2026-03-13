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
	// Token: 0x02000900 RID: 2304
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DanseMacabre : CardModel
	{
		// Token: 0x06006918 RID: 26904 RVA: 0x00258B04 File Offset: 0x00256D04
		public DanseMacabre()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001B8B RID: 7051
		// (get) Token: 0x06006919 RID: 26905 RVA: 0x00258B11 File Offset: 0x00256D11
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<DanseMacabrePower>(3m),
					new EnergyVar(2)
				});
			}
		}

		// Token: 0x17001B8C RID: 7052
		// (get) Token: 0x0600691A RID: 26906 RVA: 0x00258B35 File Offset: 0x00256D35
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x0600691B RID: 26907 RVA: 0x00258B44 File Offset: 0x00256D44
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<DanseMacabrePower>(base.Owner.Creature, base.DynamicVars["DanseMacabrePower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x0600691C RID: 26908 RVA: 0x00258B87 File Offset: 0x00256D87
		protected override void OnUpgrade()
		{
			base.DynamicVars["DanseMacabrePower"].UpgradeValueBy(1m);
		}
	}
}
