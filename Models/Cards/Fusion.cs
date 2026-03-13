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

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000965 RID: 2405
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Fusion : CardModel
	{
		// Token: 0x06006B43 RID: 27459 RVA: 0x0025CC33 File Offset: 0x0025AE33
		public Fusion()
			: base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001C87 RID: 7303
		// (get) Token: 0x06006B44 RID: 27460 RVA: 0x0025CC40 File Offset: 0x0025AE40
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<PlasmaOrb>()
				});
			}
		}

		// Token: 0x06006B45 RID: 27461 RVA: 0x0025CC64 File Offset: 0x0025AE64
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await OrbCmd.Channel<PlasmaOrb>(choiceContext, base.Owner);
		}

		// Token: 0x06006B46 RID: 27462 RVA: 0x0025CCAF File Offset: 0x0025AEAF
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
