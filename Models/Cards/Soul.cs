using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A5C RID: 2652
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Soul : CardModel
	{
		// Token: 0x06007068 RID: 28776 RVA: 0x0026709A File Offset: 0x0026529A
		public Soul()
			: base(0, CardType.Skill, CardRarity.Token, TargetType.Self, true)
		{
		}

		// Token: 0x17001EAC RID: 7852
		// (get) Token: 0x06007069 RID: 28777 RVA: 0x002670A7 File Offset: 0x002652A7
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001EAD RID: 7853
		// (get) Token: 0x0600706A RID: 28778 RVA: 0x002670AF File Offset: 0x002652AF
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(2));
			}
		}

		// Token: 0x0600706B RID: 28779 RVA: 0x002670BC File Offset: 0x002652BC
		public static async Task<IEnumerable<Soul>> CreateInHand(Player owner, int amount, CombatState combatState)
		{
			IEnumerable<Soul> souls = Soul.Create(owner, amount, combatState);
			await CardPileCmd.AddGeneratedCardsToCombat(souls, PileType.Hand, true, CardPilePosition.Bottom);
			return souls;
		}

		// Token: 0x0600706C RID: 28780 RVA: 0x00267110 File Offset: 0x00265310
		public static IEnumerable<Soul> Create(Player owner, int amount, CombatState combatState)
		{
			List<Soul> list = new List<Soul>();
			for (int i = 0; i < amount; i++)
			{
				list.Add(combatState.CreateCard<Soul>(owner));
			}
			return list;
		}

		// Token: 0x0600706D RID: 28781 RVA: 0x00267140 File Offset: 0x00265340
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}

		// Token: 0x0600706E RID: 28782 RVA: 0x0026718B File Offset: 0x0026538B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
