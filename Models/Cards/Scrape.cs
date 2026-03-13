using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A35 RID: 2613
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Scrape : CardModel
	{
		// Token: 0x06006F99 RID: 28569 RVA: 0x002658D4 File Offset: 0x00263AD4
		public Scrape()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001E50 RID: 7760
		// (get) Token: 0x06006F9A RID: 28570 RVA: 0x002658E1 File Offset: 0x00263AE1
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(7m, ValueProp.Move),
					new CardsVar(4)
				});
			}
		}

		// Token: 0x06006F9B RID: 28571 RVA: 0x00265908 File Offset: 0x00263B08
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			IEnumerable<CardModel> enumerable = await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.IntValue, base.Owner, false);
			IEnumerable<CardModel> enumerable2 = enumerable.Where((CardModel c) => c.EnergyCost.GetWithModifiers(CostModifiers.Local) != 0 || c.EnergyCost.CostsX || c.CurrentStarCost > 0 || c.HasStarCostX);
			await CardCmd.Discard(choiceContext, enumerable2);
		}

		// Token: 0x06006F9C RID: 28572 RVA: 0x0026595B File Offset: 0x00263B5B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
