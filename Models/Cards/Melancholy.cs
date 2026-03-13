using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009C5 RID: 2501
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Melancholy : CardModel
	{
		// Token: 0x06006D49 RID: 27977 RVA: 0x00260E79 File Offset: 0x0025F079
		public Melancholy()
			: base(3, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001D58 RID: 7512
		// (get) Token: 0x06006D4A RID: 27978 RVA: 0x00260E86 File Offset: 0x0025F086
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001D59 RID: 7513
		// (get) Token: 0x06006D4B RID: 27979 RVA: 0x00260E89 File Offset: 0x0025F089
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(13m, ValueProp.Move),
					new EnergyVar(1)
				});
			}
		}

		// Token: 0x17001D5A RID: 7514
		// (get) Token: 0x06006D4C RID: 27980 RVA: 0x00260EAF File Offset: 0x0025F0AF
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x06006D4D RID: 27981 RVA: 0x00260EBC File Offset: 0x0025F0BC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
		}

		// Token: 0x06006D4E RID: 27982 RVA: 0x00260F08 File Offset: 0x0025F108
		public override Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (wasRemovalPrevented)
			{
				return Task.CompletedTask;
			}
			CardPile pile = base.Pile;
			if (pile == null || !pile.IsCombatPile)
			{
				return Task.CompletedTask;
			}
			base.EnergyCost.AddThisCombat(-base.DynamicVars.Energy.IntValue, false);
			return Task.CompletedTask;
		}

		// Token: 0x06006D4F RID: 27983 RVA: 0x00260F58 File Offset: 0x0025F158
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(4m);
		}
	}
}
