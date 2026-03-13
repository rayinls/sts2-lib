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
	// Token: 0x020008FB RID: 2299
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Cruelty : CardModel
	{
		// Token: 0x060068FF RID: 26879 RVA: 0x002587C0 File Offset: 0x002569C0
		public Cruelty()
			: base(1, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001B82 RID: 7042
		// (get) Token: 0x06006900 RID: 26880 RVA: 0x002587CD File Offset: 0x002569CD
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<CrueltyPower>(25m));
			}
		}

		// Token: 0x17001B83 RID: 7043
		// (get) Token: 0x06006901 RID: 26881 RVA: 0x002587E0 File Offset: 0x002569E0
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VulnerablePower>());
			}
		}

		// Token: 0x06006902 RID: 26882 RVA: 0x002587EC File Offset: 0x002569EC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<CrueltyPower>(base.Owner.Creature, base.DynamicVars["CrueltyPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006903 RID: 26883 RVA: 0x0025882F File Offset: 0x00256A2F
		protected override void OnUpgrade()
		{
			base.DynamicVars["CrueltyPower"].UpgradeValueBy(25m);
		}
	}
}
