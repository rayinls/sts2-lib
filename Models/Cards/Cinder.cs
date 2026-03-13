using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008DF RID: 2271
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Cinder : CardModel
	{
		// Token: 0x0600686C RID: 26732 RVA: 0x00257534 File Offset: 0x00255734
		public Cinder()
			: base(2, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001B44 RID: 6980
		// (get) Token: 0x0600686D RID: 26733 RVA: 0x00257541 File Offset: 0x00255741
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(17m, ValueProp.Move),
					new DynamicVar("CardsToExhaust", 1m)
				});
			}
		}

		// Token: 0x17001B45 RID: 6981
		// (get) Token: 0x0600686E RID: 26734 RVA: 0x00257570 File Offset: 0x00255770
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x0600686F RID: 26735 RVA: 0x00257580 File Offset: 0x00255780
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitVfxNode((Creature t) => NFireBurstVfx.Create(t, 0.75f))
				.Execute(choiceContext);
			CardPile drawPile = PileType.Draw.GetPile(base.Owner);
			for (int i = 0; i < base.DynamicVars["CardsToExhaust"].IntValue; i++)
			{
				await CardPileCmd.ShuffleIfNecessary(choiceContext, base.Owner);
				CardModel cardModel = drawPile.Cards.FirstOrDefault<CardModel>();
				if (cardModel != null)
				{
					await CardCmd.Exhaust(choiceContext, cardModel, false, false);
				}
			}
		}

		// Token: 0x06006870 RID: 26736 RVA: 0x002575D3 File Offset: 0x002557D3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(5m);
		}

		// Token: 0x04002563 RID: 9571
		private const string _cardsToExhaustKey = "CardsToExhaust";
	}
}
