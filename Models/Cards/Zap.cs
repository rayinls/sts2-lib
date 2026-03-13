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
	// Token: 0x02000AC4 RID: 2756
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Zap : CardModel
	{
		// Token: 0x06007293 RID: 29331 RVA: 0x0026B489 File Offset: 0x00269689
		public Zap()
			: base(1, CardType.Skill, CardRarity.Basic, TargetType.Self, true)
		{
		}

		// Token: 0x17001F87 RID: 8071
		// (get) Token: 0x06007294 RID: 29332 RVA: 0x0026B496 File Offset: 0x00269696
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<LightningOrb>()
				});
			}
		}

		// Token: 0x06007295 RID: 29333 RVA: 0x0026B4BC File Offset: 0x002696BC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await OrbCmd.Channel<LightningOrb>(choiceContext, base.Owner);
		}

		// Token: 0x06007296 RID: 29334 RVA: 0x0026B507 File Offset: 0x00269707
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
