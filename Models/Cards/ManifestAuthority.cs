using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009C0 RID: 2496
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ManifestAuthority : CardModel
	{
		// Token: 0x06006D2F RID: 27951 RVA: 0x00260B5E File Offset: 0x0025ED5E
		public ManifestAuthority()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001D50 RID: 7504
		// (get) Token: 0x06006D30 RID: 27952 RVA: 0x00260B6B File Offset: 0x0025ED6B
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x17001D51 RID: 7505
		// (get) Token: 0x06006D31 RID: 27953 RVA: 0x00260B7D File Offset: 0x0025ED7D
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001D52 RID: 7506
		// (get) Token: 0x06006D32 RID: 27954 RVA: 0x00260B80 File Offset: 0x0025ED80
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(7m, ValueProp.Move));
			}
		}

		// Token: 0x06006D33 RID: 27955 RVA: 0x00260B94 File Offset: 0x0025ED94
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			CardModel cardModel = CardFactory.GetDistinctForCombat(base.Owner, ModelDb.CardPool<ColorlessCardPool>().GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint), 1, base.Owner.RunState.Rng.CombatCardGeneration).FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				if (base.IsUpgraded)
				{
					CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
				}
				await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, true, CardPilePosition.Bottom);
			}
		}

		// Token: 0x06006D34 RID: 27956 RVA: 0x00260BDF File Offset: 0x0025EDDF
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(1m);
		}
	}
}
