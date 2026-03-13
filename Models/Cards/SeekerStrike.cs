using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A3C RID: 2620
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SeekerStrike : CardModel
	{
		// Token: 0x06006FBB RID: 28603 RVA: 0x00265CD4 File Offset: 0x00263ED4
		public SeekerStrike()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001E5D RID: 7773
		// (get) Token: 0x06006FBC RID: 28604 RVA: 0x00265CE1 File Offset: 0x00263EE1
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Strike };
			}
		}

		// Token: 0x17001E5E RID: 7774
		// (get) Token: 0x06006FBD RID: 28605 RVA: 0x00265CF0 File Offset: 0x00263EF0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(6m, ValueProp.Move),
					new CardsVar(3)
				});
			}
		}

		// Token: 0x06006FBE RID: 28606 RVA: 0x00265D18 File Offset: 0x00263F18
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			IEnumerable<CardModel> enumerable = PileType.Draw.GetPile(base.Owner).Cards.ToList<CardModel>().StableShuffle(base.Owner.RunState.Rng.CombatCardSelection);
			IEnumerable<CardModel> enumerable2 = await CardSelectCmd.FromSimpleGrid(choiceContext, enumerable.Take(base.DynamicVars.Cards.IntValue).ToList<CardModel>(), base.Owner, new CardSelectorPrefs(base.SelectionScreenPrompt, 1));
			CardModel cardModel = enumerable2.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				await CardPileCmd.Add(cardModel, PileType.Hand, CardPilePosition.Bottom, null, false);
			}
		}

		// Token: 0x06006FBF RID: 28607 RVA: 0x00265D6B File Offset: 0x00263F6B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
