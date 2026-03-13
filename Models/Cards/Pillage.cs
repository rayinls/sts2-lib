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
	// Token: 0x020009FB RID: 2555
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Pillage : CardModel
	{
		// Token: 0x06006E68 RID: 28264 RVA: 0x00263244 File Offset: 0x00261444
		public Pillage()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001DD4 RID: 7636
		// (get) Token: 0x06006E69 RID: 28265 RVA: 0x00263251 File Offset: 0x00261451
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(6m, ValueProp.Move));
			}
		}

		// Token: 0x06006E6A RID: 28266 RVA: 0x00263264 File Offset: 0x00261464
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			CardModel cardModel;
			do
			{
				cardModel = await CardPileCmd.Draw(choiceContext, base.Owner);
			}
			while (cardModel != null && cardModel.Type == CardType.Attack && CardPile.GetCards(base.Owner, new PileType[] { PileType.Hand }).Count<CardModel>() < 10);
		}

		// Token: 0x06006E6B RID: 28267 RVA: 0x002632B7 File Offset: 0x002614B7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
