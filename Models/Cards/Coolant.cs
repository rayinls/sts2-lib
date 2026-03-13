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
	// Token: 0x020008F0 RID: 2288
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Coolant : CardModel
	{
		// Token: 0x060068C8 RID: 26824 RVA: 0x00258156 File Offset: 0x00256356
		public Coolant()
			: base(1, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001B6C RID: 7020
		// (get) Token: 0x060068C9 RID: 26825 RVA: 0x00258163 File Offset: 0x00256363
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x17001B6D RID: 7021
		// (get) Token: 0x060068CA RID: 26826 RVA: 0x00258175 File Offset: 0x00256375
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<CoolantPower>(2m));
			}
		}

		// Token: 0x060068CB RID: 26827 RVA: 0x00258188 File Offset: 0x00256388
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<CoolantPower>(base.Owner.Creature, base.DynamicVars["CoolantPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060068CC RID: 26828 RVA: 0x002581CB File Offset: 0x002563CB
		protected override void OnUpgrade()
		{
			base.DynamicVars["CoolantPower"].UpgradeValueBy(1m);
		}
	}
}
