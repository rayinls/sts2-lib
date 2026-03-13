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
	// Token: 0x02000985 RID: 2437
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Headbutt : CardModel
	{
		// Token: 0x06006BEE RID: 27630 RVA: 0x0025E17C File Offset: 0x0025C37C
		public Headbutt()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001CCF RID: 7375
		// (get) Token: 0x06006BEF RID: 27631 RVA: 0x0025E189 File Offset: 0x0025C389
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(9m, ValueProp.Move));
			}
		}

		// Token: 0x06006BF0 RID: 27632 RVA: 0x0025E1A0 File Offset: 0x0025C3A0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.SelectionScreenPrompt, 1);
			CardPile pile = PileType.Discard.GetPile(base.Owner);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromSimpleGrid(choiceContext, pile.Cards, base.Owner, cardSelectorPrefs);
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				await CardPileCmd.Add(cardModel, PileType.Draw, CardPilePosition.Top, null, false);
			}
		}

		// Token: 0x06006BF1 RID: 27633 RVA: 0x0025E1F3 File Offset: 0x0025C3F3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
