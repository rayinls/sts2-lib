using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000976 RID: 2422
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Graveblast : CardModel
	{
		// Token: 0x06006BA0 RID: 27552 RVA: 0x0025D856 File Offset: 0x0025BA56
		public Graveblast()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001CAC RID: 7340
		// (get) Token: 0x06006BA1 RID: 27553 RVA: 0x0025D863 File Offset: 0x0025BA63
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001CAD RID: 7341
		// (get) Token: 0x06006BA2 RID: 27554 RVA: 0x0025D86B File Offset: 0x0025BA6B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(4m, ValueProp.Move));
			}
		}

		// Token: 0x06006BA3 RID: 27555 RVA: 0x0025D880 File Offset: 0x0025BA80
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.SelectionScreenPrompt, 1);
			CardPile pile = PileType.Discard.GetPile(base.Owner);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromSimpleGrid(choiceContext, pile.Cards, base.Owner, cardSelectorPrefs);
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				await CardPileCmd.Add(cardModel, PileType.Hand, CardPilePosition.Bottom, null, false);
			}
		}

		// Token: 0x06006BA4 RID: 27556 RVA: 0x0025D8D3 File Offset: 0x0025BAD3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
			base.RemoveKeyword(CardKeyword.Exhaust);
		}
	}
}
