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
	// Token: 0x02000A8E RID: 2702
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Tempest : CardModel
	{
		// Token: 0x0600717B RID: 29051 RVA: 0x0026952B File Offset: 0x0026772B
		public Tempest()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001F13 RID: 7955
		// (get) Token: 0x0600717C RID: 29052 RVA: 0x00269538 File Offset: 0x00267738
		protected override bool HasEnergyCostX
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001F14 RID: 7956
		// (get) Token: 0x0600717D RID: 29053 RVA: 0x0026953B File Offset: 0x0026773B
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

		// Token: 0x0600717E RID: 29054 RVA: 0x00269560 File Offset: 0x00267760
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			int numOfOrbs = base.ResolveEnergyXValue();
			if (base.IsUpgraded)
			{
				numOfOrbs++;
			}
			for (int i = 0; i < numOfOrbs; i++)
			{
				await OrbCmd.Channel<LightningOrb>(choiceContext, base.Owner);
			}
		}
	}
}
