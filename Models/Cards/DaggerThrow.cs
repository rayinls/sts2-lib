using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008FF RID: 2303
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DaggerThrow : CardModel
	{
		// Token: 0x06006913 RID: 26899 RVA: 0x00258A58 File Offset: 0x00256C58
		public DaggerThrow()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001B8A RID: 7050
		// (get) Token: 0x06006914 RID: 26900 RVA: 0x00258A65 File Offset: 0x00256C65
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(9m, ValueProp.Move));
			}
		}

		// Token: 0x06006915 RID: 26901 RVA: 0x00258A7C File Offset: 0x00256C7C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithAttackerFx(() => NDaggerSprayFlurryVfx.Create(base.Owner.Creature, new Color("#b1ccca"), true))
				.WithHitVfxNode((Creature t) => NDaggerSprayImpactVfx.Create(t, new Color("#b1ccca"), true))
				.Execute(choiceContext);
			await CardPileCmd.Draw(choiceContext, 1m, base.Owner, false);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHandForDiscard(choiceContext, base.Owner, new CardSelectorPrefs(CardSelectorPrefs.DiscardSelectionPrompt, 1), null, this);
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				await CardCmd.Discard(choiceContext, cardModel);
			}
		}

		// Token: 0x06006916 RID: 26902 RVA: 0x00258ACF File Offset: 0x00256CCF
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
