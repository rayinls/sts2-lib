using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000660 RID: 1632
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NightmarePower : PowerModel
	{
		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x060053FE RID: 21502 RVA: 0x00223C2B File Offset: 0x00221E2B
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x060053FF RID: 21503 RVA: 0x00223C2E File Offset: 0x00221E2E
		public override bool IsInstanced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x06005400 RID: 21504 RVA: 0x00223C31 File Offset: 0x00221E31
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005401 RID: 21505 RVA: 0x00223C34 File Offset: 0x00221E34
		protected override object InitInternalData()
		{
			return new NightmarePower.Data();
		}

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x06005402 RID: 21506 RVA: 0x00223C3B File Offset: 0x00221E3B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new StringVar("Card", ""));
			}
		}

		// Token: 0x06005403 RID: 21507 RVA: 0x00223C54 File Offset: 0x00221E54
		public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
		{
			if (player == base.Owner.Player)
			{
				CardModel card = base.GetInternalData<NightmarePower.Data>().selectedCard;
				for (int i = 0; i < base.Amount; i++)
				{
					CardModel cardModel = card.CreateClone();
					await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, true, CardPilePosition.Bottom);
				}
				await PowerCmd.Remove(this);
			}
		}

		// Token: 0x06005404 RID: 21508 RVA: 0x00223C9F File Offset: 0x00221E9F
		public void SetSelectedCard(CardModel card)
		{
			base.GetInternalData<NightmarePower.Data>().selectedCard = card.CreateClone();
			((StringVar)base.DynamicVars["Card"]).StringValue = card.Title;
		}

		// Token: 0x0400225E RID: 8798
		private const string _cardKey = "Card";

		// Token: 0x02001A3A RID: 6714
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x040066E1 RID: 26337
			[Nullable(2)]
			public CardModel selectedCard;
		}
	}
}
