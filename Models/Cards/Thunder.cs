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
	// Token: 0x02000A9A RID: 2714
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Thunder : CardModel
	{
		// Token: 0x060071C2 RID: 29122 RVA: 0x00269D16 File Offset: 0x00267F16
		public Thunder()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001F2E RID: 7982
		// (get) Token: 0x060071C3 RID: 29123 RVA: 0x00269D23 File Offset: 0x00267F23
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Evoke, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<LightningOrb>()
				});
			}
		}

		// Token: 0x17001F2F RID: 7983
		// (get) Token: 0x060071C4 RID: 29124 RVA: 0x00269D46 File Offset: 0x00267F46
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<ThunderPower>(6m));
			}
		}

		// Token: 0x060071C5 RID: 29125 RVA: 0x00269D58 File Offset: 0x00267F58
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<ThunderPower>(base.Owner.Creature, base.DynamicVars["ThunderPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060071C6 RID: 29126 RVA: 0x00269D9B File Offset: 0x00267F9B
		protected override void OnUpgrade()
		{
			base.DynamicVars["ThunderPower"].UpgradeValueBy(2m);
		}
	}
}
