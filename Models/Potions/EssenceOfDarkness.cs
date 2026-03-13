using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Orbs;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x020006F3 RID: 1779
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EssenceOfDarkness : PotionModel
	{
		// Token: 0x170013C8 RID: 5064
		// (get) Token: 0x0600578C RID: 22412 RVA: 0x0022A073 File Offset: 0x00228273
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Rare;
			}
		}

		// Token: 0x170013C9 RID: 5065
		// (get) Token: 0x0600578D RID: 22413 RVA: 0x0022A076 File Offset: 0x00228276
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x0600578E RID: 22414 RVA: 0x0022A079 File Offset: 0x00228279
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x0600578F RID: 22415 RVA: 0x0022A07C File Offset: 0x0022827C
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<DarkOrb>()
				});
			}
		}

		// Token: 0x06005790 RID: 22416 RVA: 0x0022A0A0 File Offset: 0x002282A0
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			int count = base.Owner.PlayerCombatState.OrbQueue.Capacity;
			for (int i = 0; i < count; i++)
			{
				await OrbCmd.Channel<DarkOrb>(choiceContext, base.Owner);
			}
		}
	}
}
