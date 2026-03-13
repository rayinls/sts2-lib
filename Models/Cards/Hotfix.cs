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
	// Token: 0x02000992 RID: 2450
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Hotfix : CardModel
	{
		// Token: 0x06006C2E RID: 27694 RVA: 0x0025EA1E File Offset: 0x0025CC1E
		public Hotfix()
			: base(0, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001CE8 RID: 7400
		// (get) Token: 0x06006C2F RID: 27695 RVA: 0x0025EA2B File Offset: 0x0025CC2B
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<FocusPower>());
			}
		}

		// Token: 0x17001CE9 RID: 7401
		// (get) Token: 0x06006C30 RID: 27696 RVA: 0x0025EA37 File Offset: 0x0025CC37
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<FocusPower>(2m));
			}
		}

		// Token: 0x06006C31 RID: 27697 RVA: 0x0025EA4C File Offset: 0x0025CC4C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<HotfixPower>(base.Owner.Creature, base.DynamicVars["FocusPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006C32 RID: 27698 RVA: 0x0025EA8F File Offset: 0x0025CC8F
		protected override void OnUpgrade()
		{
			base.DynamicVars["FocusPower"].UpgradeValueBy(1m);
		}
	}
}
