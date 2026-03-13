using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000931 RID: 2353
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Eidolon : CardModel
	{
		// Token: 0x06006A1F RID: 27167 RVA: 0x0025A7BF File Offset: 0x002589BF
		public Eidolon()
			: base(2, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001C07 RID: 7175
		// (get) Token: 0x06006A20 RID: 27168 RVA: 0x0025A7CC File Offset: 0x002589CC
		protected override bool ShouldGlowGoldInternal
		{
			get
			{
				PlayerCombatState playerCombatState = base.Owner.PlayerCombatState;
				return playerCombatState != null && playerCombatState.Hand.Cards.Count > 9;
			}
		}

		// Token: 0x17001C08 RID: 7176
		// (get) Token: 0x06006A21 RID: 27169 RVA: 0x0025A7F2 File Offset: 0x002589F2
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromKeyword(CardKeyword.Exhaust),
					HoverTipFactory.FromPower<IntangiblePower>()
				});
			}
		}

		// Token: 0x06006A22 RID: 27170 RVA: 0x0025A810 File Offset: 0x00258A10
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			List<CardModel> list = base.Owner.PlayerCombatState.Hand.Cards.ToList<CardModel>();
			int exhaustedCount = 0;
			foreach (CardModel cardModel in list)
			{
				await CardCmd.Exhaust(choiceContext, cardModel, false, false);
				exhaustedCount++;
			}
			List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
			if (exhaustedCount >= 9)
			{
				await PowerCmd.Apply<IntangiblePower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
			}
		}

		// Token: 0x06006A23 RID: 27171 RVA: 0x0025A85B File Offset: 0x00258A5B
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}

		// Token: 0x04002573 RID: 9587
		private const int _intangibleThreshold = 9;
	}
}
