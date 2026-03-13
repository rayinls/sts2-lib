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
	// Token: 0x020009A7 RID: 2471
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Juggernaut : CardModel
	{
		// Token: 0x06006C9A RID: 27802 RVA: 0x0025F6EF File Offset: 0x0025D8EF
		public Juggernaut()
			: base(2, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001D14 RID: 7444
		// (get) Token: 0x06006C9B RID: 27803 RVA: 0x0025F6FC File Offset: 0x0025D8FC
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x17001D15 RID: 7445
		// (get) Token: 0x06006C9C RID: 27804 RVA: 0x0025F70E File Offset: 0x0025D90E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<JuggernautPower>(5m));
			}
		}

		// Token: 0x06006C9D RID: 27805 RVA: 0x0025F720 File Offset: 0x0025D920
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<JuggernautPower>(base.Owner.Creature, base.DynamicVars["JuggernautPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006C9E RID: 27806 RVA: 0x0025F763 File Offset: 0x0025D963
		protected override void OnUpgrade()
		{
			base.DynamicVars["JuggernautPower"].UpgradeValueBy(2m);
		}
	}
}
