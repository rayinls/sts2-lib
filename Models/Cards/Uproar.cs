using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000AB1 RID: 2737
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Uproar : CardModel
	{
		// Token: 0x06007238 RID: 29240 RVA: 0x0026AAB3 File Offset: 0x00268CB3
		public Uproar()
			: base(2, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001F5F RID: 8031
		// (get) Token: 0x06007239 RID: 29241 RVA: 0x0026AAC0 File Offset: 0x00268CC0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(5m, ValueProp.Move));
			}
		}

		// Token: 0x0600723A RID: 29242 RVA: 0x0026AAD4 File Offset: 0x00268CD4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).WithHitCount(2)
				.Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			CardModel cardModel = PileType.Draw.GetPile(base.Owner).Cards.Where((CardModel c) => c.Type == CardType.Attack && !c.Keywords.Contains(CardKeyword.Unplayable)).ToList<CardModel>().StableShuffle(base.Owner.RunState.Rng.Shuffle)
				.FirstOrDefault<CardModel>();
			if (cardModel == null)
			{
				cardModel = PileType.Draw.GetPile(base.Owner).Cards.Where((CardModel c) => c.Type == CardType.Attack).ToList<CardModel>().StableShuffle(base.Owner.RunState.Rng.Shuffle)
					.FirstOrDefault<CardModel>();
			}
			if (cardModel != null)
			{
				await CardCmd.AutoPlay(choiceContext, cardModel, null, AutoPlayType.Default, false, false);
			}
		}

		// Token: 0x0600723B RID: 29243 RVA: 0x0026AB27 File Offset: 0x00268D27
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
		}
	}
}
