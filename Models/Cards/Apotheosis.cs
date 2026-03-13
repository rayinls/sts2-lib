using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000893 RID: 2195
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Apotheosis : CardModel
	{
		// Token: 0x060066EF RID: 26351 RVA: 0x002545AB File Offset: 0x002527AB
		public Apotheosis()
			: base(2, CardType.Skill, CardRarity.Ancient, TargetType.Self, true)
		{
		}

		// Token: 0x17001AA8 RID: 6824
		// (get) Token: 0x060066F0 RID: 26352 RVA: 0x002545B8 File Offset: 0x002527B8
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlyArray<CardKeyword>(new CardKeyword[]
				{
					CardKeyword.Exhaust,
					CardKeyword.Innate
				});
			}
		}

		// Token: 0x060066F1 RID: 26353 RVA: 0x002545D0 File Offset: 0x002527D0
		protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			foreach (CardModel cardModel in base.Owner.PlayerCombatState.AllCards)
			{
				if (cardModel != this && cardModel.IsUpgradable)
				{
					CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
				}
			}
			return Task.CompletedTask;
		}

		// Token: 0x060066F2 RID: 26354 RVA: 0x00254638 File Offset: 0x00252838
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
