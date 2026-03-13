using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008DD RID: 2269
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ChildOfTheStars : CardModel
	{
		// Token: 0x06006862 RID: 26722 RVA: 0x0025740F File Offset: 0x0025560F
		public ChildOfTheStars()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001B40 RID: 6976
		// (get) Token: 0x06006863 RID: 26723 RVA: 0x0025741C File Offset: 0x0025561C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("BlockForStars", 2m));
			}
		}

		// Token: 0x17001B41 RID: 6977
		// (get) Token: 0x06006864 RID: 26724 RVA: 0x00257433 File Offset: 0x00255633
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06006865 RID: 26725 RVA: 0x00257448 File Offset: 0x00255648
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<ChildOfTheStarsPower>(base.Owner.Creature, base.DynamicVars["BlockForStars"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006866 RID: 26726 RVA: 0x0025748B File Offset: 0x0025568B
		protected override void OnUpgrade()
		{
			base.DynamicVars["BlockForStars"].UpgradeValueBy(1m);
		}

		// Token: 0x04002562 RID: 9570
		private const string _blockForStarsKey = "BlockForStars";
	}
}
