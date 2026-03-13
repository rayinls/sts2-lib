using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008E2 RID: 2274
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Cleanse : CardModel
	{
		// Token: 0x06006880 RID: 26752 RVA: 0x002577F5 File Offset: 0x002559F5
		public Cleanse()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001B4C RID: 6988
		// (get) Token: 0x06006881 RID: 26753 RVA: 0x00257802 File Offset: 0x00255A02
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new SummonVar(3m));
			}
		}

		// Token: 0x17001B4D RID: 6989
		// (get) Token: 0x06006882 RID: 26754 RVA: 0x00257814 File Offset: 0x00255A14
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.SummonDynamic, new DynamicVar[] { base.DynamicVars.Summon }),
					HoverTipFactory.FromKeyword(CardKeyword.Exhaust)
				});
			}
		}

		// Token: 0x06006883 RID: 26755 RVA: 0x00257848 File Offset: 0x00255A48
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await OstyCmd.Summon(choiceContext, base.Owner, base.DynamicVars.Summon.BaseValue, this);
			List<CardModel> list = (from c in PileType.Draw.GetPile(base.Owner).Cards
				orderby c.Rarity, c.Id
				select c).ToList<CardModel>();
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromSimpleGrid(choiceContext, list, base.Owner, new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1));
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				await CardCmd.Exhaust(choiceContext, cardModel, false, false);
			}
		}

		// Token: 0x06006884 RID: 26756 RVA: 0x00257893 File Offset: 0x00255A93
		protected override void OnUpgrade()
		{
			base.DynamicVars.Summon.UpgradeValueBy(2m);
		}
	}
}
