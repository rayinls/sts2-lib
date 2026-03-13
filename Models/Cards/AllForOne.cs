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
	// Token: 0x0200088F RID: 2191
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class AllForOne : CardModel
	{
		// Token: 0x060066DD RID: 26333 RVA: 0x00254362 File Offset: 0x00252562
		public AllForOne()
			: base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001AA3 RID: 6819
		// (get) Token: 0x060066DE RID: 26334 RVA: 0x0025436F File Offset: 0x0025256F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(10m, ValueProp.Move));
			}
		}

		// Token: 0x060066DF RID: 26335 RVA: 0x00254384 File Offset: 0x00252584
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_heavy_blunt", null, "blunt_attack.mp3")
				.WithHitVfxSpawnedAtBase()
				.Execute(choiceContext);
			IEnumerable<CardModel> enumerable = PileType.Discard.GetPile(base.Owner).Cards.Where(new Func<CardModel, bool>(this.Filter)).ToList<CardModel>();
			foreach (CardModel cardModel in enumerable)
			{
				await CardPileCmd.Add(cardModel, PileType.Hand, CardPilePosition.Bottom, null, false);
			}
			IEnumerator<CardModel> enumerator = null;
		}

		// Token: 0x060066E0 RID: 26336 RVA: 0x002543D8 File Offset: 0x002525D8
		private bool Filter(CardModel card)
		{
			bool flag = card.EnergyCost.GetWithModifiers(CostModifiers.All) == 0 && !card.EnergyCost.CostsX;
			bool flag2 = flag;
			if (flag2)
			{
				CardType type = card.Type;
				bool flag3 = type - CardType.Attack <= 2;
				flag2 = flag3;
			}
			return flag2;
		}

		// Token: 0x060066E1 RID: 26337 RVA: 0x00254420 File Offset: 0x00252620
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(4m);
		}
	}
}
