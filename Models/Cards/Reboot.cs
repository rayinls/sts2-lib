using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A1E RID: 2590
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Reboot : CardModel
	{
		// Token: 0x06006F18 RID: 28440 RVA: 0x0026497B File Offset: 0x00262B7B
		public Reboot()
			: base(0, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001E18 RID: 7704
		// (get) Token: 0x06006F19 RID: 28441 RVA: 0x00264988 File Offset: 0x00262B88
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(4));
			}
		}

		// Token: 0x17001E19 RID: 7705
		// (get) Token: 0x06006F1A RID: 28442 RVA: 0x00264995 File Offset: 0x00262B95
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x06006F1B RID: 28443 RVA: 0x002649A0 File Offset: 0x00262BA0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			foreach (CardModel cardModel in PileType.Hand.GetPile(base.Owner).Cards.ToList<CardModel>())
			{
				await CardPileCmd.Add(cardModel, PileType.Draw, CardPilePosition.Bottom, null, false);
			}
			List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
			await CardPileCmd.Shuffle(choiceContext, base.Owner);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}

		// Token: 0x06006F1C RID: 28444 RVA: 0x002649EB File Offset: 0x00262BEB
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(2m);
		}
	}
}
