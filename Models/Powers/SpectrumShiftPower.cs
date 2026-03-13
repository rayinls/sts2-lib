using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006A2 RID: 1698
	public sealed class SpectrumShiftPower : PowerModel
	{
		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x0600559F RID: 21919 RVA: 0x00226D15 File Offset: 0x00224F15
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x060055A0 RID: 21920 RVA: 0x00226D18 File Offset: 0x00224F18
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060055A1 RID: 21921 RVA: 0x00226D1C File Offset: 0x00224F1C
		[NullableContext(1)]
		public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
		{
			if (player == base.Owner.Player)
			{
				List<CardModel> list = CardFactory.GetDistinctForCombat(player, ModelDb.CardPool<ColorlessCardPool>().GetUnlockedCards(player.UnlockState, player.RunState.CardMultiplayerConstraint), base.Amount, player.RunState.Rng.CombatCardGeneration).ToList<CardModel>();
				await CardPileCmd.AddGeneratedCardsToCombat(list, PileType.Hand, true, CardPilePosition.Bottom);
				base.Flash();
			}
		}
	}
}
