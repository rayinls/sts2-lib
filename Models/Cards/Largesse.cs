using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009B0 RID: 2480
	public sealed class Largesse : CardModel
	{
		// Token: 0x06006CC7 RID: 27847 RVA: 0x0025FD25 File Offset: 0x0025DF25
		public Largesse()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.AnyAlly, true)
		{
		}

		// Token: 0x17001D23 RID: 7459
		// (get) Token: 0x06006CC8 RID: 27848 RVA: 0x0025FD32 File Offset: 0x0025DF32
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.MultiplayerOnly;
			}
		}

		// Token: 0x06006CC9 RID: 27849 RVA: 0x0025FD38 File Offset: 0x0025DF38
		[NullableContext(1)]
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			CardModel cardModel = CardFactory.GetDistinctForCombat(cardPlay.Target.Player, ModelDb.CardPool<ColorlessCardPool>().GetUnlockedCards(cardPlay.Target.Player.UnlockState, cardPlay.Target.Player.RunState.CardMultiplayerConstraint), 1, base.Owner.RunState.Rng.CombatCardGeneration).FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				if (base.IsUpgraded)
				{
					CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
				}
				await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, true, CardPilePosition.Bottom);
			}
		}
	}
}
