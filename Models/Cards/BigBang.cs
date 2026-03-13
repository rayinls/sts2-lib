using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008AD RID: 2221
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BigBang : CardModel
	{
		// Token: 0x06006774 RID: 26484 RVA: 0x002556B7 File Offset: 0x002538B7
		public BigBang()
			: base(0, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001AE1 RID: 6881
		// (get) Token: 0x06006775 RID: 26485 RVA: 0x002556C4 File Offset: 0x002538C4
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001AE2 RID: 6882
		// (get) Token: 0x06006776 RID: 26486 RVA: 0x002556CC File Offset: 0x002538CC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(1),
					new EnergyVar(1),
					new StarsVar(1),
					new ForgeVar(5)
				});
			}
		}

		// Token: 0x17001AE3 RID: 6883
		// (get) Token: 0x06006777 RID: 26487 RVA: 0x002556FD File Offset: 0x002538FD
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				List<IHoverTip> list = new List<IHoverTip>();
				list.Add(base.EnergyHoverTip);
				list.AddRange(HoverTipFactory.FromForge());
				return new <>z__ReadOnlyList<IHoverTip>(list);
			}
		}

		// Token: 0x06006778 RID: 26488 RVA: 0x00255720 File Offset: 0x00253920
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
			await PlayerCmd.GainStars(base.DynamicVars.Stars.BaseValue, base.Owner);
			await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);
			await ForgeCmd.Forge(base.DynamicVars.Forge.IntValue, base.Owner, this);
		}

		// Token: 0x06006779 RID: 26489 RVA: 0x0025576B File Offset: 0x0025396B
		protected override void OnUpgrade()
		{
			base.AddKeyword(CardKeyword.Innate);
		}
	}
}
