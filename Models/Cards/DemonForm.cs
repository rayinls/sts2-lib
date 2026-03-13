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
	// Token: 0x0200091A RID: 2330
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DemonForm : CardModel
	{
		// Token: 0x060069A5 RID: 27045 RVA: 0x00259A85 File Offset: 0x00257C85
		public DemonForm()
			: base(3, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001BCF RID: 7119
		// (get) Token: 0x060069A6 RID: 27046 RVA: 0x00259A92 File Offset: 0x00257C92
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x17001BD0 RID: 7120
		// (get) Token: 0x060069A7 RID: 27047 RVA: 0x00259A9E File Offset: 0x00257C9E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<StrengthPower>(2m));
			}
		}

		// Token: 0x060069A8 RID: 27048 RVA: 0x00259AB0 File Offset: 0x00257CB0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<DemonFormPower>(base.Owner.Creature, base.DynamicVars["StrengthPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060069A9 RID: 27049 RVA: 0x00259AF3 File Offset: 0x00257CF3
		protected override void OnUpgrade()
		{
			base.DynamicVars["StrengthPower"].UpgradeValueBy(1m);
		}
	}
}
