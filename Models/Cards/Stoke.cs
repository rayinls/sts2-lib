using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A6F RID: 2671
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Stoke : CardModel
	{
		// Token: 0x060070E2 RID: 28898 RVA: 0x00268123 File Offset: 0x00266323
		public Stoke()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001EDB RID: 7899
		// (get) Token: 0x060070E3 RID: 28899 RVA: 0x00268130 File Offset: 0x00266330
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x060070E4 RID: 28900 RVA: 0x00268138 File Offset: 0x00266338
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			List<CardModel> list = PileType.Hand.GetPile(base.Owner).Cards.ToList<CardModel>();
			int cardCount = list.Count;
			foreach (CardModel cardModel in list)
			{
				await CardCmd.Exhaust(choiceContext, cardModel, false, false);
			}
			List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
			await CardPileCmd.Draw(choiceContext, cardCount, base.Owner, false);
		}

		// Token: 0x060070E5 RID: 28901 RVA: 0x00268183 File Offset: 0x00266383
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
