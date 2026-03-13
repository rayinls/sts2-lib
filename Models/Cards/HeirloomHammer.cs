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
	// Token: 0x02000988 RID: 2440
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HeirloomHammer : CardModel
	{
		// Token: 0x06006BFD RID: 27645 RVA: 0x0025E394 File Offset: 0x0025C594
		public HeirloomHammer()
			: base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001CD5 RID: 7381
		// (get) Token: 0x06006BFE RID: 27646 RVA: 0x0025E3A1 File Offset: 0x0025C5A1
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(17m, ValueProp.Move),
					new RepeatVar(1)
				});
			}
		}

		// Token: 0x06006BFF RID: 27647 RVA: 0x0025E3C8 File Offset: 0x0025C5C8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.SelectionScreenPrompt, 1);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHand(choiceContext, base.Owner, cardSelectorPrefs, (CardModel c) => c.VisualCardPool.IsColorless, this);
			CardModel selection = enumerable.FirstOrDefault<CardModel>();
			if (selection != null)
			{
				for (int i = 0; i < base.DynamicVars.Repeat.IntValue; i++)
				{
					await CardPileCmd.AddGeneratedCardToCombat(selection.CreateClone(), PileType.Hand, true, CardPilePosition.Bottom);
				}
			}
		}

		// Token: 0x06006C00 RID: 27648 RVA: 0x0025E41B File Offset: 0x0025C61B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(5m);
		}
	}
}
