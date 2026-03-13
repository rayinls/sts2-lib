using System;
using System.Collections.Generic;
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
	// Token: 0x020009F9 RID: 2553
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PhotonCut : CardModel
	{
		// Token: 0x06006E5E RID: 28254 RVA: 0x002630E4 File Offset: 0x002612E4
		public PhotonCut()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001DD0 RID: 7632
		// (get) Token: 0x06006E5F RID: 28255 RVA: 0x002630F1 File Offset: 0x002612F1
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(10m, ValueProp.Move),
					new CardsVar(1),
					new DynamicVar("PutBack", 1m)
				});
			}
		}

		// Token: 0x06006E60 RID: 28256 RVA: 0x0026312C File Offset: 0x0026132C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, "dagger_throw.mp3")
				.Execute(choiceContext);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.SelectionScreenPrompt, base.DynamicVars["PutBack"].IntValue);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHand(choiceContext, base.Owner, cardSelectorPrefs, null, this);
			await CardPileCmd.Add(enumerable, PileType.Draw, CardPilePosition.Top, null, false);
		}

		// Token: 0x06006E61 RID: 28257 RVA: 0x0026317F File Offset: 0x0026137F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}

		// Token: 0x040025BD RID: 9661
		private const string _putBackKey = "PutBack";
	}
}
