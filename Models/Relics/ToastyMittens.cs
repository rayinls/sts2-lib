using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005AA RID: 1450
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ToastyMittens : RelicModel
	{
		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x06004FF2 RID: 20466 RVA: 0x0021C058 File Offset: 0x0021A258
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x06004FF3 RID: 20467 RVA: 0x0021C05B File Offset: 0x0021A25B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<StrengthPower>(1m));
			}
		}

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x06004FF4 RID: 20468 RVA: 0x0021C06C File Offset: 0x0021A26C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromKeyword(CardKeyword.Exhaust),
					HoverTipFactory.FromPower<StrengthPower>()
				});
			}
		}

		// Token: 0x06004FF5 RID: 20469 RVA: 0x0021C08C File Offset: 0x0021A28C
		public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
		{
			if (player == base.Owner.Creature.Player)
			{
				base.Flash();
				await CardPileCmd.ShuffleIfNecessary(choiceContext, base.Owner);
				IReadOnlyList<CardModel> cards = PileType.Draw.GetPile(player).Cards;
				CardModel cardModel = null;
				if (combatState.RoundNumber == 1)
				{
					cardModel = cards.FirstOrDefault((CardModel c) => !c.Keywords.Contains(CardKeyword.Innate));
				}
				if (cardModel == null)
				{
					cardModel = cards.FirstOrDefault<CardModel>();
				}
				if (cardModel != null)
				{
					await CardCmd.Exhaust(choiceContext, cardModel, false, false);
				}
				await PowerCmd.Apply<StrengthPower>(player.Creature, base.DynamicVars.Strength.BaseValue, player.Creature, null, false);
			}
		}
	}
}
